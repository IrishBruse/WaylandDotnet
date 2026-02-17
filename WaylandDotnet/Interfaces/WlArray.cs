namespace WaylandDotnet;

public readonly unsafe struct WlArray
{
    public readonly int size;
    public readonly int alloc;
    public readonly void* data;
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