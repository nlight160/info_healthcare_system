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
using LightholderCintronHealthcareSystem.Model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LightholderCintronHealthcareSystem.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MenuPage : Page
    {
        public MenuPage()
        {
            this.InitializeComponent();
            UserWelcomeTextBlock.Text = "Welcome, " + ViewModel.ViewModel.ActiveUser.NurseInfo.Firstname + " " + ViewModel.ViewModel.ActiveUser.NurseInfo.Lastname;
        }

        private void onLogout(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
            ViewModel.ViewModel.Logout();
        }

        private void OnRegisterPatient(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegistrationPage));
        }
    }
}
