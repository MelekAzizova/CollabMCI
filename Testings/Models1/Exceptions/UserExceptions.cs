namespace Core.Exceptions;

public abstract class UserExceptions : ServiceExceptions
{
    public UserExceptions(string msg = "Not determined User exception.") : base(msg) { }
}
