using Model.Business.Users;

namespace ConsoleApp
{
    internal class TermWriter
    {
        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }

        public void Write(string line)
        {
            Console.Write(line);
        }

        public void WriteErr(string err)
        {
            Console.Error.WriteLine(err);
        }

        public void WriteEntries(AbstractUser user)
        {
            int index = 0;
            foreach (var entry in user.Entries)
            {
                Console.WriteLine($"{index}. {entry.Login} - {entry.Password} - {entry.App} - {entry.Note}");
                index++;
            }
        }
    }
}