using LightholderCintronHealthcareSystem.Model;
using LightholderCintronHealthcareSystem.Model.People;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LightholderCintronHealthcareSystem.View
{
    /// <summary>
    /// View appointment class.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.ContentDialog" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class ViewAppointments : ContentDialog
    {
        private readonly Patient patient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewAppointments"/> class.
        /// </summary>
        /// <param name="patient">The patient.</param>
        public ViewAppointments(Patient patient)
        {
            this.InitializeComponent();
            this.patient = patient;
            //this.refreshDataView();
            this.appointmentDataView.ItemsSource = ViewModel.ViewModel.getAppointmentsFromPatient(int.Parse(this.patient.Patientid));
            this.Title += this.patient.Firstname + " " + this.patient.Lastname;
        }
        /// <summary>
        /// Checks if no appointments.
        /// </summary>
        private void checkIfNoAppointments()
        {

        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        /// <summary>
        /// Refreshes the data view.
        /// </summary>
        private void refreshDataView()
        {
            this.appointmentDataView.ItemsSource = ViewModel.ViewModel.getAppointmentsFromPatient(int.Parse(this.patient.Patientid));
            this.checkIfNoAppointments();
        }

        /// <summary>
        /// Ons the add appointment.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void onAddAppointment(object sender, RoutedEventArgs e)
        {
            this.addAppointmentButton.IsEnabled = false;
            var dialog = new AddAppointmentDialog(int.Parse(this.patient.Patientid), this.patient.Firstname, this.patient.Lastname);
            this.Hide();
            await dialog.ShowAsync();
            var t = this.ShowAsync();
            this.refreshDataView();
            this.addAppointmentButton.IsEnabled = true;
        }

        /// <summary>
        /// Ons the edit appointment.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void onEditAppointment(object sender, RoutedEventArgs e)
        {
            this.editAppointmentButton.IsEnabled = false;
            var selectedAppointment = this.appointmentDataView.SelectedItem as AppointmentDataGrid;
            string content;
            string title;
            if (selectedAppointment != null)
            {
                var appointmentid = selectedAppointment.Appointmentid;

                if (!ViewModel.ViewModel.checkIfAppointmentTimePassed(appointmentid, DateTime.Now))
                {

                    var dialog = new EditAppointmentDialog(appointmentid, int.Parse(this.patient.Patientid), this.patient.Firstname, this.patient.Lastname,
                        int.Parse(ViewModel.ViewModel.GetDoctoridFromAppointmentid(appointmentid)), selectedAppointment.DateTime, selectedAppointment.Description); 
                    this.Hide();
                    await dialog.ShowAsync();
                    var t = this.ShowAsync();
                }
                else
                {
                    content = "Cannot edit appointments that have passed.";
                    title = "Select another appointment";
                    confirmation(content, title);
                }
            }
            else
            {
                content = "No selected appointment.";
                title = "Select appointment";
                confirmation(content, title);
            }

            this.refreshDataView();
        }

        /// <summary>
        /// Ons the delete appointment.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void onDeleteAppointment(object sender, RoutedEventArgs e)
        {
            this.deleteAppointmentButton.IsEnabled = false;
            var selectedAppointment = this.appointmentDataView.SelectedItem as AppointmentDataGrid;
            string content;
            string title;
            if (selectedAppointment != null)
            {
                var appointmentid = selectedAppointment.Appointmentid;

                var success = ViewModel.ViewModel.deleteAppointment(appointmentid);
                

                if (success)
                {
                    content = "Appointment for " + this.patient.Firstname + " " + this.patient.Lastname + " was deleted successfully!";
                    title = "Delete successful";
                }
                else
                {
                    content = "Appointment for " + this.patient.Firstname + " " + this.patient.Lastname + " could not be deleted.";
                    title = "Deletion failed";
                }
                
                this.refreshDataView();
            }
            else
            {
                content = "No selected appointment.";
                title = "Select appointment";
            }
            confirmation(content, title);

        }

        /// <summary>
        /// Confirmations the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="title">The title.</param>
        private static async void confirmation(string content, string title)
        {
            var alert = new MessageDialog(content, title);
            await alert.ShowAsync();
        }

        private async void onViewDetails(object sender, RoutedEventArgs e)
        {
            this.viewDetailsButton.IsEnabled = false;
            if (this.appointmentDataView.SelectedItem is AppointmentDataGrid selectedAppointment)
            {
                var dialog = new ViewAppointmentDetails(selectedAppointment);
                this.Hide();
                await dialog.ShowAsync();
                dialog.Hide();
                var t = this.ShowAsync();

                this.refreshDataView();
            }
        }
    }
}
