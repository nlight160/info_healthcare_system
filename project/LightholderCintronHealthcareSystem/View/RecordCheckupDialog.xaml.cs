using LightholderCintronHealthcareSystem.Model;
using LightholderCintronHealthcareSystem.Model.DatabaseAccess;
using LightholderCintronHealthcareSystem.Model.People;
using System;
using System.Diagnostics;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LightholderCintronHealthcareSystem.View
{
    /// <summary>
    /// Record checkup dialog
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.ContentDialog" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class RecordCheckupDialog : ContentDialog
    {
        private readonly Patient patient;
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordCheckupDialog"/> class.
        /// </summary>
        /// <param name="patient">The patient.</param>
        public RecordCheckupDialog(Patient patient)
        {
            this.InitializeComponent();
            this.IsPrimaryButtonEnabled = false;
            this.patientIdAndNameTextBlock.Text = "Patient: " + patient.Patientid + ", " + patient.Firstname + " " +
                patient.Lastname;
            this.doctorNameAndIdTextBlock.Text = "Doctor: ";
            this.patient = patient;

        }

        /// <summary>
        /// Contents the dialog submit button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ContentDialogButtonClickEventArgs"/> instance containing the event data.</param>
        private async void ContentDialog_SubmitButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var cdb = new CheckupDatabaseAccess();
            var adb = new AppointmentDatabaseAccess();
            var patientid = int.Parse(this.patient.Patientid);
            try
            {
                cdb.CreateCheckup(new Checkup(null, int.Parse(adb.GetAppointmentFromPatientid(patientid)[0][0]), int.Parse(this.systolicTextBox.Text),
                    int.Parse(this.diastolicTextBox.Text), decimal.Parse(this.temperatureTextBox.Text),
                    decimal.Parse(this.weightTextBox.Text), int.Parse(this.pulseTextBox.Text), this.diagnosisTextBox.Text));
            }
            catch (ArgumentException exception)
            {
                Debug.WriteLine(exception.Message);
                var errorDialog = new MessageDialog("The checkup could not be recorded, double check provided information.", "Error recording checkup!");
                await errorDialog.ShowAsync();
            }
        }

        /// <summary>
        /// Contents the dialog cancel button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ContentDialogButtonClickEventArgs"/> instance containing the event data.</param>
        private void ContentDialog_CancelButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Hide();
        }
        /// <summary>
        /// Ons the key down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyRoutedEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Ons the key down decimal.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyRoutedEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Ons the deselect control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void onDeselectControl(object sender, RoutedEventArgs e)
        {
            this.IsPrimaryButtonEnabled = this.areFieldsComplete();
        }

        /// <summary>
        /// Ares the fields complete.
        /// </summary>
        /// <returns></returns>
        private bool areFieldsComplete()
        {
            return this.systolicTextBox.Text != "" && this.diagnosisTextBox.Text != "" &&
                   this.diastolicTextBox.Text != "" && this.temperatureTextBox.Text != "" &&
                   this.weightTextBox.Text != "" && this.pulseTextBox.Text != "";
        }
    }
}
