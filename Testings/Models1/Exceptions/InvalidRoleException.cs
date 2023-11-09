namespace Core.Exceptions;

public class InvalidRoleException : UserExceptions
{
    public InvalidRoleException(string msg = "Invalid Role.") : base(msg) { }
}
