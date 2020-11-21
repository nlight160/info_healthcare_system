using LightholderCintronHealthcareSystem.Model;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LightholderCintronHealthcareSystem.View
{
    /// <summary>
    /// Views tests
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.ContentDialog" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class ViewTestsDialog : ContentDialog
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewTestsDialog"/> class.
        /// </summary>
        /// <param name="appointment">The appointment.</param>
        public ViewTestsDialog(AppointmentDataGrid appointment)
        {
            this.InitializeComponent();
            this.patientNameTextBlock.Text = appointment.PatientName;
        }

        /// <summary>
        /// Contents the dialog primary button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ContentDialogButtonClickEventArgs"/> instance containing the event data.</param>
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Hide();
        }
    }
}
