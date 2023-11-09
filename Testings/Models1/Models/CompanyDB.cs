namespace Core.Models;

internal static class CompanyDB
{
    internal static List<User> UserList = new List<User>();
    internal static List<Product> ProductList = new List<Product>();
    internal static List<(User, Product)> OrderList = new List<(User, Product)>();
}
