using System.Security.Cryptography;


namespace NyanSpecialty.Assistance.Web.Data.Utility
{
    public class PasswordManagerUtility
    {
        public string Hash { get; set; }
        public string Salt { get; set; }

        public static PasswordManagerUtility GenerateSaltedHash(string password)
        {
            var saltBytes = new byte[64];

            // Using RandomNumberGenerator to generate salt bytes
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetNonZeroBytes(saltBytes);
            }

            var salt = Convert.ToBase64String(saltBytes);

            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);

            var hashPassword = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            PasswordManagerUtility hashSalt = new PasswordManagerUtility { Hash = hashPassword, Salt = salt };

            return hashSalt;
        }
        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000);

            // Generate the hash to compare with the stored hash
            var enteredHash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            // Compare the computed hash with the stored hash
            return enteredHash == storedHash;
        }

    }
}
