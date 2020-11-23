using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightholderCintronHealthcareSystem.Model.DatabaseAccess
{
    public class AdminDatabaseAccess
    {
        private const string ConStr = "server=160.10.25.16; port=3306; uid=cs3230f20j;" +
                                      "pwd=F1UgUzIjwlhLAQ9a;database=cs3230f20j;";

        public DataTable MakeAdminQuery(string query)
        {

            try
            {
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand { Connection = conn };

                cmd.CommandText = query;

                var reader = cmd.ExecuteReader();
                var data = new DataTable();
                data.Load(reader);

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in the admin query: " + ex);
                return null;
            }


        }
    }
}
