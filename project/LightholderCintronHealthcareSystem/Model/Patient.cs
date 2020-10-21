﻿using System;

namespace LightholderCintronHealthcareSystem.Model
{
    /// <summary>
    /// Patient class
    /// </summary>
    /// <seealso cref="LightholderCintronHealthcareSystem.Model.Person" />
    public class Patient : Person
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Patient"/> class.
        /// </summary>
        /// <param name="firstname">The firstname.</param>
        /// <param name="lastname">The lastname.</param>
        /// <param name="birthdate">The birthdate.</param>
        /// <param name="address">The address.</param>
        /// <param name="phoneNumber">The phone number.</param>
        public Patient(string firstname, string lastname, Date birthdate, Address address, string phoneNumber, Gender gender) : base(firstname, lastname, birthdate, address, phoneNumber, gender)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Patient"/> class.
        /// </summary>
        /// <param name="firstname">The firstname.</param>
        /// <param name="lastname">The lastname.</param>
        public Patient(string firstname, string lastname) : base(firstname, lastname)
        {
        }
    }
}
