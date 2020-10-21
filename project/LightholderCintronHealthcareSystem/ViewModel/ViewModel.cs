using System.Collections.Generic;
using System.Globalization;
using LightholderCintronHealthcareSystem.Model;

namespace LightholderCintronHealthcareSystem.ViewModel
{
    /// <summary>
    /// View Model
    /// </summary>
    public class ViewModel
    {
        /// <summary>
        /// Gets the active user.
        /// </summary>
        /// <value>
        /// The active user.
        /// </value>
        public static User ActiveUser { get; private set; }

        /// <summary>
        /// Attempts the login.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static bool AttemptLogin(string username, string password)
        {
            var dba = new NurseDatabaseAccess();
            var information =
                dba.AuthenticateLogin(username, password);
            
            if (information != null)
            {
                var loginCredentials = new Nurse(information[0], information[1]);
                ActiveUser = new User(loginCredentials, int.Parse(username));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Registers the patient.
        /// </summary>
        /// <param name="lname">The lname.</param>
        /// <param name="fname">The fname.</param>
        /// <param name="dob">The dob.</param>
        /// <param name="street">The street.</param>
        /// <param name="city">The city.</param>
        /// <param name="state">The state.</param>
        /// <param name="zip">The zip.</param>
        /// <param name="phone">The phone.</param>
        public static void RegisterPatient(string lname, string fname, Date dob, string street, string city, string state, string zip,
                                            string phone, Gender gender)
        {
            var dba = new PatientDatabaseAccess();
            Address a = new Address(street, city, state, zip);
            //$"{dob:yyyy MM dd}";
            Patient p = new Patient(fname, lname, dob, a, phone, gender);
            dba.CreatePatient(p);
        }

        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        /// <returns></returns>
        public static Date GetDate(string year, string month, string day)
        {
            return new Date(year, month, day);
        }

        public static List<string> searchForPatients(string search, bool byName)
        {
            var patientList = new List<string>();
            if (byName)
            {
                var patientids = new PatientDatabaseAccess().SearchPatientsWithName(search);
            }
            else
            {

            }

            return patientList;
        }

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        public static void Logout()
        {
            ActiveUser = null;
        }
    }
}
