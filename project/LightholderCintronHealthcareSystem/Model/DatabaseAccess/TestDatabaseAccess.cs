﻿using LightholderCintronHealthcareSystem.Model.People;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace LightholderCintronHealthcareSystem.Model.DatabaseAccess
{
    class TestDatabaseAccess
    {
        private const string ConStr = "server=160.10.25.16; port=3306; uid=cs3230f20j;" +
                                      "pwd=F1UgUzIjwlhLAQ9a;database=cs3230f20j;";
        /// <summary>
        /// Adds a test to the data base
        /// </summary>
        public bool AddTests(Test test)
        {
            try
            {
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand { Connection = conn };

                const string makeAppointment =
                    "INSERT INTO `test` (`testname`, `appointmentid`, `datetime`, `results`, `normailty`) VALUES (@testname, @appointmentid, @datetime, @results, @normality);";
                cmd.CommandText = makeAppointment;
                cmd.Parameters.AddWithValue("@testname", test.TestName);
                cmd.Parameters.AddWithValue("@appointmentid", test.AppointmentId);
                cmd.Parameters.AddWithValue("@datetime", test.DatePerformed);
                cmd.Parameters.AddWithValue("@results", test.TestResults);
                cmd.Parameters.AddWithValue("@normality", test.IsNormal);

                var confirmation = cmd.ExecuteNonQuery();
                return confirmation == 1;


            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in the CreatePatient: " + ex);
                return false;
            }

        }

        /// <summary>
        /// Gets tests from the database
        /// </summary>
        public List<List<string>> GetTests(int appointmentid)
        {
            var testList = new List<List<string>>();
            try
            {
                const string query =
                    "SELECT DISTINCT t.testid, t.testname, t.appointmentid, t.datetime, t.results, t.normailty FROM test t, appointment a WHERE t.`appointmentid` = @appointmentid";
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand { CommandText = query, Connection = conn };
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@appointmentid", appointmentid);
                using var reader = cmd.ExecuteReader();
                var testidOrdinal = reader.GetOrdinal("testid");
                var testnameOrdinal = reader.GetOrdinal("testname");
                var appointmentidOrdinal = reader.GetOrdinal("appointmentid");
                var datetimeOrdinal = reader.GetOrdinal("datetime");
                var resultsOrdinal = reader.GetOrdinal("results");
                var normailtyOrdinal = reader.GetOrdinal("normailty");

                while (reader.Read())
                {
                    var singleTest = new List<string> {
                        reader.GetString(testidOrdinal),            //0
                        reader.GetString(testnameOrdinal),          //1
                        reader.GetString(appointmentidOrdinal),     //2
                        reader.GetString(datetimeOrdinal),          //3
                        reader.GetString(resultsOrdinal),           //4
                        reader.GetString(normailtyOrdinal)          //5
                    };
                    testList.Add(singleTest);
                }
                return testList;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in the CreatePatient: " + ex);
                return testList;
            }

        }

        /// <summary>
        /// Updates test results in the data base using testid
        /// </summary>
        public bool EditTestResults(string results, bool normality, int testid, DateTime dateTime)
        {
            var dateTimeString = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                using var conn = new MySqlConnection(ConStr);
                conn.Open();
                using var cmd = new MySqlCommand { Connection = conn };

                const string updateCheckup = "UPDATE `test` SET `results` = @results, `normailty` = @normality, `datetime` = @dateTime WHERE `testid` = @testid;";
                cmd.CommandText = updateCheckup;
                cmd.Parameters.AddWithValue("@results", results);
                cmd.Parameters.AddWithValue("@normality", normality);
                cmd.Parameters.AddWithValue("@testid", testid);
                cmd.Parameters.AddWithValue("@dateTime", dateTimeString);

                var confirmation = cmd.ExecuteNonQuery();
                return confirmation == 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in the CreatePatient: " + ex);
                return false;
            }
        }
    }
}
