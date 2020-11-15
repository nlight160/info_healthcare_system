using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightholderCintronHealthcareSystem.Model.People;

namespace LightholderCintronHealthcareSystem.Model
{
    public class TestOrder
    {
        public ObservableCollection<Test> Order { get; }
        public Date DateOrdered { get; set; }

        public TestOrder(ObservableCollection<Test> testsToOrder)
        {
            if (testsToOrder == null)
            {
                throw new ArgumentException("Tests to order can not be null", nameof(testsToOrder));
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
