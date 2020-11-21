using LightholderCintronHealthcareSystem.Model.People;
using System;

namespace LightholderCintronHealthcareSystem.Model
{
    /// <summary>
    /// Appointment data class for data grid
    /// </summary>
    public class AppointmentDataGrid
    {
        /// <summary>
        /// Gets the patientid.
        /// </summary>
        /// <value>
        /// The patientid.
        /// </value>
        public int Patientid { get; }
        /// <summary>
        /// Gets the name of the patient.
        /// </summary>
        /// <value>
        /// The name of the patient.
        /// </value>
        public string PatientName { get; }

        /// <summary>
        /// Gets the appointmentid.
        /// </summary>
        /// <value>
        /// The appointmentid.
        /// </value>
        public int Appointmentid { get; }

        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public string Date { get; }

        /// <summary>
        /// Gets the time.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        public string Time { get; }

        /// <summary>
        /// Gets the name of the doctor.
        /// </summary>
        /// <value>
        /// The name of the doctor.
        /// </value>
        public string DoctorName { get; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; }


        /// <summary>
        /// Gets the date time.
        /// </summary>
        /// <value>
        /// The date time.
        /// </value>
        public DateTime DateTime { get; }

        public string Dob { get; }
        public int Doctorid { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentDataGrid"/> class.
        /// </summary>
        /// <param name="appointmentid">The appointmentid.</param>
        /// <param name="dateTime">The date time.</param>
        /// <param name="doctorName">Name of the doctor.</param>
        /// <param name="description">The description.</param>
        /// <param name="patientid"></param>
        /// <param name="patientName"></param>
        /// <param name="dob"></param>
        /// <param name="doctorid"></param>
        /// <exception cref="ArgumentNullException">doctorName - can not be null or empty</exception>
        public AppointmentDataGrid(int appointmentid, DateTime dateTime, string doctorName, string description, int patientid = 0, string patientName = null, string dob = "", int doctorid = 0)
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
            this.Patientid = patientid;
            this.PatientName = patientName;
            this.Dob = dob;
            this.Doctorid = doctorid;
        }

    }
}
