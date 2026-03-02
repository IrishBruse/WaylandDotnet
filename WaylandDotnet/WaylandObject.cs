namespace WaylandDotnet;

using System;

/// <summary>
/// Base class for all Wayland objects
/// </summary>
public abstract class WaylandObject : IDisposable
{
    internal bool disposed;

    public IntPtr Handle { get; set; }
    public WlDisplay? Display { get; }

    protected void CheckDisposed()
    {
        ObjectDisposedException.ThrowIf(disposed, this);
    }

    /// <summary>
    /// Dispose pattern for derived classes to override
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                // Dispose managed resources
            }

            disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~WaylandObject()
    {
        Dispose(false);
    }
}