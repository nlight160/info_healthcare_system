using System;
using System.Security.Cryptography;

namespace LightholderCintronHealthcareSystem.Model
{
    /// <summary>
    /// Class for hashing passwords
    /// </summary>
    public class HashSalt
    {
        /// <summary>
        /// The size
        /// </summary>
        private const int Size = 64;
        /// <summary>
        /// The iterations
        /// </summary>
        private const int Iterations = 64;
        /// <summary>
        /// Gets or sets the hash.
        /// </summary>
        /// <value>
        /// The hash.
        /// </value>
        public string Hash { get; set; }
        /// <summary>
        /// Gets or sets the salt.
        /// </summary>
        /// <value>
        /// The salt.
        /// </value>
        public string Salt { get; set; }

        /// <summary>
        /// Makes the hash salt.
        /// </summary>
        /// <param name="password">The password.</param>
        public void makeHashSalt(string password)
        {
            var saltBytes = new byte[Size];
            var provider = new RNGCryptoServiceProvider();
            provider.GetNonZeroBytes(saltBytes);
            var salt = Convert.ToBase64String(saltBytes);

            var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, Iterations);
            var hashPassword = Convert.ToBase64String(deriveBytes.GetBytes(256));
            this.Hash = hashPassword;
            this.Salt = salt;
        }

        /// <summary>
        /// Verifies the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="hash">The hash.</param>
        /// <param name="salt">The salt.</param>
        /// <returns></returns>
        public bool verifyPassword(string password, string hash, string salt)
        {
            var saltedBytes = Convert.FromBase64String(salt);
            var deriveBytes = new Rfc2898DeriveBytes(password, saltedBytes, Iterations);
            return Convert.ToBase64String(deriveBytes.GetBytes(256)) == hash;
        }

    }
}
