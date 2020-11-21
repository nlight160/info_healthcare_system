using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightholderCintronHealthcareSystem.Model.People
{
    public class Test
    {
        public string TestName { get; }
        public string TestId { get; set; }
        public Date DatePerformed { get; set; }
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
            this.DatePerformed = new Date("" + DateTime.Now.Year, "" + DateTime.Now.Month, "" + DateTime.Now.Day);
            this.TestResults = "";
            this.IsNormal = false;
        }
    }
}
