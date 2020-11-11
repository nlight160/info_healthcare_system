using LightholderCintronHealthcareSystem.Model;
using LightholderCintronHealthcareSystem.Model.DatabaseAccess;
using LightholderCintronHealthcareSystem.Model.People;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
        /// <returns></returns>
        public static bool checkForDoctorDoubleBook(DateTime requestedTime, int doctorid)
        {
            var adb = new AppointmentDatabaseAccess();
            var takenAppointments = adb.GetAppointmentTimeFromDoctorid(doctorid);
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
        /// <returns></returns>
        public static bool checkForPatientDoubleBook(DateTime requestedTime, int patientid)
        {
            var adb = new AppointmentDatabaseAccess();
            var currentAppointments = adb.GetAppointmentTimeFromPatientid(patientid);
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
        /// Gets the appointments from patient.
        /// </summary>
        /// <param name="patientid">The patientid.</param>
        /// <returns></returns>
        public static List<AppointmentDataGrid> getAppointmentsFromPatient(int patientid)
        {
            var adb = new AppointmentDatabaseAccess();
            var ddb = new DoctorDatabaseAccess();
            var appointmentList = adb.GetAppointmentFromPatientid(patientid);
            var appointments = new List<AppointmentDataGrid>();
            foreach (var appointment in appointmentList)
            {
                var appointmentid = int.Parse(appointment[0]);
                var dateTime = DateTime.Parse(appointment[2]);
                var doctorData = ddb.GetDoctorDataFromId(int.Parse(appointment[3]));
                var doctorName = doctorData[0] + " " + doctorData[1];
                var description = appointment[4];
                appointments.Add(new AppointmentDataGrid(appointmentid, dateTime, doctorName, description));

                //Was originally going to return a list of appointments but decided against it.

                //var patientData = pdb.GetPatientDataFromId(patientid);
                //var fullDate = patientData[2].Split(' ')[0].Split('/');
                //var date = new Date(fullDate[2], fullDate[0], fullDate[1]);
                //var address = new Address(patientData[3], patientData[4], patientData[5], patientData[6]);
                //var gender = patientData[8] == "Male" ? Gender.Male : Gender.Female;
                //var patient = new Patient(patientData[9], patientData[0], patientData[1], date, address, patientData[7],
                //    gender) { Patientid = patientData[10] };

                //var doctorData = ddb.GetDoctorDataFromId(Int32.Parse(appointment[3]));
                //var doctorFullDate = doctorData[2].Split(' ')[0].Split('/');
                //var doctorDate = new Date(fullDate[2], fullDate[0], fullDate[1]);
                //var doctorAddress = new Address(doctorData[3], doctorData[4], doctorData[5], doctorData[6]);
                //var doctorGender = doctorData[8] == "Male" ? Gender.Male : Gender.Female;
                //var doctor = new Doctor(doctorData[9], doctorData[0], doctorData[1], doctorDate, doctorAddress, doctorData[7],
                //    doctorGender, "General") { Doctorid = doctorData[10] }; //TODO add specialty

                //var appointmentDate = DateTime.Parse(appointment[2]);
                //var description = appointment[4];
                //var ap = new Appointment(appointmentid, patient, doctor, appointmentDate, description);
                //appointments.Add(ap);
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
        /// <returns></returns>
        public static bool createCheckup(int appointmentid, int systolic, int diastolic, decimal temperature, decimal weight, int pulse, string diagnosis)
        {
            var cdb = new CheckupDatabaseAccess();
            var checkup = new Checkup(null, appointmentid, systolic, diastolic, temperature, weight, pulse, diagnosis);
            return cdb.CreateCheckup(checkup);
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
