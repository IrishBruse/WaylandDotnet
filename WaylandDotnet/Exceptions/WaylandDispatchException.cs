namespace WaylandDotnet;

/// <summary> Thrown when dispatching Wayland events fails. </summary>
public class WaylandDispatchException : Exception
{
    /// <summary> Initializes a new instance of the <see cref="WaylandDispatchException"/> class. </summary>
    public WaylandDispatchException()
    {
    }

    /// <summary> Initializes a new instance of the <see cref="WaylandDispatchException"/> class. </summary>
    /// <param name="message">The error message.</param>
    public WaylandDispatchException(string? message) : base(message)
    {
    }

    /// <summary> Initializes a new instance of the <see cref="WaylandDispatchException"/> class. </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public WaylandDispatchException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}