using System.Text;
using System.Security.Cryptography;

namespace Encryption.AESEncryption
{   /// <summary>
    /// implementation of the IEncrypter which uses AES-128 to encrypt strings
    /// </summary>
    public class AesEncrypter : IEncrypter
    {
        public byte[] Encrypt(string key, string toEncrypt)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (toEncrypt == null)
                throw new ArgumentNullException(nameof(toEncrypt));

            byte[] byteKey;

            using (var md5Hasher = MD5.Create())
            {
                byteKey = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(key));
            }

            byte[] encrypted;

            using (Aes aes = Aes.Create())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.IV = Encoding.UTF8.GetBytes("nopepperforiv01561564896")[0..(aes.BlockSize / 8)];
                aes.Key = byteKey;
                ICryptoTransform cryptic = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, cryptic, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(toEncrypt);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                    return encrypted;
                }
            }
        }

        public string EncryptionType()
        {
            return "AES";
        }
    }
}
