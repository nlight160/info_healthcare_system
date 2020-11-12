using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace LightholderCintronHealthcareSystem.Model.DatabaseAccess
{
    /// <summary>
    /// Nurse database access
    /// </summary>
    public class NurseDatabaseAccess
    {
        /// <summary>
        /// The con string
        /// </summary>
        private const string ConStr = "server=160.10.25.16; port=3306; uid=cs3230f20j;" +
                              "pwd=F1UgUzIjwlhLAQ9a;database=cs3230f20j;";

        /// <summary>
        /// Authenticates the login.
        /// </summary>
        /// <param name="nurseid"></param>
        /// <returns></returns>
        public List<string> AuthenticateLogin(string nurseid)
        {
            var information = new List<string>();
            try
            {
                var user = int.Parse(nurseid);
                const string query = "SELECT n.password, n.salt FROM nurse n WHERE n.nurseid = @nurseid;";
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand {CommandText = query, Connection = conn};
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@nurseid", user);

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
        /// Gets the name of the nurses.
        /// </summary>
        /// <param name="nurseid"></param>
        /// <returns></returns>
        public List<string> GetNursesName(string nurseid)
        {
            var information = new List<string>();
            try
            {
                var user = int.Parse(nurseid);
                const string query = "SELECT p.fname, p.lname FROM person p, nurse n WHERE n.personid = p.personid AND n.nurseid = @nurseid;";
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand { CommandText = query, Connection = conn };
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@nurseid", user);

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
                Console.WriteLine("Exception in the AuthenticateLogin: " + ex);
                return information;
            }
        }

    }
}
