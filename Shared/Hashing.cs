using System.Security.Cryptography;
using System.Text;

namespace OrangeCoreApiTasks.Shared
{
    public static class PasswordHasher
    {
        public static (byte[], byte[]) CreatePasswordHash(string password)
        {
            //var hmac = new System.Security.Cryptography.HMACSHA256()
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            var passwordSalt = hmac.Key; // The Key property provides a randomly generated salt.
            var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return (passwordHash, passwordSalt);
        }

        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            //var hmac = new System.Security.Cryptography.HMACSHA256(storedSalt)
            using var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(storedHash);
        }
    }

}