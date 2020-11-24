using LightholderCintronHealthcareSystem.Model.People;
using System;
using System.Collections.ObjectModel;

namespace LightholderCintronHealthcareSystem.Model
{
    public class TestOrder
    {
        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public ObservableCollection<Test> Order { get; }

        /// <summary>
        /// Gets or sets the date ordered.
        /// </summary>
        /// <value>
        /// The date ordered.
        /// </value>
        public Date DateOrdered { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestOrder"/> class.
        /// </summary>
        /// <param name="testsToOrder">The tests to order.</param>
        /// <exception cref="ArgumentException">Tests to order can not be null - testsToOrder</exception>
        public TestOrder(ObservableCollection<Test> testsToOrder)
        {
            this.Order = testsToOrder ?? throw new ArgumentException("Tests to order can not be null", nameof(testsToOrder));

            this.DateOrdered = new Date("" + DateTime.Now.Year, "" + DateTime.Now.Month, "" + DateTime.Now.Day);
        }

    }
}
