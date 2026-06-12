namespace WaylandDotnet;

using System.Runtime.InteropServices;

/// <summary> Native Wayland interface descriptor. </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct WlInterface(byte* name, int version, int methodCount, WlMessage* methods, int eventCount, WlMessage* events)
{
    /// <summary> Interface name. </summary>
    public byte* Name = name;

    /// <summary> Interface version. </summary>
    public int Version = version;

    /// <summary> Number of request opcodes. </summary>
    public int MethodCount = methodCount;

    /// <summary> Request message descriptors. </summary>
    public WlMessage* Methods = methods;

    /// <summary> Number of event opcodes. </summary>
    public int EventCount = eventCount;

    /// <summary> Event message descriptors. </summary>
    public WlMessage* Events = events;
}

// struct wl_interface {
//     const char *name;
//     int version;
//     int method_count;
//     const struct wl_message *methods; // Array of messages (opcodes)
//     int event_count;
//     const struct wl_message *events;
// };