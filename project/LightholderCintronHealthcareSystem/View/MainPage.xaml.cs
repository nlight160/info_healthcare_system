using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using LightholderCintronHealthcareSystem.Model;

namespace LightholderCintronHealthcareSystem.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void onLogin(object sender, Windows.UI.Xaml.RoutedEventArgs e) 
        {
            if (UsernameTextBox.Text == string.Empty || UsernameTextBox.Text == null)
            {
                InvalidLoginTextBlock.Visibility = Visibility.Visible;
                this.clearTextBoxes();
            }

            if (PasswordTextBox.Text == string.Empty || PasswordTextBox.Text == null)
            {
                InvalidLoginTextBlock.Visibility = Visibility.Visible;
                this.clearTextBoxes();
            }
            if (ViewModel.ViewModel.AttemptLogin(UsernameTextBox.Text.Trim(), PasswordTextBox.Text.Trim()))
            {
                Frame.Navigate(typeof(MenuPage));
            }
            else
            {
                InvalidLoginTextBlock.Visibility = Visibility.Visible;
                this.clearTextBoxes();
            }

        }

        private void clearTextBoxes()
        {
            PasswordTextBox.Text = "";
            UsernameTextBox.Text = "";
        }
    }
}
