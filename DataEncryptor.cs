using System.Security.Cryptography;
using System.IO;
using Newtonsoft.Json;

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
        /// <param name="settings">The JSON serializer settings.</param>
        /// <returns>A byte array containing the encrypted data.</returns>
        public static byte[] Encrypt<T>(T obj, byte[] key, byte[] iv, JsonSerializerSettings settings = null)
        {
            byte[] data = SerializeObject(obj, settings);

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
        /// Serializes an object to a byte array using JSON.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="settings">The JSON serializer settings.</param>
        /// <returns>A byte array containing the serialized object.</returns>
        private static byte[] SerializeObject<T>(T obj, JsonSerializerSettings settings = null)
        {
            string json = JsonConvert.SerializeObject(obj, settings);
            return System.Text.Encoding.UTF8.GetBytes(json);
        }

        /// <summary>
        /// Decrypts a byte array to an object using AES decryption.
        /// </summary>
        /// <typeparam name="T">The type of the object to decrypt.</typeparam>
        /// <param name="bytes">The byte array containing the encrypted data.</param>
        /// <param name="key">The decryption key.</param>
        /// <param name="iv">The initialization vector.</param>
        /// <param name="settings">The JSON serializer settings.</param>
        /// <returns>The decrypted object.</returns>
        public static T Decrypt<T>(byte[] bytes, byte[] key, byte[] iv, JsonSerializerSettings settings = null)
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
                            return DeserializeObject<T>(data, settings);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Deserializes a byte array to an object using JSON.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize.</typeparam>
        /// <param name="data">The byte array containing the serialized object.</param>
        /// <param name="settings">The JSON serializer settings.</param>
        /// <returns>The deserialized object.</returns>
        private static T DeserializeObject<T>(byte[] data, JsonSerializerSettings settings = null)
        {
            string json = System.Text.Encoding.UTF8.GetString(data);
            return JsonConvert.DeserializeObject<T>(json, settings);
        }
    }
}