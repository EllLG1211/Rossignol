using Data;
using Model.Business;
using Model.Business.Entries;
using Model.Business.Users;

namespace ConsoleApp
{
    class App
    {
        public static void Main(string[] args)
        {
            IDataManager data = new Stub();
            Manager manager = new Manager(data);
            TermReader reader = new TermReader();
            TermWriter writer = new TermWriter();

            bool quit = false;
            int choix = -1;
            while(!quit)
            {
                if (manager.LoggedIn == null)
                {
                    writer.WriteLine("Menu: \n\t1. S'inscrire\n\t2. Se connecter\n\t9. Quitter");

                    try
                    {
                        choix = reader.ReadInt();
                    } catch (FormatException e)
                    {
                        writer.WriteErr(e.Message);
                        continue;
                    }
                    

                    if(choix == 2)
                    {
                        writer.Write("Login: ");
                        string login = reader.ReadLine();
                        writer.Write("Password: ");
                        string password = reader.ReadLine();
                        try
                        {
                            manager.Login(login, password);
                            writer.WriteLine("Bienvenue !");
                        } catch (Exception e)
                        {
                            writer.WriteErr(e.Message);
                        }
                        
                    } else if(choix == 1)
                    {
                        writer.Write("Entrez un email: ");
                        string mail = reader.ReadLine();
                        writer.Write("Entrez un mot de passe: ");
                        string password = reader.ReadLine();
                        writer.Write("Confirmez le mot de passe: ");
                        string confirmPassword = reader.ReadLine();
                        try
                        {
                            manager.Signin(mail, password, confirmPassword);
                            writer.WriteLine("L'utilisateur a bien été inscrit.");
                        } catch (ArgumentException e)
                        {
                            writer.WriteErr(e.Message);
                        }

                    } else if(choix == 9)
                    {
                        quit = true;
                    }
                } else
                {

                    writer.WriteLine("Menu:" +
                        "\n\t1. Voir mes mots de passes" +
                        "\n\t2. Ajouter une entrée" +
                        "\n\t3. Partager une entrée" +
                        "\n\t4. Retirer une entrée" +
                        "\n\t5. Se deconnecter" +
                        "\n\t9. Quitter");

                    try
                    {
                        choix = reader.ReadInt();
                    }
                    catch (FormatException e)
                    {
                        writer.WriteErr(e.Message);
                        continue;
                    }

                    if (choix == 1)
                    {
                        writer.WriteLine("Mes entrées: ");
                        writer.WriteEntries(manager.LoggedIn);
                    }
                    else if (choix == 2)
                    {
                        writer.Write("Saisissez le login: ");
                        string login = reader.ReadLine();
                        writer.Write("Saisissez le mot de passe: ");
                        string password = reader.ReadLine();
                        writer.Write("Saisissez le nom de l'application: ");
                        string app = reader.ReadLine();
                        writer.Write("Saisissez un commentaire: ");
                        string note = reader.ReadLine();
                        manager.CreateEntryToConnectedUser(login, login, password, app, note);
                    }
                    else if (choix == 3)
                    {
                        writer.WriteEntries(manager.LoggedIn);
                        writer.Write("Numéro de l'entrée à partager:");
                        int numero = reader.ReadInt();
                        writer.Write("\nEmail de l'utilisateur à qui partager l'entrée:");
                        string mail = reader.ReadLine();
                        if (!manager.ShareEntryWith((ProprietaryEntry)manager.LoggedIn.Entries.ToArray()[numero], mail))
                        {
                            writer.Write($"\nEmail incorrect, {mail} n'est pas un utilisateur valide");
                        }
                        else
                        {
                            writer.Write($"\nEntrée partagée avec {mail}");
                        }
                    }
                    else if (choix == 4)
                    {
                        writer.WriteEntries(manager.LoggedIn);
                        writer.Write("Numéro de l'entrée à supprimer:");
                        int numero = reader.ReadInt();
                        manager.RemoveEntry(manager.LoggedIn.Entries.ToArray()[numero]);
                    }
                    else if (choix == 5)
                    {
                        manager.logOut();
                    }
                    else if (choix == 9)
                    {
                        quit = true;
                    }
                }
            }

            manager.save();
        }
    }
}