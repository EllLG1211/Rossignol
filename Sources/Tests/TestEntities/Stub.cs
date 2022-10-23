using EF_Model.Entities;
using EF_Model.Utils;
using Model.Business.Entries;
using EncryptedModel.Business.Entries;
using EncryptedModel.Business.Managers;
using Encryption.AESEncryption;

namespace TestEntities
{
    internal class Stub
    {
        private EntryEntity CreateEntryEntity(String login, String password, String app, String? note, LocalUserEntity owner)
        {
            SharedEntry sharedEntry = new SharedEntry(login, password, app, note);
            EncryptedSharedEntry encryptedSharedEntry = EntryEncryptionManager.SharedToEncryptedEntry(sharedEntry, Program.MASTER_PASSWORD);
            EntryEntity toreturn = ProprietaryEntryConverter.ToEntity(encryptedSharedEntry);
            toreturn.Owner = owner;
            return toreturn;
        }

        public List<EntryEntity> loadEntities(LocalUserEntity owner)
        {
            List<EntryEntity> list = new List<EntryEntity>();
            list.Add(CreateEntryEntity("login1", "password1", "app1", null, owner));
            list.Add(CreateEntryEntity("login2", "password2", "app2", "note2", owner));
            list.Add(CreateEntryEntity("login3", "password3", "app3", "note3", owner));
            list.Add(CreateEntryEntity("login4", "password4", "app4", null, owner));
            list.Add(CreateEntryEntity("login5", "password5", "app5", "note4", owner));
            return list;
        }

        public List<LocalUserEntity> loadUsers(string password)=>
            new List<LocalUserEntity>() { new LocalUserEntity() { EncryptionType = "AES", Password =  new AesEncrypter().Encrypt(password, password), Uid=Guid.NewGuid().ToString()} };
       
    }
}
