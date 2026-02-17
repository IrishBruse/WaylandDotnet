namespace WaylandDotnet;

public class WaylandDispatchException : Exception
{
    public WaylandDispatchException()
    {
    }

    public WaylandDispatchException(string? message) : base(message)
    {
    }

    public WaylandDispatchException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
