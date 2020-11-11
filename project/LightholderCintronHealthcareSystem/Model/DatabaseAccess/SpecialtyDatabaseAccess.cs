using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace LightholderCintronHealthcareSystem.Model.DatabaseAccess
{
    /// <summary>
    /// Specialty and DoctorSpecialty database access
    /// </summary>
    public class SpecialtyDatabaseAccess
    {
        private const string ConStr = "server=160.10.25.16; port=3306; uid=cs3230f20j;" +
                                      "pwd=F1UgUzIjwlhLAQ9a;database=cs3230f20j;";

        /// <summary>
        /// Gets the name of the specialty.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public string GetSpecialtyName(int id)
        {
            var specialtyName = "";
            try
            {
                const string query = "SELECT s.specialtyname FROM specialty s WHERE s.specialtyid = @specialtyid;";
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand { CommandText = query, Connection = conn };
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@specialtyid", id);
                using var reader = cmd.ExecuteReader();
                var specialtynameOrdinal = reader.GetOrdinal("specialtyname");

                if (reader.HasRows)
                {
                    //Should only have one row so no while here.
                    reader.Read();
                    specialtyName = reader.GetString(specialtynameOrdinal);
                }
                else
                {
                    Console.WriteLine("No rows exist in table");
                }
                return specialtyName;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in the GetSpecialtyName: " + ex);
                return specialtyName;
            }
        }

        /// <summary>
        /// Gets the doctor specialties identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public List<int> GetDoctorSpecialtiesId(int id)
        {
            var specialtyName = new List<int>();
            try
            {
                const string query = "SELECT ds.specialtyid FROM doctor_specialty ds WHERE ds.doctorid = @doctorid;";
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand { CommandText = query, Connection = conn };
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@doctorid", id);
                using var reader = cmd.ExecuteReader();
                var specialtynameOrdinal = reader.GetOrdinal("specialtyname");

                if (reader.HasRows)
                {
                    while (reader.Read()) //TODO Needs to be tested with multiple specialties per doctor.
                    {
                        specialtyName.Add(reader.GetInt32(specialtynameOrdinal));
                    }
                }
                else
                {
                    Console.WriteLine("No rows exist in table");
                }
                return specialtyName;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in the GetDoctorSpecialtiesId: " + ex);
                return specialtyName;
            }
        }
    }
}
