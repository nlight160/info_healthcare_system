using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace LightholderCintronHealthcareSystem.Model.DatabaseAccess
{
    class CheckupDatabaseAccess
    {

        private const string ConStr = "server=160.10.25.16; port=3306; uid=cs3230f20j;" +
                                      "pwd=F1UgUzIjwlhLAQ9a;database=cs3230f20j;";

        public bool CreateCheckup(Checkup c)
        {

            var appointmentid = c.Appointment.Appointmentid;
            var systolic = c.Systolic;
            var diastolic = c.Diastolic;
            var temperature = c.Temperature;
            var weight = c.Weight;
            var pulse = c.Pulse;
            var diagnosis = c.Diagnosis;

            try
            {
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand { Connection = conn };

                const string createCheckup = "INSERT INTO `checkup` (`appointmentid`, `systolic`, `diastolic`, `temperature`, `weight`, `pulse`, `diagnosis`) VALUES (@appointmentid, @systolic, @diastolic, @temperature, @weight, @pulse, @diagnosis);";
                cmd.CommandText = createCheckup;
                cmd.Parameters.AddWithValue("@appointmentid", appointmentid);
                cmd.Parameters.AddWithValue("@systolic", systolic);
                cmd.Parameters.AddWithValue("@diastolic", diastolic);
                cmd.Parameters.AddWithValue("@temperature", temperature);
                cmd.Parameters.AddWithValue("@weight", weight);
                cmd.Parameters.AddWithValue("@pulse", pulse);
                cmd.Parameters.AddWithValue("@diagnosis", diagnosis);

                var confirmation = cmd.ExecuteNonQuery();
                return confirmation == 1;


            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in the CreateCheckup: " + ex);
                return false;
            }


        }
    }
}
