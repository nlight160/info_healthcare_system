﻿using System;

namespace LightholderCintronHealthcareSystem.Model.People
{
    /// <summary>
    /// Doctor class
    /// </summary>
    /// <seealso cref="Person" />
    public class Doctor : Person
    {

        /// <summary>
        /// Gets or sets the doctorid.
        /// </summary>
        /// <value>
        /// The doctorid.
        /// </value>
        public string Doctorid { get; set; }

        /// <summary>
        /// Gets the specialty.
        /// </summary>
        /// <value>
        /// The specialty.
        /// </value>
        public string Specialty { get; }



        /// <summary>
        /// Initializes a new instance of the <see cref="Doctor"/> class.
        /// </summary>
        /// <param name="personid">The personid.</param>
        /// <param name="firstname">The firstname.</param>
        /// <param name="lastname">The lastname.</param>
        /// <param name="birthdate">The birthdate.</param>
        /// <param name="address">The address.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="gender">The gender.</param>
        /// <param name="specialty">The specialty.</param>
        /// <exception cref="ArgumentNullException">specialty - can not be null or empty</exception>
        public Doctor(string personid, string firstname, string lastname, Date birthdate, Address address,
            string phoneNumber,
            Gender gender, string specialty) : base(personid, firstname, lastname, birthdate, address, phoneNumber,
            gender)
        {
            if (string.IsNullOrEmpty(specialty))
            {
                throw new ArgumentNullException(nameof(specialty), "can not be null or empty");
            }

            this.Specialty = specialty;

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Doctor"/> class.
        /// </summary>
        /// <param name="firstname">The firstname.</param>
        /// <param name="lastname">The lastname.</param>
        /// <param name="specialty">The specialty.</param>
        /// <exception cref="ArgumentNullException">specialty - can not be null or empty</exception>
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
