namespace Core.Exceptions;

public class InvalidAddressException : UserExceptions
{
    public InvalidAddressException(string msg = "Invalid address.") : base(msg) { }
}
