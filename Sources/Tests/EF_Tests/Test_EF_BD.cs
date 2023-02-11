using EF_Local;
using EF_Local.Managers;
using EF_Model.Entities;
using EF_Model.Managers;
using EF_Model.Utils;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Model.Business.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
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

            LocalUserEntity lu = new LocalUserEntity() { /*EncryptionType = "AES",*/ Password = /*new AesEncrypter().Encrypt(MASTER_PASSWORD, MASTER_PASSWORD)*/MASTER_PASSWORD, Uid = Guid.NewGuid().ToString() };
            LocalUserEntity la = new LocalUserEntity() {Password = MASTER_PASSWORD, Uid = Guid.NewGuid().ToString() };

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


            UserEntityManager.RAZ(options).Wait();
            EntryEntityManager.RAZ(options).Wait(); //may not be necessary in other cases, here needed because we use a raw sql command for cleanup
            EFManager.ConstructDatabase(ls, options).Wait();  //entries are included with the users


            using (var context = new RossignolContextLocal(options))
            {
                context.Database.EnsureCreated();
                IEnumerable<EntryEntity> encryptedEntries = context.EntriesSet;
                foreach (EntryEntity entry in encryptedEntries)
                {
                    Assert.True(entries.Contains(entry));
                    Entry e = entry.ToModel();
                }
            }

            lu.Password = "new password";
            UserEntityManager.updateUser(lu, options).Wait();

            using (var context = new RossignolContextLocal(options))
            {
                Assert.Equal("new password", context.LocalUsers.First(s => s.Uid == lu.Uid).Password);
            }

            lu.OwnedEntries.First().App = "Discord";
            EntryEntityManager.updateEntry(lu.OwnedEntries.First(), lu, options).Wait();

            using (var context = new RossignolContextLocal(options))
            {
                string sr = context.EntriesSet.First(s => s.Uid == lu.OwnedEntries.First().Uid)?.App;
                Assert.Equal("Discord", sr);
            }

            Assert.Equal(2, UserEntityManager.returnUserCount(options));
            UserEntityManager.removeUser(lu, options).Wait();
            Assert.Equal(1, UserEntityManager.returnUserCount(options));
            UserEntityManager.removeUser(new Guid(la.Uid), options).Wait();
            Assert.Equal(0, UserEntityManager.returnUserCount(options));
        }
    }
}