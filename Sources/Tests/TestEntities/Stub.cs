using EF_Model.Entities;
using EF_Model.Utils;
using Model.Business.Entries;
using Model.Business.Entries.Serialized;
using Model.Business.Managers;

namespace TestEntities
{
    internal class Stub
    {
        private EntryEntity CreateEntryEntity(String login, String password, String app, String? note)
        {
            SharedEntry sharedEntry = new SharedEntry(login, password, app, note);
            EncryptedSharedEntry encryptedSharedEntry = EntryEncryptionManager.SharedToEncryptedEntry(sharedEntry, Program.MASTER_PASSWORD);
            return ProprietaryEntryConverter.ToEntity(encryptedSharedEntry);
        }

        public List<EntryEntity> load()
        {
            List<EntryEntity> list = new List<EntryEntity>();
            list.Add(CreateEntryEntity("login1", "password1", "app1", null));
            list.Add(CreateEntryEntity("login2", "password2", "app2", "note2"));
            list.Add(CreateEntryEntity("login3", "password3", "app3", "note3"));
            list.Add(CreateEntryEntity("login4", "password4", "app4", null));
            list.Add(CreateEntryEntity("login5", "password5", "app5", "note4"));
            return list;
        }
    }
}
