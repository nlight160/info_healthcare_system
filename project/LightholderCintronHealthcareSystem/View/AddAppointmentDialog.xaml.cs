using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using LightholderCintronHealthcareSystem.Model.DatabaseAccess;
using LightholderCintronHealthcareSystem.Model.People;
using Org.BouncyCastle.Asn1.Cms;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LightholderCintronHealthcareSystem.View
{
    public sealed partial class AddAppointmentDialog : ContentDialog
    {

        public AddAppointmentDialog(Patient patient)
        {
            this.InitializeComponent();
            this.IsPrimaryButtonEnabled = false;
            this.patientFirstNameTextBlock.Text = patient.Firstname;
            this.patientLastNameTextBlock.Text = patient.Lastname;
            this.patientIdTextBlock.Text = patient.Patientid;
            this.dateDatePicker.MinYear = DateTimeOffset.Now;
            this.dateDatePicker.Date = DateTimeOffset.Now;
            this.timeTimePicker.Time = TimeSpan.Zero;
        }

        private void ContentDialog_SubmitButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            throw new NotImplementedException();
        }

        private void ContentDialog_CancelButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Hide();
        }

        private async void onDeselectDoctorIdTextBlock(object sender, RoutedEventArgs e)
        {
            DoctorDatabaseAccess ddb = new DoctorDatabaseAccess();
            if (!string.IsNullOrEmpty(doctorIdTextBox.Text) && !string.IsNullOrWhiteSpace(doctorIdTextBox.Text))
            {
                var doctor = ddb.GetDoctorDataFromId(int.Parse(this.doctorIdTextBox.Text));
                try 
                {
                
                    this.doctorFirstNameTextBlock.Text = doctor[0];
                    this.doctorLastNameTextBlock.Text = doctor[1];
                }
                catch(ArgumentOutOfRangeException exception)
                {
                    Debug.WriteLine(exception.Message);
                    MessageDialog notFounDialog = new MessageDialog("A doctor with the ID provided was not found.", "Doctor not found!");
                    await notFounDialog.ShowAsync();
                }
                
            }
            this.IsPrimaryButtonEnabled = this.checkForCompetion();
        }

        private bool checkForCompetion()
        {
            return this.doctorIdTextBox.Text != "" &&
                   this.descriptionTextBox.Text != "";
        }

        private void onDeselectControl(object sender, RoutedEventArgs e)
        {
            this.IsPrimaryButtonEnabled = this.checkForCompetion();
        }
    }
}
