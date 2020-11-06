using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using LightholderCintronHealthcareSystem.Model;
using LightholderCintronHealthcareSystem.Model.DatabaseAccess;
using LightholderCintronHealthcareSystem.Model.People;
using Org.BouncyCastle.Asn1.Cms;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LightholderCintronHealthcareSystem.View
{
    public sealed partial class AddAppointmentDialog : ContentDialog
    {
        private Patient patient;
        private Doctor doctor;
        private List<string> doctorParameterList;
        private DoctorDatabaseAccess ddb;
        private AppointmentDatabaseAccess adb;
        private Appointment appointment;
        private bool appointmentAlreadyExists;
        private ToolTip dateTip;
        

        public AddAppointmentDialog(Patient patient)
        {
            this.InitializeComponent();
            this.dateTip = new ToolTip();
            
            ToolTipService.SetToolTip(this.dateDatePicker, this.dateTip);

            this.patient = patient;
            this.adb = new AppointmentDatabaseAccess();
            this.ddb = new DoctorDatabaseAccess();
            var appointmentParameters = this.adb.GetAppointmentFromPatientid(int.Parse(this.patient.Patientid));
            this.appointmentAlreadyExists = appointmentParameters.Count != 0;
            if (appointmentAlreadyExists)
            {
                this.doctorParameterList = ddb.GetDoctorDataFromId(int.Parse(appointmentParameters[3]));
                var doctor = new Doctor(this.doctorParameterList[0], this.doctorParameterList[1], "General"); //TODO change specialty to whats in database.
                this.appointment = new Appointment(int.Parse(appointmentParameters[0]), this.patient, doctor, DateTime.Parse(appointmentParameters[2]), appointmentParameters[4]);
                this.doctorIdTextBox.Text = this.doctorParameterList[10];
                this.dateDatePicker.Date = this.appointment.AppointmentDateTime.Date;
                this.timeTimePicker.Time = this.appointment.AppointmentDateTime.TimeOfDay;
                this.doctorFirstNameTextBlock.Text = this.appointment.Doctor.Firstname;
                this.doctorLastNameTextBlock.Text = this.appointment.Doctor.Lastname;
                this.descriptionTextBox.Text = this.appointment.Description;
            }
            else
            {
                this.dateDatePicker.Date = DateTimeOffset.Now;
                this.timeTimePicker.Time = TimeSpan.Zero;
            }
            
            
            this.IsPrimaryButtonEnabled = false;
            this.patientFirstNameTextBlock.Text = patient.Firstname;
            this.patientLastNameTextBlock.Text = patient.Lastname;
            this.patientIdTextBlock.Text = patient.Patientid;
            this.dateDatePicker.MinYear = DateTimeOffset.Now;


        }

        private async void ContentDialog_SubmitButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            SpecialtyDatabaseAccess sdb = new SpecialtyDatabaseAccess();
            var doctor = new Doctor(this.doctorParameterList[0], this.doctorParameterList[1], "General") //sdb.GetSpecialtyName(sdb.GetDoctorSpecialtiesId(doctorid)[0])) There is no column for specialty
            {
                Doctorid = this.doctorParameterList[10]
            };
            DateTime dateTime = new DateTime(this.dateDatePicker.Date.Year, this.dateDatePicker.Date.Month, this.dateDatePicker.Date.Day, this.timeTimePicker.Time.Hours, this.timeTimePicker.Time.Minutes, 0);
            var newAppointment = new Appointment(null, this.patient, doctor, dateTime,
                this.descriptionTextBox.Text);
            if (!appointmentAlreadyExists)
            {
                
                this.adb.CreateAppointment(newAppointment);
                MessageDialog newAppointmentDialog = new MessageDialog("A new appointment was created for " + this.patient.Firstname + " " + this.patient.Lastname, "New appointment added!");
                await newAppointmentDialog.ShowAsync();
            }
            else
            {
                this.adb.UpdateAppointment(this.appointment, newAppointment);
                MessageDialog updateAppointmentDialog = new MessageDialog("Appointment for " + this.patient.Firstname + " " + this.patient.Lastname + " has been updated.", "Appointment Updated");
                await updateAppointmentDialog.ShowAsync();
            }

            this.dateTip.IsOpen = false;
            MessageDialog testDialog = new MessageDialog("Is it double booked?" + (this.checkForDoctorDoubleBook() == true? "yes": "no"), "New appointment added!");
            await testDialog.ShowAsync();

        }

        private void ContentDialog_CancelButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.dateTip.IsOpen = false;
            Hide();
        }

        private async void onDeselectDoctorIdTextBlock(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(doctorIdTextBox.Text) && !string.IsNullOrWhiteSpace(doctorIdTextBox.Text))
            {
                this.doctorParameterList = ddb.GetDoctorDataFromId(int.Parse(this.doctorIdTextBox.Text));
                try 
                {
                    this.doctorFirstNameTextBlock.Text = this.doctorParameterList[0];
                    this.doctorLastNameTextBlock.Text = this.doctorParameterList[1];
                }
                catch(ArgumentOutOfRangeException exception)
                {
                    Debug.WriteLine(exception.Message);
                    MessageDialog notFounDialog = new MessageDialog("A doctor with the ID provided was not found.", "Doctor not found!");
                    await notFounDialog.ShowAsync();
                }
                
            }
            this.IsPrimaryButtonEnabled = this.checkForCompetion();
        }

        private bool checkForCompetion()
        {
            if (!this.checkForDate())
            {
                this.dateTip.Content = "Date/Time must be in the future not the past.";
                this.dateTip.IsOpen = true;
                return false;
            }
            this.dateTip.IsOpen = false;
            return (!this.checkForDoctorDoubleBook()) && this.doctorIdTextBox.Text != "" && this.descriptionTextBox.Text != "";
        }

        private bool checkForTime()
        {
            if (this.timeTimePicker.Time.Hours == DateTime.Now.Hour)
            {
                if (this.timeTimePicker.Time.Minutes == DateTime.Now.Minute)
                {
                    return false;
                }

                if (this.dateDatePicker.Date.Minute <= DateTime.Now.Minute)
                {
                    return false;
                }
            }
            if (this.timeTimePicker.Time.Hours < DateTime.Now.Hour)
            {
                return false;
            }

            return true;

        }

        private bool checkForDate()
        {
            var returnValue = false;
            if (this.dateDatePicker.Date.Year == DateTime.Now.Year)
            {
                if (this.dateDatePicker.Date.Month < DateTime.Now.Month)
                {
                    return false;
                }
                if (this.dateDatePicker.Date.Month == DateTime.Now.Month)
                {
                    if (this.dateDatePicker.Date.Day < DateTime.Now.Day)
                    {
                        return false;
                    }
                    if (this.dateDatePicker.Date.Day == DateTime.Now.Day)
                    {
                        return this.checkForTime();
                    }
                }
            }
            if (this.dateDatePicker.Date.Year < DateTime.Now.Year)
            {
                return false;
            }

            return true;


        }

        private bool checkForDoctorDoubleBook()
        {
            if (string.IsNullOrEmpty(this.doctorIdTextBox.Text))
            {
                return true;
            }
            var requestedTime = this.dateDatePicker.Date.Date.Add(this.timeTimePicker.Time);
            if (ViewModel.ViewModel.checkForDoctorDoubleBook(requestedTime, int.Parse(this.doctorIdTextBox.Text)))
            {
                this.dateTip.Content = "Doctor already booked for this Date/Time.";
                this.dateTip.IsOpen = true;
                return true;
            }
            this.dateTip.IsOpen = false;
            return false;

        }


        private void onDeselectControl(object sender, RoutedEventArgs e)
        {
            this.IsPrimaryButtonEnabled = this.checkForCompetion();
        }
    }
}
