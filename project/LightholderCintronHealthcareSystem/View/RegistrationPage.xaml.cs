using System;
using System.Diagnostics;
using Windows.System;
using Windows.UI.Core;
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
            this.RegisterButton.IsEnabled = false;
            this.PhoneNumberTextBox.MaxLength = 10;
            this.ZipCodeTextBox.MaxLength = 5;
            this.BirthdateDatePicker.MaxYear = DateTimeOffset.Now;
            this.UserTextBlock.Text = "User: " + ViewModel.ViewModel.ActiveUser.UserId + ", "
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
            bool pressed = (state & CoreVirtualKeyStates.Down) == CoreVirtualKeyStates.Down;
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
        private void onRegister(object sender, RoutedEventArgs e)
        {
            string date = BirthdateDatePicker.Date.Year + "-" + BirthdateDatePicker.Date.Month + "-" + BirthdateDatePicker.Date.Day;
            ViewModel.ViewModel.RegisterPatient(LastnameTextBox.Text, FirstnameTextBox.Text, date, StreetTextBox.Text, CityTextBox.Text, 
                StateComboBox.SelectedItem.ToString(), ZipCodeTextBox.Text, PhoneNumberTextBox.Text);
            Frame.Navigate(typeof(MenuPage));
        }

        /// <summary>
        /// Clears the inputs.
        /// </summary>
        private void clearInputs()
        {
            FirstnameTextBox.Text = "";
            LastnameTextBox.Text = "";
            BirthdateDatePicker.Date = DateTimeOffset.Now;
            PhoneNumberTextBox.Text = "";
            StreetTextBox.Text = "";
            CityTextBox.Text = "";
            StateComboBox.SelectedItem = 0;
            ZipCodeTextBox.Text = "";
        }

        private void onDeselectControl(object sender, RoutedEventArgs e)
        {
            this.RegisterButton.IsEnabled = this.checkControlsForCompletion();
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
    }
}
