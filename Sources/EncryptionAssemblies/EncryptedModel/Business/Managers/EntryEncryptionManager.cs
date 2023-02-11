using Encryption;
using Model.Business.Entries;
using EncryptedModel.Business.Entries;
using Encryption.AESEncryption;

namespace EncryptedModel.Business.Managers
{
    public static class EntryEncryptionManager
    {
        /// <summary>
        /// Decrypts an EncryptedSharedEntry to a SharedEntry, defaults algorithm to AES 128
        /// </summary>
        /// <param name="encrypted">The EncryptedSharedEntry to decrypt</param>
        /// <param name="password">The password that was used to Encrypt the given EncryptedSharedEntry</param>
        /// <returns>the decryptes SharedEntry</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="CryptographicException"></exception>
        public static SharedEntry EncryptedToSharedEntry(EncryptedSharedEntry encrypted, string password) => EncryptedToSharedEntry(encrypted, password, new AesDecrypter());

        /// <summary>
        /// Decrypts an EncryptedSharedEntry to a SharedEntry
        /// </summary>
        /// <param name="encrypted">The EncryptedSharedEntry to decrypt</param>
        /// <param name="password">The password that was used to Encrypt the given EncryptedSharedEntry</param>
        /// <param name="decrypter">The used decryption algorithm</param>
        /// <returns>the decryptes SharedEntry</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="CryptographicException"></exception>
        public static SharedEntry EncryptedToSharedEntry(EncryptedSharedEntry encrypted, string password, IDecrypter decrypter)
        {
            if (encrypted == null) throw new ArgumentNullException(nameof(encrypted));
            if(password == null) throw new ArgumentNullException(nameof(password));
            if(decrypter == null) throw new ArgumentNullException(nameof(decrypter));

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
        /// Encrypts a sharedEntry to an EncryptedSharedEntry, defaults algorithm to AES 128
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="CryptographicException"></exception>
        public static EncryptedSharedEntry SharedToEncryptedEntry(SharedEntry toEncrypt, string password) => SharedToEncryptedEntry(toEncrypt, password, new AesEncrypter());

        /// <summary>
        /// Encrypts a sharedEntry to an EncryptedSharedEntry
        /// </summary>
        /// <param name="toEncrypt">Shared entry to encrypt</param>
        /// <param name="password">password to be used for encryption</param>
        /// <param name="encrypter">Algorithm to use for encryption</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="CryptographicException"></exception>
        public static EncryptedSharedEntry SharedToEncryptedEntry(SharedEntry toEncrypt, string password, IEncrypter encrypter)
        {
            if (toEncrypt == null) throw new ArgumentNullException(nameof(toEncrypt));
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (encrypter == null) throw new ArgumentNullException(nameof(encrypter));

            EncryptedSharedEntry toReturn = new EncryptedSharedEntry(encrypter.EncryptionType(), toEncrypt.Uid.ToString(),
                                                                                         encrypter.Encrypt(password, toEncrypt.Login),
                                                                                         encrypter.Encrypt(password, toEncrypt.Password),
                                                                                         encrypter.Encrypt(password, toEncrypt.App),
                                                                                         string.IsNullOrEmpty(toEncrypt.Note) ? Array.Empty<byte>() : encrypter.Encrypt(password, toEncrypt.Note));
            return toReturn;
        }
        /// <summary>
        /// Decrypts an EncryptedProprietaryEntry to a ProprietaryEntry, defaults algorithm to AES 128
        /// </summary>
        /// <param name="encrypted">Entry to decrypt</param>
        /// <param name="password">password to use for decryption</param>
        /// <returns>ProprietaryEntry with the entry's data</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static ProprietaryEntry EncryptedToProprietaryEntry(EncryptedProprietaryEntry encrypted, string password) => EncryptedToProprietaryEntry(encrypted, password, new AesDecrypter());

        /// <summary>
        /// Decrypts an EncryptedProprietaryEntry to a ProprietaryEntry
        /// </summary>
        /// <param name="encrypted">Entry to decrypt</param>
        /// <param name="password">password to use for decryption</param>
        /// <param name="decrypter">The used decryption algorithm</param>
        /// <returns>ProprietaryEntry with the entry's data</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static ProprietaryEntry EncryptedToProprietaryEntry(EncryptedProprietaryEntry encrypted, string password, IDecrypter decrypter)
        {
            if (encrypted == null) throw new ArgumentNullException(nameof(encrypted));
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (decrypter == null) throw new ArgumentNullException(nameof(decrypter));

            if (encrypted.encryptionType != decrypter.EncryptionType()) throw new ArgumentException("Unexcpected Encryption type for this decrypter, excpected " + decrypter.EncryptionType() +
                                                                                                    " got " + encrypted.encryptionType);

            ProprietaryEntry toReturn = new ProprietaryEntry( "shoudl not be called", 
                                                   new Guid(encrypted.Uid),
                                                   decrypter.Decrypt(password, encrypted.EncryptedLogin),
                                                   decrypter.Decrypt(password, encrypted.EncryptedPassword),
                                                   decrypter.Decrypt(password, encrypted.EncryptedApp),
                                                   encrypted.EncryptedNote.Length <= 0 ? string.Empty : decrypter.Decrypt(password, encrypted.EncryptedNote));

            if (encrypted.SharedWith.Count > 0)
                encrypted.SharedWith.ForEach(user => toReturn.ShareToUser(user));

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
        public static EncryptedProprietaryEntry ProprietaryToEncryptedEntry(ProprietaryEntry toEncrypt, string password) => ProprietaryToEncryptedEntry(toEncrypt, password, new AesEncrypter());

        /// <summary>
        /// Encrypts a ProprietaryEntry to an EncryptedProprietaryEntry
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <param name="password"></param>
        /// <param name="encrypter"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="CryptographicException"></exception>
        public static EncryptedProprietaryEntry ProprietaryToEncryptedEntry(ProprietaryEntry toEncrypt, string password, IEncrypter encrypter)
        {
            if (toEncrypt == null) throw new ArgumentNullException(nameof(toEncrypt));
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (encrypter == null) throw new ArgumentNullException(nameof(encrypter));


            EncryptedProprietaryEntry toReturn = new EncryptedProprietaryEntry(encrypter.EncryptionType(), toEncrypt.Uid.ToString(), //TODO: fix placeholder for UID
                                                                                         encrypter.Encrypt(password, toEncrypt.Login),
                                                                                         encrypter.Encrypt(password, toEncrypt.Password),
                                                                                         encrypter.Encrypt(password, toEncrypt.App),
                                                                                         string.IsNullOrEmpty(toEncrypt.Note) ? Array.Empty<byte>() : encrypter.Encrypt(password, toEncrypt.Note),
                                                                                         toEncrypt.SharedWith.ToList());
            return toReturn;
        }
    }
}