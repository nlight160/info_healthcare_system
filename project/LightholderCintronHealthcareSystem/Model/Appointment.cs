using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightholderCintronHealthcareSystem.Model
{
    public class Appointment
    {
        public DateTime AppointmentDateTime { get; }
        public Person Patient { get; }
        public Doctor Doctor { get; }
        public string Description { get; }

        public Appointment(Person patient, Doctor doctor, DateTime appointmentDateTime, string description)
        {
            this.Patient = patient ?? throw new ArgumentNullException(nameof(patient), "can not be null");
            this.Doctor = doctor ?? throw new ArgumentNullException(nameof(doctor), "can not be null");
            if (appointmentDateTime.Date > DateTime.Today || appointmentDateTime == null)
            {
                throw new ArgumentException("Birthdate can not be null or after current date");
            }
            this.AppointmentDateTime = appointmentDateTime;
            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException(nameof(description), "can not be null or empty");
            }
            this.Description = description;
        }
    }
}
