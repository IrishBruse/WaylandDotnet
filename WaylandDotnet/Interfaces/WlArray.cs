namespace WaylandDotnet;

/// <summary> Native Wayland variable-length byte array. </summary>
public unsafe struct WlArray
{
    /// <summary> Number of bytes in use. </summary>
    public int size;

    /// <summary> Allocated capacity in bytes. </summary>
    public int alloc;

    /// <summary> Pointer to array data. </summary>
    public void* data;
}

// struct wl_array
// {
//     /** Array size */
//     size_t size;
//     /** Allocated space */
//     size_t alloc;
//     /** Array data */
//     void* data;
// };