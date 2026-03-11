namespace WaylandDotnet.Internal;

#nullable enable
#pragma warning disable CS1591
#pragma warning disable CS0108
#pragma warning disable CA2255
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

public static unsafe partial class WaylandInterfaces
{
    public static WlInterface* ZwlrForeignToplevelManagerV1 = AllocateInterface();
    public static WlInterface* ZwlrForeignToplevelHandleV1 = AllocateInterface();


    /// <summary>
    /// Interface: zwlr_foreign_toplevel_manager_v1
    /// Version: 3
    /// Requests: 1, Events: 2
    /// </summary>
    [ModuleInitializer]
    public static void CreateZwlrForeignToplevelManagerV1Interface()
    {
        // Request signatures
        var requests = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 1);
        requests[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("stop"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        // Event signatures
        var events = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 2);
        events[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("toplevel"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("n"),
            Types = (WlInterface**)CreateTypesArray([ZwlrForeignToplevelHandleV1])
        };
        events[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("finished"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("zwlr_foreign_toplevel_manager_v1"),
            Version = 3,
            MethodCount = 1,
            Methods = requests,
            EventCount = 2,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)ZwlrForeignToplevelManagerV1, false);
        Interfaces.Add("zwlr_foreign_toplevel_manager_v1", (IntPtr)ZwlrForeignToplevelManagerV1);
    }


    /// <summary>
    /// Interface: zwlr_foreign_toplevel_handle_v1
    /// Version: 3
    /// Requests: 10, Events: 8
    /// </summary>
    [ModuleInitializer]
    public static void CreateZwlrForeignToplevelHandleV1Interface()
    {
        // Request signatures
        var requests = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 10);
        requests[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_maximized"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        requests[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("unset_maximized"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        requests[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_minimized"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        requests[3] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("unset_minimized"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        requests[4] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("activate"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("o"),
            Types = (WlInterface**)CreateTypesArray([WlSeat])
        };
        requests[5] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("close"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        requests[6] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_rectangle"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("oiiii"),
            Types = (WlInterface**)CreateTypesArray([WlSurface, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };
        requests[7] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("destroy"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        requests[8] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_fullscreen"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("?o"),
            Types = (WlInterface**)CreateTypesArray([WlOutput])
        };
        requests[9] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("unset_fullscreen"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        // Event signatures
        var events = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 8);
        events[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("title"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("s"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("app_id"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("s"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("output_enter"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("o"),
            Types = (WlInterface**)CreateTypesArray([WlOutput])
        };
        events[3] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("output_leave"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("o"),
            Types = (WlInterface**)CreateTypesArray([WlOutput])
        };
        events[4] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("state"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("a"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[5] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("done"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        events[6] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("closed"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        events[7] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("parent"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("?o"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("zwlr_foreign_toplevel_handle_v1"),
            Version = 3,
            MethodCount = 10,
            Methods = requests,
            EventCount = 8,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)ZwlrForeignToplevelHandleV1, false);
        Interfaces.Add("zwlr_foreign_toplevel_handle_v1", (IntPtr)ZwlrForeignToplevelHandleV1);
    }

}
