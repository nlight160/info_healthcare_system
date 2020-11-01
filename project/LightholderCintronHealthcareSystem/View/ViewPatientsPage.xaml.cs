using LightholderCintronHealthcareSystem.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using LightholderCintronHealthcareSystem.Model.People;
using SearchOption = LightholderCintronHealthcareSystem.Model.SearchOption;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LightholderCintronHealthcareSystem.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewPatientsPage : Page
    {
        private readonly PatientManager patientManager;
        private bool isItemSelected = false; 

        public ViewPatientsPage()
        {
            this.InitializeComponent();
            this.patientManager = new PatientManager {
                Patients = ViewModel.ViewModel.SearchForPatients(new List<string> {""}, SearchOption.Name)
            };
            //this.PatientListView.ItemsSource = this.patientManager.Patients;
            this.editPatientButton.IsEnabled = false;
            this.AddAppointmentButton.IsEnabled = false;
            this.patientListView.ItemsSource = this.patientManager.Patients;
            this.userTextBlock.Text = "User: " + ViewModel.ViewModel.ActiveUser.UserId + ", "
                                      + ViewModel.ViewModel.ActiveUser.NurseInfo.Firstname + " " + ViewModel.ViewModel.ActiveUser.NurseInfo.Lastname;
        }

        /// <summary>
        /// Ons the cancel.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void onCancel(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPage));
        }

        /// <summary>
        /// Ons the edit patient.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void onEditPatient(object sender, RoutedEventArgs e)
        {
            if (this.isItemSelected)
            {
                ContentDialog dialog = new EditPatientDialog(this.patientListView.SelectedItem as Patient);
                await dialog.ShowAsync();
            }
            ViewModel.ViewModel.UpdatePatient(EditPatientDialog.EditedPatient);
            Debug.Print(EditPatientDialog.EditedPatient.Firstname);
            this.patientListView.ItemsSource = ViewModel.ViewModel.SearchForPatients(new List<string> { "" }, SearchOption.Name);

        }

        /// <summary>
        /// Ons the name of the sort by.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void onSortByName(object sender, RoutedEventArgs e)
        {
            this.sortByNameAndDate();
            this.patientManager.SortPatientsByName();
            this.patientListView.ItemsSource = this.patientManager.Patients;
        }

        /// <summary>
        /// Ons the sort by date.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void onSortByDate(object sender, RoutedEventArgs e)
        {
            this.sortByNameAndDate();
            this.patientManager.SortPatientsByDate();
            this.patientListView.ItemsSource = this.patientManager.Patients;
        }

        /// <summary>
        /// Sorts the by name and date.
        /// </summary>
        private void sortByNameAndDate()
        {
            if (this.checkIfBothSearchBoxesAreChecked())
            {
                this.patientManager.SortPatientsByNameAndDate();
                this.patientListView.ItemsSource = this.patientManager.Patients;
            }
        }


        /// <summary>
        /// Checks if both search boxes are checked.
        /// </summary>
        /// <returns></returns>
        private bool checkIfBothSearchBoxesAreChecked()
        {
            return this.byDateCheckBox.IsChecked == true && this.byNameCheckBox.IsChecked == true;
        }

        /// <summary>
        /// Ons the selection change.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void onSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            if (this.patientListView.SelectedItem == null)
            {
                this.isItemSelected = false;
                this.editPatientButton.IsEnabled = false;
                this.AddAppointmentButton.IsEnabled = false;
            }
            else
            {
                this.isItemSelected = true;
                this.editPatientButton.IsEnabled = true;
                this.AddAppointmentButton.IsEnabled = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the SearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.patientSearchTextBoxByDate.Text != "" && this.patientSearchTextBoxByDate.Text != "")
            {
                this.patientListView.ItemsSource = ViewModel.ViewModel.SearchForPatients(new List<string> { this.patientSearchTextBoxByName.Text, this.patientSearchTextBoxByDate.Text }, SearchOption.Both);
            }
            else if (this.patientSearchTextBoxByName.Text != "")
            {
                this.patientListView.ItemsSource = ViewModel.ViewModel.SearchForPatients(new List<string> { this.patientSearchTextBoxByName.Text }, SearchOption.Name);
            }
            else if (this.patientSearchTextBoxByDate.Text != "")
            {
                this.patientListView.ItemsSource = ViewModel.ViewModel.SearchForPatients(new List<string> { this.patientSearchTextBoxByDate.Text }, SearchOption.Date);
            }
            else
            {
                this.patientListView.ItemsSource = ViewModel.ViewModel.SearchForPatients(new List<string> { "" }, SearchOption.Name);
            }
        }

        private async void onAddAppointment(object sender, RoutedEventArgs e)
        {
            if (this.isItemSelected)
            {
                ContentDialog dialog = new AddAppointmentDialog(this.patientListView.SelectedItem as Patient);
                await dialog.ShowAsync();
            }
        }
    }
}
