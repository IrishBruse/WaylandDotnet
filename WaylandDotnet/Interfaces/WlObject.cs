namespace WaylandDotnet;

/// <summary> Native Wayland object header. </summary>
public unsafe struct WlObject
{
    /// <summary> The object's interface. </summary>
    public WlInterface* Interface;

    /// <summary> Implementation-specific data. </summary>
    public void* Implementation;

    /// <summary> Object ID on the connection. </summary>
    public uint Id;
}

// struct wl_object {
// 	const struct wl_interface *interface;
// 	const void *implementation;
// 	uint32_t id;
// };