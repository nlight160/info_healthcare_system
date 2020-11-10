using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightholderCintronHealthcareSystem.Model.People
{
    public class Time
    {
        public string hour { get; set; }
        public string minute { get; set; }

        public Time(string hour, string minute)
        {
            if (string.IsNullOrEmpty(hour))
            {
                throw new ArgumentNullException(nameof(hour), "can not be null or empty");
            }
            if (string.IsNullOrEmpty(minute))
            {
                throw new ArgumentNullException(nameof(minute), "can not be null or empty");
            }

            this.hour = hour.PadLeft(2, '0');
            this.minute = minute.PadLeft(2, '0');
        }

        public override string ToString()
        {
            return this.hour + ":" + this.minute;
        }

    }
}
