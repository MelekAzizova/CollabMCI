using Core.Exceptions;
using Core.Models;
using System.Text.RegularExpressions;

namespace UserInterface1;

internal class Program
{
    static bool isLogin = false;
    static string SInput;
    static decimal DInput;
    static int IInput;
   

    static string MenuRequestMessage(string menu)
    {
        return $"Choose from {menu} menu: ";
    }
    static void Register()
    {
        Console.Write("\tName: ");
        string name = Console.ReadLine();

        Console.Write("\tSurname: ");
        string surname = Console.ReadLine();

        Console.Write("\tUsername: ");
        SInput = Console.ReadLine();

        Console.Write("\tPassword: ");
        if (!CompanyService.RegisterUser(name, surname, SInput, Console.ReadLine()))
        {
            Console.WriteLine("Such username is already used.");
        }
    }
    static void LoginMenu()
    {
        while (!isLogin)
        {
            Console.WriteLine("\n1. Login");
            Console.WriteLine("2. Registration");
            Console.Write(MenuRequestMessage("Login"));

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("\tUsername: ");
                    SInput = Console.ReadLine();

                    Console.Write("\tPassword: ");
                    if (CompanyService.LoginUser(ref SInput, Console.ReadLine())) isLogin = true;
                    else Console.WriteLine("Invalid username or password. Please try again.");
                    break;
                case "2":
                    Register();
                    break;
                default:
                    Console.WriteLine("Düzgün seçim edilməyib. Yenidən cəhd edin.");
                    break;
            }

            if (isLogin)
            {
                Console.WriteLine("\nWelcome back " + SInput);
            }
        }
    }
    static int CRUDMenu()
    {
        Console.WriteLine("\n1. Create");
        Console.WriteLine("2. Read All");
        Console.WriteLine("3. Update"); //(Id-ə görə) (Userlərin sadəcə rolunu dəyişmək olacaq)
        Console.WriteLine("4. Delete"); //(Id-ə görə)
        Console.Write("Action: ");

        void askID()
        {
            Console.Write("ID: ");
            Int32.TryParse(Console.ReadLine(), out IInput);
        }

        switch (Console.ReadLine())
        {
            case "1":
                return 1;
            case "2":
                return 2;
            case "3":
                askID();
                return 3;
            case "4":
                askID();
                return 4;
        }

        return 0;
    }
    static void ProductCRUD()
    {
        switch (CRUDMenu())
        {
            case 1:
                Console.Write("\tName: ");
                SInput = Console.ReadLine();

                Console.Write("\tPrice: ");
                Decimal.TryParse(Console.ReadLine(), out DInput);

                Console.Write("\tCount: ");
                Int32.TryParse(Console.ReadLine(), out IInput);

                CompanyService.CreateProduct(SInput, DInput, IInput);
                break;
            case 2:
                CompanyService.GetProductsData().ForEach(x => Console.WriteLine(x.name));
                break;
            case 3:
                Console.WriteLine("\t1. Name");
                Console.WriteLine("\t2. Price");
                Console.WriteLine("\t3. Count");
                Console.Write("What to change?: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Write("New name: ");
                        CompanyService.UpdateProduct(Console.ReadLine(), IInput);
                        break;
                    case "2":
                        Console.Write("New price: ");
                        Decimal.TryParse(Console.ReadLine(), out DInput);
                        CompanyService.UpdateProduct(DInput, IInput);
                        break; 
                    case "3":
                        Console.Write("New count: ");
                        Int32.TryParse(Console.ReadLine(), out int count);
                        CompanyService.UpdateProduct(count, IInput);
                        break;
                }
                break;
            case 4:
                CompanyService.RemoveProduct(IInput);
                break;
        }
    }
    static void UserCRUD()
    {
        switch (CRUDMenu())
        {
            case 1:
                Register();
                break;
            case 2:
                CompanyService.GetUsersData().ForEach(x => Console.WriteLine(x));
                break;
            case 3:
                CompanyService.UpdateUser(IInput);
                break;
            case 4:
                CompanyService.RemoveUser(IInput);
                break;
        }
    }
    static void ProductMenu()
    {
        List<(int count, int id)> Basket = new List<(int count, int id)>();
        List<(int id, int count, decimal price, string name)> Data = CompanyService.GetProductsData();
        (int id, int count, decimal price, string name) data1;

        void CountPart()
        {
            for (int i = 0; i < Basket.Count;)
            {
                if (Basket[i].count != 0)
                {
                    i++;
                    continue;
                }
                data1 = Data.Find(x => x.id == Basket[i].id);
                Console.WriteLine("\n" + data1.name);

                Console.Write("How many you would like (or G to order more): ");
                SInput = Console.ReadLine();

                if (SInput == "G") break;

                Int32.TryParse(SInput, out IInput);
                if (IInput > 0 && data1.count >= IInput)
                {
                    Basket[i] = (IInput, Basket[i].id);
                    CompanyService.UpdateProduct(data1.count - IInput, data1.id);
                    i++;
                }
            }
            Console.WriteLine();
            Data.ForEach(x => Console.WriteLine(x.name));
        }
        Console.WriteLine();
        Data.ForEach(x => Console.WriteLine(x.name));

        do
        {
            Console.Write("Add to basket (ID or S to complete orders count or 0 to finish order): ");
            SInput = Console.ReadLine();
            if (SInput == "0") break;
            if (SInput == "S")
            {
                CountPart();
                //Data.ForEach(x => Console.WriteLine(x.name));
                continue;
            }

            Int32.TryParse(SInput, out IInput);
            if (IInput == 0) continue;
            data1 = Data.Find(x => x.id == IInput);

            if (data1.id != 0)
            {
                if(Basket.Find(b => b.id == data1.id).id == 0)
                {
                    Basket.Add((0, data1.id));
                    Console.WriteLine("added.");
                }
                else Console.WriteLine("Varda onsuzda");
            }
        } while (true);

        Basket = Basket.FindAll(b => b.count > 0);
        Basket.ForEach(b => CompanyService.AddToOrder(b.id, b.count));
    }
    static void OrderMenu()
    {
        var orders = CompanyService.GetOrders();

        if (orders.Count == 0)
        {
            Console.WriteLine("No proper orders made.");
            return;
        }

        decimal TotalPrice = 0;
        foreach (var item in orders)
        {
            Console.Write("\n\t" + item.name);
            TotalPrice += (item.price * (decimal)item.count);
        }

        Console.WriteLine("\n\tTotal price will be: " + TotalPrice);
        Console.WriteLine("0. Remove order");
        Console.WriteLine("1. Send them to new address");
        Console.WriteLine("2. Send them to last address");
        Console.Write("Action (any other to cansel): ");

        switch (Console.ReadLine())
        {
            case "0":
                Console.Write("ID of product: ");
                Int32.TryParse(Console.ReadLine(), out IInput);
                CompanyService.RemoveOrder(IInput);
                return;
            case "1":
                Console.Write("Address: ");
                SInput = Console.ReadLine();

                Console.Write("Phone number: ");
                CompanyService.MakeAdvanced(SInput, Console.ReadLine());
                break;
            case "2":
                if (!CompanyService.DoesHaveLastTime()) 
                {
                    Console.WriteLine("You dont have last time.");
                    return;
                }
                break;
            default:
                return;
        }

        CompanyService.SendOrders();
        Console.WriteLine("Bon apetit.");
    }
    static void NextMenu()
    {
        while (isLogin)
        {
            bool admin = CompanyService.IsAdmin();
            Console.WriteLine("\n0. Log out\n1. Look at pizzas\n2. Order menu");
            if (admin)
            {
                Console.WriteLine("3. Product CRUD (Pizzzas)");
                Console.WriteLine("4. User CRUD");
                Console.Write(MenuRequestMessage("Admin"));
            }
            else Console.Write(MenuRequestMessage("User"));

            switch (Console.ReadLine())
            {
                case "0":
                    isLogin = false;
                    break;
                case "1":
                    ProductMenu();
                    break;
                case "2":
                    OrderMenu();
                    break;
                case "3":
                    if (admin) ProductCRUD();
                    break;
                case "4":
                    if (admin) UserCRUD();
                    break;
                default:
                    break;
            }
        }
    }


    static void Main(string[] args)
    {
        bool admin = false;
        do
        {
            try
            {
                LoginMenu();
                NextMenu();
            }
            catch (ServiceExceptions ex)
            {
                Console.WriteLine(ex.Message);
            }

        } while (true);
    }
}