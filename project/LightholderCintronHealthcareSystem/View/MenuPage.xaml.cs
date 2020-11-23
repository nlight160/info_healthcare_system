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
            //this.isAdminPageButtonViewable();
            this.userWelcomeTextBlock.Text = "Welcome, " +"User: " + ViewModel.ViewModel.ActiveUser.UserId + ", " 
                                             + ViewModel.ViewModel.ActiveUser.PersonInfo.Firstname + " " + ViewModel.ViewModel.ActiveUser.PersonInfo.Lastname;
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
        private void onRegisterPatient(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegistrationPage));
        }

        /// <summary>
        /// Ons the view patients.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void onViewPatients(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ViewPatientsPage));
        }

        /// <summary>
        /// Ons the view appointments.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void onViewAppointments(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ViewAllAppointmentsPage));
        }

        private void onAdminControls(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AdminPage));
        }

        private void isAdminPageButtonViewable()
        {
            if (ViewModel.ViewModel.ActiveUser.IsAdmin)
            {
                this.adminControlsButton.Visibility = Visibility.Visible;
            }
            else
            {
                this.adminControlsButton.Visibility = Visibility.Collapsed;
            }
        }
    }
}
