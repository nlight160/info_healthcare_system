using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace LightholderCintronHealthcareSystem.Model
{
    class DatabaseAccess
    {
        const string conStr = "server=160.10.25.16; port=3306; uid=cs3230f20j;" +
                              "pwd=F1UgUzIjwlhLAQ9a;database=cs3230f20j;";

        public List<string> loginQuery(string query)
        {
            try
            {
                using (var conn = new MySqlConnection(conStr))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
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
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in the loginQuery: " + ex.ToString());
                return null;
            }
        }


    }
}
