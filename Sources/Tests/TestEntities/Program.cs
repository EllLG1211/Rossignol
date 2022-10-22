using EF_Model;
using EF_Model.Entities;
using EF_Model.Managers;
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

            Stub stub = new Stub();

            EFManager efm = new EFManager();

            Task dbconstruct = efm.ConstructDatabase(stub.loadEntities(), stub.loadUsers(MASTER_PASSWORD));

            Decryptor decryptor = new Decryptor();

            dbconstruct.Wait();
            dbconstruct.Dispose();

            using (var context = new RossignolContext())
            {
                IEnumerable<EntryEntity> encryptedEntries = context.EncryptedEntriesSet;
                IEnumerable<LocalUserEntity> users = context.LocalUserSet;
                foreach (EntryEntity entry in encryptedEntries)
                {
                    SharedEntry p = decryptor.Decrypt(entry);
                    Console.WriteLine($"{p.Uid} - {p.Login} - {p.Password} - {p.App} - {p.Label} - {p.Note}");
                }
                foreach (LocalUserEntity user in users)
                {

                }
            }
        }
    }
}