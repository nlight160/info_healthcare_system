using LightholderCintronHealthcareSystem.Model.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightholderCintronHealthcareSystem.Model
{
    public class AppointmentDataGrid
    {
        public int appointmentid { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string doctorName { get; set; }
        public string description { get; set; }

        public AppointmentDataGrid(int appointmentid, DateTime dateTime, string doctorName, string description)
        {

            if (string.IsNullOrEmpty(doctorName))
            {
                throw new ArgumentNullException(nameof(doctorName), "can not be null or empty");
            }

            this.appointmentid = appointmentid;
            var date = new Date(dateTime.Year.ToString(), dateTime.Month.ToString(), dateTime.Day.ToString());
            this.date = date.ToString();
            var time = new Time(dateTime.Hour.ToString(), dateTime.Minute.ToString());
            this.time = time.ToString();
            this.doctorName = doctorName;
            this.description = description;
        }
    }
}
