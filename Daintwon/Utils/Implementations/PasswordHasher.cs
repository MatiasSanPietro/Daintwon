using Daintwon.Utils.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Daintwon.Utils.Implementations
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password, out string salt)
        {
            using var rng = RandomNumberGenerator.Create();
            var saltBytes = new byte[32];
            rng.GetBytes(saltBytes);

            salt = Convert.ToBase64String(saltBytes);
            var saltedPassword = Encoding.UTF8.GetBytes(password + salt);
            var hashedBytes = SHA256.HashData(saltedPassword);
            return Convert.ToBase64String(hashedBytes);
        }

        public bool VerifyPassword(string password, string hashedPassword, string salt)
        {
            var saltedPassword = Encoding.UTF8.GetBytes(password + salt);
            var hashedBytes = SHA256.HashData(saltedPassword);
            var hashedPasswordToCheck = Convert.ToBase64String(hashedBytes);

            return hashedPasswordToCheck == hashedPassword;
        }
    }
}
