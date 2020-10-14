using System;
using System.Collections.Generic;
using System.Linq;
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
                db.loginQuery(
                    "SELECT p.fname, p.lname FROM person p, nurse n WHERE n.personid = p.personid AND n.nurseid = " +
                    username + " AND n.password = " + password + ";");
            var loginCredentials = new LoginCredentials(information[0], information[1]);
            ActiveUser = new User(loginCredentials);
            return information != null;
        }
    }
}
