using EF_Model;
using EF_Model.Entities;
using EF_Model.Managers;
using Encryption.AESEncryption;
using Model.Business.Entries;
using Model.Business.Users;

namespace TestEntities
{
    class Program
    {
        public static readonly string MASTER_PASSWORD = "masterPassword";

        public static void Main(string[] args)
        {
            Console.WriteLine("Test console for Entity Framework\n");

            EFManager efm = new EFManager();

            List<LocalUserEntity> users = Stub.loadUsers(MASTER_PASSWORD);
            List<EntryEntity> entries  = Stub.loadEntities(users.First());
            users.First().OwnedEntries = entries;

            Task dbconstruct = efm.ConstructDatabase(entries, users);

            Decryptor decryptor = new Decryptor();

            dbconstruct.Wait();
            dbconstruct.Dispose();

            using (var context = new RossignolContextLocal())
            {
                IEnumerable<EntryEntity> encryptedEntries = context.EncryptedEntriesSet;
                IEnumerable<LocalUserEntity> usersNm = context.LocalUser;
                foreach (EntryEntity entry in encryptedEntries)
                {
                    ProprietaryEntry p = decryptor.Decrypt(entry);
                    Console.WriteLine($"{p.Uid} - {p.Login} - {p.Password} - {p.App} - {p.Label} - {p.Note} - {entry.Owner}|");
                }
                Console.WriteLine("users:");
                foreach (LocalUserEntity user in usersNm)
                {
                    Console.WriteLine($"{user.Uid} - {user.Password} - {new AesDecrypter().Decrypt(MASTER_PASSWORD, user.Password)} - {new AesDecrypter().Decrypt(MASTER_PASSWORD,user.OwnedEntries.First().App)}");
                }
            }
        }
    }
}