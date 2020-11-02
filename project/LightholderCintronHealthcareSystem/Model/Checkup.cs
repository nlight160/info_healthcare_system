using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LightholderCintronHealthcareSystem.Model
{
    class Checkup
    {
        public int? CheckupId { get; set; }
        public int AppointmentId { get; set; }
        public int Systolic { get; set; }
        public int Diastolic { get; set; }
        public decimal Temperature { get; set; }
        public decimal Weight { get; set; }
        public int Pulse { get; set; }
        public string Diagnosis { get; set; }

        public Checkup(int? checkupId, int appointmentId, int systolic, int diastolic, decimal temperature,
            decimal weight, int pulse, string diagnosis)
        {
            if (appointmentId < 0)
            {
                throw new ArgumentException("appointmentId must be greater than 0");
            }

            AppointmentId = appointmentId;
            if (systolic < 0)
            {
                throw new ArgumentException("Systolic must be greater than 0");
            }

            Systolic = systolic;
            if (diastolic < 0)
            {
                throw new ArgumentException("Diastolic must be greater than 0");
            }

            Diastolic = diastolic;
            if (temperature < 0)
            {
                throw new ArgumentException("temperature must be greater than 0");
            }

            Temperature = temperature;
            if (weight < 0)
            {
                throw new ArgumentException("weight must be greater than 0");
            }

            Weight = weight;
            if (pulse < 0)
            {
                throw new ArgumentException("pulse must be greater than 0");
            }

            Pulse = pulse;

            Diagnosis = diagnosis ?? throw new ArgumentNullException(nameof(diagnosis), "diagnosis can not be null");
        }
    }
}
