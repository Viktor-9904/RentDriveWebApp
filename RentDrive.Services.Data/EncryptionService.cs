using RentDrive.Services.Data.Interfaces;
using System.Security.Cryptography;

namespace RentDrive.Services.Data
{
    public class EncryptionService : IEncryptionService
    {
        private readonly byte[] _key;

        public EncryptionService(string base64Key)
        {
            if (string.IsNullOrWhiteSpace(base64Key))
                throw new ArgumentNullException(nameof(base64Key));

            _key = Convert.FromBase64String(base64Key);

            if (_key.Length != 32)
                throw new ArgumentException("The encryption key must be 256 bits (32 bytes).");
        }

        public string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return plainText;

            using var aes = Aes.Create();
            aes.Key = _key;
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }

            var ivAndCipher = new byte[aes.IV.Length + ms.ToArray().Length];
            Buffer.BlockCopy(aes.IV, 0, ivAndCipher, 0, aes.IV.Length);
            Buffer.BlockCopy(ms.ToArray(), 0, ivAndCipher, aes.IV.Length, ms.ToArray().Length);

            return Convert.ToBase64String(ivAndCipher);
        }

        public string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) return cipherText;

            var fullCipher = Convert.FromBase64String(cipherText);

            using var aes = Aes.Create();
            aes.Key = _key;

            var iv = new byte[aes.BlockSize / 8];
            var cipher = new byte[fullCipher.Length - iv.Length];
            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, cipher.Length);

            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream(cipher);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
    }
}
