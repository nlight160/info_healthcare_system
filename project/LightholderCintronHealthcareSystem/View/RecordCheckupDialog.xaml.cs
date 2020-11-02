using System;
using System.Collections.Generic;
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
    public sealed partial class RecordCheckupDialog : ContentDialog
    {
        public RecordCheckupDialog()
        {
            this.InitializeComponent();
            this.IsPrimaryButtonEnabled = false;
        }

        private void ContentDialog_SubmitButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_CancelButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Hide();
        }
        private void onKeyDown(object sender, KeyRoutedEventArgs e)
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

        private void onKeyDownDecimal(object sender, KeyRoutedEventArgs e)
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
            if ((e.Key == VirtualKey.Decimal || e.Key == (VirtualKey)190) && !pressed)
            {
                e.Handled = false;
                return;
            }
            e.Handled = true;
        }

        private void onDeselectControl(object sender, RoutedEventArgs e)
        {
            this.IsPrimaryButtonEnabled = this.areFieldsComplete();
        }

        private bool areFieldsComplete()
        {
            return this.SystolicTextBox.Text != "" && this.DiagnosisTextBox.Text != "" &&
                   this.DiastolicTextBox.Text != "" && this.TemperatureTextBox.Text != "" &&
                   this.WeightTextBox.Text != "" && this.PulseTextBox.Text != "";
        }
    }
}
