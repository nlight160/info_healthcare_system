﻿using System;
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
        private readonly int appointmentid;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordCheckupDialog"/> class.
        /// </summary>
        /// <param name="appointmentid"></param>
        public RecordCheckupDialog(int appointmentid)
        {
            this.appointmentid = appointmentid;
            this.InitializeComponent();
            this.IsPrimaryButtonEnabled = false;
            this.patientIdAndNameTextBlock.Text =
                "Patient: " + ViewModel.ViewModel.getPatientNameFromAppointmentid(appointmentid);
            this.doctorNameAndIdTextBlock.Text = "Doctor: " + ViewModel.ViewModel.getDoctorNameFromAppointmentid(appointmentid);
        }

        /// <summary>
        /// Contents the dialog submit button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ContentDialogButtonClickEventArgs"/> instance containing the event data.</param>
        private void ContentDialog_SubmitButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            try
            {
                string content;
                string title;
                var systolic = int.Parse(this.systolicTextBox.Text);
                var diastolic = int.Parse(this.diastolicTextBox.Text);
                var temperature = decimal.Parse(this.temperatureTextBox.Text);
                var weight = decimal.Parse(this.weightTextBox.Text);
                var pulse = int.Parse(this.pulseTextBox.Text);
                var diagnosis = this.diagnosisTextBox.Text;

                var successful = ViewModel.ViewModel.createCheckup(this.appointmentid, systolic, diastolic, temperature, weight, pulse,
                    diagnosis, "");

                if (successful)
                {
                    content = "The checkup was recorded successfully!";
                    title = "Success";
                }
                else
                {
                    content = "The checkup could not be recorded, please try again.";
                    title = "Error";
                }
                confirmations(content, title);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                const string content = "The checkup could not be recorded, double check provided information.";
                const string title = "Error recording checkup!";
                confirmations(content, title);
            }

        }

        /// <summary>
        /// Confirmationses the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="title">The title.</param>
        private static async void confirmations(string content, string title)
        {
            var dialog = new MessageDialog(content, title);
            await dialog.ShowAsync();
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
