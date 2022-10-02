using Model;
using Utils;
using Xunit;

namespace Utils_Tests
{
    public class EncryptDecrypt_Tests
    {
        [Fact]
        public void TryEncryptDecrypt()
        {
            Entry myEntry = new ProprietaryEntry("kanken","motDePasseSuperSecurisé","codefirst");
            IEncrypter encrypter = new AesEncrypter();
            string key = "leMasterPassword";
            byte[] crypted = encrypter.Encrypt(key, myEntry);

            IDecrypter decrypter = new AesDecrypter();
            string deciphered = decrypter.Decrypt(key, crypted);
        }
    }
}