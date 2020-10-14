using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightholderCintronHealthcareSystem.Model
{
    public class Patient : Person
    {
        public Patient(string firstname, string lastname, DateTime birthdate, Address address, string phoneNumber) : base(firstname, lastname, birthdate, address, phoneNumber)
        {
        }

        public Patient(string firstname, string lastname) : base(firstname, lastname)
        {
        }
    }
}
