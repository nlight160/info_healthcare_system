using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightholderCintronHealthcareSystem.Model
{
    public class User
    {

        public Nurse NurseInfo { get; }

        public User(Nurse person)
        {
            this.NurseInfo = person ?? throw new ArgumentNullException(nameof(person), "can not be null");
        }


    }
}
