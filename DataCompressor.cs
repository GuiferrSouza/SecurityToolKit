using System.IO;
using System.IO.Compression;

namespace SecurityToolkit
{
    public class DataCompressor
    {
        /// <summary>
        /// Compresses a byte array using GZip compression.
        /// </summary>
        /// <param name="data">The byte array to compress.</param>
        /// <returns>A compressed byte array.</returns>
        public static byte[] Compress(byte[] data)
        {
            using (var ms = new MemoryStream())
            {
                using (var gzip = new GZipStream(ms, CompressionMode.Compress))
                {
                    gzip.Write(data, 0, data.Length);
                }

                return ms.ToArray();
            }
        }

        /// <summary>
        /// Decompresses a GZip compressed byte array.
        /// </summary>
        /// <param name="data">The compressed byte array to decompress.</param>
        /// <returns>A decompressed byte array.</returns>
        public static byte[] Decompress(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var gzip = new GZipStream(ms, CompressionMode.Decompress))
                {
                    using (var decompressedStream = new MemoryStream())
                    {
                        gzip.CopyTo(decompressedStream);
                        return decompressedStream.ToArray();
                    }
                }
            }
        }
    }
}
