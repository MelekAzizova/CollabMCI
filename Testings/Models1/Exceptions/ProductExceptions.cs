namespace Core.Exceptions;

public abstract class ProductExceptions : ServiceExceptions
{
    public ProductExceptions(string msg = "Not determined Product exception.") : base(msg) { }
}
