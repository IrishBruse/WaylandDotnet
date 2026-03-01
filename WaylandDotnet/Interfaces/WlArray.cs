namespace WaylandDotnet;

public unsafe struct WlArray
{
    public int size;
    public int alloc;
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