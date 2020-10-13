using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightholderCintronHealthcareSystem.Model
{
    public class User
    {
        private LoginCredentials dummyLogin = new LoginCredentials("john", "123");
        public LoginCredentials login { get; }

        public User(LoginCredentials login)
        {
            this.login = login ?? throw new ArgumentNullException(nameof(login), "can not be null");
        }

        public bool VerifyUserExists()
        {
            //would use the real credentials we pull from text here
           return dummyLogin.CompareLoginCredentials(login);
        }
    }
}
