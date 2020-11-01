using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
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
            this.patientFirstNameTextBlock.Text = patient.Firstname;
            this.patientLastNameTextBlock.Text = patient.Lastname;
            this.patientIdTextBlock.Text = patient.Patientid;
            this.dateDatePicker.MinYear = DateTimeOffset.Now;
            this.doctorFirstNameTextBlock.Text = "";
            this.doctorLastNameTextBlock.Text = "";
        }

        private void ContentDialog_SubmitButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            throw new NotImplementedException();
        }

        private void ContentDialog_CancelButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Hide();
        }
    }
}
