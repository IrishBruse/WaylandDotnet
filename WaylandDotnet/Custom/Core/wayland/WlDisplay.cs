namespace WaylandDotnet;

using System;
using WaylandDotnet.Internal;

public sealed partial class WlDisplay
{

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

        return new WlDisplay(handle, null);
    }

    /// <summary>
    /// Dispatch pending events
    /// </summary>
    public unsafe int Dispatch()
    {
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
    public int DispatchPending() => WaylandNative.DispatchPending(Handle);

    /// <summary>
    /// Send requests and wait for events (blocking)
    /// </summary>
    public int Roundtrip() => WaylandNative.DisplayRoundtrip(Handle);

    /// <summary>
    /// TODO: move to dispose
    /// </summary>
    public void Disconnect() => WaylandNative.DisplayDisconnect(Handle);

    /// <summary>
    /// Flush buffered requests to the server
    /// </summary>
    public int Flush() => WaylandNative.DisplayFlush(Handle);

    public static implicit operator IntPtr(WlDisplay? from) => from?.Handle ?? IntPtr.Zero;
}