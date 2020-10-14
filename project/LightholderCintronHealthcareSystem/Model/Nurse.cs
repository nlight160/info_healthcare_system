using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightholderCintronHealthcareSystem.Model
{
    public class Nurse : Person
    {
        public Nurse(string firstname, string lastname, DateTime birthdate, Address address, string phoneNumber) : base(firstname, lastname, birthdate, address, phoneNumber)
        {

        }

        public Nurse(string firstname, string lastname) : base(firstname, lastname)
        {
        }
    }
}
