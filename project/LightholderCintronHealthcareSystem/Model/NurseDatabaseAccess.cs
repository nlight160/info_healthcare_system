using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace LightholderCintronHealthcareSystem.Model
{
    public class NurseDatabaseAccess
    {
        private const string ConStr = "server=160.10.25.16; port=3306; uid=cs3230f20j;" +
                              "pwd=F1UgUzIjwlhLAQ9a;database=cs3230f20j;";

        /// <summary>
        /// Authenticates the login.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public List<string> AuthenticateLogin(string username, string password)
        {
            var user = int.Parse(username);
            try
            {
                var query =
                    "SELECT p.fname, p.lname FROM person p, nurse n WHERE n.personid = p.personid AND n.nurseid = @username AND n.password = @password;";
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand {CommandText = query, Connection = conn};
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@username", user);
                cmd.Parameters.AddWithValue("@password", password);

                using var reader = cmd.ExecuteReader();
                var fnameOrdinal = reader.GetOrdinal("fname");
                var lnameOrdinal = reader.GetOrdinal("lname");
                if (!reader.HasRows)
                {
                    Console.WriteLine("No rows exist in table");
                    return null;
                }
                reader.Read();
                return new List<string>
                    {reader.GetString(fnameOrdinal), reader.GetString(lnameOrdinal)};
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in the AuthenticateLogin: " + ex.ToString());
                return null;
            }
        }

    }
}
