namespace WaylandDotnet;

using System;

/// <summary>
/// Base class for all Wayland objects
/// </summary>
public class WaylandObject : IDisposable
{
    internal bool disposed;

    private nint handle;
    public IntPtr Handle { get => handle; private set => handle = value; }

    public WlDisplay Display { get; }// TODO: possibly remove
    public string InterfaceName { get; }
    public uint Version { get; }

    internal WaylandObject(IntPtr handle, WlDisplay display, string interfaceName, uint version)
    {
        Handle = handle;
        Display = display;
        InterfaceName = interfaceName;
        Version = version;
    }

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