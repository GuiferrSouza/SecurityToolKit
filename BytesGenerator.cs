using System;
using System.Security.Cryptography;

namespace SecurityToolkit
{
    public class BytesGenerator
    {
        /// <summary>
        /// Generates an array of random bytes with the specified size.
        /// </summary>
        /// <param name="size">The size of the byte array to generate.</param>
        /// <returns>An array of random bytes.</returns>
        public static byte[] RandomBytes(int size)
        {
            byte[] bytes = new byte[size];
            new Random().NextBytes(bytes);
            return bytes;
        }

        /// <summary>
        /// Derives a byte array from a password and a salt using the Rfc2898 algorithm.
        /// </summary>
        /// <param name="password">The password used to derive the bytes.</param>
        /// <param name="salt">The salt used in the derivation process.</param>
        /// <param name="cb">The number of bytes to generate.</param>
        /// <returns>A byte array derived from the password and salt.</returns>
        public static byte[] BytesFromPassword(string password, byte[] salt, int cb)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt))
            {
                return deriveBytes.GetBytes(cb);
            }
        }

        /// <summary>
        /// Converts a byte array to a hexadecimal string.
        /// </summary>
        /// <param name="byteArray">The byte array to convert.</param>
        /// <returns>A string representing the byte array in hexadecimal format.</returns>
        public static string BytesToString(byte[] byteArray)
        {
            return BitConverter.ToString(byteArray);
        }

        /// <summary>
        /// Converts a hexadecimal string to a byte array.
        /// </summary>
        /// <param name="byteString">The hexadecimal string to convert.</param>
        /// <returns>A byte array corresponding to the hexadecimal string.</returns>
        public static byte[] StringToBytes(string byteString)
        {
            string[] byteValues = byteString.Split('-');
            byte[] byteArray = new byte[byteValues.Length];

            for (int i = 0; i < byteValues.Length; i++)
            {
                byteArray[i] = Convert.ToByte(byteValues[i], 16);
            }

            return byteArray;
        }
    }
}
