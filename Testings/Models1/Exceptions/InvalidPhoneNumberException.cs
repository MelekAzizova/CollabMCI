namespace Core.Exceptions;

public class InvalidPhoneNumberException : UserExceptions
{
    public InvalidPhoneNumberException(string msg = "Invalid phone number.") : base(msg) { }
}
