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
            var loginCredentials = new LoginCredentials(username, password);
            ActiveUser = new User(loginCredentials);
            return ActiveUser.VerifyUserExists();
        }
    }
}
