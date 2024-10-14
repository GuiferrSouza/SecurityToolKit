# SecurityToolkit

SecurityToolkit is a C# library providing various utilities for cryptographic operations, including byte generation, data compression, encryption, file encryption, and hashing.

## Classes

### BytesGenerator

- **RandomBytes(int size)**: Generates an array of random bytes with the specified size.
- **BytesFromPassword(string password, byte[] salt, int cb)**: Derives a byte array from a password and a salt using the Rfc2898 algorithm.
- **BytesToString(byte[] byteArray)**: Converts a byte array to a hexadecimal string.
- **StringToBytes(string byteString)**: Converts a hexadecimal string to a byte array.

### DataCompressor

- **Compress(byte[] data)**: Compresses a byte array using GZip compression.
- **Decompress(byte[] data)**: Decompresses a GZip compressed byte array.

### DataEncryptor

- **Encrypt<T>(T obj, byte[] key, byte[] iv)**: Encrypts an object using AES encryption.
- **Decrypt<T>(byte[] bytes, byte[] key, byte[] iv)**: Decrypts a byte array to an object using AES decryption.

### FileEncryptor

- **Encrypt(string inputFilePath, string outputFilePath, byte[] key, byte[] iv)**: Encrypts a file using AES encryption.
- **Decrypt(string inputFilePath, string outputFilePath, byte[] key, byte[] iv)**: Decrypts a file using AES decryption.

### HashComputer

- **Compute(byte[] data)**: Computes the SHA-256 hash of the given data.
- **Verify(byte[] data, byte[] hash)**: Verifies that the given data matches the provided hash.

## Installation

To install SecurityToolkit, clone the repository and include the source files in your project.

## Usage

### BytesGenerator

```cs
using SecurityToolkit;

// Generate random bytes
byte[] randomBytes = BytesGenerator.RandomBytes(16);

// Derive bytes from a password
string password = "securepassword";
byte[] salt = BytesGenerator.RandomBytes(8);
byte[] derivedBytes = BytesGenerator.BytesFromPassword(password, salt, 16);

// Convert bytes to string
string hexString = BytesGenerator.BytesToString(randomBytes);

// Convert string to bytes
byte[] bytesFromHex = BytesGenerator.StringToBytes(hexString);

```

### DataCompressor

```cs
using SecurityToolkit;

// Compress data
byte[] data = System.Text.Encoding.UTF8.GetBytes("This is some data to compress");
byte[] compressedData = DataCompressor.Compress(data);

// Decompress data
byte[] decompressedData = DataCompressor.Decompress(compressedData);
string decompressedString = System.Text.Encoding.UTF8.GetString(decompressedData);
```

### DataEncryptor

```cs
using SecurityToolkit;
using System.Security.Cryptography;

// Encrypt an object
string originalData = "Sensitive data";
byte[] key = BytesGenerator.RandomBytes(32); // AES-256 key
byte[] iv = BytesGenerator.RandomBytes(16);  // AES IV
byte[] encryptedData = DataEncryptor.Encrypt(originalData, key, iv);

// Decrypt the object
string decryptedData = DataEncryptor.Decrypt<string>(encryptedData, key, iv);

```

### FileEncryptor

```cs
using SecurityToolkit;
using System.Security.Cryptography;

// Encrypt a file
string inputFilePath = "path/to/input/file.txt";
string outputFilePath = "path/to/output/encryptedFile.enc";
byte[] key = BytesGenerator.RandomBytes(32); // AES-256 key
byte[] iv = BytesGenerator.RandomBytes(16);  // AES IV
FileEncryptor.Encrypt(inputFilePath, outputFilePath, key, iv);

// Decrypt a file
string decryptedFilePath = "path/to/output/decryptedFile.txt";
FileEncryptor.Decrypt(outputFilePath, decryptedFilePath, key, iv);
```

### HashComputer

```cs
using SecurityToolkit;

// Compute hash
byte[] data = System.Text.Encoding.UTF8.GetBytes("Data to hash");
byte[] hash = HashComputer.Compute(data);

// Verify hash
bool isMatch = HashComputer.Verify(data, hash);
```

## License
This project is licensed under the MIT License - see the LICENSE file for details.