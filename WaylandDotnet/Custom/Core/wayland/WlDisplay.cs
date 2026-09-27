namespace WaylandDotnet;

using System;
using System.Diagnostics;
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

        Debug.Assert(handle != IntPtr.Zero);
        return new WlDisplay(handle);
    }

    /// <summary>
    /// Dispatch pending events
    /// </summary>
    public unsafe int Dispatch()
    {
        ObjectDisposedException.ThrowIf(disposed, this);
        Debug.Assert(Handle != IntPtr.Zero && !disposed);

        int code = WaylandNative.DisplayDispatch(Handle);
        Debug.Assert(code >= -1);

#if DEBUG
        if (code == -1)
        {
            int error = WaylandNative.DisplayGetError(Handle);

            WlInterface* iface;
            uint id;

            int protocolError = WaylandNative.DisplayGetProtocolError(Handle, &iface, &id);

            if (error == 71)
            {
                Debug.Assert(protocolError != 0, "EPROTO dispatch has no protocol error code");
                Debug.Assert(iface != null, "EPROTO dispatch has no interface");
                Debug.Assert(id != 0, "EPROTO dispatch has no object id");
            }
        }
#endif

        return code;
    }

    /// <summary>
    /// Dispatch pending events
    /// </summary>
    public int DispatchPending()
    {
        ObjectDisposedException.ThrowIf(disposed, this);
        Debug.Assert(Handle != IntPtr.Zero && !disposed);

        int code = WaylandNative.DispatchPending(Handle);
        Debug.Assert(code >= -1);
        return code;
    }

    /// <summary>
    /// Send requests and wait for events (blocking)
    /// </summary>
    public int Roundtrip()
    {
        ObjectDisposedException.ThrowIf(disposed, this);
        Debug.Assert(Handle != IntPtr.Zero && !disposed);

        int code = WaylandNative.DisplayRoundtrip(Handle);
        Debug.Assert(code >= -1);
        return code;
    }

    /// <summary>
    /// Disconnect from the Wayland display
    /// </summary>
    public void Disconnect()
    {
        ObjectDisposedException.ThrowIf(disposed, this);
        Debug.Assert(Handle != IntPtr.Zero && !disposed);
        WaylandNative.DisplayDisconnect(Handle);
        disposed = true;
    }

    /// <summary>
    /// Flush buffered requests to the server
    /// </summary>
    public int Flush()
    {
        ObjectDisposedException.ThrowIf(disposed, this);
        Debug.Assert(Handle != IntPtr.Zero && !disposed);

        int bytes = WaylandNative.DisplayFlush(Handle);
        Debug.Assert(bytes >= -1);
        return bytes;
    }

    /// <summary> Converts a display wrapper to its native handle. </summary>
    /// <param name="from">The display wrapper.</param>
    public static implicit operator IntPtr(WlDisplay? from) => from?.Handle ?? IntPtr.Zero;
}