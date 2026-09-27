namespace WaylandDotnet;

using System.Diagnostics;
using System.Runtime.InteropServices.Marshalling;
using WaylandDotnet.Internal;

public sealed partial class WlRegistry : WaylandObject
{
    /// <summary>
    /// Binds a global registry entry to a typed Wayland object.
    /// </summary>
    /// <typeparam name="T">The interface type to bind.</typeparam>
    /// <param name="name">The global object name.</param>
    /// <param name="version">The interface version to request.</param>
    /// <returns>The bound object.</returns>
    public unsafe T Bind<T>(uint name, uint version) where T : WaylandObject, IWaylandObjectFactory<T>
    {
        const uint opcode = 0;

        Debug.Assert(version >= 1);

        var interfaceName = T._StaticInterfaceName;
        Debug.Assert(!string.IsNullOrEmpty(interfaceName));

        var ifacePtr = WaylandInterfaces.GetInterfacePtr(interfaceName);
        Debug.Assert(ifacePtr != null);

        var args = stackalloc WlArgument[4];
        args[0].u = name;
        args[1].s = Utf8StringMarshaller.ConvertToUnmanaged(interfaceName);
        args[2].u = version;
        args[3].n = 0;

        Debug.Assert(args[1].s != null);
        Debug.Assert(Handle != IntPtr.Zero);

        nint newProxy = WaylandNative.ProxyMarshalArrayFlags(
            Handle,
            opcode,
            ifacePtr,
            version,
            0,
            (nint)args
        );

        Debug.Assert(newProxy != IntPtr.Zero);
        if (newProxy == IntPtr.Zero) throw new InvalidOperationException($"Failed to bind {interfaceName} v{version}");

        return T.Create(newProxy, Display);
    }
}