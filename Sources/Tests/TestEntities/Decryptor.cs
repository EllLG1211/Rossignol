using EF_Model.Entities;
using EF_Model.Utils;
using Model.Business.Entries;
using EncryptedModel.Business.Entries;
using EncryptedModel.Business.Managers;

namespace TestEntities
{
    internal class Decryptor
    {
        public SharedEntry Decrypt(EntryEntity entryEntity)
        {
            EncryptedSharedEntry encryptedSharedEntry = ProprietaryEntryConverter.ToModel(entryEntity);
            return EntryEncryptionManager.EncryptedToSharedEntry(encryptedSharedEntry, Program.MASTER_PASSWORD);
        }
    }
}
