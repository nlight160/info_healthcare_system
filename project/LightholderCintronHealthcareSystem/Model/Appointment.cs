﻿using System;
using LightholderCintronHealthcareSystem.Model.People;

namespace LightholderCintronHealthcareSystem.Model
{
    /// <summary>
    /// Appointment class
    /// </summary>
    public class Appointment
    {
        /// <summary>
        /// Gets or sets the appointmentid. 
        /// </summary>
        /// <value>
        /// The appointmentid.
        /// </value>
        public int? Appointmentid { get; set; }
        /// <summary>
        /// Gets the appointment date time.
        /// </summary>
        /// <value>
        /// The appointment date time.
        /// </value>
        public DateTime AppointmentDateTime { get; }
        /// <summary>
        /// Gets the patient.
        /// </summary>
        /// <value>
        /// The patient.
        /// </value>
        public Patient Patient { get; } 
        /// <summary>
        /// Gets the doctor.
        /// </summary>
        /// <value>
        /// The doctor.
        /// </value>
        public Doctor Doctor { get; }
        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Appointment"/> class.
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <param name="patient">The patient.</param>
        /// <param name="doctor">The doctor.</param>
        /// <param name="appointmentDateTime">The appointment date time.</param>
        /// <param name="description">The description.</param>
        /// <exception cref="ArgumentNullException">
        /// patient - can not be null
        /// or
        /// doctor - can not be null
        /// or
        /// description - can not be null or empty
        /// </exception>
        /// <exception cref="ArgumentException">Birthdate can not be null or after current date</exception>
        public Appointment(int? appointmentId, Patient patient, Doctor doctor, DateTime appointmentDateTime, string description)
        {
            this.Appointmentid = appointmentId;
            this.Patient = patient ?? throw new ArgumentNullException(nameof(patient), "can not be null");
            this.Doctor = doctor ?? throw new ArgumentNullException(nameof(doctor), "can not be null");
            this.AppointmentDateTime = appointmentDateTime;
            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException(nameof(description), "can not be null or empty");
            }
            this.Description = description;
        }
    }
}
