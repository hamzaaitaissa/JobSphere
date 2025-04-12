using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace JobSphere.Services
{
    public class Pbkdf2PasswordHasherService : IPasswordHasherService
    {
        private const int SaltSize = 16; // 128 bit
        private const int KeySize = 32; // 256 bit
        private const int Iterations = 10000;
        private static readonly KeyDerivationPrf Prf = KeyDerivationPrf.HMACSHA256;

        public (string Hash, byte[] Salt) HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));

            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(password, salt, Prf, Iterations, KeySize));
            return (hashed, salt);
        }

        public bool VerifyPassword(string passwordAttempt, string storedHash, byte[] storedSalt)
        {
            if (string.IsNullOrEmpty(passwordAttempt) || string.IsNullOrEmpty(storedHash) || storedSalt == null || storedSalt.Length != SaltSize) return false;

            try
            {
                string hashOfAttempt = Convert.ToBase64String(KeyDerivation.Pbkdf2(passwordAttempt, storedSalt, Prf, Iterations, KeySize));
                byte[] hashBytesOfAttempt = Convert.FromBase64String(hashOfAttempt);
                byte[] storedHashBytes = Convert.FromBase64String(storedHash);

                if (hashBytesOfAttempt.Length != storedHashBytes.Length) return false;

                return CryptographicOperations.FixedTimeEquals(hashBytesOfAttempt, storedHashBytes);
            }
            catch (FormatException) { return false; }
        }
    }
}
