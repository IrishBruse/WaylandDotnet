namespace WaylandDotnet;

using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WlMessage(byte* name, byte* signature, WlInterface** types)
{
    public byte* Name = name;
    public byte* Signature = signature;
    public WlInterface** Types = types;
}

// struct wl_message {
//     const char *name;
//     const char *signature;
//     const struct wl_interface **types;
// };