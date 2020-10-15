using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LightholderCintronHealthcareSystem.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MenuPage : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuPage"/> class.
        /// </summary>
        public MenuPage()
        {
            this.InitializeComponent();
            UserWelcomeTextBlock.Text = "Welcome, " +"User: " + ViewModel.ViewModel.ActiveUser.UserId + ", " 
                                        + ViewModel.ViewModel.ActiveUser.NurseInfo.Firstname + " " + ViewModel.ViewModel.ActiveUser.NurseInfo.Lastname;
        }

        /// <summary>
        /// Ons the logout.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void onLogout(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
            ViewModel.ViewModel.Logout();
        }

        /// <summary>
        /// Called when [register patient].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnRegisterPatient(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegistrationPage));
        }
    }
}
