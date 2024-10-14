using System.Security.Cryptography;
using System.IO;

namespace SecurityToolkit
{
    public class FileEncryptor
    {
        /// <summary>
        /// Encrypts a file using AES encryption.
        /// </summary>
        /// <param name="inputFilePath">The path of the file to encrypt.</param>
        /// <param name="outputFilePath">The path where the encrypted file will be saved.</param>
        /// <param name="key">The encryption key.</param>
        /// <param name="iv">The initialization vector.</param>
        public static void Encrypt(string inputFilePath, string outputFilePath, byte[] key, byte[] iv)
        {
            using (var aes = Aes.Create())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);

                using (var inputFileStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
                {
                    using (var outputFileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                    {
                        using (var cryptoStream = new CryptoStream(outputFileStream, encryptor, CryptoStreamMode.Write))
                        {
                            inputFileStream.CopyTo(cryptoStream);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Decrypts a file using AES decryption.
        /// </summary>
        /// <param name="inputFilePath">The path of the encrypted file.</param>
        /// <param name="outputFilePath">The path where the decrypted file will be saved.</param>
        /// <param name="key">The decryption key.</param>
        /// <param name="iv">The initialization vector.</param>
        public static void Decrypt(string inputFilePath, string outputFilePath, byte[] key, byte[] iv)
        {
            using (var aes = Aes.Create())
            {
                ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);

                using (var inputFileStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
                {
                    using (var outputFileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                    {
                        using (var cryptoStream = new CryptoStream(inputFileStream, decryptor, CryptoStreamMode.Read))
                        {
                            cryptoStream.CopyTo(outputFileStream);
                        }
                    }
                }
            }
        }
    }
}