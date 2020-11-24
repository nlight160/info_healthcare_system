using System;
using System.Collections.Generic;
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
using LightholderCintronHealthcareSystem.Model.DatabaseAccess;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LightholderCintronHealthcareSystem.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdminPage : Page
    {
        public AdminPage()
        {
            this.InitializeComponent();
            this.disableSubmit();
            this.ActiveUserTextBlock.Text = "User: " + ViewModel.ViewModel.ActiveUser.PersonInfo.Personid 
                                                     + ", " + ViewModel.ViewModel.ActiveUser.PersonInfo.Firstname
                                                     + " " + ViewModel.ViewModel.ActiveUser.PersonInfo.Firstname;
        }

        /// <summary>
        /// Ons the go back.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void onGoBack(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPage));
        }

        /// <summary>
        /// Ons the submit.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.RoutedEventArgs" /> instance containing the event data.</param>
        private async void onSubmit(object sender, RoutedEventArgs e)
        {
            AdminDatabaseAccess ada = new AdminDatabaseAccess();
            try
            {
                ViewModel.ViewModel.FillDataGrid(ada.MakeAdminQuery(this.QueryTextBox.Text), this.ResultsDataGrid);
                var dialog = new MessageDialog("Query has been successfully processed", "Success");
                await dialog.ShowAsync();
            }
            catch (Exception error)
            {
                var dialog = new MessageDialog("Something went wrong check your syntax and try again!", "Uh-Oh");
                await dialog.ShowAsync();
            }
        }

        /// <summary>
        /// Disables the submit.
        /// </summary>
        private void disableSubmit()
        {
            if (string.IsNullOrEmpty(this.QueryTextBox.Text))
            {
                this.SubmitButton.IsEnabled = false;
            }
            else
            {
                this.SubmitButton.IsEnabled = true;
            }
        }

        /// <summary>
        /// Ons the disable submit.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Windows.UI.Xaml.Input.LosingFocusEventArgs" /> instance containing the event data.</param>
        private void onDisableSubmit(UIElement sender, LosingFocusEventArgs args)
        {
            this.disableSubmit();
        }
    }
}
