using System.Security.Cryptography;
using System.Text;

namespace CodeFusion
{
    public static class Encryptor
    {
        public static byte[] Salt { private get; set; } = Encoding.ASCII.GetBytes("SetNewSalt1234321");

        public static string EncryptStringAES(string plainText, string key)
        {
            byte[] encryptedBytes;
            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Salt;

                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using var msEncrypt = new MemoryStream();
                using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
                using (var swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                }
                encryptedBytes = msEncrypt.ToArray();
            }
            return Convert.ToBase64String(encryptedBytes);
        }
        public static string DecryptStringAES(string cipherText, string key)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            string decryptedText;
            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Salt;

                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using var msDecrypt = new MemoryStream(cipherBytes);
                using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                using var srDecrypt = new StreamReader(csDecrypt);
                decryptedText = srDecrypt.ReadToEnd();
            }
            return decryptedText;
        }
        public static void EncryptFileAES(string inputFile, string outputFile, string key)
        {
            using var aesAlg = Aes.Create();
            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            aesAlg.IV = Salt;

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using var fsInput = new FileStream(inputFile, FileMode.Open);
            using var fsOutput = new FileStream(outputFile, FileMode.Create);
            using var csEncrypt = new CryptoStream(fsOutput, encryptor, CryptoStreamMode.Write);
            fsInput.CopyTo(csEncrypt);
        }
        public static void DecryptFileAES(string inputFile, string outputFile, string key)
        {
            using var aesAlg = Aes.Create();
            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            aesAlg.IV = Salt;

            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using var fsInput = new FileStream(inputFile, FileMode.Open);
            using var fsOutput = new FileStream(outputFile, FileMode.Create);
            using var csDecrypt = new CryptoStream(fsInput, decryptor, CryptoStreamMode.Read);
            csDecrypt.CopyTo(fsOutput);
        }
        public static string EncryptStringRSA(string plainText, string publicKeyXml)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encryptedBytes;
            using (var rsa = RSA.Create())
            {
                rsa.FromXmlString(publicKeyXml);
                encryptedBytes = rsa.Encrypt(plainBytes, RSAEncryptionPadding.OaepSHA256);
            }
            return Convert.ToBase64String(encryptedBytes);
        }
        public static string DecryptStringRSA(string cipherText, string privateKeyXml)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            byte[] decryptedBytes;
            using (var rsa = RSA.Create())
            {
                rsa.FromXmlString(privateKeyXml);
                decryptedBytes = rsa.Decrypt(cipherBytes, RSAEncryptionPadding.OaepSHA256);
            }
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}