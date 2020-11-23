using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightholderCintronHealthcareSystem.Model.People
{
    public class Admin : Person
    {
        public Admin(string personid, string firstname, string lastname, Date birthdate, Address address, string phoneNumber, Gender gender) : base(personid, firstname, lastname, birthdate, address, phoneNumber, gender)
        {
        }

        public Admin(string firstname, string lastname) : base(firstname, lastname)
        {
        }
    }
}
