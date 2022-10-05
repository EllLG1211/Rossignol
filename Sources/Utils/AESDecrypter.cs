using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class AesDecrypter : IDecrypter
    {
        public string Decrypt(string key, byte[] entry)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            byte[] byteKey = new byte[16];

            using (var md5Hasher = MD5.Create())
            {
                byteKey = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(key));
            }

            byte[] ciphered = entry;
            string deciphered = "";

            using (Aes aes = Aes.Create())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.IV = Encoding.UTF8.GetBytes("nopepperforiv01561564896")[0..(aes.BlockSize / 8)];
                if (aes.IV == null)
                    throw new CryptographicUnexpectedOperationException(nameof(aes.IV));
                aes.Key = byteKey;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(ciphered))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            deciphered = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return deciphered;
        }
    }
}
