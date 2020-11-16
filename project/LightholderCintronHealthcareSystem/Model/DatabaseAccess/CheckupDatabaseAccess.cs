using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace LightholderCintronHealthcareSystem.Model.DatabaseAccess
{
    /// <summary>
    /// Checkup database access
    /// </summary>
    public class CheckupDatabaseAccess
    {

        private const string ConStr = "server=160.10.25.16; port=3306; uid=cs3230f20j;" +
                                      "pwd=F1UgUzIjwlhLAQ9a;database=cs3230f20j;";

        /// <summary>
        /// Creates the checkup.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns></returns>
        public bool CreateCheckup(Checkup c)
        {

            var appointmentid = c.AppointmentId;
            var systolic = c.Systolic;
            var diastolic = c.Diastolic;
            var temperature = c.Temperature;
            var weight = c.Weight;
            var pulse = c.Pulse;
            var diagnosis = c.Diagnosis;
            var finaldiagnosis = c.FinalDiagnosis;

            try
            {
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand { Connection = conn };

                const string createCheckup = "INSERT INTO `checkup` (`appointmentid`, `systolic`, `diastolic`, `temp`, `weight`, `pulse`, `diagnosis`, `finaldiagnosis`) VALUES (@appointmentid, @systolic, @diastolic, @temperature, @weight, @pulse, @diagnosis, @finaldiagnosis);";
                cmd.CommandText = createCheckup;
                cmd.Parameters.AddWithValue("@appointmentid", appointmentid);
                cmd.Parameters.AddWithValue("@systolic", systolic);
                cmd.Parameters.AddWithValue("@diastolic", diastolic);
                cmd.Parameters.AddWithValue("@temperature", temperature);
                cmd.Parameters.AddWithValue("@weight", weight);
                cmd.Parameters.AddWithValue("@pulse", pulse);
                cmd.Parameters.AddWithValue("@diagnosis", diagnosis);
                cmd.Parameters.AddWithValue("@finaldiagnosis", finaldiagnosis);

                var confirmation = cmd.ExecuteNonQuery();
                return confirmation == 1;


            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in the CreateCheckup: " + ex);
                return false;
            }


        }

        /// <summary>
        /// Gets the checkup from appointmentid.
        /// </summary>
        /// <param name="appointmentid">The appointmentid.</param>
        /// <returns></returns>
        public List<string> GetCheckupFromAppointmentid(int appointmentid)
        {

            
            var information = new List<string>();
            try
            {
                const string query = "SELECT c.systolic, c.diastolic, c.temp, c.weight, c.pulse, c.diagnosis, c.finaldiagnosis, c.checkupid FROM checkup c WHERE c.appointmentid = @appointmentid;";
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand { CommandText = query, Connection = conn };
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@appointmentid", appointmentid);
                using var reader = cmd.ExecuteReader();
                var systolicOrdinal = reader.GetOrdinal("systolic");
                var diastolicOrdinal = reader.GetOrdinal("diastolic");
                var tempOrdinal = reader.GetOrdinal("temp");
                var weightOrdinal = reader.GetOrdinal("weight");
                var pulseOrdinal = reader.GetOrdinal("pulse");
                var diagnosisOrdinal = reader.GetOrdinal("diagnosis");
                var finaldiagnosisOrdinal = reader.GetOrdinal("finaldiagnosis");
                var checkupidOrdinal = reader.GetOrdinal("checkupid");

                if (reader.HasRows)
                {
                    reader.Read();
                    information.Add(reader.GetString(systolicOrdinal));     //0
                    information.Add(reader.GetString(diastolicOrdinal));    //1
                    information.Add(reader.GetString(tempOrdinal));         //2
                    information.Add(reader.GetString(weightOrdinal));       //3
                    information.Add(reader.GetString(pulseOrdinal));        //4
                    information.Add(reader.GetString(diagnosisOrdinal));    //5
                    information.Add(reader.GetString(finaldiagnosisOrdinal)); //6
                    information.Add(reader.GetString(checkupidOrdinal)); //7

                }
                else
                {
                    Console.WriteLine("No rows exist in table");
                }
                return information;


            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in the CreateCheckup: " + ex);
                return information;
            }


        }

        public bool EditCheckupFinalDiagnosis(string finalDiagnosis, int checkupid)
        {
            try
            {
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand { Connection = conn };

                const string updateCheckup = "UPDATE `checkup` SET `finaldiagnosis` = @finaldiagnosis WHERE `checkupid` = @checkupid;";
                cmd.CommandText = updateCheckup;
                cmd.Parameters.AddWithValue("@finaldiagnosis", finalDiagnosis);
                cmd.Parameters.AddWithValue("@checkupid", checkupid);

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
