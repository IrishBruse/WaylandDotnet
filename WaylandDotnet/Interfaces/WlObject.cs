namespace WaylandDotnet;

public unsafe struct WlObject
{
    public WlInterface* Interface;
    public void* Implementation;
    public uint Id;
}

// struct wl_object {
// 	const struct wl_interface *interface;
// 	const void *implementation;
// 	uint32_t id;
// };