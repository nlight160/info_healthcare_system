using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightholderCintronHealthcareSystem.Model
{
    /// <summary>
    /// Patient manager
    /// </summary>
    public class PatientManager
    {
        /// <summary>
        /// Gets or sets the patients.
        /// </summary>
        /// <value>
        /// The patients.
        /// </value>
        public List<Patient> Patients { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientManager"/> class.
        /// </summary>
        public PatientManager()
        {
            this.Patients = new List<Patient>();
        }

        /// <summary>
        /// Sorts the name of the patients by.
        /// </summary>
        public void SortPatientsByName()
        {
            this.Patients = this.Patients = this.Patients.OrderBy(x => x.Firstname + x.Lastname).ToList();
        }

        /// <summary>
        /// Sorts the patients by date.
        /// </summary>
        public void SortPatientsByDate()
        {
            this.Patients = this.Patients = this.Patients.OrderBy(x => x.Birthdate.year).ThenBy(x => x.Birthdate.month).ThenBy(x => x.Birthdate.day).ToList();
        }

        /// <summary>
        /// Sorts the patients by name and date.
        /// </summary>
        public void SortPatientsByNameAndDate()
        {
            this.Patients = this.Patients.OrderBy(x => x.Birthdate.year).ThenBy(x => x.Birthdate.month).ThenBy(x => x.Birthdate.day)
                .ThenBy(x => x.Firstname + x.Lastname).ToList();
        }
    }
}
