using LightholderCintronHealthcareSystem.Model.People;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightholderCintronHealthcareSystem.Model.DatabaseAccess
{
    class TestDatabaseAccess
    {
        private const string ConStr = "server=160.10.25.16; port=3306; uid=cs3230f20j;" +
                                      "pwd=F1UgUzIjwlhLAQ9a;database=cs3230f20j;";

        public bool AddTests(Test test)
        {
            try
            {
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand { Connection = conn };

                const string makeAppointment =
                    "INSERT INTO `test` (`testname`, `appointmentid`, `datetime`, `results`, `normailty`) VALUES (@testname, @appointmentid, @datetime, @results, @normality);";
                cmd.CommandText = makeAppointment;
                cmd.Parameters.AddWithValue("@testname", test.TestName);
                cmd.Parameters.AddWithValue("@appointmentid", test.AppointmentId);
                cmd.Parameters.AddWithValue("@datetime", test.DatePerformed);
                cmd.Parameters.AddWithValue("@results", test.TestResults);
                cmd.Parameters.AddWithValue("@normality", test.IsNormal);

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
