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
            IEncrypter encrypter = new AESEncrypter();
            string crypted = encrypter.Encrypt("leMasterPassword",myEntry);
        }
    }
}