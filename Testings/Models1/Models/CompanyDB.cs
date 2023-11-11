namespace Core.Models;

internal static class CompanyDB
{
    internal static List<User> UserList = new List<User>();
    internal static List<Product> ProductList = new List<Product>();
    internal static List<(int userID, Product product, bool isSend)> OrderList = new List<(int, Product, bool)>();
    
    static CompanyDB()
    {
        User admin = new User()
        {
            Name = "Jafar",
            Surname = "Zorzade",
            Username = "admin1",
            Password = "Admin1",
            Role = Roles.admin
        };
        admin.UpdateID();
        CompanyDB.UserList.Add(admin);

        Product pizaa = new Pizza()
        {
            Name = "Matsorella",
            Price = (decimal)1.99f,
            Count = 99,
            Type = KnownProductTypes.pizza
        };
        pizaa.UpdateID();
        CompanyDB.ProductList.Add(pizaa);
    }
}
