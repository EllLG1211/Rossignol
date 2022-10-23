using Encryption;
using Encryption.AESEncryption;
using EncryptedModel.Business.Users;
using Model.Business.Users;

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

            return new LocalUser(new Guid(encrypted.Uid),
                                 decrypter.Decrypt(password, encrypted.EncryptedPassword), null);
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
        /// Encrypts a LocalUser to an EncryptedLocalUser, defaults algorithm to AES 128
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

            return new EncryptedLocalUser(encrypter.EncryptionType(),
                                          toEncrypt.Uid.ToString(),
                                          encrypter.Encrypt(password, toEncrypt.Password));

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
        /// Decrypts an EncryptedConnectedUser to a ConnectedUser, defaults algorithm to AES 128
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
            return new ConnectedUser(new Guid(encrypted.Uid),
                                     decrypter.Decrypt(password, encrypted.EncryptedMail),
                                     decrypter.Decrypt(password, encrypted.EncryptedPassword), null);
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
        /// Encrypts a ConnectedUser to an EncryptedConnectedUser, defaults algorithm to AES 128
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

            return new EncryptedConnectedUser(encrypter.EncryptionType(),
                                              toEncrypt.Uid.ToString(),
                                              encrypter.Encrypt(password, toEncrypt.Mail),
                                              encrypter.Encrypt(password, toEncrypt.Password));
        }
    }
}