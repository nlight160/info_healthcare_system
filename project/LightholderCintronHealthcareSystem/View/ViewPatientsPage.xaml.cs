using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
            this.patientManager = new PatientManager();
            this.patientManager.Patients = ViewModel.ViewModel.searchForPatients(new List<string> { "" }, SearchOption.Name);
            //this.PatientListView.ItemsSource = this.patientManager.Patients;
            this.EditPatientButton.IsEnabled = false;
            this.PatientListView.ItemsSource = this.patientManager.Patients;
            this.UserTextBlock.Text = "User: " + ViewModel.ViewModel.ActiveUser.UserId + ", "
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
            if (this.isItemSelected == true)
            {
                ContentDialog dialog = new EditPatientDialog(this.PatientListView.SelectedItem as Patient);
                await dialog.ShowAsync();
            }
            ViewModel.ViewModel.UpdatePatient(EditPatientDialog.EditedPatient);
            Debug.Print(EditPatientDialog.EditedPatient.Firstname);
            this.PatientListView.ItemsSource = ViewModel.ViewModel.searchForPatients(new List<string> { "" }, SearchOption.Name);

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
            this.PatientListView.ItemsSource = this.patientManager.Patients;
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
            this.PatientListView.ItemsSource = this.patientManager.Patients;
        }

        /// <summary>
        /// Sorts the by name and date.
        /// </summary>
        private void sortByNameAndDate()
        {
            if (this.checkIfBothSearchBoxesAreChecked())
            {
                this.patientManager.SortPatientsByNameAndDate();
                this.PatientListView.ItemsSource = this.patientManager.Patients;
            }
        }


        /// <summary>
        /// Checks if both search boxes are checked.
        /// </summary>
        /// <returns></returns>
        private bool checkIfBothSearchBoxesAreChecked()
        {
            return this.ByDateCheckBox.IsChecked == true && this.ByNameCheckBox.IsChecked == true;
        }

        /// <summary>
        /// Ons the selection change.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void onSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            if (PatientListView.SelectedItem == null)
            {
                this.isItemSelected = false;
                this.EditPatientButton.IsEnabled = false;
            }
            else
            {
                this.isItemSelected = true;
                this.EditPatientButton.IsEnabled = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the SearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.PatientSearchTextBoxByDate.Text != "" && this.PatientSearchTextBoxByDate.Text != "")
            {
                this.PatientListView.ItemsSource = ViewModel.ViewModel.searchForPatients(new List<string> { this.PatientSearchTextBoxByName.Text, this.PatientSearchTextBoxByDate.Text }, SearchOption.Both);
            }
            else if (this.PatientSearchTextBoxByName.Text != "")
            {
                this.PatientListView.ItemsSource = ViewModel.ViewModel.searchForPatients(new List<string> { this.PatientSearchTextBoxByName.Text }, SearchOption.Name);
            }
            else if (this.PatientSearchTextBoxByDate.Text != "")
            {
                this.PatientListView.ItemsSource = ViewModel.ViewModel.searchForPatients(new List<string> { this.PatientSearchTextBoxByDate.Text }, SearchOption.Date);
            }
            else
            {
                this.PatientListView.ItemsSource = ViewModel.ViewModel.searchForPatients(new List<string> { "" }, SearchOption.Name);
            }
        }
    }
}
