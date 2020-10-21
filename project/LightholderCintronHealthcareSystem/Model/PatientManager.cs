using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightholderCintronHealthcareSystem.Model
{
    public class PatientManager
    {
        public List<Patient> Patients { get; private set; }

        public PatientManager()
        {
            this.Patients = new List<Patient>();
        }

        public void SortPatientsByName()
        {
            this.Patients = this.Patients = this.Patients.OrderBy(x => x.Firstname + x.Lastname).ToList();
        }

        public void SortPatientsByDate()
        {
            this.Patients = this.Patients = this.Patients.OrderBy(x => x.Birthdate).ToList();
        }

        public void SortPatientsByNameAndDate()
        {
            this.Patients = this.Patients.OrderBy(x => x.Birthdate).ThenBy(x => x.Firstname + x.Lastname).ToList();
        }
    }
}
