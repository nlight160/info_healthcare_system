using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace LightholderCintronHealthcareSystem.Model.DatabaseAccess
{
    /// <summary>
    /// Appointment Database access.
    /// </summary>
    public class AppointmentDatabaseAccess
    {
        private const string ConStr = "server=160.10.25.16; port=3306; uid=cs3230f20j;" +
                                      "pwd=F1UgUzIjwlhLAQ9a;database=cs3230f20j;";

        /// <summary>
        /// Creates the appointment.
        /// </summary>
        /// <param name="a">a.</param>
        /// <returns></returns>
        public bool CreateAppointment(Appointment a)
        {

            var patientid = a.Patient.Patientid;
            var date = a.AppointmentDateTime;
            var doctorid = a.Doctor.Doctorid;
            var description = a.Description;

            try
            {
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand { Connection = conn };

                const string makeAppointment = "INSERT INTO `appointment` (`patientid`, `date`, `doctorid`, `description`) VALUES (@patientid, @date, @doctorid, @description);";
                cmd.CommandText = makeAppointment;
                cmd.Parameters.AddWithValue("@patientid", patientid);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@doctorid", doctorid);
                cmd.Parameters.AddWithValue("@description", description);

                var confirmation = cmd.ExecuteNonQuery();
                return confirmation == 1;


            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in the CreatePatient: " + ex);
                return false;
            }


        }

        /// <summary>
        /// Updates the appointment.
        /// </summary>
        /// <param name="originalAppointment">The original appointment.</param>
        /// <param name="newAppointment">The new appointment.</param>
        /// <returns></returns>
        public bool UpdateAppointment(Appointment originalAppointment, Appointment newAppointment)
        {

            var originalAppointmentid = originalAppointment.Appointmentid;

            var patientid = newAppointment.Patient.Patientid;
            var date = newAppointment.AppointmentDateTime;
            var doctorid = newAppointment.Doctor.Doctorid;
            var description = newAppointment.Description;

            try
            {
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand { Connection = conn };

                const string updateAppointment = "UPDATE `appointment` SET `patientid` = @patientid, `date` = @date, `doctorid` = @doctorid, `description` = @description WHERE `appointmentid` = @originalAppointmentid;";
                cmd.CommandText = updateAppointment;
                cmd.Parameters.AddWithValue("@patientid", patientid);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@doctorid", doctorid);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@originalAppointmentid", originalAppointmentid);

                var confirmation = cmd.ExecuteNonQuery();
                return confirmation == 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in the CreatePatient: " + ex);
                return false;
            }
        }


        /// <summary>
        /// Deletes the appointment.
        /// </summary>
        /// <param name="appointmentid">The appointmentid.</param>
        /// <returns></returns>
        public bool DeleteAppointment(int appointmentid)
        {
            try
            {
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand { Connection = conn };

                const string deleteAppointment = "DELETE FROM `appointment` WHERE `appointmentid` = @appointmentid;";
                cmd.CommandText = deleteAppointment;
                cmd.Parameters.AddWithValue("@appointmentid", appointmentid);

                var confirmation = cmd.ExecuteNonQuery();
                return confirmation == 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in the DeleteAppointment: " + ex);
                return false;
            }
        }

        /// <summary>
        /// Gets the appointment.
        /// </summary>
        /// <param name="patientid">The patient identifier.</param>
        /// <returns></returns>
        public List<List<string>> GetAppointmentFromPatientid(int patientid)
        {

            var appointmentList = new List<List<string>>();
            try
            {
                const string query = "SELECT a.appointmentid, a.date, a.doctorid, a.description FROM appointment a WHERE a.patientid = @patientid;";
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand { CommandText = query, Connection = conn };
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@patientid", patientid);
                using var reader = cmd.ExecuteReader();
                var appointmentidOrdinal = reader.GetOrdinal("appointmentid");
                var dateOrdinal = reader.GetOrdinal("date");
                var doctoridOrdinal = reader.GetOrdinal("doctorid");
                var descriptionOrdinal = reader.GetOrdinal("description");


                while (reader.Read())
                {
                    var singleAppointment = new List<string>();
                    singleAppointment.Add(reader.GetString(appointmentidOrdinal));  //0
                    singleAppointment.Add(patientid.ToString());                    //1
                    singleAppointment.Add(reader.GetString(dateOrdinal));           //2
                    singleAppointment.Add(reader.GetString(doctoridOrdinal));       //3
                    singleAppointment.Add(reader.GetString(descriptionOrdinal));    //4
                    appointmentList.Add(singleAppointment);
                }
                return appointmentList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in the GetAppointment: " + ex);
                return appointmentList;
            }
        }


        /// <summary>
        /// Gets the appointment time from appointmnetid.
        /// </summary>
        /// <param name="appointmentid">The appointmentid.</param>
        /// <returns></returns>
        public DateTime GetAppointmentTimeFromAppointmentid(int appointmentid)
        {

            var appointmentTime = new DateTime();
            try
            {
                const string query = "SELECT a.date FROM appointment a WHERE a.appointmentid = @appointmentid;";
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand { CommandText = query, Connection = conn };
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@appointmentid", appointmentid);
                using var reader = cmd.ExecuteReader();
                var dateOrdinal = reader.GetOrdinal("date");

                if (reader.HasRows)
                {
                    reader.Read();
                    appointmentTime = reader.GetDateTime(dateOrdinal);

                }
                else
                {
                    Console.WriteLine("No rows exist in table");
                }
                return appointmentTime;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in the GetAppointment: " + ex);
                return appointmentTime;
            }
        }

        /// <summary>
        /// Gets the appointment time from doctorid.
        /// </summary>
        /// <param name="doctorid">The doctorid.</param>
        /// <returns></returns>
        public List<string> GetAppointmentTimeFromDoctorid(int doctorid)
        {

            var appointmentTimes = new List<string>();
            try
            {
                const string query = "SELECT a.date FROM appointment a WHERE a.doctorid = @doctorid;";
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand { CommandText = query, Connection = conn };
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@doctorid", doctorid);
                using var reader = cmd.ExecuteReader();
                var dateOrdinal = reader.GetOrdinal("date");

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        appointmentTimes.Add(reader.GetString(dateOrdinal));
                    }

                }
                else
                {
                    Console.WriteLine("No rows exist in table");
                }
                return appointmentTimes;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in the GetAppointment: " + ex);
                return appointmentTimes;
            }
        }


    }
}
