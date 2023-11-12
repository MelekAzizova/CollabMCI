using Core.Exceptions;

namespace Core.Models;

public enum KnownProductTypes
{
    none,
    pizza
}
internal abstract class Product
{
    KnownProductTypes _type = KnownProductTypes.none;
    static int _uniqueID = 1;

    public int ID { get; protected set; }
    public string Name { get; set; }
    public decimal Price { get; set; } = 45;
    public int Count { get; set; } = 0;

    public KnownProductTypes Type 
    { 
        get => this._type;
        set
        {
            if (!Enum.IsDefined<KnownProductTypes>(value)) throw new InvalidTypeException();
            this._type = value;
        }
    }

    public void UpdateID()
    {
        this.ID = Product._uniqueID++;
    }
    public abstract Product CopyItForOrder(int count);

    public override string ToString()
    {
        return "[" + this.ID + "]: " + this.Name + " with price: " + this.Price + "$ (with count of " + this.Count + ")";
    }
}
