namespace WaylandDotnet;

using System;
using WaylandDotnet.Internal;

public sealed partial class WlDisplay
{
    private bool disposed;

    /// <summary>
    /// Connect to the Wayland display
    /// </summary>
    /// <param name="name">Display name (null for default)</param>
    /// <returns>Connected display</returns>
    public static WlDisplay Connect(string? name = null)
    {
        IntPtr namePtr = IntPtr.Zero;
        if (name != null)
        {
            throw new NotImplementedException("Named connections not yet implemented");
        }

        var handle = WaylandNative.DisplayConnect(namePtr);
        if (handle == IntPtr.Zero)
        {
            throw new InvalidOperationException("Failed to connect to Wayland display");
        }

        return new WlDisplay(handle);
    }

    /// <summary>
    /// Dispatch pending events
    /// </summary>
    public unsafe int Dispatch()
    {
        ObjectDisposedException.ThrowIf(disposed, this);

        int code = WaylandNative.DisplayDispatch(Handle);

        if (code == -1)
        {
            int error = WaylandNative.DisplayGetError(Handle);

            WlInterface* iface;
            uint id;

            int protocolError = WaylandNative.DisplayGetProtocolError(Handle, &iface, &id);
        }

        return code;
    }

    /// <summary>
    /// Dispatch pending events
    /// </summary>
    public int DispatchPending()
    {
        ObjectDisposedException.ThrowIf(disposed, this);
        return WaylandNative.DispatchPending(Handle);
    }

    /// <summary>
    /// Send requests and wait for events (blocking)
    /// </summary>
    public int Roundtrip()
    {
        ObjectDisposedException.ThrowIf(disposed, this);
        return WaylandNative.DisplayRoundtrip(Handle);
    }

    /// <summary>
    /// Disconnect from the Wayland display
    /// </summary>
    public void Disconnect()
    {
        ObjectDisposedException.ThrowIf(disposed, this);
        WaylandNative.DisplayDisconnect(Handle);
        disposed = true;
    }

    /// <summary>
    /// Flush buffered requests to the server
    /// </summary>
    public int Flush()
    {
        ObjectDisposedException.ThrowIf(disposed, this);
        return WaylandNative.DisplayFlush(Handle);
    }

    public static implicit operator IntPtr(WlDisplay? from) => from?.Handle ?? IntPtr.Zero;
}