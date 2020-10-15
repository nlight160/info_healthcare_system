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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LightholderCintronHealthcareSystem.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            this.InitializeComponent();
            PhoneNumberTextBox.MaxLength = 10;
            ZipCodeTextBox.MaxLength = 5;
            BirthdateDatePicker.MaxYear = DateTimeOffset.Now;
        }

        private void onKeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key.ToString().Equals("Back"))
            {
                e.Handled = false;
                return;
            }
            for (int i = 0; i < 10; i++)
            {
                if (e.Key.ToString() == string.Format("Number{0}", i))
                {
                    e.Handled = false;
                    return;
                }
            }
            e.Handled = true;
        }

        private void onCancel(object sender, RoutedEventArgs e)
        {
            this.clearInputs();
            Frame.Navigate(typeof(MenuPage));
        }

        private void onRegister(object sender, RoutedEventArgs e)
        {

        }

        private void clearInputs()
        {
            FirstnameTextBox.Text = "";
            LastnameTextBox.Text = "";
            BirthdateDatePicker.Date = DateTimeOffset.Now;
            PhoneNumberTextBox.Text = "";
            StreetTextBox.Text = "";
            CityTextBox.Text = "";
            StateTextBox.Text = "";
            ZipCodeTextBox.Text = "";
        }
    }
}
