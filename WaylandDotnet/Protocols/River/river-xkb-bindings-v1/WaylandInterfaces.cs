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
    public static WlInterface* RiverXkbBindingsV1 = AllocateInterface();
    public static WlInterface* RiverXkbBindingV1 = AllocateInterface();
    public static WlInterface* RiverXkbBindingsSeatV1 = AllocateInterface();


    /// <summary>
    /// Interface: river_xkb_bindings_v1
    /// Version: 2
    /// Requests: 3, Events: 0
    /// </summary>
    [ModuleInitializer]
    public static void CreateRiverXkbBindingsV1Interface()
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
            Name = Utf8StringMarshaller.ConvertToUnmanaged("get_xkb_binding"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("onuu"),
            Types = (WlInterface**)CreateTypesArray([RiverSeatV1, RiverXkbBindingV1, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };
        requests[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("get_seat"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("no"),
            Types = (WlInterface**)CreateTypesArray([RiverXkbBindingsSeatV1, RiverSeatV1])
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("river_xkb_bindings_v1"),
            Version = 2,
            MethodCount = 3,
            Methods = requests,
            EventCount = 0,
            Events = (WlMessage*)IntPtr.Zero
        };

        Marshal.StructureToPtr(iface, (IntPtr)RiverXkbBindingsV1, false);
        Interfaces.Add("river_xkb_bindings_v1", (IntPtr)RiverXkbBindingsV1);
    }


    /// <summary>
    /// Interface: river_xkb_binding_v1
    /// Version: 2
    /// Requests: 4, Events: 3
    /// </summary>
    [ModuleInitializer]
    public static void CreateRiverXkbBindingV1Interface()
    {
        // Request signatures
        var requests = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 4);
        requests[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("destroy"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        requests[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_layout_override"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        requests[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("enable"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        requests[3] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("disable"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        // Event signatures
        var events = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 3);
        events[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("pressed"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        events[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("released"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        events[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("stop_repeat"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("river_xkb_binding_v1"),
            Version = 2,
            MethodCount = 4,
            Methods = requests,
            EventCount = 3,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)RiverXkbBindingV1, false);
        Interfaces.Add("river_xkb_binding_v1", (IntPtr)RiverXkbBindingV1);
    }


    /// <summary>
    /// Interface: river_xkb_bindings_seat_v1
    /// Version: 2
    /// Requests: 3, Events: 1
    /// </summary>
    [ModuleInitializer]
    public static void CreateRiverXkbBindingsSeatV1Interface()
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
            Name = Utf8StringMarshaller.ConvertToUnmanaged("ensure_next_key_eaten"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        requests[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("cancel_ensure_next_key_eaten"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        // Event signatures
        var events = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 1);
        events[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("ate_unbound_key"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("river_xkb_bindings_seat_v1"),
            Version = 2,
            MethodCount = 3,
            Methods = requests,
            EventCount = 1,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)RiverXkbBindingsSeatV1, false);
        Interfaces.Add("river_xkb_bindings_seat_v1", (IntPtr)RiverXkbBindingsSeatV1);
    }

}
