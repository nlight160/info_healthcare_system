﻿using System;

namespace LightholderCintronHealthcareSystem.Model
{
    /// <summary>
    /// Data class that contains date information.
    /// </summary>
    public class Date
    {
        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        public string year { get; set; }
        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        /// <value>
        /// The month.
        /// </value>
        public string month { get; set; }
        /// <summary>
        /// Gets or sets the day.
        /// </summary>
        /// <value>
        /// The day.
        /// </value>
        public string day { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Date"/> class.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        /// <exception cref="ArgumentNullException">
        /// year - can not be null or empty
        /// or
        /// month - can not be null or empty
        /// or
        /// day - can not be null or empty
        /// </exception>
        public Date(string year, string month, string day)
        {
            if (string.IsNullOrEmpty(year))
            {
                throw new ArgumentNullException(nameof(year), "can not be null or empty");
            }
            if (string.IsNullOrEmpty(month))
            {
                throw new ArgumentNullException(nameof(month), "can not be null or empty");
            }
            if (string.IsNullOrEmpty(day))
            {
                throw new ArgumentNullException(nameof(day), "can not be null or empty");
            }

            this.year = year.PadLeft(4, '0');
            this.month = month.PadLeft(2, '0');
            this.day = day.PadLeft(2, '0');
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.year + "-" + this.month + "-" + this.day;
        }
    }
}