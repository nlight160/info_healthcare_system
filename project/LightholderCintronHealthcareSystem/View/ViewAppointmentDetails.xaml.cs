using LightholderCintronHealthcareSystem.Model;
using LightholderCintronHealthcareSystem.Model.DatabaseAccess;
using LightholderCintronHealthcareSystem.Model.People;
using System;
using System.Collections.Generic;
using System.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LightholderCintronHealthcareSystem.View
{
    /// <summary>
    /// View appointment details.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.ContentDialog" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class ViewAppointmentDetails : ContentDialog
    {
        private Dictionary<string, string> dataDictionary;
        private Dictionary<string, string> checkupDictionary;
        private readonly int appointmentid;
        private readonly AppointmentDataGrid appointment;

        private int checkupid;
        /*  TODO
         *  Would like to have some structure on the top right of the dialog with the checkup information filled out (if checkup was already done if not grey everything out + context menu saying to fill
         *      checkout out.
         *  then on the lower to mid right have a spot for ordering tests and entering tests and below that have a data grid with a dictionary to show test results
         *  
         *  Finally below all that have a final diagnosis textbox which is only active after checkup and all tests that were ordered were completed. grayed out with context menu until it is.
         *      + have to make sure user is sure to submit final diagnosis.
         *
         *
         */


        /// <summary>
        /// Initializes a new instance of the <see cref="ViewAppointmentDetails"/> class.
        /// </summary>
        /// <param name="appointment">The appointment.</param>
        public ViewAppointmentDetails(AppointmentDataGrid appointment)
        {
            this.InitializeComponent();
            this.appointment = appointment;
            this.appointmentid = appointment.Appointmentid;
            this.dataDictionary = new Dictionary<string, string>();
            this.appointmentDataView.ItemsSource = this.dataDictionary;
            this.dataDictionary.Add("Patient ID", appointment.Patientid.ToString());
            this.dataDictionary.Add("Patient Name", appointment.PatientName);
            this.dataDictionary.Add("Patient DOB", appointment.Dob);
            this.dataDictionary.Add("Doctor ID", appointment.Doctorid.ToString());
            this.dataDictionary.Add("Doctor Name", appointment.DoctorName);
            this.dataDictionary.Add("Appointment Date", appointment.Date);
            this.dataDictionary.Add("Appointment Time", appointment.Time);
            this.dataDictionary.Add("Description", appointment.Description);


            this.checkupDictionary = new Dictionary<string, string>();
            this.checkupDataView.ItemsSource = this.checkupDictionary;
            this.enterTestsButton.IsEnabled = false;
            this.orderTestsButton.IsEnabled = false;
            this.makeCheckupButton.IsEnabled = false;
            this.updateTests();
            this.updateCheckupInformation();

            this.updateIfAllTestsDone();

            this.checkIfCheckupDone();

            

            if (this.checkIfAppointmentPassed() | this.checkIfFinalDiagnosis())
            {
                this.enterTestsButton.IsEnabled = false;
                this.orderTestsButton.IsEnabled = false;
                this.makeCheckupButton.IsEnabled = false;
                this.submitFinalDiagnosisButton.IsEnabled = false;
                this.finalDiagnosisTextBox.IsEnabled = false;
            }

        }

        /// <summary>
        /// Checks if checkup done.
        /// </summary>
        private void checkIfCheckupDone()
        {
            var db = new CheckupDatabaseAccess();
            var data = db.GetCheckupFromAppointmentid(this.appointmentid);
            if (data.Count == 0)
            {
                this.finalDiagnosisTextBox.IsEnabled = false;
                this.submitFinalDiagnosisButton.IsEnabled = false;
                this.enterTestsButton.IsEnabled = false;
            }
        }

        /// <summary>
        /// Updates if all tests done.
        /// </summary>
        private void updateIfAllTestsDone()
        {
            if (this.checkIfAllTestsDone())
            {
                this.finalDiagnosisTextBox.IsEnabled = true;
                this.submitFinalDiagnosisButton.IsEnabled = true;
                this.enterTestsButton.IsEnabled = false;
            }
            else
            {
                this.finalDiagnosisTextBox.IsEnabled = false;
                this.submitFinalDiagnosisButton.IsEnabled = false;
                this.enterTestsButton.IsEnabled = true;
            }
        }

        /// <summary>
        /// Checks if all tests done.
        /// </summary>
        /// <returns></returns>
        private bool checkIfAllTestsDone()
        {
            foreach (Test test in this.testDataView.ItemsSource)
            {
                if (string.IsNullOrEmpty(test.TestResults))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Updates the tests.
        /// </summary>
        private void updateTests()
        {
            this.testDataView.ItemsSource = this.getTests();
        }
        private List<Test> getTests()
        {
            TestDatabaseAccess tbd = new TestDatabaseAccess();
            var testStringList = tbd.GetTests(this.appointmentid);
            var tests = new List<Test>();
            foreach (var test in testStringList)
            {
                var test1 = new Test(test[1]) {TestId = test[0]};
                var date = DateTime.Parse(test[3]);
                test1.DatePerformed = new Date("" + date.Year, "" + date.Month, "" + date.Day);
                test1.TestResults = test[4];
                test1.IsNormal = bool.Parse(test[5]);
                tests.Add(test1);
            }

            return tests;
        }

        /// <summary>
        /// Checks if appointment passed.
        /// </summary>
        /// <returns></returns>
        private bool checkIfAppointmentPassed()
        {
            return this.appointment.DateTime < DateTime.Now;
        }

        /// <summary>
        /// Updates the checkup information.
        /// </summary>
        private void updateCheckupInformation()
        {
            var checkup = ViewModel.ViewModel.GetCheckupFromAppointmentid(this.appointmentid);
            this.checkupDictionary = new Dictionary<string, string>();
            this.checkupDataView.ItemsSource = this.checkupDictionary;
            if (checkup == null)
            {
                this.enterTestsButton.IsEnabled = false;
                this.orderTestsButton.IsEnabled = false;
                this.finalDiagnosisTextBox.IsEnabled = false;
                this.submitFinalDiagnosisButton.IsEnabled = false;
                this.makeCheckupButton.IsEnabled = true;
                this.checkupTextBlock.Text = "Please Create a Checkup";
                this.checkupDictionary.Add("Systolic", "N/A");
                this.checkupDictionary.Add("Diastolic", "N/A");
                this.checkupDictionary.Add("Temperature", "N/A");
                this.checkupDictionary.Add("Weight", "N/A");
                this.checkupDictionary.Add("Pulse", "N/A");
                this.checkupDictionary.Add("Initial Diagnosis", "N/A");
            }
            else
            {
                this.enterTestsButton.IsEnabled = true;
                this.orderTestsButton.IsEnabled = true;
                this.finalDiagnosisTextBox.IsEnabled = true;
                this.submitFinalDiagnosisButton.IsEnabled = true;
                this.makeCheckupButton.IsEnabled = false;
                this.checkupTextBlock.Text = "Checkup Information";
                this.checkupDictionary.Add("Systolic", checkup.Systolic.ToString());
                this.checkupDictionary.Add("Diastolic", checkup.Diastolic.ToString());
                this.checkupDictionary.Add("Temperature", checkup.Temperature.ToString(CultureInfo.CurrentCulture));
                this.checkupDictionary.Add("Weight", checkup.Weight.ToString(CultureInfo.CurrentCulture));
                this.checkupDictionary.Add("Pulse", checkup.Pulse.ToString());
                this.checkupDictionary.Add("Initial Diagnosis", checkup.Diagnosis);
                this.checkupid = checkup.CheckupId;

            }
            
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        /// <summary>
        /// Ons the click create checkup.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void onClickCreateCheckup(object sender, RoutedEventArgs e)
        {
            this.makeCheckupButton.IsEnabled = false;
            if (this.appointmentid != 0)
            {
                var dialog = new RecordCheckupDialog(this.appointmentid);
                this.Hide();
                await dialog.ShowAsync();
                dialog.Hide();
                var t = this.ShowAsync();


                this.updateCheckupInformation();
            }
        }

        /// <summary>
        /// Ons the order tests click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void onOrderTestsClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OrderTestsDialog(this.appointment);
            this.Hide();
            await dialog.ShowAsync();
            dialog.Hide();
            var t = this.ShowAsync();
            this.updateTests();
        }

        /// <summary>
        /// Ons the enter tests click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="InvalidOperationException"></exception>
        private void onEnterTestsClick(object sender, RoutedEventArgs e)
        {
            
            this.enterTestFlyout.Hide();
            TestDatabaseAccess tdb = new TestDatabaseAccess();
            var testObject = this.testDataView.SelectedItem;
            var testid = testObject as Test;
            var flyoutCheckboxIsChecked = this.flyoutCheckbox.IsChecked;
            bool isAbnormal = flyoutCheckboxIsChecked != null && (bool) flyoutCheckboxIsChecked;
            var result = this.flyoutTextBox.Text;

            tdb.EditTestResults(result, !isAbnormal, int.Parse(testid?.TestId ?? throw new InvalidOperationException()));

            this.flyoutTextBox.Text = "";
            this.flyoutCheckbox.IsChecked = false;
            this.updateTests();
        }

        /// <summary>
        /// Ons the test selection change.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void onTestSelectionChange(object sender, SelectionChangedEventArgs e)
        {

            if (this.testDataView.SelectedItem != null)
            {
                this.enterTestsButton.IsEnabled = true;
            }

        }

        /// <summary>
        /// Checks if final diagnosis.
        /// </summary>
        /// <returns></returns>
        private bool checkIfFinalDiagnosis()
        {
            var db = new CheckupDatabaseAccess();
            var checkup = db.GetCheckupFromAppointmentid(this.appointmentid);
            if (checkup.Count == 8 && !string.IsNullOrEmpty(checkup[6]))
            {
                this.finalDiagnosisTextBox.Text = checkup[6];
                return true;
            }
            return false;
        }

        /// <summary>
        /// Ons the confirmation final diagnosis.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void onConfirmationFinalDiagnosis(object sender, RoutedEventArgs e)
        {
            this.enterTestFlyout.Hide();
            if (this.confirmationCheckBox.IsChecked == true)
            {
                //TODO do final diagnosis
                var db = new CheckupDatabaseAccess();
                db.EditCheckupFinalDiagnosis(this.finalDiagnosisTextBox.Text, this.checkupid);
                this.enterTestsButton.IsEnabled = false;
                this.orderTestsButton.IsEnabled = false;
                this.makeCheckupButton.IsEnabled = false;
                this.submitFinalDiagnosisButton.IsEnabled = false;
            }
        }

        private void disableButtonsIfFinalDiagnosisExists()
        {

        }
    }
}
