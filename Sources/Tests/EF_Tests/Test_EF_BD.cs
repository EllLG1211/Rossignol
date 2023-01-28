using EF_Local.Managers;
using EF_Model;
using EF_Model.Entities;
using EF_Model.Managers;
using EF_Model.Utils;
using EncryptedModel.Business.Entries;
using EncryptedModel.Business.Managers;
using Encryption.AESEncryption;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Model.Business.Entries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EF_Tests
{
    public class Test_EF_BD_Local
    {
        private static readonly string MASTER_PASSWORD = "masterPassword";
        private static EntryEntity CreateEntryEntity(string mail,String login, String password, String app, String? note, LocalUserEntity owner)
        {
            ProprietaryEntry proprietaryEntry = new ProprietaryEntry(mail,login, password, app, note);
            //EncryptedProprietaryEntry encryptedSharedEntry = EntryEncryptionManager.ProprietaryToEncryptedEntry(proprietaryEntry, MASTER_PASSWORD);
            EntryEntity toreturn = EntryConverter.ToEntity(proprietaryEntry, owner);
            return toreturn;
        }

        [Fact]
        public void TestEF()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<RossignolContextLocal>()
            .UseSqlite(connection)
            .Options;


            EFManager efm = new EFManager();

            UserEntityManager.RAZ(options).Wait();
            EntryEntityManager.RAZ(options).Wait();

            LocalUserEntity lu = new LocalUserEntity() { /*EncryptionType = "AES",*/ Password = /*new AesEncrypter().Encrypt(MASTER_PASSWORD, MASTER_PASSWORD)*/MASTER_PASSWORD, Uid = Guid.NewGuid().ToString() };
            LocalUserEntity la = new LocalUserEntity() {Password = MASTER_PASSWORD, Uid = Guid.NewGuid().ToString() };

            //UserEntityManager.addUser(lu, options).Wait();
            //UserEntityManager.addUser(la, options).Wait();

            List<LocalUserEntity> ls = new List<LocalUserEntity>();
            ls.Add(la);
            ls.Add(lu);

            List<EntryEntity> entries = new List<EntryEntity>();
            entries.Add(CreateEntryEntity("lg@a.com","login1", "password1", "app1", null, lu));
            entries.Add(CreateEntryEntity("lg@a.com", "login2", "password2", "app2", "note2", lu));
            entries.Add(CreateEntryEntity("lg@a.com", "login3", "password3", "app3", "note3", lu));
            entries.Add(CreateEntryEntity("lg@a.com", "login4", "password4", "app4", null, lu));
            entries.Add(CreateEntryEntity("lg@a.com", "login5", "password5", "app5", "note4", lu));

            lu.OwnedEntries = entries;

            efm.ConstructDatabase(ls, options).Wait();  //entries are included with the users


            using (var context = new RossignolContextLocal(options))
            {
                context.Database.EnsureCreated();
                IEnumerable<EntryEntity> encryptedEntries = context.EntriesSet;
                foreach (EntryEntity entry in encryptedEntries)
                {
                    Assert.True(entries.Contains(entry));
                    //EncryptedProprietaryEntry encryptedSharedEntry = EntryConverter.ToModel(entry);
                    //ProprietaryEntry pl = EntryEncryptionManager.EncryptedToProprietaryEntry(encryptedSharedEntry, MASTER_PASSWORD);
                    Entry e = entry.ToModel();
                }
                //Task p = EntryEntityManager.clearDB(options);
            }
            /*
            using (var context = new RossignolContextLocal(options))
            {
                var entities = context.EntriesSet.Select(e=> context.EntriesSet.Remove(e));
             
                context.SaveChanges();

                Assert.True(context.EntriesSet.Count() == 0);
            }
            */
            Assert.Equal(2, UserEntityManager.returnUserCount(options));
            UserEntityManager.removeUser(lu, options).Wait();
            Assert.Equal(1, UserEntityManager.returnUserCount(options));
            UserEntityManager.removeUser(new Guid(la.Uid), options).Wait();
            Assert.Equal(0, UserEntityManager.returnUserCount(options));
        }
    }
}