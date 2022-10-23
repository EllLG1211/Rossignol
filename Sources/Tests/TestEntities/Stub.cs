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
        private static EntryEntity CreateEntryEntity(String login, String password, String app, String? note, LocalUserEntity owner)
        {
            ProprietaryEntry proprietaryEntry = new ProprietaryEntry(login, password, app, note);
            EncryptedProprietaryEntry encryptedSharedEntry = EntryEncryptionManager.ProprietaryToEncryptedEntry(proprietaryEntry, Program.MASTER_PASSWORD);
            EntryEntity toreturn = ProprietaryEntryConverter.ToEntity(encryptedSharedEntry);
            toreturn.Owner = owner;
            return toreturn;
        }

        public static List<EntryEntity> loadEntities(LocalUserEntity owner)
        {
            List<EntryEntity> list = new List<EntryEntity>();
            list.Add(CreateEntryEntity("login1", "password1", "app1", null, owner));
            list.Add(CreateEntryEntity("login2", "password2", "app2", "note2", owner));
            list.Add(CreateEntryEntity("login3", "password3", "app3", "note3", owner));
            list.Add(CreateEntryEntity("login4", "password4", "app4", null, owner));
            list.Add(CreateEntryEntity("login5", "password5", "app5", "note4", owner));
            return list;
        }

        public static List<LocalUserEntity> loadUsers(string password)=>
            new List<LocalUserEntity>() { new LocalUserEntity() { EncryptionType = "AES", Password =  new AesEncrypter().Encrypt(password, password), Uid=Guid.NewGuid().ToString()} };
       
    }
}
