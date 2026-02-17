namespace WaylandDotnet.Internal;

using System.Runtime.InteropServices;

public unsafe static partial class WaylandInterfaces
{
    // Static constructor ensures Core interfaces are initialized first
    // before Unstable/Stable interfaces that depend on them
    static WaylandInterfaces()
    {
        // Force initialization of Core interface static fields
        // by accessing them before any Unstable/Stable interfaces are created
        _ = WlDisplay;
        _ = WlRegistry;
        _ = WlCompositor;
        _ = WlSurface;
        _ = WlOutput;
        _ = WlShm;
    }

    private static IntPtr CreateTypesArray(WlInterface*[] types)
    {
        if (types.Length == 0) return IntPtr.Zero;

        var size = IntPtr.Size * types.Length;
        var ptr = Marshal.AllocHGlobal(size);

        for (int i = 0; i < types.Length; i++)
        {
            // Write the pointer value (WlInterface*), not the address of the pointer
            // types[i] is already WlInterface* (pointer to the interface struct)
            IntPtr typePtr = types[i] == null ? IntPtr.Zero : (IntPtr)types[i];
            Marshal.WriteIntPtr(ptr, i * IntPtr.Size, typePtr);
        }

        return ptr;
    }

    public static WlInterface* GetInterfacePtr(string interfaceName)
    {
        var ptr =
            GetCoreRuntimeInterface(interfaceName) ??
            GetStableRuntimeInterface(interfaceName) ??
            GetWlrRuntimeInterface(interfaceName) ??
            throw new InvalidOperationException($"Interface '{interfaceName}' not found");

        return (WlInterface*)ptr;
    }
}