using LightholderCintronHealthcareSystem.Model;
using LightholderCintronHealthcareSystem.Model.DatabaseAccess;
using LightholderCintronHealthcareSystem.Model.People;
using System;
using System.Collections.Generic;
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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LightholderCintronHealthcareSystem.View
{
    public sealed partial class ViewAppointments : ContentDialog
    {
        private Patient patient;

        public ViewAppointments(Patient patient)
        {
            this.InitializeComponent();
            this.patient = patient;
            this.refreshDataView();
            
            this.Title += this.patient.Firstname + " " + this.patient.Lastname;
        }
        private void checkIfNoAppointments()
        {

        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void refreshDataView()
        {
            this.appointmnetDataView.ItemsSource = ViewModel.ViewModel.getAppointmentsFromPatient(int.Parse(this.patient.Patientid));
            this.checkIfNoAppointments();
        }

        private async void onAddAppointment(object sender, RoutedEventArgs e)
        {

            var dialog = new AddAppointmentDialog(this.patient);
            await dialog.ShowAsync();
            this.refreshDataView();
        }

        private async void onEditAppointment(object sender, RoutedEventArgs e)
        {
            var dialog = new AddAppointmentDialog(this.patient);
            await dialog.ShowAsync();
            this.refreshDataView();
        }

        private async void onDeleteAppointment(object sender, RoutedEventArgs e)
        {

            AppointmentDataGrid selectedAppointment = this.appointmnetDataView.SelectedItem as AppointmentDataGrid;
            var appointmentid = selectedAppointment.appointmentid;

            var success = ViewModel.ViewModel.deleteAppointment(appointmentid);
            var content = "";
            var title = "";

            if (success)
            {
                content = "Appointment for " + this.patient.Firstname + " " + this.patient.Lastname + " was deleted successfully!";
                title = "Delete successful";
            }
            else
            {
                content = "Appointment for " + patient.Firstname + " " + patient.Lastname + " could not be deleted.";
                title = "Deletion failed";
            }
            this.confirmation(content, title);
            this.refreshDataView();
        }

        private async void confirmation(string content, string title)
        {
            MessageDialog alert = new MessageDialog(content, title);
            await alert.ShowAsync();
        }

        private async void onRecordCheckup(object sender, RoutedEventArgs e)
        { 
            ContentDialog dialog = new RecordCheckupDialog(this.patient);
            await dialog.ShowAsync();
            this.refreshDataView();
        }

    }
}
