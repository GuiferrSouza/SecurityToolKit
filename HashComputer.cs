using System.Linq;
using System.Security.Cryptography;

namespace SecurityToolkit
{
    public class HashComputer
    {
        /// <summary>
        /// Computes the SHA-256 hash of the given data.
        /// </summary>
        /// <param name="data">The data to hash.</param>
        /// <returns>A byte array containing the SHA-256 hash of the data.</returns>
        public static byte[] Compute(byte[] data)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(data);
            }
        }

        /// <summary>
        /// Verifies that the given data matches the provided hash.
        /// </summary>
        /// <param name="data">The data to verify.</param>
        /// <param name="hash">The hash to compare against.</param>
        /// <returns>True if the computed hash matches the provided hash; otherwise, false.</returns>
        public static bool Verify(byte[] data, byte[] hash)
        {
            byte[] computedHash = Compute(data);
            return computedHash.SequenceEqual(hash);
        }
    }
}