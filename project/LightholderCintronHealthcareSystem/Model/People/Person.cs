using System;
using System.Linq;

namespace LightholderCintronHealthcareSystem.Model.People
{
    /// <summary>
    /// Person class
    /// </summary>
    public abstract class Person
    {

        /// <summary>
        /// Gets or sets the personid.
        /// </summary>
        /// <value>
        /// The personid.
        /// </value>
        public string Personid { get; set; }
        /// <summary>
        /// Gets or sets the firstname.
        /// </summary>
        /// <value>
        /// The firstname.
        /// </value>
        public string Firstname { get; set; }
        /// <summary>
        /// Gets or sets the lastname.
        /// </summary>
        /// <value>
        /// The lastname.
        /// </value>
        public string Lastname { get; set; }
        /// <summary>
        /// Gets or sets the birthdate.
        /// </summary>
        /// <value>
        /// The birthdate.
        /// </value>
        public Date Birthdate { get; set; }
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public Address Address { get; set; }
        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        public Gender Gender { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        /// <param name="personid">The personid.</param>
        /// <param name="firstname">The firstname.</param>
        /// <param name="lastname">The lastname.</param>
        /// <param name="birthdate">The birthdate.</param>
        /// <param name="address">The address.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="gender">The gender.</param>
        /// <exception cref="ArgumentNullException">
        /// firstname - can not be null or empty
        /// or
        /// lastname - can not be null or empty
        /// or
        /// address - can not be null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Birthdate can not be null or after current date
        /// or
        /// Phone number must be 10 digits with no special characters and not null
        /// </exception>
        protected Person(string personid, string firstname, string lastname, Date birthdate, Address address, string phoneNumber, Gender gender)
        {
            this.Personid = personid;
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
            this.Birthdate = birthdate ?? throw new ArgumentException("Birthdate can not be null or after current date");
            if (phoneNumber == null || phoneNumber.Length != 10 || !phoneNumber.All(char.IsNumber))
            {
                throw new ArgumentException("Phone number must be 10 digits with no special characters and not null");
            }
            this.PhoneNumber = phoneNumber;
            this.Gender = gender;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        /// <param name="firstname">The firstname.</param>
        /// <param name="lastname">The lastname.</param>
        /// <exception cref="ArgumentNullException">
        /// firstname - can not be null
        /// or
        /// lastname - can not be null
        /// </exception>
        protected Person(string firstname, string lastname)
        {
            this.Firstname = firstname ?? throw new ArgumentNullException(nameof(firstname), "can not be null");
            this.Lastname = lastname ?? throw new ArgumentNullException(nameof(lastname), "can not be null");
        }

    }
}
