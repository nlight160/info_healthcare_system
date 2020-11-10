using System;

namespace LightholderCintronHealthcareSystem.Model.People
{
    /// <summary>
    /// Time data class
    /// </summary>
    public class Time
    {
        /// <summary>
        /// Gets or sets the hour.
        /// </summary>
        /// <value>
        /// The hour.
        /// </value>
        public string Hour { get; set; }
        /// <summary>
        /// Gets or sets the minute.
        /// </summary>
        /// <value>
        /// The minute.
        /// </value>
        public string Minute { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Time"/> class.
        /// </summary>
        /// <param name="hour">The hour.</param>
        /// <param name="minute">The minute.</param>
        /// <exception cref="ArgumentNullException">
        /// hour - can not be null or empty
        /// or
        /// minute - can not be null or empty
        /// </exception>
        public Time(string hour, string minute)
        {
            if (string.IsNullOrEmpty(hour))
            {
                throw new ArgumentNullException(nameof(hour), "can not be null or empty");
            }
            if (string.IsNullOrEmpty(minute))
            {
                throw new ArgumentNullException(nameof(minute), "can not be null or empty");
            }

            this.Hour = hour.PadLeft(2, '0');
            this.Minute = minute.PadLeft(2, '0');
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.Hour + ":" + this.Minute;
        }

    }
}
