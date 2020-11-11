using LightholderCintronHealthcareSystem.Model;
using LightholderCintronHealthcareSystem.Model.DatabaseAccess;
using LightholderCintronHealthcareSystem.Model.People;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LightholderCintronHealthcareSystem.View
{
    /// <summary>
    /// Add appointment dialog
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.ContentDialog" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class AddAppointmentDialog : ContentDialog
    {
        private readonly Patient patient;
        private List<string> doctorParameterList;
        private readonly DoctorDatabaseAccess ddb;
        private readonly AppointmentDatabaseAccess adb;
        private readonly Appointment appointment;
        private readonly bool appointmentAlreadyExists;
        private readonly ToolTip dateTip;


        /// <summary>
        /// Initializes a new instance of the <see cref="AddAppointmentDialog"/> class.
        /// </summary>
        /// <param name="patient">The patient.</param>
        public AddAppointmentDialog(Patient patient)
        {
            this.InitializeComponent();
            this.dateTip = new ToolTip();
            
            ToolTipService.SetToolTip(this.dateDatePicker, this.dateTip);

            this.patient = patient;
            this.adb = new AppointmentDatabaseAccess();
            this.ddb = new DoctorDatabaseAccess();
            var appointmentsParameters = this.adb.GetAppointmentFromPatientid(int.Parse(this.patient.Patientid));
            this.appointmentAlreadyExists = appointmentsParameters.Count != 0;
            if (appointmentAlreadyExists)
            {
                var appointmentParameters = appointmentsParameters[0];
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

        /// <summary>
        /// Contents the dialog submit button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ContentDialogButtonClickEventArgs"/> instance containing the event data.</param>
        private async void ContentDialog_SubmitButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var doctor = new Doctor(this.doctorParameterList[0], this.doctorParameterList[1], "General") //sdb.GetSpecialtyName(sdb.GetDoctorSpecialtiesId(doctorid)[0])) There is no column for specialty
            {
                Doctorid = this.doctorParameterList[10]
            };
            var dateTime = new DateTime(this.dateDatePicker.Date.Year, this.dateDatePicker.Date.Month, this.dateDatePicker.Date.Day, this.timeTimePicker.Time.Hours, this.timeTimePicker.Time.Minutes, 0);
            var newAppointment = new Appointment(null, this.patient, doctor, dateTime,
                this.descriptionTextBox.Text);
            if (!this.appointmentAlreadyExists)
            {
                
                this.adb.CreateAppointment(newAppointment);
                var newAppointmentDialog = new MessageDialog("A new appointment was created for " + this.patient.Firstname + " " + this.patient.Lastname, "New appointment added!");
                await newAppointmentDialog.ShowAsync();
            }
            else
            {
                this.adb.UpdateAppointment(this.appointment, newAppointment);
                var updateAppointmentDialog = new MessageDialog("Appointment for " + this.patient.Firstname + " " + this.patient.Lastname + " has been updated.", "Appointment Updated");
                await updateAppointmentDialog.ShowAsync();
            }

            this.dateTip.IsOpen = false;
            var testDialog = new MessageDialog("Is it double booked?" + (this.checkForDoctorDoubleBook()? "yes": "no"), "New appointment added!");
            await testDialog.ShowAsync();

        }

        /// <summary>
        /// Contents the dialog cancel button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ContentDialogButtonClickEventArgs"/> instance containing the event data.</param>
        private void ContentDialog_CancelButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.dateTip.IsOpen = false;
            Hide();
        }

        /// <summary>
        /// Ons the deselect doctor identifier text block.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Checks for competion.
        /// </summary>
        /// <returns></returns>
        private bool checkForCompetion()
        {
            if (!this.checkForDate())
            {
                this.dateTip.Content = "Date/Time must be in the future not the past.";
                this.dateTip.IsOpen = true;
                return false;
            }
            this.dateTip.IsOpen = false;
            return !this.checkForDoubleBook() && this.doctorIdTextBox.Text != "" && this.descriptionTextBox.Text != "";
        }

        /// <summary>
        /// Checks for time.
        /// </summary>
        /// <returns></returns>
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
            return this.timeTimePicker.Time.Hours >= DateTime.Now.Hour;
        }

        /// <summary>
        /// Checks for date.
        /// </summary>
        /// <returns></returns>
        private bool checkForDate()
        {
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
            return this.dateDatePicker.Date.Year >= DateTime.Now.Year;
        }

        /// <summary>
        /// Checks for doctor double book.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Checks for patient double book.
        /// </summary>
        /// <returns></returns>
        private bool checkForPatientDoubleBook()
        {
            
            var requestedTime = this.dateDatePicker.Date.Date.Add(this.timeTimePicker.Time);
            if (ViewModel.ViewModel.checkForPatientDoubleBook(requestedTime, int.Parse(this.patient.Patientid)))
            {
                this.dateTip.Content = "Patient already booked for this Date/Time.";
                this.dateTip.IsOpen = true;
                return true;
            }
            this.dateTip.IsOpen = false;
            return false;

        }

        /// <summary>
        /// Checks for double book.
        /// </summary>
        /// <returns></returns>
        private bool checkForDoubleBook()
        {
            return this.checkForDoctorDoubleBook() || this.checkForPatientDoubleBook();
        }


        /// <summary>
        /// Ons the deselect control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void onDeselectControl(object sender, RoutedEventArgs e)
        {
            this.IsPrimaryButtonEnabled = this.checkForCompetion();
        }
    }
}
