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
    public class AesEncrypter : IEncrypter
    {
        public byte[] Encrypt(string key, Entry entry)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            byte[] byteKey = Encoding.UTF8.GetBytes(key);
            byte[] encrypted;

            using (Aes aes = Aes.Create())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                if (aes.IV == null)
                    throw new ArgumentNullException(nameof(aes.IV));
                aes.Key = byteKey;
                aes.IV = Encoding.UTF8.GetBytes("nopepperforiv01561564896")[0..(aes.BlockSize/8)];
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
                    return encrypted;
                }
            }
        }
    }
}
