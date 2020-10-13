using System;

namespace LightholderCintronHealthcareSystem.Model
{
    public class LoginCredentials
    {
        public string username { get; private set; }
        public string password { get; private set; }

        public LoginCredentials(string username, string password)
        {
            this.username = username ?? throw new ArgumentNullException(nameof(username), "can not be null");
            this.password = password ?? throw new ArgumentNullException(nameof(password), "can not be null");
        }

    }
}
