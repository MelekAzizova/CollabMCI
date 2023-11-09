using Core.Models;

namespace UserInterface1;

internal class Program
{
    static void Main(string[] args)
    {
        bool login = false;
        while (!login)
        {
            Console.WriteLine("Login Menu:");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Registration");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Login:");

                    Console.Write("Username: ");
                    string username = Console.ReadLine();
                    Console.Write("Password: ");
                    string password = Console.ReadLine();
                    CompanyService.LoginUser(username,password);
                    login= true;
                    break;
                case 2:
                    Console.WriteLine("Registration:");
                    Console.Write("Name: ");
                    string name = Console.ReadLine();

                    Console.Write("Surname: ");
                    string surname = Console.ReadLine();

                    Console.Write("Username: ");
                    string newUsername = Console.ReadLine();

                    Console.Write("Password: ");
                    string newPassword = Console.ReadLine();
                    CompanyService.RegisterUser(name,surname,newUsername,newPassword);
                    break;
                default:
                    Console.WriteLine("Düzgün seçim edilməyib. Yenidən cəhd edin.");
                    break;
            }
        }
        bool admin = true;
        if (!admin)
        {
            while (true)
            {
                Console.WriteLine("User Menu:");
                Console.WriteLine("1. Look at the pizzas \n2. Order pizza");

                int choice = Convert.ToInt32(Console.ReadLine());
               
                switch (choice)
                {
                    case 1:
                        
                        int n = Convert.ToInt32(Console.ReadLine());
                        switch (n)
                        {
                            case 0:
                                break;
                            case 1:
                                CompanyService.GetProductNames();
                                break;
                            case 2:
                                break;
                            default:
                                Console.WriteLine("duzgun deyil");
                                break;
                        }

                        //if (n == 0) goto GO;

                        //char save = Convert.ToChar(Console.ReadLine());
                        //if (save == 's' || save == 'S')
                        //{
                        //    Console.WriteLine("How much pizza do you want?");
                        //    int count = Convert.ToInt32(Console.ReadLine());
                        //}
                        //Console.Write("Do you want to return to the product menu? ");
                        //char go= Convert.ToChar(Console.ReadLine());
                        //if (go=='g' || go=='G')
                        //{
                        //    goto Go;
                        //}
                        break;
                    case 2:
                        // sifaris metodu
                        break;
                    default:
                        Console.WriteLine("Düzgün seçim edilməyib. Yenidən cəhd edin.");
                        break;
                }
            }
        }
        else
        {

        }
    }
}