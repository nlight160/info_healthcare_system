using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightholderCintronHealthcareSystem.Model.People;

namespace LightholderCintronHealthcareSystem.Model
{
    public class TestOrder
    {
        public List<Test> Order { get; }
        public Date DateOrdered { get; set; }

        public TestOrder(List<Test> testsToOrder)
        {
            if (testsToOrder == null || testsToOrder.Count == 0)
            {
                throw new ArgumentException("Tests to order can not be null or empty", nameof(testsToOrder));
            }
            this.Order = testsToOrder;

            this.DateOrdered = new Date("" + DateTime.Now.Year, "" + DateTime.Now.Month, "" + DateTime.Now.Day);
        }

        //Gets the amount of tests with the matching name
        //in the order
        public int getTestAmount(string testName)
        {
            int count = 0;
            foreach (var test in this.Order)
            {
                if (test.TestName.Equals(testName))
                {
                    count++;
                }
            }

            return count;
        }
    }
}
