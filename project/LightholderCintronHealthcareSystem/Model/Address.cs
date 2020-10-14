using System;

namespace LightholderCintronHealthcareSystem.Model
{
    public class Address
    {
        public string Street { get; }
        public string City { get; }
        public string Zip { get; }

        public Address(string street, string city, string zip)
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
        }

    }
}
