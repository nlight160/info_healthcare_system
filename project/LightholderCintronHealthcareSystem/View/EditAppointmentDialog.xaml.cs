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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LightholderCintronHealthcareSystem.View
{
    public sealed partial class EditAppointmentDialog : ContentDialog
    {
        public EditAppointmentDialog()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void onDeselectDoctorIdTextBlock(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void onDeselectControl(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ContentDialog_CancelButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            throw new NotImplementedException();
        }

        private void ContentDialog_SubmitButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
