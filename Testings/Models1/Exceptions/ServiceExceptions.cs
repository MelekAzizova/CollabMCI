namespace Core.Exceptions;

public abstract class ServiceExceptions : Exception
{
    public ServiceExceptions(string msg = "Not determined Service exception.") : base(msg) { }
}
