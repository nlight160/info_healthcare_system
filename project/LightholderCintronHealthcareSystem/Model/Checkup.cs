using System;

namespace LightholderCintronHealthcareSystem.Model
{
    /// <summary>
    /// Checkup data class
    /// </summary>
    public class Checkup
    {
        /// <summary>
        /// Gets or sets the checkup identifier.
        /// </summary>
        /// <value>
        /// The checkup identifier.
        /// </value>
        public int CheckupId { get; set; }
        /// <summary>
        /// Gets or sets the appointment identifier.
        /// </summary>
        /// <value>
        /// The appointment identifier.
        /// </value>
        public int AppointmentId { get; set; }
        /// <summary>
        /// Gets or sets the systolic.
        /// </summary>
        /// <value>
        /// The systolic.
        /// </value>
        public int Systolic { get; set; }
        /// <summary>
        /// Gets or sets the diastolic.
        /// </summary>
        /// <value>
        /// The diastolic.
        /// </value>
        public int Diastolic { get; set; }
        /// <summary>
        /// Gets or sets the temperature.
        /// </summary>
        /// <value>
        /// The temperature.
        /// </value>
        public decimal Temperature { get; set; }
        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        public decimal Weight { get; set; }
        /// <summary>
        /// Gets or sets the pulse.
        /// </summary>
        /// <value>
        /// The pulse.
        /// </value>
        public int Pulse { get; set; }
        /// <summary>
        /// Gets or sets the diagnosis.
        /// </summary>
        /// <value>
        /// The diagnosis.
        /// </value>
        public string Diagnosis { get; set; }
        /// <summary>
        /// Gets or sets the final diagnosis.
        /// </summary>
        /// <value>
        /// The diagnosis.
        /// </value>
        public string FinalDiagnosis { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Checkup"/> class.
        /// </summary>
        /// <param name="checkupId">The checkup identifier.</param>
        /// <param name="appointmentId">The appointment identifier.</param>
        /// <param name="systolic">The systolic.</param>
        /// <param name="diastolic">The diastolic.</param>
        /// <param name="temperature">The temperature.</param>
        /// <param name="weight">The weight.</param>
        /// <param name="pulse">The pulse.</param>
        /// <param name="diagnosis">The diagnosis.</param>
        /// <param name="finalDiagnosis"></param>
        /// <exception cref="ArgumentException">
        /// appointmentId must be greater than 0
        /// or
        /// Systolic must be greater than 0
        /// or
        /// Diastolic must be greater than 0
        /// or
        /// temperature must be greater than 0
        /// or
        /// weight must be greater than 0
        /// or
        /// pulse must be greater than 0
        /// </exception>
        /// <exception cref="ArgumentNullException">diagnosis - diagnosis can not be null</exception>
        public Checkup(int checkupId, int appointmentId, int systolic, int diastolic, decimal temperature,
            decimal weight, int pulse, string diagnosis, string finalDiagnosis)
        {
            if (appointmentId < 0)
            {
                throw new ArgumentException("appointmentId must be greater than 0");
            }

            this.AppointmentId = appointmentId;
            if (systolic < 0)
            {
                throw new ArgumentException("Systolic must be greater than 0");
            }

            this.Systolic = systolic;
            if (diastolic < 0)
            {
                throw new ArgumentException("Diastolic must be greater than 0");
            }

            this.Diastolic = diastolic;
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

            this.Pulse = pulse;
            this.CheckupId = checkupId;
            this.Diagnosis = diagnosis ?? throw new ArgumentNullException(nameof(diagnosis), "diagnosis can not be null");
            this.FinalDiagnosis = finalDiagnosis ?? throw new ArgumentNullException(nameof(finalDiagnosis), "final diagnosis can not be null");
        }
    }
}
