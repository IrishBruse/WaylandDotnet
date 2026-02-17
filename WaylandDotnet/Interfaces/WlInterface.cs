namespace WaylandDotnet;

using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct WlInterface(byte* name, int version, int methodCount, WlMessage* methods, int eventCount, WlMessage* events)
{
    public byte* Name = name;
    public int Version = version;
    public int MethodCount = methodCount;
    public WlMessage* Methods = methods;
    public int EventCount = eventCount;
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