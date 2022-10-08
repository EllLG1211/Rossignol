using Encryption;
using Encryption.AESEncryption;
using Model.Business.Entries;
using Model.Business.Entries.Serialized;
using Model.Business.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Business.Managers
{
    public class EntryEncryptionManager
    {
        /// <summary>
        /// Decrypts an EncryptedSharedEntry to a SharedEntry
        /// </summary>
        /// <param name="encrypted">The EncryptedSharedEntry to decrypt</param>
        /// <param name="password">The password that was used to Encrypt the given EncryptedSharedEntry</param>
        /// <returns>the decryptes SharedEntry</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="CryptographicException"></exception>
        public ProprietaryEntry EncryptedToSharedEntry(EncryptedSharedEntry encrypted, string password)
        {
            if (encrypted == null) throw new ArgumentNullException(nameof(encrypted));

            IDecrypter decrypter = new AesDecrypter();

            ProprietaryEntry toReturn = new ProprietaryEntry(decrypter.Decrypt(password,encrypted.EncryptedLogin),
                                                   decrypter.Decrypt(password, encrypted.EncryptedPassword),
                                                   decrypter.Decrypt(password, encrypted.EncryptedApp),
                                                   encrypted.EncryptedNote.Length == 0 ? string.Empty : decrypter.Decrypt(password, encrypted.EncryptedNote));



            return toReturn;
        }

        /// <summary>
        /// Encrypts a SharedEntry to an EncryptedSharedEntry
        /// </summary>
        /// <param name="entry">Entry to encrypt</param>
        /// <param name="password">password to use for encryption</param>
        /// <returns>ProprietaryEntry with the entry's data</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public SharedEntry EncryptedToProprietaryEntry(EncryptedSharedEntry entry, string password)
        {
            if (entry == null) throw new ArgumentNullException(nameof(entry));

            IDecrypter decrypter = new AesDecrypter();

            SharedEntry toReturn = new SharedEntry(decrypter.Decrypt(password, entry.EncryptedLogin),
                                                   decrypter.Decrypt(password, entry.EncryptedPassword),
                                                   decrypter.Decrypt(password, entry.EncryptedApp),
                                                   entry.EncryptedNote.Length == 0 ? string.Empty : decrypter.Decrypt(password, entry.EncryptedNote));

            return toReturn;
        }
    }
}
