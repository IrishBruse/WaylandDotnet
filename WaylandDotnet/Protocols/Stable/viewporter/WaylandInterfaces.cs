namespace WaylandDotnet.Internal;

#nullable enable
#pragma warning disable CA2255
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

/// <summary> Native Wayland interface descriptors for this protocol. </summary>
public static unsafe partial class WaylandInterfaces
{
    /// <summary> Native interface descriptor for wp_viewporter. </summary>
    public static WlInterface* WpViewporter = AllocateInterface();
    /// <summary> Native interface descriptor for wp_viewport. </summary>
    public static WlInterface* WpViewport = AllocateInterface();


    /// <summary>
    /// Interface: wp_viewporter
    /// Version: 1
    /// Requests: 2, Events: 0
    /// </summary>
    [ModuleInitializer]
    public static void CreateWpViewporterInterface()
    {
        // Request signatures
        var requests = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 2);
        requests[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("destroy"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        requests[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("get_viewport"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("no"),
            Types = (WlInterface**)CreateTypesArray([WpViewport, WlSurface])
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("wp_viewporter"),
            Version = 1,
            MethodCount = 2,
            Methods = requests,
            EventCount = 0,
            Events = (WlMessage*)IntPtr.Zero
        };

        Marshal.StructureToPtr(iface, (IntPtr)WpViewporter, false);
        Interfaces.Add("wp_viewporter", (IntPtr)WpViewporter);
    }


    /// <summary>
    /// Interface: wp_viewport
    /// Version: 1
    /// Requests: 3, Events: 0
    /// </summary>
    [ModuleInitializer]
    public static void CreateWpViewportInterface()
    {
        // Request signatures
        var requests = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 3);
        requests[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("destroy"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        requests[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_source"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("ffff"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };
        requests[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_destination"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("ii"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("wp_viewport"),
            Version = 1,
            MethodCount = 3,
            Methods = requests,
            EventCount = 0,
            Events = (WlMessage*)IntPtr.Zero
        };

        Marshal.StructureToPtr(iface, (IntPtr)WpViewport, false);
        Interfaces.Add("wp_viewport", (IntPtr)WpViewport);
    }

}
