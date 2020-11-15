using LightholderCintronHealthcareSystem.Model;
using LightholderCintronHealthcareSystem.Model.People;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LightholderCintronHealthcareSystem.View
{
    public sealed partial class OrderTestsDialog : ContentDialog
    {
        private TestOrder order;

        public OrderTestsDialog(AppointmentDataGrid appointment)
        {
            this.InitializeComponent();
            this.order = new TestOrder(new ObservableCollection<Test>());
            this.PatientNameTextBlock.Text = appointment.PatientName;
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (this.order.Order.Count > 0)
            {
                ContentDialog placeOrderConfirmationDialog = new ContentDialog
                {
                    Title = "Are you sure?",
                    Content = "Are you sure you would like to place an order for " + this.order.Order.Count + " tests?\n\n" + appendEachTestToString(),
                    PrimaryButtonText = "Ok",
                    CloseButtonText = "Cancel"
                };
                Hide();
                ContentDialogResult result = await placeOrderConfirmationDialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    // TODO
                }
            }
            else
            {
                var message = new MessageDialog("You must add tests to your order to place one!", "Order Empty!");
                await message.ShowAsync();
            }
            
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

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Hide();
        }

        private void onAddTestClick(object sender, RoutedEventArgs e)
        {
            if (TestComboBox.SelectedItem != null)
            {
                Test test = new Test(this.TestComboBox.SelectionBoxItem.ToString());
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
    }
}
