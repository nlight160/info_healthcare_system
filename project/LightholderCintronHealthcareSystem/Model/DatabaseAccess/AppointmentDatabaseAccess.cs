using System;
using System.Collections.Generic;
using LightholderCintronHealthcareSystem.Model.People;
using MySql.Data.MySqlClient;

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

            var patientid = a.Patient.Personid;
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
        /// <param name="patient">The patient.</param>
        /// <returns></returns>
        public bool DeleteAppointment(Patient patient)
        {
            var patientid = patient.Patientid; 
            try
            {
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand { Connection = conn };

                const string deleteAppointment = "DELETE FROM `appointment` WHERE `patientid` = @patientid;";
                cmd.CommandText = deleteAppointment;
                cmd.Parameters.AddWithValue("@patientid", patientid);

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
        public List<string> GetAppointmentFromPatientid(int patientid)
        {

            var specialtyName = new List<string>();
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

                if (reader.HasRows)
                {
                    reader.Read();
                    specialtyName.Add(reader.GetString(appointmentidOrdinal));  //0
                    specialtyName.Add(patientid.ToString());                    //1
                    specialtyName.Add(reader.GetString(dateOrdinal));           //2
                    specialtyName.Add(reader.GetString(doctoridOrdinal));       //3
                    specialtyName.Add(reader.GetString(descriptionOrdinal));    //4

                }
                else
                {
                    Console.WriteLine("No rows exist in table");
                }
                return specialtyName;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in the GetAppointment: " + ex);
                return specialtyName;
            }


        }

    }
}
