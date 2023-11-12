namespace Core.Exceptions;

public class UserNotFoundException : UserExceptions
{
    public UserNotFoundException(string msg = "User not founded.") : base(msg) { }
}
