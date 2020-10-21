using LightholderCintronHealthcareSystem.Model;

namespace LightholderCintronHealthcareSystem.ViewModel
{
    /// <summary>
    /// View Model
    /// </summary>
    public class ViewModel
    {
        /// <summary>
        /// Gets the active user.
        /// </summary>
        /// <value>
        /// The active user.
        /// </value>
        public static User ActiveUser { get; private set; }

        /// <summary>
        /// Attempts the login.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static bool AttemptLogin(string username, string password)
        {
            DatabaseAccess db = new DatabaseAccess();
            QueryBuilder builder = new QueryBuilder();
            var information = 
                db.LoginQuery(builder.loginQuery(username, password));
            
            if (information != null)
            {
                var loginCredentials = new Nurse(information[0], information[1]);
                ActiveUser = new User(loginCredentials, int.Parse(username));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Registers the patient.
        /// </summary>
        /// <param name="lname">The lname.</param>
        /// <param name="fname">The fname.</param>
        /// <param name="dob">The dob.</param>
        /// <param name="street">The street.</param>
        /// <param name="city">The city.</param>
        /// <param name="state">The state.</param>
        /// <param name="zip">The zip.</param>
        /// <param name="phone">The phone.</param>
        public static void RegisterPatient(string lname, string fname, string dob, string street, string city, string state, string zip,
                                            string phone, Gender gender)
        {
            QueryBuilder qb = new QueryBuilder();
            DatabaseAccess db = new DatabaseAccess();
            string query = qb.addPatient(lname, fname,  dob,  street,  city,  state,  zip, phone, gender);
            db.CreatePatient(query);
        }

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        public static void Logout()
        {
            ActiveUser = null;
        }
    }
}
