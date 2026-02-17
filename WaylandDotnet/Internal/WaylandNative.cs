namespace WaylandDotnet.Internal;

using System;
using System.Runtime.InteropServices;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void wl_log_func_t(IntPtr fmt, IntPtr args);

/// <summary>
/// /// <summary> LibraryImport </summary>
/// Native Wayland library imports (AOT-friendly using LibraryImport)
/// </summary>
public static partial class WaylandNative
{
    private const string LibWayland = "libwayland-client.so.0";
    // private const string LibWayland = "libwayland-client-debug.so";

    [LibraryImport(LibWayland, EntryPoint = "wl_log_set_handler_client")]
    public static partial void LogSetHandlerClient(wl_log_func_t handler);

    /// <summary> wl_display_connect </summary>
    [LibraryImport(LibWayland, EntryPoint = "wl_display_connect")]
    public static partial IntPtr DisplayConnect(IntPtr name);

    /// <summary> wl_display_disconnect </summary>
    [LibraryImport(LibWayland, EntryPoint = "wl_display_disconnect")]
    public static partial void DisplayDisconnect(IntPtr display);

    [LibraryImport(LibWayland, EntryPoint = "wl_display_dispatch_pending")]
    public static partial int DispatchPending(IntPtr display);

    /// <summary> wl_display_dispatch </summary>
    [LibraryImport(LibWayland, EntryPoint = "wl_display_dispatch")]
    public static partial int DisplayDispatch(IntPtr display);

    /// <summary> wl_display_get_error </summary>
    [LibraryImport(LibWayland, EntryPoint = "wl_display_get_error")]
    public static partial int DisplayGetError(IntPtr display);

    /// <summary> wl_display_get_protocol_error </summary>
    [LibraryImport(LibWayland, EntryPoint = "wl_display_get_protocol_error")]
    public static unsafe partial int DisplayGetProtocolError(IntPtr display, WlInterface** iface, uint* id);

    /// <summary> wl_display_roundtrip </summary>
    [LibraryImport(LibWayland, EntryPoint = "wl_display_roundtrip")]
    public static partial int DisplayRoundtrip(IntPtr display);

    /// <summary> wl_display_flush </summary>
    [LibraryImport(LibWayland, EntryPoint = "wl_display_flush")]
    public static partial int DisplayFlush(IntPtr display);

    /// <summary> wl_proxy_marshal_array_flags </summary>
    [LibraryImport(LibWayland, EntryPoint = "wl_proxy_marshal_array_flags")]
    public static unsafe partial IntPtr ProxyMarshalArrayFlags(IntPtr proxy, uint opcode, WlInterface* interfacePtr, uint version, uint flags, IntPtr args);

    /// <summary> wl_proxy_add_dispatcher </summary>
    [LibraryImport(LibWayland, EntryPoint = "wl_proxy_add_dispatcher")]
    public unsafe static partial int ProxyAddDispatcher(
        IntPtr proxy,
        delegate* unmanaged<
            IntPtr,        // implementation
            IntPtr,        // data
            uint,          // opcode
            WlMessage*,    // message
            WlArgument*,   // args
            int
        > dispatcher,
        IntPtr implementation,
        IntPtr data
    );
}