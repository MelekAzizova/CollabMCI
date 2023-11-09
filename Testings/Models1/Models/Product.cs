namespace Core.Models;

internal abstract class Product
{
    static int _uniqueID = 1;

    public int ID { get; private set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public void UpdateID()
    {
        this.ID = Product._uniqueID++;
    }
}
