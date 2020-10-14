using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightholderCintronHealthcareSystem.Model
{
    public class Doctor : Person
    {
        public string Specialty { get; }

        public Doctor(string firstname, string lastname, DateTime birthdate, Address address, string phoneNumber, string specialty) : base(firstname, lastname, birthdate, address, phoneNumber)
        {
            if (string.IsNullOrEmpty(specialty))
            {
                throw new ArgumentNullException(nameof(specialty), "can not be null or empty");
            }

            this.Specialty = specialty;

        }

        public Doctor(string firstname, string lastname, string specialty) : base(firstname, lastname)
        {
            if (string.IsNullOrEmpty(specialty))
            {
                throw new ArgumentNullException(nameof(specialty), "can not be null or empty");
            }
            this.Specialty = specialty;
        }
    }
}
