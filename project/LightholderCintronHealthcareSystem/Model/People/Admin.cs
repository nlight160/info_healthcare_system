using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightholderCintronHealthcareSystem.Model.People
{
    /// <summary>
    ///Admin of person used for determining if user is an admin
    /// </summary>
    /// <seealso cref="LightholderCintronHealthcareSystem.Model.People.Person" />
    public class Admin : Person
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Admin"/> class.
        /// </summary>
        /// <param name="personid">The personid.</param>
        /// <param name="firstname">The firstname.</param>
        /// <param name="lastname">The lastname.</param>
        /// <param name="birthdate">The birthdate.</param>
        /// <param name="address">The address.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="gender">The gender.</param>
        public Admin(string personid, string firstname, string lastname, Date birthdate, Address address, string phoneNumber, Gender gender) : base(personid, firstname, lastname, birthdate, address, phoneNumber, gender)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Admin"/> class.
        /// </summary>
        /// <param name="firstname">The firstname.</param>
        /// <param name="lastname">The lastname.</param>
        public Admin(string firstname, string lastname) : base(firstname, lastname)
        {
        }
    }
}
