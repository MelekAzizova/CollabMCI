namespace Core.Models;

internal class Pizza : Product
{
    public override string ToString()
    {
        return "Pizza " + this.Name;
    }
}
