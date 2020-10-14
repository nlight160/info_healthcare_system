using System;
using System.Linq;

namespace LightholderCintronHealthcareSystem.Model
{
    public abstract class Person
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthdate { get; set; }
        public Address Address { get; set; }
        public string PhoneNumber { get; set; }

        protected Person(string firstname, string lastname, DateTime birthdate, Address address, string phoneNumber)
        {
            if (string.IsNullOrEmpty(firstname))
            {
                throw new ArgumentNullException(nameof(firstname), "can not be null or empty");
            }
            this.Firstname = firstname;
            if (string.IsNullOrEmpty(lastname))
            {
                throw new ArgumentNullException(nameof(lastname), "can not be null or empty");
            }
            this.Lastname = lastname;
            this.Address = address ?? throw new ArgumentNullException(nameof(address), "can not be null");
            if (birthdate.Date > DateTime.Today || birthdate == null)
            {
                throw new ArgumentException("Birthdate can not be null or after current date");
            }
            this.Birthdate = birthdate;
            if (phoneNumber != null || phoneNumber.Length != 10 || !phoneNumber.All(char.IsNumber))
            {
                throw new ArgumentException("Phone number must be 10 digits with no special characters and not null");
            }
            this.PhoneNumber = phoneNumber;
        }

        protected Person(string firstname, string lastname)
        {
            this.Firstname = firstname ?? throw new ArgumentNullException(nameof(firstname), "can not be null");
            this.Lastname = lastname ?? throw new ArgumentNullException(nameof(lastname), "can not be null");
        }

    }
}
