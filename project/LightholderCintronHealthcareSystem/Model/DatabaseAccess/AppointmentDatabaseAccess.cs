using System;
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

            var originalAppointmentid = originalAppointment.Appointmentid; //TODO make appointmentid

            var patientid = newAppointment.Patient;
            var date = newAppointment.AppointmentDateTime;
            var doctorid = newAppointment.Doctor;
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

    }
}
