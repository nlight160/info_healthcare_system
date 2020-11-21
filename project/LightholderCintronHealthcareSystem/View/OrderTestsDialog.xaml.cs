using LightholderCintronHealthcareSystem.Model;
using LightholderCintronHealthcareSystem.Model.DatabaseAccess;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LightholderCintronHealthcareSystem.View
{
    /// <summary>
    /// Order tests dialog
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.ContentDialog" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class OrderTestsDialog : ContentDialog
    {
        private TestOrder order;
        private int appointmentid;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderTestsDialog"/> class.
        /// </summary>
        /// <param name="appointment">The appointment.</param>
        public OrderTestsDialog(AppointmentDataGrid appointment)
        {
            this.InitializeComponent();
            this.confirmationFlyout.Hide();
            this.order = new TestOrder(new ObservableCollection<Test>());
            this.patientNameTextBlock.Text = appointment.PatientName;
            this.appointmentid = appointment.Appointmentid;
        }

        /// <summary>
        /// Appends the each test to string.
        /// </summary>
        /// <returns></returns>
        private string appendEachTestToString()
        {
            var totalString = "";
            foreach (var test in this.order.Order)
            {
                totalString += test.TestName + "\n";
            }
            return totalString;
        }

        /// <summary>
        /// Handles the SecondaryButtonClick event of the ContentDialog control.
        /// </summary>
        /// <param name="o">The source of the event.</param>
        /// <param name="routedEventArgs">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ContentDialog_SecondaryButtonClick(object o, RoutedEventArgs routedEventArgs)
        {
            this.confirmationFlyout.Hide();
            Hide();
        }

        /// <summary>
        /// Ons the add test click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void onAddTestClick(object sender, RoutedEventArgs e)
        {
            if (this.testComboBox.SelectedItem != null)
            {
                var test = new Test(this.testComboBox.SelectionBoxItem.ToString()) {
                    AppointmentId = this.appointmentid
                };
                this.order.Order.Add(test);
                this.testOrderGrid.ItemsSource = this.order.Order;
            }
            
        }

        /// <summary>
        /// Ons the remove test click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void onRemoveTestClick(object sender, RoutedEventArgs e)
        {
            if (this.testOrderGrid.SelectedItem != null)
            {
                this.order.Order.Remove(this.testOrderGrid.SelectedItem as Test);
                this.testOrderGrid.ItemsSource = this.order.Order;
            }

        }

        /// <summary>
        /// Handles the OnClick event of the ConfirmButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void ConfirmButton_OnClick(object sender, RoutedEventArgs e)
        {

            if (this.order.Order.Count > 0)
            {
                TestDatabaseAccess tdb = new TestDatabaseAccess();
                foreach (var test in this.order.Order)
                {
                    tdb.AddTests(test);
                }
                this.confirmationFlyout.Hide();
                this.Hide();

            }
            else
            {

                var message = new MessageDialog("You must add tests to your order to place one!", "Order Empty!");
                await message.ShowAsync();
                this.confirmationFlyout.Hide();
            }

        }

        /// <summary>
        /// Handles the OnClick event of the DenyButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void DenyButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.confirmationFlyout.Hide();
        }
    }
}
