namespace WaylandDotnet;

using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Explicit)]
public unsafe struct WlArgument
{
    [FieldOffset(0)] public int i;
    [FieldOffset(0)] public uint u;
    [FieldOffset(0)] public WlFixed f;
    [FieldOffset(0)] public byte* s;
    [FieldOffset(0)] public WlObject* o;
    [FieldOffset(0)] public uint n;
    [FieldOffset(0)] public WlArray* a;
    [FieldOffset(0)] public int h;
}

// union wl_argument {
//     int32_t i;
//     uint32_t u;
//     fixed f;
//     const char *s;
//     struct wl_object *o;
//     uint32_t n;
//     struct wl_array *a;
//     int32_t h;
// };