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
        public string TestCode { get; }
        public Date DatePerformed { get; set; }
        public string TestResults { get; set; }
        public bool IsNormal { get; set; }

        public Test(string testName, string testCode)
        {
            if (string.IsNullOrEmpty(testName))
            {
                throw new ArgumentException("test name can not be null or empty", nameof(testName));
            }
            this.TestName = testName;

            if (string.IsNullOrEmpty(testCode))
            {
                throw new ArgumentException("test code can not be null or empty", nameof(testCode));
            }
            this.TestCode = testCode;

            this.DatePerformed = new Date("" + DateTime.Now.Year, "" + DateTime.Now.Month, "" + DateTime.Now.Day);
            this.TestResults = "";
            this.IsNormal = true;
        }
    }
}
