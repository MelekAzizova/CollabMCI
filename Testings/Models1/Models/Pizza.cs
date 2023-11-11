namespace Core.Models;

internal class Pizza : Product
{
    public override Product CopyItForOrder(int count)
    {
        Pizza product = new Pizza();

        product.ID = this.ID;
        product.Name = this.Name;
        product.Price = this.Price;
        product.Count = count;
        product.Type = this.Type;

        return product;
    }
}
