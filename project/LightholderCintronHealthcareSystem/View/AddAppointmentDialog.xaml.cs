using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LightholderCintronHealthcareSystem.View
{
    /// <summary>
    /// Add appointment dialog
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.ContentDialog" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class AddAppointmentDialog : ContentDialog
    {
        private readonly int patientid;
        private readonly string patientFirstName;
        private readonly string patientLastName;
        private readonly ToolTip dateTip;
        private readonly List<int> doctorids;


        /// <summary>
        /// Initializes a new instance of the <see cref="AddAppointmentDialog"/> class.
        /// </summary>
        /// <param name="patientid">The patientid.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        public AddAppointmentDialog(int patientid, string firstName, string lastName)
        {
            this.InitializeComponent();
            this.dateTip = new ToolTip();
            
            ToolTipService.SetToolTip(this.descriptionTextBox, this.dateTip);
            this.dateTip.Placement = PlacementMode.Bottom;

            this.patientid = patientid;
            this.patientFirstName = firstName;
            this.patientLastName = lastName;

            this.dateDatePicker.Date = DateTimeOffset.Now;
            this.timeTimePicker.Time = TimeSpan.Zero;

            this.IsPrimaryButtonEnabled = false;
            this.patientFirstNameTextBlock.Text = this.patientFirstName;
            this.patientLastNameTextBlock.Text = this.patientLastName;
            this.patientIdTextBlock.Text = this.patientid.ToString();
            this.dateDatePicker.MinYear = DateTimeOffset.Now;

            this.doctorids = ViewModel.ViewModel.GetEveryDoctorId();
            this.doctorIdComboBox.ItemsSource = this.doctorids;
        }

        /// <summary>
        /// Contents the dialog submit button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ContentDialogButtonClickEventArgs"/> instance containing the event data.</param>
        private async void ContentDialog_SubmitButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var dateTime = new DateTime(this.dateDatePicker.Date.Year, this.dateDatePicker.Date.Month, this.dateDatePicker.Date.Day, this.timeTimePicker.Time.Hours, this.timeTimePicker.Time.Minutes, 0);

            var selectedItem = this.doctorIdComboBox.SelectedItem;
            var confirmation = selectedItem != null && ViewModel.ViewModel.createAppointment(this.patientid.ToString(), dateTime, selectedItem.ToString(), this.descriptionTextBox.Text);

            if (confirmation)
            {
                var newAppointmentDialog = new MessageDialog("A new appointment was created for " + this.patientFirstName + " " + this.patientLastName, "Appointment Added!");
                await newAppointmentDialog.ShowAsync();
            }
            else
            {
                var updateAppointmentDialog = new MessageDialog("Appointment for " + this.patientFirstName + " " + this.patientLastName + " was not added, please try again.", "Failed");
                await updateAppointmentDialog.ShowAsync();
            }

            this.dateTip.IsOpen = false;
        }

        /// <summary>
        /// Contents the dialog cancel button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ContentDialogButtonClickEventArgs"/> instance containing the event data.</param>
        private void ContentDialog_CancelButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.dateTip.IsOpen = false;
            Hide();
        }

        /// <summary>
        /// Ons the deselect doctor identifier text block.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void onDeselectDoctorIdTextBlock(object sender, RoutedEventArgs e)
        {
            if ( this.doctorIdComboBox.SelectedIndex > -1 && this.doctorIdComboBox.SelectedItem != null)
            {
                
                try 
                {
                    var doctorName = ViewModel.ViewModel.getDoctorName(int.Parse(this.doctorIdComboBox.SelectedItem.ToString()));
                    this.doctorFirstNameTextBlock.Text = doctorName[0];
                    this.doctorLastNameTextBlock.Text = doctorName[1];
                }
                catch(Exception exception)
                {
                    Debug.WriteLine(exception.Message);
                    var notFoundDialog = new MessageDialog("A doctor with the ID provided was not found.", "Doctor not found!");
                    await notFoundDialog.ShowAsync();
                }
            }
            this.IsPrimaryButtonEnabled = this.checkForCompetion();
        }

        /// <summary>
        /// Checks for competion.
        /// </summary>
        /// <returns></returns>
        private bool checkForCompetion()
        {
            if (!this.checkForDate())
            {
                this.dateTip.Content = "Date/Time must be in the future not the past.";
                this.dateTip.IsOpen = true;
                return false;
            }
            this.dateTip.IsOpen = false;
            return this.doctorFirstNameTextBlock.Text != "" && !this.checkForDoubleBook() && this.descriptionTextBox.Text != "";
        }

        /// <summary>
        /// Checks for time.
        /// </summary>
        /// <returns></returns>
        private bool checkForTime()
        {
            if (this.timeTimePicker.Time.Hours == DateTime.Now.Hour)
            {
                if (this.timeTimePicker.Time.Minutes == DateTime.Now.Minute)
                {
                    return false;
                }

                if (this.dateDatePicker.Date.Minute <= DateTime.Now.Minute)
                {
                    return false;
                }
            }
            return this.timeTimePicker.Time.Hours >= DateTime.Now.Hour;
        }

        /// <summary>
        /// Checks for date.
        /// </summary>
        /// <returns></returns>
        private bool checkForDate()
        {
            if (this.dateDatePicker.Date.Year == DateTime.Now.Year)
            {
                if (this.dateDatePicker.Date.Month < DateTime.Now.Month)
                {
                    return false;
                }
                if (this.dateDatePicker.Date.Month == DateTime.Now.Month)
                {
                    if (this.dateDatePicker.Date.Day < DateTime.Now.Day)
                    {
                        return false;
                    }
                    if (this.dateDatePicker.Date.Day == DateTime.Now.Day)
                    {
                        return this.checkForTime();
                    }
                }
            }
            return this.dateDatePicker.Date.Year >= DateTime.Now.Year;
        }

        /// <summary>
        /// Checks for doctor double book.
        /// </summary>
        /// <returns></returns>
        private bool checkForDoctorDoubleBook()
        {
            if (this.doctorIdComboBox.SelectedIndex !> -1 && this.doctorIdComboBox.SelectedItem == null)
            {
                return true;
            }
            var requestedTime = this.dateDatePicker.Date.Date.Add(this.timeTimePicker.Time);
            if (ViewModel.ViewModel.checkForDoctorDoubleBook(requestedTime, int.Parse(this.doctorIdComboBox.SelectedItem.ToString())))
            {
                this.dateTip.Content = "Doctor already booked for this Date/Time.";
                this.dateTip.IsOpen = true;
                return true;
            }
            this.dateTip.IsOpen = false;
            return false;

        }

        /// <summary>
        /// Checks for patient double book.
        /// </summary>
        /// <returns></returns>
        private bool checkForPatientDoubleBook()
        {
            
            var requestedTime = this.dateDatePicker.Date.Date.Add(this.timeTimePicker.Time);
            if (ViewModel.ViewModel.checkForPatientDoubleBook(requestedTime, this.patientid))
            {
                this.dateTip.Content = "Patient already booked for this Date/Time.";
                this.dateTip.IsOpen = true;
                return true;
            }
            this.dateTip.IsOpen = false;
            return false;

        }

        /// <summary>
        /// Checks for double book.
        /// </summary>
        /// <returns></returns>
        private bool checkForDoubleBook()
        {
            return this.checkForDoctorDoubleBook() || this.checkForPatientDoubleBook();
        }


        /// <summary>
        /// Ons the deselect control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void onDeselectControl(object sender, RoutedEventArgs e)
        {
            this.IsPrimaryButtonEnabled = this.checkForCompetion();
        }
    }
}
