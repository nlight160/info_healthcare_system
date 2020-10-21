using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace LightholderCintronHealthcareSystem.Model
{
    public class PaitentDatabaseAccess
    {
        private const string ConStr = "server=160.10.25.16; port=3306; uid=cs3230f20j;" +
                                      "pwd=F1UgUzIjwlhLAQ9a;database=cs3230f20j;";

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
            MySqlTransaction transaction = null;
            try
            {
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                transaction = conn.BeginTransaction();
                using var cmd = new MySqlCommand {Connection = conn, Transaction = transaction};

                var createPerson =
                    "INSERT INTO `person` (`personid`, `lname`, `fname`, `dob`, `street`, `city`, `state`, `zip`, `phone`) VALUES (null, @lname, @fname, @dob, @street, @city, @state, @zip, @phone);";
                var createPatient =
                    "INSERT INTO patient (patientid, personid) SELECT null, p.personid FROM person p WHERE p.lname = @lname AND p.fname = @fname AND p.dob = @dob AND p.phone = @phone;";

                cmd.CommandText = createPerson;
                //cmd.Parameters.Add("@lname", MySqlDbType.VarChar);
                //cmd.Parameters.Add("@fname", MySqlDbType.VarChar);
                //cmd.Parameters.Add("@dob", MySqlDbType.Date);
                //cmd.Parameters.Add("@street", MySqlDbType.VarChar);
                //cmd.Parameters.Add("@city", MySqlDbType.VarChar);
                //cmd.Parameters.Add("@state", MySqlDbType.VarChar);
                //cmd.Parameters.Add("@zip", MySqlDbType.VarChar);
                //cmd.Parameters.Add("@phone", MySqlDbType.VarChar);
                //cmd.Parameters["@lname"].Value = lname;
                //cmd.Parameters["@fname"].Value = fname;
                //cmd.Parameters["@dob"].Value = dob;
                //cmd.Parameters["@street"].Value = street;
                //cmd.Parameters["@city"].Value = city;
                //cmd.Parameters["@state"].Value = state;
                //cmd.Parameters["@zip"].Value = zip;
                //cmd.Parameters["@phone"].Value = phone;

                cmd.Parameters.AddWithValue("@lname", lname);
                cmd.Parameters.AddWithValue("@fname", fname);
                cmd.Parameters.AddWithValue("@dob", dob);
                cmd.Parameters.AddWithValue("@street", street);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@state", state);
                cmd.Parameters.AddWithValue("@zip", zip);
                cmd.Parameters.AddWithValue("@phone", phone);


                cmd.ExecuteNonQuery();

                cmd.CommandText = createPatient;
                //cmd.Parameters.Add("@lname", MySqlDbType.VarChar);
                //cmd.Parameters.Add("@fname", MySqlDbType.VarChar);
                //cmd.Parameters.Add("@dob", MySqlDbType.Date);
                //cmd.Parameters.Add("@phone", MySqlDbType.VarChar);

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
                Console.WriteLine("Exception in the CreatePatient: " + ex.ToString());
                transaction?.Rollback();

            }
        }

    }
}
