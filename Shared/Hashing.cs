using System.Security.Cryptography;
using System.Text;

namespace OrangeCoreApiTasks.Shared
{
    public class Hashing
    {
        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256 object
            using var sha256Hash = SHA512.Create();
            // ComputeHash - returns byte array
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a string
            var builder = new StringBuilder();
            foreach (var bt in bytes)
            {
                builder.Append(bt.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
