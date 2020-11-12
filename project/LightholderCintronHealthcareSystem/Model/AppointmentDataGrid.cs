﻿using LightholderCintronHealthcareSystem.Model.People;
using System;
using Windows.Devices.Bluetooth.Background;

namespace LightholderCintronHealthcareSystem.Model
{
    /// <summary>
    /// Appointment data class for datagrid
    /// </summary>
    public class AppointmentDataGrid
    {
        /// <summary>
        /// Gets or sets the appointmentid.
        /// </summary>
        /// <value>
        /// The appointmentid.
        /// </value>
        public int Appointmentid { get;}
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public string Date { get;}
        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        public string Time { get;}
        /// <summary>
        /// Gets or sets the name of the doctor.
        /// </summary>
        /// <value>
        /// The name of the doctor.
        /// </value>
        public string DoctorName { get;}
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get;}

        /// <summary>
        /// Gets or sets the date time.
        /// </summary>
        /// <value>
        /// The date time.
        /// </value>
        public DateTime DateTime { get;}

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentDataGrid"/> class.
        /// </summary>
        /// <param name="appointmentid">The appointmentid.</param>
        /// <param name="dateTime">The date time.</param>
        /// <param name="doctorName">Name of the doctor.</param>
        /// <param name="description">The description.</param>
        /// <exception cref="ArgumentNullException">doctorName - can not be null or empty</exception>
        public AppointmentDataGrid(int appointmentid, DateTime dateTime, string doctorName, string description)
        {

            if (string.IsNullOrEmpty(doctorName))
            {
                throw new ArgumentNullException(nameof(doctorName), "can not be null or empty");
            }

            this.Appointmentid = appointmentid;
            var date = new Date(dateTime.Year.ToString(), dateTime.Month.ToString(), dateTime.Day.ToString());
            this.Date = date.ToString();
            var time = new Time(dateTime.Hour.ToString(), dateTime.Minute.ToString());
            this.Time = time.ToString();
            this.DoctorName = doctorName;
            this.Description = description;
            this.DateTime = dateTime;
        }

    }
}
