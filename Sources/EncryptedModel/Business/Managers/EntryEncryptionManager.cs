using Encryption;
using Encryption.AESEncryption;
using Model.Business.Entries;
using EncryptedModel.Business.Entries;
using Model.Business.Users.UserDataUtilities;

namespace EncryptedModel.Business.Managers
{
    public static class EntryEncryptionManager
    {
        /// <summary>
        /// Decrypts an EncryptedSharedEntry to a SharedEntry
        /// </summary>
        /// <param name="encrypted">The EncryptedSharedEntry to decrypt</param>
        /// <param name="password">The password that was used to Encrypt the given EncryptedSharedEntry</param>
        /// <returns>the decryptes SharedEntry</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="CryptographicException"></exception>
        public static SharedEntry EncryptedToSharedEntry(EncryptedSharedEntry encrypted, string password)
        {
            if (encrypted == null) throw new ArgumentNullException(nameof(encrypted));
            if(password == null) throw new ArgumentNullException(nameof(password));

            IDecrypter decrypter = new AesDecrypter();

            if(encrypted.encryptionType != decrypter.EncryptionType()) throw new ArgumentException("Unexcpected Encryption type for this decrypter, excpected "+decrypter.EncryptionType()+
                                                                                                   " got " + encrypted.encryptionType);

            SharedEntry toReturn = new SharedEntry(new Guid(encrypted.Uid),
                                                   decrypter.Decrypt(password,encrypted.EncryptedLogin),
                                                   decrypter.Decrypt(password, encrypted.EncryptedPassword),
                                                   decrypter.Decrypt(password, encrypted.EncryptedApp),
                                                   encrypted.EncryptedNote.Length <= 0 ? string.Empty : decrypter.Decrypt(password, encrypted.EncryptedNote));

            return toReturn;
        }

        /// <summary>
        /// Encrypts a sharedEntry to an EncryptedSharedEntry
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="CryptographicException"></exception>
        public static EncryptedSharedEntry SharedToEncryptedEntry(SharedEntry toEncrypt, string password)
        {
            if (toEncrypt == null) throw new ArgumentNullException(nameof(toEncrypt));
            if (password == null) throw new ArgumentNullException(nameof(password));

            IEncrypter encrypter = new AesEncrypter();

            EncryptedSharedEntry toReturn = new EncryptedSharedEntry(encrypter.EncryptionType(), toEncrypt.Uid.ToString(),
                                                                                         encrypter.Encrypt(password, toEncrypt.Login),
                                                                                         encrypter.Encrypt(password, toEncrypt.Password),
                                                                                         encrypter.Encrypt(password, toEncrypt.App),
                                                                                         string.IsNullOrEmpty(toEncrypt.Note) ? Array.Empty<byte>() : encrypter.Encrypt(password, toEncrypt.Note));
            return toReturn;
        }

        /// <summary>
        /// Decrypts an EncryptedProprietaryEntry to a ProprietaryEntry
        /// </summary>
        /// <param name="encrypted">Entry to decrypt</param>
        /// <param name="password">password to use for decryption</param>
        /// <returns>ProprietaryEntry with the entry's data</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static ProprietaryEntry EncryptedToProprietaryEntry(EncryptedProprietaryEntry encrypted, string password)
        {
            if (encrypted == null) throw new ArgumentNullException(nameof(encrypted));
            if (password == null) throw new ArgumentNullException(nameof(password));

            IDecrypter decrypter = new AesDecrypter();

            if (encrypted.encryptionType != decrypter.EncryptionType()) throw new ArgumentException("Unexcpected Encryption type for this decrypter, excpected " + decrypter.EncryptionType() +
                                                                                                    " got " + encrypted.encryptionType);

            ProprietaryEntry toReturn = new ProprietaryEntry( new Guid(encrypted.Uid),
                                                   decrypter.Decrypt(password, encrypted.EncryptedLogin),
                                                   decrypter.Decrypt(password, encrypted.EncryptedPassword),
                                                   decrypter.Decrypt(password, encrypted.EncryptedApp),
                                                   encrypted.EncryptedNote.Length <= 0 ? string.Empty : decrypter.Decrypt(password, encrypted.EncryptedNote));
                
            if (encrypted.EncryptedSharedWith.Length > 0)
                decrypter.Decrypt(password, encrypted.EncryptedSharedWith).ToMailedUserList().ForEach(user => toReturn.ShareToUser(user));

            return toReturn;
        }

        /// <summary>
        /// Encrypts a ProprietaryEntry to an EncryptedProprietaryEntry
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="CryptographicException"></exception>
        public static EncryptedProprietaryEntry ProprietaryToEncryptedEntry(ProprietaryEntry toEncrypt, string password)
        {
            if (toEncrypt == null) throw new ArgumentNullException(nameof(toEncrypt));
            if (password == null) throw new ArgumentNullException(nameof(password));

            IEncrypter encrypter = new AesEncrypter();

            EncryptedProprietaryEntry toReturn = new EncryptedProprietaryEntry(encrypter.EncryptionType(), toEncrypt.Uid.ToString(), //TODO: fix placeholder for UID
                                                                                         encrypter.Encrypt(password, toEncrypt.Login),
                                                                                         encrypter.Encrypt(password, toEncrypt.Password),
                                                                                         encrypter.Encrypt(password, toEncrypt.App),
                                                                                         string.IsNullOrEmpty(toEncrypt.Note) ? Array.Empty<byte>() : encrypter.Encrypt(password, toEncrypt.Note),
                                                                                         encrypter.Encrypt(password,toEncrypt.SharedWith.ConcatToString()));
            return toReturn;
        }
    }
}