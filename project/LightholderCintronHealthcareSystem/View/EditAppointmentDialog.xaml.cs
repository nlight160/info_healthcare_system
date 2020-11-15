using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LightholderCintronHealthcareSystem.View
{
    /// <summary>
    /// Edit appointment dialog
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.ContentDialog" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class EditAppointmentDialog : ContentDialog
    {
        private readonly int appointmentid;
        private readonly int patientid;
        private readonly string firstName;
        private readonly string lastName;
        private readonly int doctorid;
        private DateTime dateTime;
        private readonly string description;
        private readonly ToolTip dateTip;
        private readonly List<int> doctorids;


        /// <summary>
        /// Initializes a new instance of the <see cref="EditAppointmentDialog"/> class.
        /// </summary>
        /// <param name="appointmentid">The appointmentid.</param>
        /// <param name="patientid">The patientid.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="doctorid">The doctorid.</param>
        /// <param name="dateTime">The date time.</param>
        /// <param name="description">The description.</param>
        public EditAppointmentDialog(int appointmentid, int patientid, string firstName, string lastName, int doctorid, DateTime dateTime, string description)
        {
            this.InitializeComponent();
            this.IsPrimaryButtonEnabled = false;
            this.dateTip = new ToolTip();
            ToolTipService.SetToolTip(this.descriptionTextBox, this.dateTip);
            this.dateTip.Placement = PlacementMode.Bottom;
            this.appointmentid = appointmentid;
            this.patientid = patientid;
            this.firstName = firstName;
            this.lastName = lastName;
            this.doctorid = doctorid;
            this.dateTime = dateTime;
            this.description = description;
            this.setAppointmentDetails();
            this.doctorids = ViewModel.ViewModel.GetEveryDoctorId();
            this.doctorIdComboBox.ItemsSource = this.doctorids;
            this.doctorIdComboBox.SelectedItem = this.doctorid;
        }

        /// <summary>
        /// Sets the appointment details.
        /// </summary>
        private void setAppointmentDetails()
        {
            this.patientIdTextBlock.Text = this.patientid.ToString();
            this.patientFirstNameTextBlock.Text = this.firstName;
            this.patientLastNameTextBlock.Text = this.lastName;
            this.updateDoctorName();
            this.dateDatePicker.Date = this.dateTime.Date;
            this.timeTimePicker.Time = this.dateTime.TimeOfDay;
            this.descriptionTextBox.Text = this.description;
            this.doctorIdComboBox.Text = this.doctorid.ToString();

        }

        /// <summary>
        /// Updates the name of the doctor.
        /// </summary>
        private async void updateDoctorName()
        {
            if (this.doctorIdComboBox.SelectedIndex > -1 && this.doctorIdComboBox.SelectedItem != null)
            {
                var doctorName = ViewModel.ViewModel.getDoctorName(int.Parse(this.doctorIdComboBox.SelectedItem.ToString()));
                try
                {
                    this.doctorFirstNameTextBlock.Text = doctorName[0];
                    this.doctorLastNameTextBlock.Text = doctorName[1];
                }
                catch (ArgumentOutOfRangeException exception)
                {
                    Debug.WriteLine(exception.Message);
                    MessageDialog notFounDialog = new MessageDialog("A doctor with the ID provided was not found.", "Doctor not found!");
                    await notFounDialog.ShowAsync();
                }
            }
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
            var confirmation = selectedItem != null && ViewModel.ViewModel.EditAppointment(this.appointmentid, dateTime, int.Parse(selectedItem.ToString()), this.descriptionTextBox.Text);

            if (confirmation)
            {
                var dialog = new MessageDialog("Appointment was edited for " + this.firstName + " " + this.lastName, "Appointment edited!");
                await dialog.ShowAsync();
            }
            else
            {
                var dialog = new MessageDialog("Appointment for " + this.firstName + " " + this.lastName + " was not edited, please try again.", "Failed");
                await dialog.ShowAsync();
            }

            this.dateTip.IsOpen = false;
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
            if (this.doctorIdComboBox.SelectedIndex! > -1 && this.doctorIdComboBox.SelectedItem == null)
            {
                return true;
            }
            var requestedTime = this.dateDatePicker.Date.Date.Add(this.timeTimePicker.Time);
            var selectedItem = this.doctorIdComboBox.SelectedItem;
            if (selectedItem != null && ViewModel.ViewModel.checkForDoctorDoubleBook(requestedTime, int.Parse(selectedItem.ToString()), this.appointmentid))
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
            if (ViewModel.ViewModel.checkForPatientDoubleBook(requestedTime, this.patientid, this.appointmentid))
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
        /// Ons the key down edit.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyRoutedEventArgs"/> instance containing the event data.</param>
        private void onKeyDownEdit(object sender, KeyRoutedEventArgs e)
        {
            this.IsPrimaryButtonEnabled = this.checkForCompetion();
        }

        /// <summary>
        /// Ons the deselect doctor identifier text block.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void onDeselectDoctorIdTextBlock(object sender, RoutedEventArgs e)
        {
            this.updateDoctorName();
        }
    }
}
