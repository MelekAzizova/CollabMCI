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
            username = CompanyService._user.Name + " " + CompanyService._user.Surname 
                + "\n" + (int)CompanyService._user.Role;
            return true;
        }
    }

    public static bool RegisterUser(string name, string surname, string username, string password)
    {
        CompanyService._user = CompanyDB.UserList.SingleOrDefault(u => u.Username == username);
        if (CompanyService._user == null)
        {
            CompanyService._user = new();
            CompanyService._user.Name = name;
            CompanyService._user.Surname = surname;
            CompanyService._user.Username = username;
            CompanyService._user.Password = password;

            CompanyService._user.Role = Roles.user;
            CompanyService._user.UpdateID();

            CompanyDB.UserList.Add(CompanyService._user);
            return true;
        }
        else return false;
    }
    public static bool CreateProduct(string name, decimal price, int count, KnownProductTypes type = KnownProductTypes.pizza)
    {
        Product product = null;
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

    public static void UpdateUser(int id) => CompanyDB.UserList.SingleOrDefault(u => u.ID == id).Role = Roles.admin;
    public static void UpdateProduct(int count, int id) => CompanyDB.ProductList.SingleOrDefault(p => p.ID == id).Count = count;
    public static void UpdateProduct(decimal price, int id) => CompanyDB.ProductList.SingleOrDefault(p => p.ID == id).Price = price;
    public static void UpdateProduct(string name, int id) => CompanyDB.ProductList.SingleOrDefault(p => p.ID == id).Name = name;

    public static bool RemoveUser(int id)
    {
        if (id == CompanyService._user.ID) return false;

        User user = CompanyDB.UserList.SingleOrDefault(u => u.ID == id);

        if (user != null) 
        {
            CompanyDB.UserList.Remove(user);
            return true;
        }

        return false;
    }
    public static bool RemoveProduct(int id)
    {
        Product product = CompanyDB.ProductList.SingleOrDefault(u => u.ID == id);

        if (product != null)
        {
            CompanyDB.ProductList.Remove(product);
            return true;
        }

        return false;
    }

    public static void AddToOrder(int id, int count)
    {
        CompanyDB.OrderList.Add((CompanyService._user.ID, CompanyDB.ProductList.Find(p => p.ID == id).CopyItForOrder(count), false));
    }
    public static void RemoveOrders()
    {
        foreach (var item in CompanyDB.OrderList.FindAll(o => !o.isSend && CompanyService._user.ID == o.userID))
        {
            CompanyDB.ProductList.Find(p => p.ID == item.product.ID).Count += item.product.Count;
            CompanyDB.OrderList.Remove(item);
        }
    }
    public static List<(string name, decimal price, int count)> GetOrders()
    {
        List<(string, decimal, int)> Orders = new List<(string, decimal, int)>();
        CompanyDB.OrderList.FindAll(o => (!o.isSend) && CompanyService._user.ID == o.userID)
            .ForEach(o => Orders.Add((o.product.ToString(), o.product.Price, o.product.Count)));
        return Orders;
    }
    public static void SendOrders()
    {
        for (int i = 0; i < CompanyDB.OrderList.Count; i++)
        {
            var item = CompanyDB.OrderList[i];
            if (item.isSend || item.userID != CompanyService._user.ID) continue;
            CompanyDB.OrderList[i] = (item.userID, item.product, true);
        }
    }
}