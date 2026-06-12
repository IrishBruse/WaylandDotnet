namespace WaylandDotnet;

using System.Runtime.InteropServices;

/// <summary> Native Wayland message argument union. </summary>
[StructLayout(LayoutKind.Explicit)]
public unsafe struct WlArgument
{
    /// <summary> Signed 32-bit integer argument. </summary>
    [FieldOffset(0)] public int i;

    /// <summary> Unsigned 32-bit integer argument. </summary>
    [FieldOffset(0)] public uint u;

    /// <summary> Fixed-point argument. </summary>
    [FieldOffset(0)] public WlFixed f;

    /// <summary> NUL-terminated string argument. </summary>
    [FieldOffset(0)] public byte* s;

    /// <summary> Object argument. </summary>
    [FieldOffset(0)] public WlObject* o;

    /// <summary> New object ID argument. </summary>
    [FieldOffset(0)] public uint n;

    /// <summary> Array argument. </summary>
    [FieldOffset(0)] public WlArray* a;

    /// <summary> File descriptor argument. </summary>
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