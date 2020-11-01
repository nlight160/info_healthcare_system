using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace LightholderCintronHealthcareSystem.Model.DatabaseAccess
{
    /// <summary>
    /// This is the DAL of this program for now. We plan on upgrading it later once our planning is done.
    /// </summary>
    internal class DatabaseAccess
    {
        /// <summary>
        /// The con string
        /// </summary>
        private const string ConStr = "server=160.10.25.16; port=3306; uid=cs3230f20j;" +
                              "pwd=F1UgUzIjwlhLAQ9a;database=cs3230f20j;";

        /// <summary>
        /// Logins the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public List<string> LoginQuery(string query)
        {
            try
            {
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand(query, conn);
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
                Console.WriteLine("Exception in the loginQuery: " + ex);
                return null;
            }
        }

        /// <summary>
        /// Creates the patient.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <exception cref="Exception"></exception>
        public void CreatePatient(string query)
        {
            try
            {
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand(query, conn);
                var check = cmd.ExecuteNonQuery();
                if (check < 0)
                {
                    throw new Exception(); //ExecuteNonQuery returns number of rows changed/added/deleted so this is a quick check for it.
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in the loginQuery: " + ex);
            }
        }
    }
}
