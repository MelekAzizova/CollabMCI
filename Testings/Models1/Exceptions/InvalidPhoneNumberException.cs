namespace Core.Exceptions;

public class InvalidPhoneNumberException : UserExceptions
{
    public InvalidPhoneNumberException(string msg = "Invalid address.") : base(msg) { }
}
