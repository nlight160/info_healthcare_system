﻿using LightholderCintronHealthcareSystem.Model;
using LightholderCintronHealthcareSystem.Model.DatabaseAccess;
using LightholderCintronHealthcareSystem.Model.People;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Microsoft.Toolkit.Uwp.UI.Controls;

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
            var hashAndSalt = dba.AuthenticateLogin(username);
            //Just to get hash and salt to put into database.
            //var t = new HashSalt();
            //t.makeHashSalt(password);
            //var h = t.Hash;
            //var s = t.Salt;
            if (hashAndSalt.Count == 2)
            {
                var hashSalt = new HashSalt();
                var hash = hashAndSalt[0];
                var salt = hashAndSalt[1];
                var verify = hashSalt.verifyPassword(password, hash, salt);
                if (verify)
                {
                    var information = dba.GetNursesName(username);
                    var loginCredentials = new Nurse(information[0], information[1]);
                    ActiveUser = new User(loginCredentials, int.Parse(username));
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Attempts the admin login.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static bool AttemptAdminLogin(string username, string password)
        {
            try
            {
                var dba = new AdminDatabaseAccess();
                var hashAndSalt = dba.AuthenticateAdminLogin(username);
                var t = new HashSalt();
                t.makeHashSalt(password);
                var h = t.Hash;
                var s = t.Salt;
                if (hashAndSalt.Count == 2)
                {
                    var hashSalt = new HashSalt();
                    var hash = hashAndSalt[0];
                    var salt = hashAndSalt[1];
                    var verify = hashSalt.verifyPassword(password, hash, salt);
                    if (verify)
                    {
                        var information = dba.GetAdminsName(username);
                        var loginCredentials = new Admin(information[0], information[1]);
                        ActiveUser = new User(loginCredentials, int.Parse(username))
                        {
                            IsAdmin = true
                        };
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }

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

        public static List<int> GetEveryDoctorId()
        {
            var ddb = new DoctorDatabaseAccess();
            return ddb.GetEveryDoctorId();
        }

        public static bool UpdatePatient(Patient p)
        {
            var dba = new PatientDatabaseAccess();
            return dba.UpdatePatient(p);
        }

        /// <summary>
        /// Searches for patients.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="option">The option.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">option - null</exception>
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
        /// Deletes the appointment.
        /// </summary>
        /// <param name="appointmentid">The appointmentid.</param>
        /// <returns></returns>
        public static bool deleteAppointment(int appointmentid)
        {
            var adb = new AppointmentDatabaseAccess();

            return adb.DeleteAppointment(appointmentid);
        }

        /// <summary>
        /// Gets the doctor specialty.
        /// </summary>
        /// <param name="doctorid">The doctorid.</param>
        /// <returns></returns>
        public static Dictionary<int, string> getDoctorSpecialty(int doctorid)
        {
            var specialties = new Dictionary<int, string>();
            var sdb = new SpecialtyDatabaseAccess();
            var ids = sdb.GetDoctorSpecialtiesId(doctorid);

            foreach (var id in ids)
            {
                var name = sdb.GetSpecialtyName(id);
                specialties.Add(id, name);
            }

            return specialties;
        }

        /// <summary>
        /// Checks for doctor double book.
        /// </summary>
        /// <param name="requestedTime">The requested time.</param>
        /// <param name="doctorid">The doctorid.</param>
        /// <param name="appointmentid"></param>
        /// <returns></returns>
        public static bool checkForDoctorDoubleBook(DateTime requestedTime, int doctorid, int appointmentid = -1)
        {
            var adb = new AppointmentDatabaseAccess();
            var takenAppointments = adb.GetAppointmentTimeFromDoctorid(doctorid, appointmentid);
            const int appointmentDuration = 30; //In minutes
            foreach (var takenAppointment in takenAppointments)
            {
                var takenDate = DateTime.Parse(takenAppointment);
                var unavailable = requestedTime - takenDate;
                if (Math.Abs(unavailable.TotalSeconds) <= appointmentDuration * 60) //&& unavailable.TotalSeconds > 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks for patient double book.
        /// </summary>
        /// <param name="requestedTime">The requested time.</param>
        /// <param name="patientid">The patientid.</param>
        /// <param name="appointmentid"></param>
        /// <returns></returns>
        public static bool checkForPatientDoubleBook(DateTime requestedTime, int patientid, int appointmentid = -1)
        {
            var adb = new AppointmentDatabaseAccess();
            var currentAppointments = adb.GetAppointmentTimeFromPatientid(patientid, appointmentid);
            const int appointmentDuration = 30; //In minutes
            foreach (var currentAppointment in currentAppointments)
            {
                var takenDate = DateTime.Parse(currentAppointment);
                var unavailable = requestedTime - takenDate;
                if (Math.Abs(unavailable.TotalSeconds) <= appointmentDuration * 60) //&& unavailable.TotalSeconds > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Creates the appointment.
        /// </summary>
        /// <param name="patientid">The patientid.</param>
        /// <param name="date">The date.</param>
        /// <param name="doctorid">The doctorid.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public static bool createAppointment(string patientid, DateTime date, string doctorid, string description)
        {
            var adb = new AppointmentDatabaseAccess();
            return adb.CreateAppointment(patientid, date, doctorid, description);
        }

        /// <summary>
        /// Gets the name of the doctor.
        /// </summary>
        /// <param name="doctorid">The doctorid.</param>
        /// <returns></returns>
        public static List<string> getDoctorName(int doctorid)
        {
            var ddb = new DoctorDatabaseAccess();
            return ddb.GetDoctorNameFromId(doctorid);
        }

        /// <summary>
        /// Checks if appointment time passed.
        /// </summary>
        /// <param name="appointmentid">The appointmentid.</param>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static bool checkIfAppointmentTimePassed(int appointmentid, DateTime dateTime)
        {
            var adb = new AppointmentDatabaseAccess();
            var appointmentTime = adb.GetAppointmentTimeFromAppointmentid(appointmentid);
            return dateTime > appointmentTime;
        }

        /// <summary>
        /// Updates the appointment.
        /// </summary>
        /// <param name="appointmentid">The appointmentid.</param>
        /// <param name="date">The date.</param>
        /// <param name="doctorid">The doctorid.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public static bool EditAppointment(int appointmentid, DateTime date, int doctorid, string description)
        {
            var adb = new AppointmentDatabaseAccess();
            return adb.EditAppointment(appointmentid, date, doctorid, description);
        }

        /// <summary>
        /// Gets the doctorid from appointmentid.
        /// </summary>
        /// <param name="appointmentid">The appointmentid.</param>
        /// <returns></returns>
        public static string GetDoctoridFromAppointmentid(int appointmentid)
        {
            var adb = new AppointmentDatabaseAccess();
            return adb.GetDoctoridFromAppointmentid(appointmentid);
        }

        public static List<AppointmentDataGrid> getAllAppointments()
        {
            var adb = new AppointmentDatabaseAccess();
            var ddb = new DoctorDatabaseAccess();
            var pdb = new PatientDatabaseAccess();
            var appointmentList = adb.GetAllAppointments();
            var appointments = new List<AppointmentDataGrid>();
            foreach (var appointment in appointmentList)
            {
                //var appointmentid = int.Parse(appointment[0]);
                //var dateTime = DateTime.Parse(appointment[2]);
                //var doctorData = ddb.GetDoctorDataFromId(int.Parse(appointment[3]));
                //var doctorName = doctorData[0] + " " + doctorData[1];
                //var description = appointment[4];

                //var patientData = pdb.GetPatientDataFromId(int.Parse(appointment[1]));
                //var patientName = patientData[0] + " " + patientData[1];
                //appointments.Add(new AppointmentDataGrid(appointmentid, dateTime, doctorName, description, int.Parse(appointment[1]), patientName));


                var appointmentid = int.Parse(appointment[0]);
                var patientid = int.Parse(appointment[1]);
                var dateTime = DateTime.Parse(appointment[2]);
                var doctorData = ddb.GetDoctorDataFromId(int.Parse(appointment[3]));
                var doctorName = doctorData[0] + " " + doctorData[1];
                var description = appointment[4];
                var patientData = pdb.GetPatientDataFromId(patientid);
                var patientName = patientData[0] + " " + patientData[1];
                var dob = new Date(DateTime.Parse(patientData[2])).ToString();
                appointments.Add(new AppointmentDataGrid(appointmentid, dateTime, doctorName, description, patientid, patientName, dob, int.Parse(appointment[3])));

            }

            return appointments;
        }

        /// <summary>
        /// Gets the checkup from appointmentid.
        /// </summary>
        /// <param name="appointmentid">The appointmentid.</param>
        /// <returns></returns>
        public static Checkup GetCheckupFromAppointmentid(int appointmentid)
        {
            var cdb = new CheckupDatabaseAccess();
            var information = cdb.GetCheckupFromAppointmentid(appointmentid);
            if (information.Count != 8)
            {
                return null;
            }
            var systolic = int.Parse(information[0]);
            var diastolic = int.Parse(information[1]);
            var temperature = decimal.Parse(information[2]);
            var weight = decimal.Parse(information[3]);
            var pulse = int.Parse(information[4]);
            var diagnosis = information[5];
            var finaldiagnosis = information[6];
            var checkupid = int.Parse(information[7]);
            return new Checkup(
                checkupid, appointmentid, systolic, diastolic, temperature, weight, pulse, diagnosis, finaldiagnosis);
        }

        /// <summary>
        /// Gets the appointments from patient.
        /// </summary>
        /// <param name="patientid">The patientid.</param>
        /// <returns></returns>
        public static List<AppointmentDataGrid> getAppointmentsFromPatient(int patientid)
        {
            var adb = new AppointmentDatabaseAccess();
            var ddb = new DoctorDatabaseAccess();
            var pdb = new PatientDatabaseAccess();
            var appointmentList = adb.GetAppointmentFromPatientid(patientid);
            var appointments = new List<AppointmentDataGrid>();
            foreach (var appointment in appointmentList)
            {
                var appointmentid = int.Parse(appointment[0]);
                var dateTime = DateTime.Parse(appointment[2]);
                var doctorData = ddb.GetDoctorDataFromId(int.Parse(appointment[3]));
                var doctorName = doctorData[0] + " " + doctorData[1];
                var description = appointment[4];
                var patientData = pdb.GetPatientDataFromId(patientid);
                var patientName = patientData[0] + " " + patientData[1];
                var dob = new Date(DateTime.Parse(patientData[2])).ToString();
                appointments.Add(new AppointmentDataGrid(appointmentid, dateTime, doctorName, description, patientid, patientName, dob, int.Parse(appointment[3])));

            }

            return appointments;
        }

        /// <summary>
        /// Gets the patient name from appointmentid.
        /// </summary>
        /// <param name="appointmentid">The appointmentid.</param>
        /// <returns></returns>
        public static string getPatientNameFromAppointmentid(int appointmentid)
        {
            var adb = new AppointmentDatabaseAccess();
            return adb.GetPatientNameFromAppointmentid(appointmentid);
        }

        /// <summary>
        /// Gets the doctor name from appointmentid.
        /// </summary>
        /// <param name="appointmentid">The appointmentid.</param>
        /// <returns></returns>
        public static string getDoctorNameFromAppointmentid(int appointmentid)
        {
            var adb = new AppointmentDatabaseAccess();
            return adb.GetDoctorNameFromAppointmentid(appointmentid);
        }

        /// <summary>
        /// Creates the checkup.
        /// </summary>
        /// <param name="appointmentid">The appointmentid.</param>
        /// <param name="systolic">The systolic.</param>
        /// <param name="diastolic">The diastolic.</param>
        /// <param name="temperature">The temperature.</param>
        /// <param name="weight">The weight.</param>
        /// <param name="pulse">The pulse.</param>
        /// <param name="diagnosis">The diagnosis.</param>
        /// <param name="finaldiagnosis"></param>
        /// <returns></returns>
        public static bool createCheckup(int appointmentid, int systolic, int diastolic, decimal temperature, decimal weight, int pulse, string diagnosis, string finaldiagnosis)
        {
            var cdb = new CheckupDatabaseAccess();
            var checkup = new Checkup(0, appointmentid, systolic, diastolic, temperature, weight, pulse, diagnosis, finaldiagnosis);
            return cdb.CreateCheckup(checkup);
        }

        /// <summary>
        /// Log outs this instance.
        /// </summary>
        public static void Logout()
        {
            ActiveUser = null;
        }

        public static void FillDataGrid(DataTable table, DataGrid grid)
        {
            grid.Columns.Clear();
            grid.AutoGenerateColumns = false;
            for (int i = 0; i < table.Columns.Count; i++)
            {
                grid.Columns.Add(new DataGridTextColumn()
                {
                    Header = table.Columns[i].ColumnName,
                    Binding = new Binding { Path = new PropertyPath("[" + i.ToString() + "]") }
                });
            }

            var collection = new ObservableCollection<object>();
            foreach (DataRow row in table.Rows)
            {
                collection.Add(row.ItemArray);
            }

            grid.ItemsSource = collection;
        }
    }
}
