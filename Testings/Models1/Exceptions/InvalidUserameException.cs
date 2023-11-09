namespace Core.Exceptions;

public class InvalidUserameException : UserExceptions
{
    public InvalidUserameException (string msg = "Invalid username (should be in length of 3-16 characters).") : base(msg) { }
}
