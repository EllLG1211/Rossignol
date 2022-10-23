using EF_Model.Entities;
using EF_Model.Utils;
using Model.Business.Entries;
using EncryptedModel.Business.Entries;
using EncryptedModel.Business.Managers;

namespace TestEntities
{
    internal class Decryptor
    {
        public ProprietaryEntry Decrypt(EntryEntity entryEntity)
        {
            EncryptedProprietaryEntry encryptedSharedEntry = ProprietaryEntryConverter.ToModel(entryEntity);
            return EntryEncryptionManager.EncryptedToProprietaryEntry(encryptedSharedEntry, Program.MASTER_PASSWORD);
        }
    }
}
