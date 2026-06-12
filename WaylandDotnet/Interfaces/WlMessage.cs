namespace WaylandDotnet;

using System.Runtime.InteropServices;

/// <summary> Native Wayland request or event message descriptor. </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct WlMessage(byte* name, byte* signature, WlInterface** types)
{
    /// <summary> Message name. </summary>
    public byte* Name = name;

    /// <summary> Wire argument signature. </summary>
    public byte* Signature = signature;

    /// <summary> Referenced interface types. </summary>
    public WlInterface** Types = types;
}

// struct wl_message {
//     const char *name;
//     const char *signature;
//     const struct wl_interface **types;
// };