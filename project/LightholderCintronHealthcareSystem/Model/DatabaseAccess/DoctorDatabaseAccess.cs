using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace LightholderCintronHealthcareSystem.Model.DatabaseAccess
{
    public class DoctorDatabaseAccess
    {
        private const string ConStr = "server=160.10.25.16; port=3306; uid=cs3230f20j;" +
                                      "pwd=F1UgUzIjwlhLAQ9a;database=cs3230f20j;";

        public List<string> GetDoctorDataFromId(int id)
        {
            var patientData = new List<string>();

            try
            {
                const string query = "SELECT DISTINCT p.personid, p.fname, p.lname, p.dob, p.street, p.city, p.state, p.zip, p.phone, p.gender FROM person p, doctor d WHERE p.personid = d.personid AND d.doctorid = @doctorid;";
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand { CommandText = query, Connection = conn };
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@patientid", id);
                using var reader = cmd.ExecuteReader();
                var pidOrdinal = reader.GetOrdinal("personid");
                var fnameOrdinal = reader.GetOrdinal("fname");
                var lnameOrdinal = reader.GetOrdinal("lname");
                var dobOrdinal = reader.GetOrdinal("dob");
                var streetOrdinal = reader.GetOrdinal("street");
                var cityOrdinal = reader.GetOrdinal("city");
                var stateOrdinal = reader.GetOrdinal("state");
                var zipOrdinal = reader.GetOrdinal("zip");
                var phoneOrdinal = reader.GetOrdinal("phone");
                var genderOrdinal = reader.GetOrdinal("gender");

                if (reader.HasRows)
                {
                    //Should only have one row so no while here.
                    reader.Read();
                    patientData.Add(reader.GetString(fnameOrdinal));    //0
                    patientData.Add(reader.GetString(lnameOrdinal));    //1
                    patientData.Add(reader.GetString(dobOrdinal));      //2
                    patientData.Add(reader.GetString(streetOrdinal));   //3
                    patientData.Add(reader.GetString(cityOrdinal));     //4
                    patientData.Add(reader.GetString(stateOrdinal));    //5
                    patientData.Add(reader.GetString(zipOrdinal));      //6
                    patientData.Add(reader.GetString(phoneOrdinal));    //7
                    patientData.Add(reader.GetString(genderOrdinal));   //8
                    patientData.Add(reader.GetString(pidOrdinal));      //9
                    patientData.Add(id.ToString());                     //10
                }
                else
                {
                    Console.WriteLine("No rows exist in table");
                }
                return patientData;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in the SearchPatientsWithName: " + ex);
                return patientData;
            }
        }
    }
}
