using LightholderCintronHealthcareSystem.Model;
using LightholderCintronHealthcareSystem.Model.DatabaseAccess;
using LightholderCintronHealthcareSystem.Model.People;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LightholderCintronHealthcareSystem.View
{
    public sealed partial class OrderTestsDialog : ContentDialog
    {
        private TestOrder order;
        private int appointmentid;

        public OrderTestsDialog(AppointmentDataGrid appointment)
        {
            this.InitializeComponent();
            this.ConfirmationFlyout.Hide();
            this.order = new TestOrder(new ObservableCollection<Test>());
            this.PatientNameTextBlock.Text = appointment.PatientName;
            this.appointmentid = appointment.Appointmentid;
        }

        private string appendEachTestToString()
        {
            var totalString = "";
            foreach (var test in this.order.Order)
            {
                totalString += test.TestName + "\n";
            }
            return totalString;
        }

        private void ContentDialog_SecondaryButtonClick(object o, RoutedEventArgs routedEventArgs)
        {
            this.ConfirmationFlyout.Hide();
            Hide();
        }

        private void onAddTestClick(object sender, RoutedEventArgs e)
        {
            if (TestComboBox.SelectedItem != null)
            {
                Test test = new Test(this.TestComboBox.SelectionBoxItem.ToString());
                test.AppointmentId = this.appointmentid;
                this.order.Order.Add(test);
                this.TestOrderGrid.ItemsSource = this.order.Order;
            }
            
        }

        private void onRemoveTestClick(object sender, RoutedEventArgs e)
        {
            if (this.TestOrderGrid.SelectedItem != null)
            {
                this.order.Order.Remove(this.TestOrderGrid.SelectedItem as Test);
                this.TestOrderGrid.ItemsSource = this.order.Order;
            }

        }

        private async void ConfirmButton_OnClick(object sender, RoutedEventArgs e)
        {

            if (this.order.Order.Count > 0)
            {
                TestDatabaseAccess tdb = new TestDatabaseAccess();
                foreach (var test in this.order.Order)
                {
                    tdb.AddTests(test);
                }
                this.ConfirmationFlyout.Hide();
                this.Hide();

            }
            else
            {

                var message = new MessageDialog("You must add tests to your order to place one!", "Order Empty!");
                await message.ShowAsync();
                this.ConfirmationFlyout.Hide();
            }

        }

        private void DenyButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.ConfirmationFlyout.Hide();
        }
    }
}
