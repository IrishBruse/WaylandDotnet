namespace WaylandDotnet;

using System.Runtime.InteropServices.Marshalling;
using WaylandDotnet.Internal;

public sealed partial class WlRegistry : WaylandObject
{
    public unsafe T Bind<T>(uint name, uint version) where T : WaylandObject, IWaylandObjectFactory<T>
    {
        CheckDisposed();

        const uint opcode = 0;

        var interfaceName = T._StaticInterfaceName;

        var ifacePtr = WaylandInterfaces.GetInterfacePtr(interfaceName);

        var args = stackalloc WlArgument[4];
        args[0].u = name;
        args[1].s = Utf8StringMarshaller.ConvertToUnmanaged(interfaceName);
        args[2].u = version;
        args[3].n = 0;

        nint newProxy = WaylandNative.ProxyMarshalArrayFlags(
            Handle,
            opcode,
            ifacePtr,
            version,
            0,
            (nint)args
        );

        if (newProxy == IntPtr.Zero) throw new InvalidOperationException($"Failed to bind {interfaceName} v{version}");

        return WaylandMarshal.CreateTypedObject<T>(newProxy, Display);
    }
}