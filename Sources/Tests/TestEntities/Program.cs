using EF_Model;
using EF_Model.Entities;
using Model.Business.Entries;

namespace TestEntities
{
    class Program
    {
        public static String MASTER_PASSWORD = "masterPassword";

        public static void Main(string[] args)
        {
            Console.WriteLine("Test console for Entity Framework\n");

            Stub stub = new Stub();
            Decryptor decryptor = new Decryptor();

            using (var context = new RossignolContext())
            {
                context.AddRange(stub.load());
                context.SaveChanges();
            }

            using (var context = new RossignolContext())
            {
                IEnumerable<EntryEntity> encryptedEntries = context.EncryptedEntriesSet;

                foreach (EntryEntity entry in encryptedEntries)
                {
                    SharedEntry p = decryptor.Decrypt(entry);
                    Console.WriteLine($"{p.Uid} - {p.Login} - {p.Password} - {p.App} - {p.Label} - {p.Note}");
                }
            }
        }
    }
}