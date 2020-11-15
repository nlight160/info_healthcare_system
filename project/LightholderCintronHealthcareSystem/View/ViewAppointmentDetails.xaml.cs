using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using LightholderCintronHealthcareSystem.Model;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LightholderCintronHealthcareSystem.View
{
    public sealed partial class ViewAppointmentDetails : ContentDialog
    {
        private Dictionary<string, string> dataDictionary;
        private Dictionary<string, string> checkupDictionary;
        private readonly int appointmentid;

        /*  TODO
         *  Would like to have some structure on the top right of the dialog with the checkup information filled out (if checkup was already done if not grey everything out + context menu saying to fill
         *      checkout out.
         *  then on the lower to mid right have a spot for ordering tests and entering tests and below that have a data grid with a dictionary to show test results
         *  
         *  Finally below all that have a final diagnosis textbox which is only active after checkup and all tests that were ordered were completed. grayed out with context menu until it is.
         *      + have to make sure user is sure to submit final diagnosis.
         *
         *
         */





        public ViewAppointmentDetails(AppointmentDataGrid appointment)
        {
            this.InitializeComponent();
            this.appointmentid = appointment.Appointmentid;
            this.dataDictionary = new Dictionary<string, string>();
            this.appointmentDataView.ItemsSource = this.dataDictionary;
            this.dataDictionary.Add("Patient ID", appointment.Patientid.ToString());
            this.dataDictionary.Add("Patient Name", appointment.PatientName);
            this.dataDictionary.Add("Patient DOB", "TODO");
            this.dataDictionary.Add("Doctor ID", appointment.DoctorName);
            this.dataDictionary.Add("Doctor Name", appointment.DoctorName);
            this.dataDictionary.Add("Appointment Date", appointment.Date);
            this.dataDictionary.Add("Appointment Time", appointment.Time);
            this.dataDictionary.Add("Description", appointment.Description);


            //TODO TODO TODO check to see if checkup exists first! Probably need to do it in the viewmodel and return null and check for null. ***************

            var checkup = ViewModel.ViewModel.GetCheckupFromAppointmentid(appointment.Appointmentid);
            this.checkupDictionary = new Dictionary<string, string>();
            this.checkupDataView.ItemsSource = this.checkupDictionary;
            this.checkupDictionary.Add("Systolic", checkup.Systolic.ToString());
            this.checkupDictionary.Add("Diastolic", checkup.Diastolic.ToString());
            this.checkupDictionary.Add("Temperature", checkup.Temperature.ToString(CultureInfo.CurrentCulture));
            this.checkupDictionary.Add("Weight", checkup.Weight.ToString(CultureInfo.CurrentCulture));
            this.checkupDictionary.Add("Pulse", checkup.Pulse.ToString());
            this.checkupDictionary.Add("Initial Diagnosis", checkup.Diagnosis);


        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private async void onClickCreateCheckup(object sender, RoutedEventArgs e)
        {
            if (this.appointmentid != 0) //TODO need to check to see if checkup already exists.
            {
                var dialog = new RecordCheckupDialog(this.appointmentid);
                this.Hide();
                await dialog.ShowAsync();
                var t = this.ShowAsync();


                //TODO update checkup info.
            }
        }
    }
}
