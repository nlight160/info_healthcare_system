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
                using var cmd = new MySqlCommand {Connection = conn};

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

        public List<string> AuthenticateAdminLogin(string adminid)
        {
            var information = new List<string>();
            try
            {
                var user = int.Parse(adminid);
                const string query = "SELECT n.password, n.salt FROM admin n WHERE n.adminid = @adminid;";
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand {CommandText = query, Connection = conn};
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@adminid", user);

                using var reader = cmd.ExecuteReader();
                var passwordOrdinal = reader.GetOrdinal("password");
                var saltOrdinal = reader.GetOrdinal("salt");
                if (!reader.HasRows)
                {
                    Console.WriteLine("No rows exist in table");
                    return information;
                }

                reader.Read();
                information.Add(reader.GetString(passwordOrdinal));
                information.Add(reader.GetString(saltOrdinal));
                return information;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in the AuthenticateLogin: " + ex);
                return information;
            }
        }
        /// <summary>
        /// Gets the name of the admins.
        /// </summary>
        /// <param name="adminid">The adminid.</param>
        /// <returns></returns>
        public List<string> GetAdminsName(string adminid)
        {
            var information = new List<string>();
            try
            {
                var user = int.Parse(adminid);
                const string query = "SELECT p.fname, p.lname FROM person p, admin a WHERE a.personid = p.personid AND a.adminid = @adminid;";
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand { CommandText = query, Connection = conn };
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@adminid", user);

                using var reader = cmd.ExecuteReader();
                var fnameOrdinal = reader.GetOrdinal("fname");
                var lnameOrdinal = reader.GetOrdinal("lname");
                if (!reader.HasRows)
                {
                    Console.WriteLine("No rows exist in table");
                    return null;
                }
                reader.Read();
                information.Add(reader.GetString(fnameOrdinal));
                information.Add(reader.GetString(lnameOrdinal));
                return information;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in the AuthenticateLogin for admins: " + ex);
                return information;
            }
        }
    }
}
