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
    public static WlInterface* ZwpTabletManagerV2 = AllocateInterface();
    public static WlInterface* ZwpTabletSeatV2 = AllocateInterface();
    public static WlInterface* ZwpTabletToolV2 = AllocateInterface();
    public static WlInterface* ZwpTabletV2 = AllocateInterface();
    public static WlInterface* ZwpTabletPadRingV2 = AllocateInterface();
    public static WlInterface* ZwpTabletPadStripV2 = AllocateInterface();
    public static WlInterface* ZwpTabletPadGroupV2 = AllocateInterface();
    public static WlInterface* ZwpTabletPadV2 = AllocateInterface();
    public static WlInterface* ZwpTabletPadDialV2 = AllocateInterface();

    /// <summary>
    /// Interface: zwp_tablet_manager_v2
    /// Version: 2
    /// Requests: 2, Events: 0
    /// </summary>
    [ModuleInitializer]
    public static void CreateZwpTabletManagerV2Interface()
    {
        // Request signatures
        var requests = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 2);
        requests[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("get_tablet_seat"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("no"),
            Types = (WlInterface**)CreateTypesArray([ZwpTabletSeatV2, WlSeat])
        };
        requests[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("destroy"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("zwp_tablet_manager_v2"),
            Version = 2,
            MethodCount = 2,
            Methods = requests,
            EventCount = 0,
            Events = (WlMessage*)IntPtr.Zero
        };

        Marshal.StructureToPtr(iface, (IntPtr)ZwpTabletManagerV2, false);
        Interfaces.Add("zwp_tablet_manager_v2", (IntPtr)ZwpTabletManagerV2);
    }

    /// <summary>
    /// Interface: zwp_tablet_seat_v2
    /// Version: 2
    /// Requests: 1, Events: 3
    /// </summary>
    [ModuleInitializer]
    public static void CreateZwpTabletSeatV2Interface()
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
            Name = Utf8StringMarshaller.ConvertToUnmanaged("tablet_added"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("n"),
            Types = (WlInterface**)CreateTypesArray([ZwpTabletV2])
        };
        events[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("tool_added"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("n"),
            Types = (WlInterface**)CreateTypesArray([ZwpTabletToolV2])
        };
        events[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("pad_added"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("n"),
            Types = (WlInterface**)CreateTypesArray([ZwpTabletPadV2])
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("zwp_tablet_seat_v2"),
            Version = 2,
            MethodCount = 1,
            Methods = requests,
            EventCount = 3,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)ZwpTabletSeatV2, false);
        Interfaces.Add("zwp_tablet_seat_v2", (IntPtr)ZwpTabletSeatV2);
    }

    /// <summary>
    /// Interface: zwp_tablet_tool_v2
    /// Version: 2
    /// Requests: 2, Events: 19
    /// </summary>
    [ModuleInitializer]
    public static void CreateZwpTabletToolV2Interface()
    {
        // Request signatures
        var requests = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 2);
        requests[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_cursor"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u?oii"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, WlSurface, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };
        requests[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("destroy"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        // Event signatures
        var events = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 19);
        events[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("type"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("hardware_serial"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("uu"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };
        events[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("hardware_id_wacom"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("uu"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };
        events[3] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("capability"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[4] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("done"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        events[5] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("removed"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        events[6] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("proximity_in"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("uoo"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, ZwpTabletV2, WlSurface])
        };
        events[7] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("proximity_out"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        events[8] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("down"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[9] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("up"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        events[10] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("motion"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("ff"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };
        events[11] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("pressure"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[12] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("distance"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[13] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("tilt"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("ff"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };
        events[14] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("rotation"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("f"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[15] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("slider"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("i"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[16] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("wheel"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("fi"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };
        events[17] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("button"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("uuu"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };
        events[18] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("frame"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("zwp_tablet_tool_v2"),
            Version = 2,
            MethodCount = 2,
            Methods = requests,
            EventCount = 19,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)ZwpTabletToolV2, false);
        Interfaces.Add("zwp_tablet_tool_v2", (IntPtr)ZwpTabletToolV2);
    }

    /// <summary>
    /// Interface: zwp_tablet_v2
    /// Version: 2
    /// Requests: 1, Events: 6
    /// </summary>
    [ModuleInitializer]
    public static void CreateZwpTabletV2Interface()
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
        var events = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 6);
        events[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("name"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("s"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("id"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("uu"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };
        events[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("path"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("s"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[3] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("done"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        events[4] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("removed"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        events[5] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("bustype"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("zwp_tablet_v2"),
            Version = 2,
            MethodCount = 1,
            Methods = requests,
            EventCount = 6,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)ZwpTabletV2, false);
        Interfaces.Add("zwp_tablet_v2", (IntPtr)ZwpTabletV2);
    }

    /// <summary>
    /// Interface: zwp_tablet_pad_ring_v2
    /// Version: 2
    /// Requests: 2, Events: 4
    /// </summary>
    [ModuleInitializer]
    public static void CreateZwpTabletPadRingV2Interface()
    {
        // Request signatures
        var requests = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 2);
        requests[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_feedback"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("su"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };
        requests[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("destroy"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        // Event signatures
        var events = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 4);
        events[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("source"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("angle"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("f"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("stop"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        events[3] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("frame"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("zwp_tablet_pad_ring_v2"),
            Version = 2,
            MethodCount = 2,
            Methods = requests,
            EventCount = 4,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)ZwpTabletPadRingV2, false);
        Interfaces.Add("zwp_tablet_pad_ring_v2", (IntPtr)ZwpTabletPadRingV2);
    }

    /// <summary>
    /// Interface: zwp_tablet_pad_strip_v2
    /// Version: 2
    /// Requests: 2, Events: 4
    /// </summary>
    [ModuleInitializer]
    public static void CreateZwpTabletPadStripV2Interface()
    {
        // Request signatures
        var requests = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 2);
        requests[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_feedback"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("su"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };
        requests[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("destroy"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        // Event signatures
        var events = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 4);
        events[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("source"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("position"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("stop"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        events[3] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("frame"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("zwp_tablet_pad_strip_v2"),
            Version = 2,
            MethodCount = 2,
            Methods = requests,
            EventCount = 4,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)ZwpTabletPadStripV2, false);
        Interfaces.Add("zwp_tablet_pad_strip_v2", (IntPtr)ZwpTabletPadStripV2);
    }

    /// <summary>
    /// Interface: zwp_tablet_pad_group_v2
    /// Version: 2
    /// Requests: 1, Events: 7
    /// </summary>
    [ModuleInitializer]
    public static void CreateZwpTabletPadGroupV2Interface()
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
        var events = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 7);
        events[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("buttons"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("a"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("ring"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("n"),
            Types = (WlInterface**)CreateTypesArray([ZwpTabletPadRingV2])
        };
        events[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("strip"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("n"),
            Types = (WlInterface**)CreateTypesArray([ZwpTabletPadStripV2])
        };
        events[3] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("modes"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[4] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("done"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        events[5] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("mode_switch"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("uuu"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };
        events[6] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("dial"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("n"),
            Types = (WlInterface**)CreateTypesArray([ZwpTabletPadDialV2])
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("zwp_tablet_pad_group_v2"),
            Version = 2,
            MethodCount = 1,
            Methods = requests,
            EventCount = 7,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)ZwpTabletPadGroupV2, false);
        Interfaces.Add("zwp_tablet_pad_group_v2", (IntPtr)ZwpTabletPadGroupV2);
    }

    /// <summary>
    /// Interface: zwp_tablet_pad_v2
    /// Version: 2
    /// Requests: 2, Events: 8
    /// </summary>
    [ModuleInitializer]
    public static void CreateZwpTabletPadV2Interface()
    {
        // Request signatures
        var requests = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 2);
        requests[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_feedback"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("usu"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };
        requests[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("destroy"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        // Event signatures
        var events = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 8);
        events[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("group"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("n"),
            Types = (WlInterface**)CreateTypesArray([ZwpTabletPadGroupV2])
        };
        events[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("path"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("s"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("buttons"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[3] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("done"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        events[4] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("button"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("uuu"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };
        events[5] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("enter"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("uoo"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, ZwpTabletV2, WlSurface])
        };
        events[6] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("leave"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("uo"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, WlSurface])
        };
        events[7] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("removed"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("zwp_tablet_pad_v2"),
            Version = 2,
            MethodCount = 2,
            Methods = requests,
            EventCount = 8,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)ZwpTabletPadV2, false);
        Interfaces.Add("zwp_tablet_pad_v2", (IntPtr)ZwpTabletPadV2);
    }

    /// <summary>
    /// Interface: zwp_tablet_pad_dial_v2
    /// Version: 2
    /// Requests: 2, Events: 2
    /// </summary>
    [ModuleInitializer]
    public static void CreateZwpTabletPadDialV2Interface()
    {
        // Request signatures
        var requests = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 2);
        requests[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_feedback"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("su"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };
        requests[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("destroy"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        // Event signatures
        var events = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 2);
        events[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("delta"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("i"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("frame"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("zwp_tablet_pad_dial_v2"),
            Version = 2,
            MethodCount = 2,
            Methods = requests,
            EventCount = 2,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)ZwpTabletPadDialV2, false);
        Interfaces.Add("zwp_tablet_pad_dial_v2", (IntPtr)ZwpTabletPadDialV2);
    }

}
