

namespace LightholderCintronHealthcareSystem.Model
{
    /// <summary>
    /// Builds the query's that will be sent to the database.
    /// </summary>
    public class QueryBuilder
    {
        /// <summary>
        /// Builds the query to add a patient, first adds a person then a patient.
        /// </summary>
        /// <param name="lname">The lname.</param>
        /// <param name="fname">The fname.</param>
        /// <param name="dob">The dob.</param>
        /// <param name="street">The street.</param>
        /// <param name="city">The city.</param>
        /// <param name="state">The state.</param>
        /// <param name="zip">The zip.</param>
        /// <param name="phone">The phone.</param>
        /// <returns></returns>
        public string addPatient(string lname, string fname, string dob, string street, string city, string state, string zip,
            string phone, Gender gender)
        {
            var createPerson =
                $"INSERT INTO `person` (`personid`, `lname`, `fname`, `dob`, `street`, `city`, `state`, `zip`, `phone`) VALUES (null, \"{lname}\", \"{fname}\", \"{dob}\", \"{street}\", \"{city}\", \"{state}\", \"{zip}\", \"{phone}\");";
            var createPatient =
                $"INSERT INTO patient (patientid, personid) SELECT null, p.personid FROM person p WHERE p.lname = \"{lname}\" AND p.fname = \"{fname}\" AND p.dob = \"{dob}\" AND p.phone = \"{phone}\";";
            return createPerson + createPatient;
        }

        /// <summary>
        /// Gets the gender as string.
        /// </summary>
        /// <param name="gender">The gender.</param>
        /// <returns></returns>
        private string getGenderAsString(Gender gender)
        {
            return gender == 0 ? "Female" : "Male";
        }

        /// <summary>
        /// Builds the query for logging in.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public string loginQuery(string username, string password)
        {
            return $"SELECT p.fname, p.lname FROM person p, nurse n WHERE n.personid = p.personid AND n.nurseid = {username} AND n.password = '{password}';";
        }
    }
}
