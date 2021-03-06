﻿using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace LightholderCintronHealthcareSystem.Model.DatabaseAccess
{
    /// <summary>
    /// Doctor database access
    /// </summary>
    public class DoctorDatabaseAccess
    {
        private const string ConStr = "server=160.10.25.16; port=3306; uid=cs3230f20j;" +
                                      "pwd=F1UgUzIjwlhLAQ9a;database=cs3230f20j;";

        /// <summary>
        /// Gets the doctor data from identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public List<string> GetDoctorDataFromId(int id)
        {
            var doctorData = new List<string>();

            try
            {
                const string query = "SELECT DISTINCT p.personid, p.fname, p.lname, p.dob, p.street, p.city, p.state, p.zip, p.phone, p.gender FROM person p, doctor d WHERE p.personid = d.personid AND d.doctorid = @doctorid;";
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand { CommandText = query, Connection = conn };
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@doctorid", id);
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
                    doctorData.Add(reader.GetString(fnameOrdinal));    //0
                    doctorData.Add(reader.GetString(lnameOrdinal));    //1
                    doctorData.Add(reader.GetString(dobOrdinal));      //2
                    doctorData.Add(reader.GetString(streetOrdinal));   //3
                    doctorData.Add(reader.GetString(cityOrdinal));     //4
                    doctorData.Add(reader.GetString(stateOrdinal));    //5
                    doctorData.Add(reader.GetString(zipOrdinal));      //6
                    doctorData.Add(reader.GetString(phoneOrdinal));    //7
                    doctorData.Add(reader.GetString(genderOrdinal));   //8
                    doctorData.Add(reader.GetString(pidOrdinal));      //9
                    doctorData.Add(id.ToString());                     //10
                }
                else
                {
                    Console.WriteLine("No rows exist in table");
                }
                return doctorData;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in the GetDoctorDataById: " + ex);
                return doctorData;
            }
        }

        /// <summary>
        /// Gets the doctor name from identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public List<string> GetDoctorNameFromId(int id)
        {
            var doctorData = new List<string>();

            try
            {
                const string query = "SELECT DISTINCT p.fname, p.lname FROM person p, doctor d WHERE p.personid = d.personid AND d.doctorid = @doctorid;";
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand { CommandText = query, Connection = conn };
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@doctorid", id);
                using var reader = cmd.ExecuteReader();
                var fnameOrdinal = reader.GetOrdinal("fname");
                var lnameOrdinal = reader.GetOrdinal("lname");

                if (reader.HasRows)
                {
                    //Should only have one row so no while here.
                    reader.Read();
                    doctorData.Add(reader.GetString(fnameOrdinal));    //0
                    doctorData.Add(reader.GetString(lnameOrdinal));    //1
                }
                else
                {
                    Console.WriteLine("No rows exist in table");
                }
                return doctorData;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in the GetDoctorDataById: " + ex);
                return doctorData;
            }
        }

        /// <summary>
        /// Gets the every doctor identifier.
        /// </summary>
        /// <returns></returns>
        public List<int> GetEveryDoctorId()
        {
            var doctorData = new List<int>();

            try
            {
                const string query = "SELECT DISTINCT d.doctorid FROM doctor d;";
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand { CommandText = query, Connection = conn };
                cmd.Prepare();
                using var reader = cmd.ExecuteReader();
                var doctoridOrdinal = reader.GetOrdinal("doctorid");


                if (reader.HasRows)
                {
                    //Should only have one row so no while here.
                    while (reader.Read())
                    {
                        doctorData.Add(reader.GetInt32(doctoridOrdinal));
                    }
                }
                else
                {
                    Console.WriteLine("No rows exist in table");
                }
                return doctorData;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in the GetDoctorDataById: " + ex);
                return doctorData;
            }
        }
    }
}
