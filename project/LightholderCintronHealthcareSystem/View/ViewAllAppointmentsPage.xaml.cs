using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LightholderCintronHealthcareSystem.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewAllAppointmentsPage : Page
    {

        /*  TODO
         *  Add buttons - thinking of adding a View details and making a content dialog class to view details of the appointment
         *      to where you can then add a checkup, diagnosis, order tests, enter tests, ect.
         *  Add edit appointment button and delete appointment button.
         *  Do not think add appointment should be on here because then we have to make a new dialog but to where user types in for name.
         *
         * Also thinking of adding checkboxes to refine searching of appointments from show only past appointments or show only future appointments
         * Searching by doctor to only show appointments given a certain doctor.
         *
         *
         *  I think having the buttons on one side and then the filtering and searching on the other side of the data view would be best.
         *
         *
         */







        /// <summary>
        /// Initializes a new instance of the <see cref="ViewAllAppointmentsPage"/> class.
        /// </summary>
        public ViewAllAppointmentsPage()
        {
            this.InitializeComponent();
            this.appointmentDataView.ItemsSource = ViewModel.ViewModel.getAllAppointments();
            this.userTextBlock.Text = "User: " + ViewModel.ViewModel.ActiveUser.UserId + ", "
                                      + ViewModel.ViewModel.ActiveUser.NurseInfo.Firstname + " " + ViewModel.ViewModel.ActiveUser.NurseInfo.Lastname;
        }
    }
}
