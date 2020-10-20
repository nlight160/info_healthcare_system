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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LightholderCintronHealthcareSystem.View
{
    public sealed partial class EditPatientDialog : ContentDialog
    {
        public EditPatientDialog()
        {
            this.InitializeComponent();
            this.PhoneNumberTextBox.MaxLength = 10;
            this.ZipCodeTextBox.MaxLength = 5;
            this.BirthdateDatePicker.MaxYear = DateTimeOffset.Now;
            this.IsPrimaryButtonEnabled = false;
        }

        private void ContentDialog_SubmitButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
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
