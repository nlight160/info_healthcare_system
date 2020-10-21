using System;

namespace LightholderCintronHealthcareSystem.Model
{
    /// <summary>
    /// Address class
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets the street.
        /// </summary>
        /// <value>
        /// The street.
        /// </value>
        public string Street { get; }
        /// <summary>
        /// Gets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; }
        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public string State { get; }
        /// <summary>
        /// Gets the zip.
        /// </summary>
        /// <value>
        /// The zip.
        /// </value>
        public string Zip { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Address"/> class.
        /// </summary>
        /// <param name="street">The street.</param>
        /// <param name="city">The city.</param>
        /// <param name="zip">The zip.</param>
        /// <exception cref="ArgumentNullException">
        /// zip - can not be null or empty
        /// or
        /// city - can not be null or empty
        /// or
        /// street - can not be null or empty
        /// </exception>
        public Address(string street, string city, string state, string zip)
        {
            if (string.IsNullOrEmpty(zip))
            {
                throw new ArgumentNullException(nameof(zip), "can not be null or empty");
            }
            this.Zip = zip;
            if (string.IsNullOrEmpty(city))
            {
                throw new ArgumentNullException(nameof(city), "can not be null or empty");
            }

            this.City = city;
            if (string.IsNullOrEmpty(street))
            {
                throw new ArgumentNullException(nameof(street), "can not be null or empty");
            }
            this.Street = street;
            if (string.IsNullOrEmpty(state))
            {
                throw new ArgumentNullException(nameof(state), "can not be null or empty");
            }
            this.State = state;
        }

    }
}
