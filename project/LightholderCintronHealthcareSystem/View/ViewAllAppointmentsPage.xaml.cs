﻿using System;
using Windows.UI.Xaml.Controls;
using LightholderCintronHealthcareSystem.Model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LightholderCintronHealthcareSystem.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewAllAppointmentsPage : Page
    {


        private bool isItemSelected;



        /*  TODO
         *  Add buttons - thinking of adding a View details and making a content dialog class to view details of the appointment
         *      to where you can then add a checkup, diagnosis, order tests, enter tests, ect.
         *  Add edit appointment button and delete appointment button.
         *  Do not think add appointment should be on here because then we have to make a new dialog but to where user types in for name.
         *
         * Also thinking of adding checkboxes to refine searching of appointments from show only past appointments or show only future appointments
         * Searching by doctor to only show appointments given a certain doctor.
         *
         *
         *  I think having the buttons on one side and then the filtering and searching on the other side of the data view would be best.
         *
         *
         */


        /// <summary>
        /// Initializes a new instance of the <see cref="ViewAllAppointmentsPage"/> class.
        /// </summary>
        public ViewAllAppointmentsPage()
        {
            this.InitializeComponent();
            this.appointmentDataView.ItemsSource = ViewModel.ViewModel.getAllAppointments();
            this.userTextBlock.Text = "User: " + ViewModel.ViewModel.ActiveUser.UserId + ", "
                                      + ViewModel.ViewModel.ActiveUser.PersonInfo.Firstname + " " +
                                      ViewModel.ViewModel.ActiveUser.PersonInfo.Lastname;
        }

        /// <summary>
        /// Ons the view details.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.RoutedEventArgs"/> instance containing the event data.</param>
        private async void onViewDetails(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.viewDetailsButton.IsEnabled = false;
            var currentAppointment = this.appointmentDataView.SelectedItem as AppointmentDataGrid;
            if (this.isItemSelected)
            {
                var dialog = new ViewAppointmentDetails(currentAppointment);
                await dialog.ShowAsync();
            }

            this.viewDetailsButton.IsEnabled = true;
        }

        /// <summary>
        /// Ons the selection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void onSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.appointmentDataView.SelectedItem == null)
            {
                this.isItemSelected = false;
            }
            else
            {
                this.isItemSelected = true;

            }
        }

        /// <summary>
        /// Ons the return click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.RoutedEventArgs"/> instance containing the event data.</param>
        private void onReturnClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPage));
        }
    }
}
