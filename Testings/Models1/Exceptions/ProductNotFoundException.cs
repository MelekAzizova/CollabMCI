namespace Core.Exceptions;

public class ProductNotFoundException : ProductExceptions
{
    public ProductNotFoundException(string msg = "Product Not Found") : base(msg) { }
}
