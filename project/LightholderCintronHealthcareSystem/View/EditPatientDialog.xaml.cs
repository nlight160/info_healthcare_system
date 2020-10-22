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
    /// <summary>
    /// Edit Patient Dialog
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.ContentDialog" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class EditPatientDialog : ContentDialog
    {
        /// <summary>
        /// Gets or sets the edited patient.
        /// </summary>
        /// <value>
        /// The edited patient.
        /// </value>
        public static Patient EditedPatient { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditPatientDialog"/> class.
        /// </summary>
        /// <param name="patient">The patient.</param>
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

        /// <summary>
        /// Contents the dialog submit button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ContentDialogButtonClickEventArgs"/> instance containing the event data.</param>
        private void ContentDialog_SubmitButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

            EditedPatient.Firstname = this.FirstnameTextBox.Text;
            EditedPatient.Lastname = this.LastnameTextBox.Text;
            var date = ViewModel.ViewModel.GetDate(this.BirthdateDatePicker.Date.Year.ToString(), this.BirthdateDatePicker.Date.Month.ToString(),
                this.BirthdateDatePicker.Date.Day.ToString());
            EditedPatient.Birthdate = date;
            EditedPatient.PhoneNumber = this.PhoneNumberTextBox.Text;
            EditedPatient.Address = new Address(this.StreetTextBox.Text, this.CityTextBox.Text, this.StateComboBox.SelectedItem.ToString(), this.ZipCodeTextBox.Text);
            EditedPatient.Gender = this.GenderComboBox.SelectedItem == "Male" ? Gender.Male : Gender.Female;
            this.Hide();
        }

        /// <summary>
        /// Contents the dialog cancel button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ContentDialogButtonClickEventArgs"/> instance containing the event data.</param>
        private void ContentDialog_CancelButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.Hide();
        }

        /// <summary>
        /// Ons the deselect control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void onDeselectControl(object sender, RoutedEventArgs e)
        {
            this.IsPrimaryButtonEnabled = this.checkControlsForCompletion();
        }

        /// <summary>
        /// Checks the controls for completion.
        /// </summary>
        /// <returns></returns>
        private bool checkControlsForCompletion()
        {
            if (FirstnameTextBox.Text != "" && LastnameTextBox.Text != "" && PhoneNumberTextBox.Text != "" && StreetTextBox.Text != "" && CityTextBox.Text != ""
                && ZipCodeTextBox.Text != "" && StateComboBox.SelectedItem != null && !this.BirthdateDatePicker.Date.Equals(DateTimeOffset.MinValue) && this.GenderComboBox.SelectedItem != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Ons the key down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.Input.KeyRoutedEventArgs"/> instance containing the event data.</param>
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
