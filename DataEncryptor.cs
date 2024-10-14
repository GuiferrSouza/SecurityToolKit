using System.Security.Cryptography;
using System.Text;
using System.IO;

using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;

namespace SecurityToolkit
{
    public class DataEncryptor
    {
        /// <summary>
        /// Encrypts an object using AES encryption.
        /// </summary>
        /// <typeparam name="T">The type of the object to encrypt.</typeparam>
        /// <param name="obj">The object to encrypt.</param>
        /// <param name="key">The encryption key.</param>
        /// <param name="iv">The initialization vector.</param>
        /// <returns>A byte array containing the encrypted data.</returns>
        public static byte[] Encrypt<T>(T obj, byte[] key, byte[] iv)
        {
            byte[] data = SerializeObject(obj);

            using (var aes = Aes.Create())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                    }

                    return ms.ToArray();
                }
            }
        }

        /// <summary>
        /// Serializes an object to a byte array.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>A byte array containing the serialized object.</returns>
        private static byte[] SerializeObject<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Decrypts a byte array to an object using AES decryption.
        /// </summary>
        /// <typeparam name="T">The type of the object to decrypt.</typeparam>
        /// <param name="bytes">The byte array containing the encrypted data.</param>
        /// <param name="key">The decryption key.</param>
        /// <param name="iv">The initialization vector.</param>
        /// <returns>The decrypted object.</returns>
        public static T Decrypt<T>(byte[] bytes, byte[] key, byte[] iv)
        {
            using (var aes = Aes.Create())
            {
                ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);

                using (var ms = new MemoryStream(bytes))
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (var outputMs = new MemoryStream())
                        {
                            cs.CopyTo(outputMs);
                            byte[] data = outputMs.ToArray();
                            return DeserializeObject<T>(data);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Deserializes a byte array to an object.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize.</typeparam>
        /// <param name="data">The byte array containing the serialized object.</param>
        /// <returns>The deserialized object.</returns>
        private static T DeserializeObject<T>(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                var formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(ms);
            }
        }
    }
}
