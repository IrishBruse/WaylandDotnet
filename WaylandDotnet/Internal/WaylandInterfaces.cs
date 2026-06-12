namespace WaylandDotnet.Internal;

using System.Runtime.InteropServices;

/// <summary> Native Wayland interface registry and lookup helpers. </summary>
public unsafe static partial class WaylandInterfaces
{
    private static Dictionary<string, IntPtr> Interfaces = new();

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

    /// <summary> Returns the native interface descriptor for the given interface name. </summary>
    /// <param name="interfaceName">The Wayland interface name.</param>
    /// <returns>A pointer to the interface descriptor.</returns>
    /// <exception cref="InvalidOperationException">The interface was not registered.</exception>
    public static WlInterface* GetInterfacePtr(string interfaceName)
    {
        if (Interfaces.TryGetValue(interfaceName, out nint ptr))
        {
            return (WlInterface*)ptr;
        }

        throw new InvalidOperationException($"Interface {interfaceName} not found");
    }

    private static WlInterface* AllocateInterface()
    {
        var size = Marshal.SizeOf<WlInterface>();
        var ptr = (WlInterface*)Marshal.AllocHGlobal(size);
        return ptr;
    }
}