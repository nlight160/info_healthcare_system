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
        private User user;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void onLogin(object sender, Windows.UI.Xaml.RoutedEventArgs e) 
        {
            if (UsernameTextBox.Text == String.Empty || UsernameTextBox.Text == null)
            {
                InvalidLoginTextBlock.Visibility = Visibility.Visible;
                this.clearTextBoxes();
            }

            if (PasswordTextBox.Text == String.Empty || PasswordTextBox.Text == null)
            {
                InvalidLoginTextBlock.Visibility = Visibility.Visible;
                this.clearTextBoxes();
            }

            if (attemptLogin())
            {
                var messageDialog = new MessageDialog("Congrats! You logged in.");
                await messageDialog.ShowAsync();
            }

        }

        private void clearTextBoxes()
        {
            PasswordTextBox.Text = "";
            UsernameTextBox.Text = "";
        }

        private bool attemptLogin()
        {
            var loginCredentials = new LoginCredentials(UsernameTextBox.Text, PasswordTextBox.Text);
            this.user = new User(loginCredentials);
            return this.user.VerifyUserExists();
        }
    }
}
