using System;
using LightholderCintronHealthcareSystem.Model.People;

namespace LightholderCintronHealthcareSystem.Model
{
    public class Test
    {
        public string TestName { get; }
        public string TestId { get; set; }
        public DateTime DatePerformed { get; set; }
        public int AppointmentId { get; set; }
        public string TestResults { get; set; }
        public bool? IsNormal { get; set; }

        public Test(string testName)
        {
            if (string.IsNullOrEmpty(testName))
            {
                throw new ArgumentException("test name can not be null or empty", nameof(testName));
            }
            this.TestName = testName;
            this.DatePerformed = DateTime.MinValue;
            this.TestResults = "";
            this.IsNormal = false;
        }
    }
}
