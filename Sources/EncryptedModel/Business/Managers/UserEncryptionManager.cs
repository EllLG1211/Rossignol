using Encryption;
using EncryptedModel.Business.Users;
using Model.Business.Users;
using EncryptedModel.Business.Entries;
using Model.Business.Entries;
using Encryption.AESEncryption;

namespace EncryptedModel.Business.Managers
{
    public static class UserEncryptionManager
    {
        /// <summary>
        /// Decrypts an EncryptedLocalUser to a LocalUser, defaults algorithm to AES 128
        /// </summary>
        /// <param name="encrypted">The EncryptedLocalUser to decrypt</param>
        /// <param name="password">The password that was used to Encrypt the given EncryptedLocalUser</param>
        /// <returns> A LocalUser</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static LocalUser EncryptedToLocalUser(EncryptedLocalUser encrypted, string password) => EncryptedToLocalUser(encrypted, password, new AesDecrypter());

        /// <summary>
        /// Decrypts an EncryptedLocalUser to a LocalUser
        /// </summary>
        /// <param name="encrypted">The EncryptedLocalUser to decrypt</param>
        /// <param name="password">The password that was used to encrypt the given EncryptedLocalUser</param>
        /// <returns> A LocalUser</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static LocalUser EncryptedToLocalUser(EncryptedLocalUser encrypted, string password, IDecrypter decrypter)
        {
            if (encrypted == null) throw new ArgumentNullException(nameof(encrypted));
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (decrypter == null) throw new ArgumentNullException(nameof(decrypter));

            if (encrypted.EncryptionType != decrypter.EncryptionType()) throw new ArgumentException("Unexcpected Encryption type for this decrypter, excpected " + decrypter.EncryptionType() +
                                                                                                   " got " + encrypted.EncryptionType);

            LocalUser user = new LocalUser(new Guid(encrypted.Uid),
                                 decrypter.Decrypt(password, encrypted.EncryptedPassword), null);

            encrypted.ownedEncryptedEntries?.ForEach(ee =>  user.AddEntry(EntryEncryptionManager.EncryptedToProprietaryEntry(ee, password)));

            return user;
        }

        /// <summary>
        /// Encrypts a LocalUser to an EncryptedLocalUser, defaults algorithm to AES 128
        /// </summary>
        /// <param name="toEncrypt">The LocalUser to encrypt</param>
        /// <param name="password">The password that will be used to encrypt the LocalUser</param>
        /// <returns> An EncryptedLocalUser </returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static EncryptedLocalUser LocalToEncryptedUser(LocalUser toEncrypt, string password) => LocalToEncryptedUser(toEncrypt, password, new AesEncrypter());

        /// <summary>
        /// Encrypts a LocalUser to an EncryptedLocalUser
        /// </summary>
        /// <param name="toEncrypt">The LocalUser to encrypt</param>
        /// <param name="password">The password that will be used to encrypt the LocalUser</param>
        /// <returns> An EncryptedLocalUser </returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static EncryptedLocalUser LocalToEncryptedUser(LocalUser toEncrypt, string password, IEncrypter encrypter)
        {
            if (toEncrypt == null) throw new ArgumentNullException(nameof(toEncrypt));
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (encrypter == null) throw new ArgumentNullException(nameof(encrypter));

            List<EncryptedProprietaryEntry> entries = new List<EncryptedProprietaryEntry>();
            toEncrypt.Entries.OfType<ProprietaryEntry>().ToList().ForEach(pe => entries.Add(EntryEncryptionManager.ProprietaryToEncryptedEntry(pe,password)));

            EncryptedLocalUser eUser =  new EncryptedLocalUser(encrypter.EncryptionType(),
                                          toEncrypt.Uid.ToString(),
                                          encrypter.Encrypt(password, toEncrypt.Password),
                                          entries);
            return eUser;
        }

        /// <summary>
        /// Decrypts an EncryptedConnectedUser to a ConnectedUser, defaults algorithm to AES 128
        /// </summary>
        /// <param name="encrypted">The EncryptedConnectedUser to decrypt</param>
        /// <param name="password">The password that was used to Encrypt the given EncryptedConnectedUser</param>
        /// <returns> A ConnectedUser</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static ConnectedUser EncryptedToConnectedUser(EncryptedConnectedUser encrypted, string password) => EncryptedToConnectedUser(encrypted, password, new AesDecrypter());

        /// <summary>
        /// Decrypts an EncryptedConnectedUser to a ConnectedUser
        /// </summary>
        /// <param name="encrypted">The EncryptedConnectedUser to decrypt</param>
        /// <param name="password">The password that was used to Encrypt the given EncryptedConnectedUser</param>
        /// <returns> A ConnectedUser</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static ConnectedUser EncryptedToConnectedUser(EncryptedConnectedUser encrypted, string password, IDecrypter decrypter)
        {
            if (encrypted == null) throw new ArgumentNullException(nameof(encrypted));
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (decrypter == null) throw new ArgumentNullException(nameof(decrypter));

            if (encrypted.EncryptionType != decrypter.EncryptionType()) throw new ArgumentException("Unexcpected Encryption type for this decrypter, excpected " + decrypter.EncryptionType() +
                                                                                                   " got " + encrypted.EncryptionType);

            ConnectedUser user = new ConnectedUser(new Guid(encrypted.Uid),
                                     decrypter.Decrypt(password, encrypted.EncryptedMail),
                                     decrypter.Decrypt(password, encrypted.EncryptedPassword), null);

            encrypted.ownedEncryptedEntries?.ForEach(ee => user.AddEntry(EntryEncryptionManager.EncryptedToProprietaryEntry(ee, password)));
            encrypted.encryptedSharedWith?.ForEach(ee => user.AddEntry(EntryEncryptionManager.EncryptedToSharedEntry(ee, password)));

            return user;
        }

        /// <summary>
        /// Encrypts a ConnectedUser to an EncryptedConnectedUser, defaults algorithm to AES 128
        /// </summary>
        /// <param name="toEncrypt">The ConnectedUser to encrypt</param>
        /// <param name="password">The password that will be used to encrypt the ConnectedUser</param>
        /// <returns> An EncryptedConnectedUser </returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static EncryptedConnectedUser ConnectedToEncryptedUser(ConnectedUser toEncrypt, string password) => ConnectedToEncryptedUser(toEncrypt, password, new AesEncrypter());

        /// <summary>
        /// Encrypts a ConnectedUser to an EncryptedConnectedUser
        /// </summary>
        /// <param name="toEncrypt">The ConnectedUser to encrypt</param>
        /// <param name="password">The password that will be used to encrypt the ConnectedUser</param>
        /// <returns> An EncryptedConnectedUser </returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static EncryptedConnectedUser ConnectedToEncryptedUser(ConnectedUser toEncrypt, string password, IEncrypter encrypter)
        {
            if (toEncrypt == null) throw new ArgumentNullException(nameof(toEncrypt));
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (encrypter == null) throw new ArgumentNullException(nameof(encrypter));

            List<EncryptedProprietaryEntry> entries = new List<EncryptedProprietaryEntry>();
            List<EncryptedSharedEntry> sharedEntries = new List<EncryptedSharedEntry>();
            toEncrypt.Entries.OfType<ProprietaryEntry>().ToList().ForEach(pe => entries.Add(EntryEncryptionManager.ProprietaryToEncryptedEntry(pe, password)));
            toEncrypt.Entries.OfType<SharedEntry>().ToList().ForEach(se => sharedEntries.Add(EntryEncryptionManager.SharedToEncryptedEntry(se, password)));

            EncryptedConnectedUser user =  new EncryptedConnectedUser(encrypter.EncryptionType(),
                                              toEncrypt.Uid.ToString(),
                                              encrypter.Encrypt(password, toEncrypt.Mail),
                                              encrypter.Encrypt(password, toEncrypt.Password),
                                              entries,
                                              sharedEntries);

            return user;
        }
    }
}