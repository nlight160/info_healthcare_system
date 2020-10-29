using LightholderCintronHealthcareSystem.Model;
using System;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
            this.phoneNumberTextBox.MaxLength = 10;
            this.zipCodeTextBox.MaxLength = 5;
            this.birthdateDatePicker.MaxYear = DateTimeOffset.Now;
            this.IsPrimaryButtonEnabled = false;
            this.firstnameTextBox.Text = patient.Firstname;
            this.lastnameTextBox.Text = patient.Lastname;
            this.phoneNumberTextBox.Text = patient.PhoneNumber;
            this.stateComboBox.SelectedItem = patient.Address.State;
            this.streetTextBox.Text = patient.Address.Street;
            this.genderComboBox.SelectedItem = patient.Gender == Gender.Male ? "Male" : "Female";
            this.cityTextBox.Text = patient.Address.City;
            this.zipCodeTextBox.Text = patient.Address.Zip;
            this.birthdateDatePicker.Date = new DateTime(int.Parse(patient.Birthdate.Year),
                int.Parse(patient.Birthdate.Month), int.Parse(patient.Birthdate.Day));
            EditedPatient = patient;
        }

        /// <summary>
        /// Contents the dialog submit button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ContentDialogButtonClickEventArgs"/> instance containing the event data.</param>
        private void ContentDialog_SubmitButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

            EditedPatient.Firstname = this.firstnameTextBox.Text;
            EditedPatient.Lastname = this.lastnameTextBox.Text;
            var date = ViewModel.ViewModel.GetDate(this.birthdateDatePicker.Date.Year.ToString(), this.birthdateDatePicker.Date.Month.ToString(),
                this.birthdateDatePicker.Date.Day.ToString());
            EditedPatient.Birthdate = date;
            EditedPatient.PhoneNumber = this.phoneNumberTextBox.Text;
            EditedPatient.Address = new Address(this.streetTextBox.Text, this.cityTextBox.Text, this.stateComboBox.SelectedItem.ToString(), this.zipCodeTextBox.Text);
            EditedPatient.Gender = this.genderComboBox.SelectedItem == "Male" ? Gender.Male : Gender.Female;
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
            return this.firstnameTextBox.Text != "" && this.lastnameTextBox.Text != "" && this.phoneNumberTextBox.Text != "" && this.streetTextBox.Text != "" && this.cityTextBox.Text != ""
                   && this.zipCodeTextBox.Text != "" && this.stateComboBox.SelectedItem != null && !this.birthdateDatePicker.Date.Equals(DateTimeOffset.MinValue) && this.genderComboBox.SelectedItem != null;
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
            var pressed = (state & CoreVirtualKeyStates.Down) == CoreVirtualKeyStates.Down;
            if (System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), "Number") && !pressed)
            {
                e.Handled = false;
                return;
            }
            e.Handled = true;
        }
    }
}
