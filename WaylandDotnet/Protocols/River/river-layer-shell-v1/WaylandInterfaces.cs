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
    public static WlInterface* RiverLayerShellV1 = AllocateInterface();
    public static WlInterface* RiverLayerShellOutputV1 = AllocateInterface();
    public static WlInterface* RiverLayerShellSeatV1 = AllocateInterface();


    /// <summary>
    /// Interface: river_layer_shell_v1
    /// Version: 1
    /// Requests: 3, Events: 0
    /// </summary>
    [ModuleInitializer]
    public static void CreateRiverLayerShellV1Interface()
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
            Name = Utf8StringMarshaller.ConvertToUnmanaged("get_output"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("no"),
            Types = (WlInterface**)CreateTypesArray([RiverLayerShellOutputV1, RiverOutputV1])
        };
        requests[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("get_seat"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("no"),
            Types = (WlInterface**)CreateTypesArray([RiverLayerShellSeatV1, RiverSeatV1])
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("river_layer_shell_v1"),
            Version = 1,
            MethodCount = 3,
            Methods = requests,
            EventCount = 0,
            Events = (WlMessage*)IntPtr.Zero
        };

        Marshal.StructureToPtr(iface, (IntPtr)RiverLayerShellV1, false);
        Interfaces.Add("river_layer_shell_v1", (IntPtr)RiverLayerShellV1);
    }


    /// <summary>
    /// Interface: river_layer_shell_output_v1
    /// Version: 1
    /// Requests: 2, Events: 1
    /// </summary>
    [ModuleInitializer]
    public static void CreateRiverLayerShellOutputV1Interface()
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
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_default"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        // Event signatures
        var events = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 1);
        events[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("non_exclusive_area"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("iiii"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("river_layer_shell_output_v1"),
            Version = 1,
            MethodCount = 2,
            Methods = requests,
            EventCount = 1,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)RiverLayerShellOutputV1, false);
        Interfaces.Add("river_layer_shell_output_v1", (IntPtr)RiverLayerShellOutputV1);
    }


    /// <summary>
    /// Interface: river_layer_shell_seat_v1
    /// Version: 1
    /// Requests: 1, Events: 3
    /// </summary>
    [ModuleInitializer]
    public static void CreateRiverLayerShellSeatV1Interface()
    {
        // Request signatures
        var requests = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 1);
        requests[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("destroy"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        // Event signatures
        var events = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 3);
        events[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("focus_exclusive"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        events[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("focus_non_exclusive"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        events[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("focus_none"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("river_layer_shell_seat_v1"),
            Version = 1,
            MethodCount = 1,
            Methods = requests,
            EventCount = 3,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)RiverLayerShellSeatV1, false);
        Interfaces.Add("river_layer_shell_seat_v1", (IntPtr)RiverLayerShellSeatV1);
    }

}
