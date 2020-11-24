using System;
using LightholderCintronHealthcareSystem.Model.People;

namespace LightholderCintronHealthcareSystem.Model
{
    public class Test
    {
        /// <summary>
        /// Gets the name of the test.
        /// </summary>
        /// <value>
        /// The name of the test.
        /// </value>
        public string TestName { get; }
        /// <summary>
        /// Gets or sets the test identifier.
        /// </summary>
        /// <value>
        /// The test identifier.
        /// </value>
        public string TestId { get; set; }
        /// <summary>
        /// Gets or sets the date performed.
        /// </summary>
        /// <value>
        /// The date performed.
        /// </value>
        public Date DatePerformed { get; set; }
        /// <summary>
        /// Gets or sets the appointment identifier.
        /// </summary>
        /// <value>
        /// The appointment identifier.
        /// </value>
        public int AppointmentId { get; set; }
        /// <summary>
        /// Gets or sets the test results.
        /// </summary>
        /// <value>
        /// The test results.
        /// </value>
        public string TestResults { get; set; }
        /// <summary>
        /// Gets or sets the is normal.
        /// </summary>
        /// <value>
        /// The is normal.
        /// </value>
        public bool? IsNormal { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Test"/> class.
        /// </summary>
        /// <param name="testName">Name of the test.</param>
        /// <exception cref="ArgumentException">test name can not be null or empty - testName</exception>
        public Test(string testName)
        {
            if (string.IsNullOrEmpty(testName))
            {
                throw new ArgumentException("test name can not be null or empty", nameof(testName));
            }
            this.TestName = testName;
            this.DatePerformed = new Date("" + DateTime.Now.Year, "" + DateTime.Now.Month, "" + DateTime.Now.Day);
            this.TestResults = "";
            this.IsNormal = false;
        }
    }
}
