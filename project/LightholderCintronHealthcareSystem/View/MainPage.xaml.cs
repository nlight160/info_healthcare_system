using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LightholderCintronHealthcareSystem.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Ons the login.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.RoutedEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Clears the text boxes.
        /// </summary>
        private void clearTextBoxes()
        {
            PasswordTextBox.Text = "";
            UsernameTextBox.Text = "";
        }
    }
}
