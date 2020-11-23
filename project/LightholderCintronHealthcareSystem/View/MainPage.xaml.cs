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
        private void onLogin(object sender, RoutedEventArgs e) 
        {
            if (string.IsNullOrEmpty(this.usernameTextBox.Text))
            {
                this.invalidLoginTextBlock.Visibility = Visibility.Visible;
                this.clearTextBoxes();
            }

            if (string.IsNullOrEmpty(this.passwordTextBox.Text))
            {
                this.invalidLoginTextBlock.Visibility = Visibility.Visible;
                this.clearTextBoxes();
            }
            if (ViewModel.ViewModel.AttemptLogin(this.usernameTextBox.Text.Trim(), this.passwordTextBox.Text.Trim()))
            {
                Frame.Navigate(typeof(MenuPage));
            }
            if (ViewModel.ViewModel.AttemptAdminLogin(this.usernameTextBox.Text.Trim(),
                              this.passwordTextBox.Text.Trim()))
            {
                Frame.Navigate(typeof(MenuPage));
            }
            else
            {
                this.invalidLoginTextBlock.Visibility = Visibility.Visible;
                this.clearTextBoxes();
            }

        }

        /// <summary>
        /// Clears the text boxes.
        /// </summary>
        private void clearTextBoxes()
        {
            this.passwordTextBox.Text = "";
            this.usernameTextBox.Text = "";
        }
    }
}
