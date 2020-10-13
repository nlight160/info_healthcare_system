using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using LightholderCintronHealthcareSystem.Model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LightholderCintronHealthcareSystem.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private LoginCredentials login;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void onLogin(object sender, Windows.UI.Xaml.RoutedEventArgs e) 
        {
            if (UsernameTextBox.Text == String.Empty || UsernameTextBox.Text == null)
            {
                InvalidLoginTextBlock.Visibility = Visibility.Visible;
            }
            if (PasswordTextBox.Text == String.Empty || PasswordTextBox.Text == null)
            {
                InvalidLoginTextBlock.Visibility = Visibility.Visible;
            }
            this.setLoginCredentials();
        }

        private void setLoginCredentials()
        {
            this.login = new LoginCredentials(UsernameTextBox.Text, PasswordTextBox.Text);
        }
    }
}
