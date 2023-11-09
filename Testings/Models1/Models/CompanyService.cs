namespace Core.Models;

public static class CompanyService
{
    static User _user { get; set; }
    public enum KnownProductTypes
    {
        pizza
    }

    public static bool LoginUser(string username, string password)
    {
        return default;
    }
    public static bool RegisterUser(string name, string surname, string username, string password)
    {
        return default;
    }
    public static string[] KnownProducts()
    {
        return Enum.GetNames(typeof(KnownProductTypes));
    }
    public static bool CreateProduct(KnownProductTypes type = KnownProductTypes.pizza)
    {
        return default;
    }
    public static string[] GetUserNames()
    {
        return default;
    }
    public static string[] GetProductNames()
    {
        return default;
    }
    public static bool UpdateUser()
    {
        return default;
    }
    public static bool UpdateProduct()
    {
        return default;
    }
    public static bool RemoveUser()
    {
        return default;
    }
    public static bool RemoveProduct()
    {
        return default;
    }
}