using CardVault.Application.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace CardVault.Infrastructure.Helpers
{
    public class PasswordHasher : IPasswordHasher
    {
        public (byte[] passwordHash, byte[] passwordSalt) HashPassword(string plainPassword)
        {
            using var hmac = new HMACSHA512();

            var passwordSalt = hmac.Key;
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(plainPassword));

            return (passwordHash, passwordSalt);
        }

        public bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return CryptographicOperations.FixedTimeEquals(computedHash, passwordHash);
        }
    }
}