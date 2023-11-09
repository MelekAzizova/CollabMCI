namespace Core.Exceptions;

public class InvalidPasswordException : UserExceptions
{
    public InvalidPasswordException(string msg = "Invalid password. Length should be 6-16 characters.") : base(msg) { }
}