using Encryption;
using Encryption.AESEncryption;
using Xunit;

namespace Utils_Tests
{
    public class EncryptDecrypt_Tests
    {

        [Theory]
        [InlineData("mysupersecretpassword", "leMasterPassword", false)]
        [InlineData("averylongpasswordforaesencryptionwithmorethan16bytes", "leMasterPassword", false)]
        [InlineData("shortpass", "leMasterPassword", false)]
        [InlineData("mysupersecretpassword", "smallpass", false)]
        [InlineData("mysupersecretpassword", "AveryLongPasswordWith.Lots.of.bytes.forasecurewebsite", false)]
        [InlineData(null, "leMasterPassword", true)]
        [InlineData("monmdpmaisjedonnepasdecle", null, true)]
        [InlineData(null, null, true)]
        [InlineData("", "leMasterPassword", true)]
        [InlineData("mysupersecretpassword", "", true)]
        [InlineData("mysupersecretpassword", "password$withspécialchâracters:)", false)]
        public void TryFailEncrypt(string toEncrypt, string key, bool shouldThrow)
        {
            try
            {
                using (IEncrypter encrypter = new AesEncrypter())
                {
                    byte[] crypted = encrypter.Encrypt(key, toEncrypt);
                    Assert.False(shouldThrow);
                }
            }
            catch
            {
                Assert.True(shouldThrow);
            }
        }

        [Theory]
        [InlineData("mysupersecretpassword", "leMasterPassword", false)]
        [InlineData("averylongpasswordforaesencryptionwithmorethan16bytes", "leMasterPassword", false)]
        [InlineData("shortpass", "leMasterPassword", false)]
        [InlineData("mysupersecretpassword", "smallpass", false)]
        [InlineData("mysupersecretpassword", "AveryLongPasswordWith.Lots.of.bytes.forasecurewebsite", false)]
        [InlineData(null, "leMasterPassword", true)]
        [InlineData("monmdpmaisjedonnepasdecle", null, true)]
        [InlineData(null, null, true)]
        [InlineData("", "leMasterPassword", true)]
        [InlineData("mysupersecretpassword", "", true)]
        [InlineData("mysupersecretpassword", "password$withspécialchâracters:)", false)]
        public void TryFailEncryptDecrypt(string toEncrypt, string key, bool shouldThrow)
        {
            try
            {
                IEncrypter encrypter = new AesEncrypter();
                byte[] crypted = encrypter.Encrypt(key, toEncrypt);
                using(IDecrypter decrypter = new AesDecrypter())
                {
                    string deciphered = decrypter.Decrypt(key, crypted);
                    Assert.Equal(toEncrypt, deciphered);
                    Assert.False(shouldThrow);
                }
            }
            catch
            {
                Assert.True(shouldThrow);
            }
        }


        [Theory]
        [InlineData("mysupersecretpassword", "password$withspécialchâracters:)", "password$withspécialchâracters:)", false,false)]
        [InlineData("mysupersecretpassword", "password$withspécialchâracters:)", "notthesamepassword°-°", true, false)]
        [InlineData("mysupersecretpassword", "password$withspécialchâracters:)", null, true, false)]
        [InlineData("mysupersecretpassword", "password$withspécialchâracters:)", "password$withspécialchâracters:)", true, true)]
        public void TryWrongPassword(string toEncrypt, string encryptionKey, string decryptionKey, bool shouldThrow, bool nullEntry)
        {
            try
            {
                IEncrypter encrypter = new AesEncrypter();
                byte[] crypted = encrypter.Encrypt(encryptionKey, toEncrypt);
                if (nullEntry)
                    crypted = null;
                IDecrypter decrypter = new AesDecrypter();
                string deciphered = decrypter.Decrypt(decryptionKey, crypted);
                Assert.Equal(toEncrypt, deciphered);
                Assert.False(shouldThrow);
            }
            catch
            {
                Assert.True(shouldThrow);
            }
        }

        [Fact]
        public void AESEncryptionType_Test()
        {
            IEncrypter encrypter = new AesEncrypter();
            Assert.Equal("AES", encrypter.EncryptionType());
        }
    }
}