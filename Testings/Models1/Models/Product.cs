namespace Core.Models;

internal abstract class Product
{
    static int _uniqueID = 1;

    public int ID { get; }
    public string Name { get; set; }
    public float Price { get; set; }
}
