namespace Core.Exceptions;

public class InvalidRoleException : UserExceptions
{
    public InvalidRoleException(string msg = "Invalid User role.") : base(msg) { }
}
