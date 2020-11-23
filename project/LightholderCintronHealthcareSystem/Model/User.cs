using System;
using LightholderCintronHealthcareSystem.Model.People;

namespace LightholderCintronHealthcareSystem.Model
{
    /// <summary>
    /// User class
    /// </summary>
    public class User
    {

        /// <summary>
        /// Gets the nurse information.
        /// </summary>
        /// <value>
        /// The nurse information.
        /// </value>
        public Person PersonInfo { get; }
        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; }

        /// <summary>
        /// Gets the IsAdmin identifier.
        /// </summary>
        /// <value>
        /// The IsAdmin identifier.
        /// </value>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <param name="userid">The userid.</param>
        /// <exception cref="ArgumentNullException">person - can not be null</exception>
        public User(Person person, int userid)
        {
            this.PersonInfo = person ?? throw new ArgumentNullException(nameof(person), "can not be null");
            this.UserId = userid;
            this.IsAdmin = this.PersonInfo.GetType() == typeof(Admin);
        }


    }
}
