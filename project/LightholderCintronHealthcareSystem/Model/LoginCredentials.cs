using System;

namespace LightholderCintronHealthcareSystem.Model
{
    public class LoginCredentials
    {
        public string firstname { get; private set; }
        public string lastname { get; set; }

        public LoginCredentials(string firstname, string lastname)
        {
            this.firstname = firstname ?? throw new ArgumentNullException(nameof(firstname), "can not be null");
            this.lastname = lastname ?? throw new ArgumentNullException(nameof(lastname), "can not be null");
        }

        //public bool CompareLoginCredentials(LoginCredentials credentials)
        //{
        //    return username == credentials.username && password == credentials.password;
        //}

    }
}
