using LightholderCintronHealthcareSystem.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LightholderCintronHealthcareSystem.Model.DatabaseAccess;
using LightholderCintronHealthcareSystem.Model.People;

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
        /// <param name="gender">The gender.</param>
        public static bool RegisterPatient(string lname, string fname, Date dob, string street, string city, string state, string zip,
                                            string phone, Gender gender)
        {
            var dba = new PatientDatabaseAccess();
            var a = new Address(street, city, state, zip);
            //$"{dob:yyyy MM dd}";
            var p = new Patient(null, fname, lname, dob, a, phone, gender);
            return dba.CreatePatient(p);
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

        public static bool UpdatePatient(Patient p)
        {
            var dba = new PatientDatabaseAccess();
            return dba.UpdatePatient(p);
        }

        public static List<Patient> SearchForPatients(List<string> search, SearchOption option)
        {
            var patientList = new List<Patient>();
            List<int> patientids;

            switch (option)
            {
                case SearchOption.Name:
                    patientids = new PatientDatabaseAccess().SearchPatientsWithName(search[0]);
                    break;
                case SearchOption.Date:
                    patientids = new PatientDatabaseAccess().SearchPatientsWithDate(search[0]);
                    break;
                case SearchOption.Both:
                    var byName = new PatientDatabaseAccess().SearchPatientsWithName(search[0]);
                    var byDate = new PatientDatabaseAccess().SearchPatientsWithDate(search[1]);
                    patientids = byName.Intersect(byDate).ToList();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(option), option, null);
            }

            foreach (var patient in patientids)
            {
                var patientData = new PatientDatabaseAccess().GetPatientDataFromId(patient);
                var fullDate = patientData[2].Split(' ')[0].Split('/');
                Debug.WriteLine(fullDate);
                var date = new Date(fullDate[2], fullDate[0], fullDate[1]);
                var address = new Address(patientData[3], patientData[4], patientData[5], patientData[6]);
                var gender = patientData[8] == "Male" ? Gender.Male : Gender.Female;
                var pt = new Patient(patientData[9], patientData[0], patientData[1], date, address, patientData[7],
                    gender) {Patientid = patientData[10]};
                patientList.Add(pt);
            }

            return patientList;
        }

        /// <summary>
        /// Log outs this instance.
        /// </summary>
        public static void Logout()
        {
            ActiveUser = null;
        }
    }
}
