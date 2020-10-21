using System;

namespace LightholderCintronHealthcareSystem.Model
{
    /// <summary>
    /// Nurse class
    /// </summary>
    /// <seealso cref="LightholderCintronHealthcareSystem.Model.Person" />
    public class Nurse : Person
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Nurse"/> class.
        /// </summary>
        /// <param name="firstname">The firstname.</param>
        /// <param name="lastname">The lastname.</param>
        /// <param name="birthdate">The birthdate.</param>
        /// <param name="address">The address.</param>
        /// <param name="phoneNumber">The phone number.</param>
        public Nurse(string firstname, string lastname, Date birthdate, Address address, string phoneNumber, Gender gender) : base(firstname, lastname, birthdate, address, phoneNumber, gender)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Nurse"/> class.
        /// </summary>
        /// <param name="firstname">The firstname.</param>
        /// <param name="lastname">The lastname.</param>
        public Nurse(string firstname, string lastname) : base(firstname, lastname)
        {
        }
    }
}
