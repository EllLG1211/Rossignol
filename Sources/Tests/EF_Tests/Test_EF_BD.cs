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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EF_Tests
{
    public class Test_EF_BD
    {
        private static readonly string MASTER_PASSWORD = "masterPassword";
        private static EntryEntity CreateEntryEntity(String login, String password, String app, String? note, LocalUserEntity owner)
        {
            ProprietaryEntry proprietaryEntry = new ProprietaryEntry(login, password, app, note);
            EncryptedProprietaryEntry encryptedSharedEntry = EntryEncryptionManager.ProprietaryToEncryptedEntry(proprietaryEntry, MASTER_PASSWORD);
            EntryEntity toreturn = ProprietaryEntryConverter.ToEntity(encryptedSharedEntry, owner);
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

            List<LocalUserEntity> users = new List<LocalUserEntity>();

            LocalUserEntity lu = new LocalUserEntity() { EncryptionType = "AES", Password = new AesEncrypter().Encrypt(MASTER_PASSWORD, MASTER_PASSWORD), Uid = Guid.NewGuid().ToString() };
            users.Add(lu);

            List<EntryEntity> entries = new List<EntryEntity>();
            entries.Add(CreateEntryEntity("login1", "password1", "app1", null, lu));
            entries.Add(CreateEntryEntity("login2", "password2", "app2", "note2", lu));
            entries.Add(CreateEntryEntity("login3", "password3", "app3", "note3", lu));
            entries.Add(CreateEntryEntity("login4", "password4", "app4", null, lu));
            entries.Add(CreateEntryEntity("login5", "password5", "app5", "note4", lu));

            lu.OwnedEntries = entries;

            using (var context = new RossignolContextLocal(options))
            {
                context.Database.EnsureCreated();



                context.EncryptedEntriesSet.AddRange(entries);
                context.SaveChanges();
            }


            using (var context = new RossignolContextLocal(options))
            {
                context.Database.EnsureCreated();
                IEnumerable<EntryEntity> encryptedEntries = context.EncryptedEntriesSet;
                foreach (EntryEntity entry in encryptedEntries)
                {
                    Assert.True(entries.Contains(entry));
                    EncryptedProprietaryEntry encryptedSharedEntry = ProprietaryEntryConverter.ToModel(entry);
                    ProprietaryEntry pl = EntryEncryptionManager.EncryptedToProprietaryEntry(encryptedSharedEntry, MASTER_PASSWORD);
                }
                Task p = EntryEntityManager.clearDB(options);
            }

            using (var context = new RossignolContextLocal(options))
            {
                var entities = context.EncryptedEntriesSet.Select(e=> context.EncryptedEntriesSet.Remove(e));
             
                context.SaveChanges();

                Assert.True(context.EncryptedEntriesSet.Count() == 0);
            }
        }
    }
}