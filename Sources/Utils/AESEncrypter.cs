using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace Utils
{
    public class AESEncrypter : IEncrypter
    {
        public string Encrypt(string key, Entry entry)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (entry == null)
                throw new ArgumentNullException("Entry");

            byte[] byteKey = Encoding.UTF8.GetBytes(key);
            byte[] encrypted;

            using (Aes aes = Aes.Create())
            {
                if (aes.IV == null)
                    throw new ArgumentNullException("IV");
                aes.Key = byteKey;
                ICryptoTransform cryptic = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, cryptic, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(entry);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                    return Encoding.UTF8.GetString(encrypted);
                }
            }
        }
    }
}
