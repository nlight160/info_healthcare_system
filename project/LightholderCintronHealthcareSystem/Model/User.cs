using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightholderCintronHealthcareSystem.Model
{
    public class User
    {

        public LoginCredentials login { get; }

        public User(LoginCredentials login)
        {
            this.login = login ?? throw new ArgumentNullException(nameof(login), "can not be null");
        }
    }
}
