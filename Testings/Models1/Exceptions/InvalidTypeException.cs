namespace Core.Exceptions;

public class InvalidTypeException : ProductExceptions
{
    public InvalidTypeException(string msg = "Invalid product type.") : base(msg) { }
}