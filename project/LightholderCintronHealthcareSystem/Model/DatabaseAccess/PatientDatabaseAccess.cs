using System;
using System.Collections.Generic;
using LightholderCintronHealthcareSystem.Model.People;
using MySql.Data.MySqlClient;

namespace LightholderCintronHealthcareSystem.Model.DatabaseAccess
{
    /// <summary>
    /// Patient Database access.
    /// </summary>
    public class PatientDatabaseAccess
    {
        /// <summary>
        /// The con string
        /// </summary>
        private const string ConStr = "server=160.10.25.16; port=3306; uid=cs3230f20j;" +
                                      "pwd=F1UgUzIjwlhLAQ9a;database=cs3230f20j;";

        /// <summary>
        /// Creates the patient.
        /// </summary>
        /// <param name="p">The p.</param>
        public void CreatePatient(Patient p)
        {
            var lname = p.Lastname;
            var fname = p.Firstname;
            var dob = p.Birthdate.ToString();
            var street = p.Address.Street;
            var city = p.Address.City;
            var state = p.Address.State;
            var zip = p.Address.Zip;
            var phone = p.PhoneNumber;
            const string gender = nameof(p.Gender);
            MySqlTransaction transaction = null;
            try
            {
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                transaction = conn.BeginTransaction();
                using var cmd = new MySqlCommand {Connection = conn, Transaction = transaction};

                const string createPerson = "INSERT INTO `person` (`personid`, `lname`, `fname`, `dob`, `street`, `city`, `state`, `zip`, `phone`, `gender`) VALUES (null, @lname, @fname, @dob, @street, @city, @state, @zip, @phone, @gender);";
                const string createPatient = "INSERT INTO patient (patientid, personid) SELECT null, p.personid FROM person p WHERE p.lname = @lname AND p.fname = @fname AND p.dob = @dob AND p.phone = @phone;";

                cmd.CommandText = createPerson;

                cmd.Parameters.AddWithValue("@lname", lname);
                cmd.Parameters.AddWithValue("@fname", fname);
                cmd.Parameters.AddWithValue("@dob", dob);
                cmd.Parameters.AddWithValue("@street", street);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@state", state);
                cmd.Parameters.AddWithValue("@zip", zip);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@gender", gender);

                cmd.ExecuteNonQuery();

                cmd.CommandText = createPatient;

                //Don't need the below lines but keeping them for now.
                cmd.Parameters["@lname"].Value = lname;
                cmd.Parameters["@fname"].Value = fname;
                cmd.Parameters["@dob"].Value = dob;
                cmd.Parameters["@phone"].Value = phone;
                cmd.ExecuteNonQuery();
                transaction.Commit();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in the CreatePatient: " + ex);
                transaction?.Rollback();

            }
        }

        /// <summary>
        /// Updates the patient.
        /// </summary>
        /// <param name="p">The p.</param>
        public void UpdatePatient(Patient p)
        {
            var pid = p.Personid;
            var lname = p.Lastname;
            var fname = p.Firstname;
            var dob = p.Birthdate.ToString();
            var street = p.Address.Street;
            var city = p.Address.City;
            var state = p.Address.State;
            var zip = p.Address.Zip;
            var phone = p.PhoneNumber;
            const string gender = nameof(p.Gender);
            try
            {
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand { Connection = conn};

                const string updatePaitent = "UPDATE person p SET p.lname = @lname, p.fname = @fname, p.dob = @dob, p.street = @street, p.city = @city, p.state = @state, p.zip = @zip, p.phone = @phone, p.gender = @gender WHERE p.personid = @pid;";
                
                cmd.CommandText = updatePaitent;

                cmd.Parameters.AddWithValue("@lname", lname);
                cmd.Parameters.AddWithValue("@fname", fname);
                cmd.Parameters.AddWithValue("@dob", dob);
                cmd.Parameters.AddWithValue("@street", street);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@state", state);
                cmd.Parameters.AddWithValue("@zip", zip);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@pid", pid);
                //TODO add confirmation
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in the CreatePatient: " + ex);

            }
        }

        /// <summary>
        /// Gets the patient data from identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public List<string> GetPatientDataFromId(int id)
        {
            var patientData = new List<string>();

            try
            {
                const string query = "SELECT DISTINCT p.personid, p.fname, p.lname, p.dob, p.street, p.city, p.state, p.zip, p.phone, p.gender FROM person p, patient pt WHERE p.personid = pt.personid AND pt.patientid = @patientid;";
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

        /// <summary>
        /// Searches the name of the patients with.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public List<int> SearchPatientsWithName(string name)
        {
            name = "%" + name + "%";
            var patientList = new List<int>();
            try
            {
                const string query = "SELECT DISTINCT pt.patientid FROM patient pt, person p WHERE CONCAT(p.fname, p.lname) LIKE @name AND p.personid = pt.personid;";
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand { CommandText = query, Connection = conn };
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@name", name);
                using var reader = cmd.ExecuteReader();
                var patientidOrdinal = reader.GetOrdinal("patientid");
                
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        patientList.Add(reader.GetInt32(patientidOrdinal));
                    }
                }
                else
                {
                    Console.WriteLine("No rows exist in table");
                }
                return patientList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in the SearchPatientsWithName: " + ex);
                return patientList;
            }
        }

        /// <summary>
        /// Searches the patients with date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public List<int> SearchPatientsWithDate(string date)
        {
            var patientList = new List<int>();
            try
            {
                const string query = "SELECT DISTINCT pt.patientid FROM patient pt, person p WHERE p.personid = pt.personid AND p.dob = @date;";
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand { CommandText = query, Connection = conn };
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@date", date);
                using var reader = cmd.ExecuteReader();
                var patientidOrdinal = reader.GetOrdinal("patientid");

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        patientList.Add(reader.GetInt32(patientidOrdinal));
                    }
                }
                else
                {
                    Console.WriteLine("No rows exist in table");
                }
                return patientList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in the SearchPatientsWithName: " + ex);
                return patientList;
            }
        }
    }
}
