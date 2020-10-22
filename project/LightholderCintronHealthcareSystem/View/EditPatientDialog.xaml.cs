using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
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
    public sealed partial class EditPatientDialog : ContentDialog
    {
        public static Patient EditedPatient { get; set; }

        public EditPatientDialog(Patient patient)
        {
            this.InitializeComponent();
            this.PhoneNumberTextBox.MaxLength = 10;
            this.ZipCodeTextBox.MaxLength = 5;
            this.BirthdateDatePicker.MaxYear = DateTimeOffset.Now;
            this.IsPrimaryButtonEnabled = false;
            this.FirstnameTextBox.Text = patient.Firstname;
            this.LastnameTextBox.Text = patient.Lastname;
            this.PhoneNumberTextBox.Text = patient.PhoneNumber;
            this.StateComboBox.SelectedItem = patient.Address.State;
            this.StreetTextBox.Text = patient.Address.Street;
            this.GenderComboBox.SelectedItem = patient.Gender == Gender.Male ? "Male" : "Female";
            this.CityTextBox.Text = patient.Address.City;
            this.ZipCodeTextBox.Text = patient.Address.Zip;
            this.BirthdateDatePicker.Date = new DateTime(int.Parse(patient.Birthdate.year),
                int.Parse(patient.Birthdate.month), int.Parse(patient.Birthdate.day));
            EditedPatient = patient;
        }

        private void ContentDialog_SubmitButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

            EditedPatient.Firstname = this.FirstnameTextBox.Text;
            EditedPatient.Lastname = this.LastnameTextBox.Text;
            var date = ViewModel.ViewModel.GetDate(this.BirthdateDatePicker.Date.Year.ToString(), this.BirthdateDatePicker.Date.Month.ToString(),
                this.BirthdateDatePicker.Date.Day.ToString());
            EditedPatient.Birthdate = date;
            EditedPatient.PhoneNumber = this.PhoneNumberTextBox.Text;
            EditedPatient.Address = new Address(this.StreetTextBox.Text, this.CityTextBox.Text, this.ZipCodeTextBox.Text, this.StateComboBox.SelectedItem.ToString());
            EditedPatient.Gender = this.GenderComboBox.SelectedItem == "Male" ? Gender.Male : Gender.Female;
            this.Hide();
        }

        private void ContentDialog_CancelButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.Hide();
        }

        private void onDeselectControl(object sender, RoutedEventArgs e)
        {
            this.IsPrimaryButtonEnabled = this.checkControlsForCompletion();
        }

        private bool checkControlsForCompletion()
        {
            if (FirstnameTextBox.Text != "" && LastnameTextBox.Text != "" && PhoneNumberTextBox.Text != "" && StreetTextBox.Text != "" && CityTextBox.Text != ""
                && ZipCodeTextBox.Text != "" && StateComboBox.SelectedItem != null && !this.BirthdateDatePicker.Date.Equals(DateTimeOffset.MinValue) && this.GenderComboBox.SelectedItem != null)
            {
                return true;
            }

            return false;
        }

        private void onKeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key.ToString().Equals("Back"))
            {
                e.Handled = false;
                return;
            }
            var state = CoreWindow.GetForCurrentThread().GetKeyState(VirtualKey.Shift);
            bool pressed = (state & CoreVirtualKeyStates.Down) == CoreVirtualKeyStates.Down;
            if (System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), "Number") && !pressed)
            {
                e.Handled = false;
                return;
            }
            e.Handled = true;
        }
    }
}
