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

        private void onCancel(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPage));
        }

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

        private void onSortByName(object sender, RoutedEventArgs e)
        {
            this.sortByNameAndDate();
            this.patientManager.SortPatientsByName();
            this.PatientListView.ItemsSource = this.patientManager.Patients;
        }

        private void onSortByDate(object sender, RoutedEventArgs e)
        {
            this.sortByNameAndDate();
            this.patientManager.SortPatientsByDate();
            this.PatientListView.ItemsSource = this.patientManager.Patients;
        }

        private void sortByNameAndDate()
        {
            if (this.checkIfBothSearchBoxesAreChecked())
            {
                this.patientManager.SortPatientsByNameAndDate();
                this.PatientListView.ItemsSource = this.patientManager.Patients;
            }
        }

        private bool checkIfBothSearchBoxesAreChecked()
        {
            if (this.ByDateCheckBox.IsChecked == true && this.ByNameCheckBox.IsChecked == true)
            {
                return true;
            }

            return false;
        }

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
    }
}
