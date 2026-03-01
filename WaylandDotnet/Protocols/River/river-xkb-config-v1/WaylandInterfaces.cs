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
    public static WlInterface* RiverXkbConfigV1 = AllocateInterface();
    public static WlInterface* RiverXkbKeymapV1 = AllocateInterface();
    public static WlInterface* RiverXkbKeyboardV1 = AllocateInterface();

    /// <summary>
    /// Interface: river_xkb_config_v1
    /// Version: 1
    /// Requests: 3, Events: 2
    /// </summary>
    [ModuleInitializer]
    public static void CreateRiverXkbConfigV1Interface()
    {
        // Request signatures
        var requests = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 3);
        requests[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("stop"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        requests[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("destroy"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        requests[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("create_keymap"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("nhu"),
            Types = (WlInterface**)CreateTypesArray([RiverXkbKeymapV1, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };

        // Event signatures
        var events = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 2);
        events[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("finished"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        events[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("xkb_keyboard"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("n"),
            Types = (WlInterface**)CreateTypesArray([RiverXkbKeyboardV1])
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("river_xkb_config_v1"),
            Version = 1,
            MethodCount = 3,
            Methods = requests,
            EventCount = 2,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)RiverXkbConfigV1, false);
        Interfaces.Add("river_xkb_config_v1", (IntPtr)RiverXkbConfigV1);
    }

    /// <summary>
    /// Interface: river_xkb_keymap_v1
    /// Version: 1
    /// Requests: 1, Events: 2
    /// </summary>
    [ModuleInitializer]
    public static void CreateRiverXkbKeymapV1Interface()
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
        var events = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 2);
        events[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("success"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        events[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("failure"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("s"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("river_xkb_keymap_v1"),
            Version = 1,
            MethodCount = 1,
            Methods = requests,
            EventCount = 2,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)RiverXkbKeymapV1, false);
        Interfaces.Add("river_xkb_keymap_v1", (IntPtr)RiverXkbKeymapV1);
    }

    /// <summary>
    /// Interface: river_xkb_keyboard_v1
    /// Version: 1
    /// Requests: 8, Events: 7
    /// </summary>
    [ModuleInitializer]
    public static void CreateRiverXkbKeyboardV1Interface()
    {
        // Request signatures
        var requests = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 8);
        requests[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("destroy"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        requests[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_keymap"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("o"),
            Types = (WlInterface**)CreateTypesArray([RiverXkbKeymapV1])
        };
        requests[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_layout_by_index"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("i"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        requests[3] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_layout_by_name"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("s"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        requests[4] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("capslock_enable"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        requests[5] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("capslock_disable"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        requests[6] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("numlock_enable"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        requests[7] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("numlock_disable"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        // Event signatures
        var events = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 7);
        events[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("removed"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        events[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("input_device"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("o"),
            Types = (WlInterface**)CreateTypesArray([RiverInputDeviceV1])
        };
        events[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("layout"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u?s"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };
        events[3] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("capslock_enabled"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        events[4] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("capslock_disabled"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        events[5] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("numlock_enabled"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        events[6] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("numlock_disabled"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("river_xkb_keyboard_v1"),
            Version = 1,
            MethodCount = 8,
            Methods = requests,
            EventCount = 7,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)RiverXkbKeyboardV1, false);
        Interfaces.Add("river_xkb_keyboard_v1", (IntPtr)RiverXkbKeyboardV1);
    }

}
