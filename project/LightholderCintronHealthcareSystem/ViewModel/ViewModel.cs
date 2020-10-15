using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store.Preview.InstallControl;
using LightholderCintronHealthcareSystem.Model;

namespace LightholderCintronHealthcareSystem.ViewModel
{
    public class ViewModel
    {
        public static User ActiveUser { get; private set; }

        public static bool AttemptLogin(string username, string password)
        {
            DatabaseAccess db = new DatabaseAccess();
            var information = 
                db.LoginQuery(
                    "SELECT p.fname, p.lname FROM person p, nurse n WHERE n.personid = p.personid AND n.nurseid = " +
                    username + " AND n.password = " + "'" + password + "'" + ";");
            
            if (information != null)
            {
                var loginCredentials = new Nurse(information[0], information[1]);
                ActiveUser = new User(loginCredentials, int.Parse(username));
                return true;
            }
            return false;
        }

        public static void RegisterPatient(string lname, string fname, string dob, string street, string city, string state, string zip,
                                            string phone)
        {
            QueryBuilder qb = new QueryBuilder();
            DatabaseAccess db = new DatabaseAccess();
            string query = qb.addPatient(lname, fname,  dob,  street,  city,  state,  zip, phone);
            db.CreatePatient(query);
        }

        public static void Logout()
        {
            ActiveUser = null;
        }
    }
}
