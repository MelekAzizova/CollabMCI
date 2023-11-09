using System.Linq;

namespace Core.Models;

public static class CompanyService
{
    static User _user { get; set; }
    public enum KnownProductTypes
    {
        pizza
    }

    public static void LoginUser(string username, string password)
    {
        Console.WriteLine("We're so excited to see you again");
        _user = CompanyDB.UserList.SingleOrDefault(u => u.Name == username && u.Password == password);
        if(_user != null)
        {
            Console.WriteLine($"Welcome  {username}  ");
        }
        else
        {
            Console.WriteLine("Invalid username or password.Please again try");
        }
        
    }
    public static void RegisterUser(string name, string surname, string username, string password)
    {
        Console.WriteLine("Enter name: ");
        name=Console.ReadLine();
        if(name==null)
        {
            throw new ArgumentNullException();
        }

        Console.WriteLine("Enter surname: ");
        surname=Console.ReadLine();
        if(surname==null)
        {
            throw new ArgumentNullException();
        }



    }
    public static string[] KnownProducts()
    {

        return Enum.GetNames(typeof(KnownProductTypes));
    }
    public static void CreateProduct(string name, int price,KnownProductTypes type = KnownProductTypes.pizza)
    {
        Product product;
        switch (type)
        {
            case KnownProductTypes.pizza:
                product = new Pizza();
                break;
           
        }
       
    }
    public static string[] GetUserNames()
    {
        string[] username = new string[CompanyDB.UserList.Count];
        for (int i = 0; i < username.Length; i++)
        {
            username[i] = CompanyDB.UserList[i].Username;
        }
        return username;
    }
    public static string[] GetProductNames()
    {
        string[] names = new string[CompanyDB.ProductList.Count];
        for (int i = 0; i < names.Length; i++)
        {
            names[i] = CompanyDB.ProductList[i].Name;
        }
        return names;
    }
    public static bool UpdateUser()
    {
        

        return default;
    }
    public static bool UpdateProduct()
    {
        return default;
    }
    public static bool RemoveUser(int id)
    {
        User user =  CompanyDB.UserList.Find(u => u.ID == id);
        if(user != null)
        {

         CompanyDB.UserList.Remove(user);
            return true;
        }

        return false;
    }
    public static bool RemoveProduct(int id)
    {
        Product product = CompanyDB.ProductList.Find(u => u.ID == id);
        if (product != null)
        {

            CompanyDB.ProductList.Remove(product);
            return true;
        }

        return false;
    }
}