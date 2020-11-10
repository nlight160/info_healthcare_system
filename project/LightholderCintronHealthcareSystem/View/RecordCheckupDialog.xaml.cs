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
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using LightholderCintronHealthcareSystem.Model;
using LightholderCintronHealthcareSystem.Model.DatabaseAccess;
using LightholderCintronHealthcareSystem.Model.People;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LightholderCintronHealthcareSystem.View
{
    public sealed partial class RecordCheckupDialog : ContentDialog
    {
        private Patient patient;
        public RecordCheckupDialog(Patient patient)
        {
            this.InitializeComponent();
            this.IsPrimaryButtonEnabled = false;
            this.PatientIdAndNameTextBlock.Text = "Patient: " + patient.Patientid + ", " + patient.Firstname + " " +
                patient.Lastname;
            this.DoctorNameAndIdTextBlock.Text = "Doctor: ";
            this.patient = patient;

        }

        private async void ContentDialog_SubmitButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            CheckupDatabaseAccess cdb = new CheckupDatabaseAccess();
            AppointmentDatabaseAccess adb = new AppointmentDatabaseAccess();
            int patientid = int.Parse(this.patient.Patientid);
            try
            {
                cdb.CreateCheckup(new Checkup(null, int.Parse(adb.GetAppointmentFromPatientid(patientid)[0][0]), int.Parse(this.SystolicTextBox.Text),
                    int.Parse(this.DiastolicTextBox.Text), decimal.Parse(this.TemperatureTextBox.Text),
                    decimal.Parse(this.WeightTextBox.Text), int.Parse(this.PulseTextBox.Text), DiagnosisTextBox.Text));
            }
            catch (ArgumentException exception)
            {
                Debug.WriteLine(exception.Message);
                MessageDialog errorDialog = new MessageDialog("The checkup could not be recorded, double check provided information.", "Error recording checkup!");
                await errorDialog.ShowAsync();
            }
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
