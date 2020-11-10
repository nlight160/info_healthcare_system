using LightholderCintronHealthcareSystem.Model.People;
using System;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LightholderCintronHealthcareSystem.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegistrationPage : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationPage"/> class.
        /// </summary>
        public RegistrationPage()
        {
            this.InitializeComponent();
            this.registerButton.IsEnabled = false;
            this.phoneNumberTextBox.MaxLength = 10;
            this.zipCodeTextBox.MaxLength = 5;
            this.birthdateDatePicker.MaxYear = DateTimeOffset.Now;
            this.userTextBlock.Text = "User: " + ViewModel.ViewModel.ActiveUser.UserId + ", "
                                 + ViewModel.ViewModel.ActiveUser.NurseInfo.Firstname + " " + ViewModel.ViewModel.ActiveUser.NurseInfo.Lastname;
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

        /// <summary>
        /// Ons the cancel.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void onCancel(object sender, RoutedEventArgs e)
        {
            this.clearInputs();
            Frame.Navigate(typeof(MenuPage));
        }

        /// <summary>
        /// Ons the register.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void onRegister(object sender, RoutedEventArgs e)
        {
            var gender = this.getGender();
            var date = ViewModel.ViewModel.GetDate(this.birthdateDatePicker.Date.Year.ToString(), this.birthdateDatePicker.Date.Month.ToString(),
                this.birthdateDatePicker.Date.Day.ToString());
            //var date = BirthdateDatePicker.Date.Year + "-" + BirthdateDatePicker.Date.Month + "-" + BirthdateDatePicker.Date.Day;
            var isSuccessful = ViewModel.ViewModel.RegisterPatient(this.lastnameTextBox.Text, this.firstnameTextBox.Text, date, this.streetTextBox.Text, this.cityTextBox.Text, 
                this.stateComboBox.SelectedItem.ToString(), this.zipCodeTextBox.Text, this.phoneNumberTextBox.Text, gender);

            if (isSuccessful)
            {
                var deleteAlert =
                    new MessageDialog("Patient " + this.firstnameTextBox.Text + " " + this.lastnameTextBox.Text + " was added successfully!", "Add successful");
                await deleteAlert.ShowAsync();
            }
            else
            {
                var deleteAlert =
                    new MessageDialog("Patient " + this.firstnameTextBox.Text + " " + this.lastnameTextBox.Text + " was not added successfully.", "Add failed");
                await deleteAlert.ShowAsync();
            }

            Frame.Navigate(typeof(MenuPage));
        }

        /// <summary>
        /// Gets the gender.
        /// </summary>
        /// <returns></returns>
        private Gender getGender()
        {
            if (this.genderComboBox.SelectedItem != null && this.genderComboBox.SelectedItem.Equals("Female"))
            {
                return Gender.Female;
            }

            return Gender.Male;
        }

        /// <summary>
        /// Clears the inputs.
        /// </summary>
        private void clearInputs()
        {
            this.firstnameTextBox.Text = "";
            this.lastnameTextBox.Text = "";
            this.birthdateDatePicker.Date = DateTimeOffset.Now;
            this.phoneNumberTextBox.Text = "";
            this.streetTextBox.Text = "";
            this.cityTextBox.Text = "";
            this.stateComboBox.SelectedItem = 0;
            this.zipCodeTextBox.Text = "";
        }

        /// <summary>
        /// Ons the deselect control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void onDeselectControl(object sender, RoutedEventArgs e)
        {
            this.registerButton.IsEnabled = this.checkControlsForCompletion();
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
    }
}
