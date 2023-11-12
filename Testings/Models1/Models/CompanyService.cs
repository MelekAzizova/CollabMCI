using Core.Exceptions;

namespace Core.Models;

public static class CompanyService
{
    static User _user;
    public static bool LoginUser(ref string username, string password)
    {
        string localname = username;
        CompanyService._user = CompanyDB.UserList.SingleOrDefault(u => u.Username == localname && u.Password == password);
        
        if (CompanyService._user == null) return false;
        else
        {
            username = CompanyService._user.Name + " " + CompanyService._user.Surname;
            return true;
        }
    }
    public static bool IsAdmin()
    {
        return CompanyService._user.Role == Roles.admin;
    }


    public static bool RegisterUser(string name, string surname, string username, string password)
    {
        User user = CompanyDB.UserList.SingleOrDefault(u => u.Username == username);
        if (user == null)
        {
            user = new();
            user.Name = name;
            user.Surname = surname;
            user.Username = username;
            user.Password = password;

            user.Role = Roles.user;
            user.UpdateID();

            CompanyDB.UserList.Add(user);
            return true;
        }
        else return false;
    }
    public static bool CreateProduct(string name, decimal price, int count, KnownProductTypes type = KnownProductTypes.pizza)
    {
        Product product;
        switch (type)
        {
            case KnownProductTypes.pizza:
                product = new Pizza();
                break;
            default:
                return false;
        }

        product.Name = name;
        product.Price = price;
        product.Count = count;
        product.Type = type;

        product.UpdateID();

        CompanyDB.ProductList.Add(product);
        return true;
    }


    public static List<(int id, int count, decimal price, string name)> GetProductsData(KnownProductTypes type = KnownProductTypes.pizza)
    {
        List<(int id, int count, decimal price, string name)> data = new List<(int id, int count, decimal price, string name)>();

        if (type == KnownProductTypes.none)
        {
            CompanyDB.ProductList.ForEach(p => data.Add((p.ID, p.Count, p.Price, p.ToString())));
        }
        else
        {
            CompanyDB.ProductList.FindAll(p => p.Type == type).ForEach(p => data.Add((p.ID, p.Count, p.Price, p.ToString())));
        }

        return data;
    }
    public static List<string> GetUsersData()
    {
        List<string> names = new List<string>();
        CompanyDB.UserList.ForEach(u => names.Add(u.ToString()));
        return names;
    }


    static Product ThatProduct(int id)
    {
        Product product = CompanyDB.ProductList.SingleOrDefault(p => p.ID == id);
        if (product == null) throw new ProductNotFoundException();
        return product;
    }
    static User ThatUser(int id)
    {
        User user = CompanyDB.UserList.SingleOrDefault(u => u.ID == id);
        if (user == null) throw new UserNotFoundException();
        return user;
    }
    public static void UpdateUser(int id) => ThatUser(id).Role = Roles.admin;
    public static void UpdateProduct(int count, int id) => ThatProduct(id).Count = count;
    public static void UpdateProduct(decimal price, int id) => ThatProduct(id).Price = price;
    public static void UpdateProduct(string name, int id) => ThatProduct(id).Name = name;


    public static bool RemoveUser(int id)
    {
        if (id == CompanyService._user.ID) return false;
        User user = ThatUser(id);
        CompanyDB.UserList.Remove(user);
        return true;
    }
    public static bool RemoveProduct(int id)
    {
        Product product = ThatProduct(id);

        if (product != null)
        {
            CompanyDB.ProductList.Remove(product);
            return true;
        }

        return false;
    }


    public static void AddToOrder(int id, int count)
    {
        Product product = ThatProduct(id);
        if (product == null) return;
        CompanyDB.OrderList.Add((CompanyService._user, product.CopyItForOrder(count), false));
    }
    public static void RemoveOrder(int id)
    {
        var item = CompanyDB.OrderList.Find(o => !o.isSend && CompanyService._user.ID == o.user.ID && o.product.ID == id);
        if (item.product == null) return;

        ThatProduct(id).Count += item.product.Count;
        CompanyDB.OrderList.Remove(item);
    }

    public static List<(string name, decimal price, int count)> GetOrders()
    {
        List<(string, decimal, int)> Orders = new List<(string, decimal, int)>();
        CompanyDB.OrderList.FindAll(o => (!o.isSend) && CompanyService._user.ID == o.user.ID)
            .ForEach(o => Orders.Add((o.product.ToString(), o.product.Price, o.product.Count)));
        return Orders;
    }
    public static bool DoesHaveLastTime()
    {
        if (CompanyService._user.Adress == "none") return false;
        if (CompanyService._user.PhoneNumber == "none") return false;
        return true;
    }
    public static bool SendOrders()
    { 
        bool once = false;
        for (int i = 0; i < CompanyDB.OrderList.Count; i++)
        {
            var item = CompanyDB.OrderList[i];
            if (item.isSend || item.user.ID != CompanyService._user.ID) continue;
            CompanyDB.OrderList[i] = (item.user, item.product, true);
            once = true;
        }

        return once;
    }
    public static void MakeAdvanced(string address, string phone)
    {
        CompanyService._user.Adress = address;
        CompanyService._user.PhoneNumber = phone;
    }
}