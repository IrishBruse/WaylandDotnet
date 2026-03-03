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
    public static WlInterface* ZwlrOutputManagerV1 = AllocateInterface();
    public static WlInterface* ZwlrOutputHeadV1 = AllocateInterface();
    public static WlInterface* ZwlrOutputModeV1 = AllocateInterface();
    public static WlInterface* ZwlrOutputConfigurationV1 = AllocateInterface();
    public static WlInterface* ZwlrOutputConfigurationHeadV1 = AllocateInterface();


    /// <summary>
    /// Interface: zwlr_output_manager_v1
    /// Version: 4
    /// Requests: 2, Events: 3
    /// </summary>
    [ModuleInitializer]
    public static void CreateZwlrOutputManagerV1Interface()
    {
        // Request signatures
        var requests = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 2);
        requests[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("create_configuration"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("nu"),
            Types = (WlInterface**)CreateTypesArray([ZwlrOutputConfigurationV1, (WlInterface*)IntPtr.Zero])
        };
        requests[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("stop"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        // Event signatures
        var events = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 3);
        events[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("head"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("n"),
            Types = (WlInterface**)CreateTypesArray([ZwlrOutputHeadV1])
        };
        events[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("done"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("finished"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("zwlr_output_manager_v1"),
            Version = 4,
            MethodCount = 2,
            Methods = requests,
            EventCount = 3,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)ZwlrOutputManagerV1, false);
        Interfaces.Add("zwlr_output_manager_v1", (IntPtr)ZwlrOutputManagerV1);
    }


    /// <summary>
    /// Interface: zwlr_output_head_v1
    /// Version: 4
    /// Requests: 1, Events: 14
    /// </summary>
    [ModuleInitializer]
    public static void CreateZwlrOutputHeadV1Interface()
    {
        // Request signatures
        var requests = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 1);
        requests[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("release"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        // Event signatures
        var events = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 14);
        events[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("name"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("s"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("description"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("s"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("physical_size"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("ii"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };
        events[3] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("mode"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("n"),
            Types = (WlInterface**)CreateTypesArray([ZwlrOutputModeV1])
        };
        events[4] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("enabled"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("i"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[5] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("current_mode"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("o"),
            Types = (WlInterface**)CreateTypesArray([ZwlrOutputModeV1])
        };
        events[6] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("position"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("ii"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };
        events[7] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("transform"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("i"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[8] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("scale"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("f"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[9] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("finished"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        events[10] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("make"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("s"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[11] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("model"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("s"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[12] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("serial_number"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("s"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[13] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("adaptive_sync"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("zwlr_output_head_v1"),
            Version = 4,
            MethodCount = 1,
            Methods = requests,
            EventCount = 14,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)ZwlrOutputHeadV1, false);
        Interfaces.Add("zwlr_output_head_v1", (IntPtr)ZwlrOutputHeadV1);
    }


    /// <summary>
    /// Interface: zwlr_output_mode_v1
    /// Version: 3
    /// Requests: 1, Events: 4
    /// </summary>
    [ModuleInitializer]
    public static void CreateZwlrOutputModeV1Interface()
    {
        // Request signatures
        var requests = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 1);
        requests[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("release"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        // Event signatures
        var events = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 4);
        events[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("size"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("ii"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };
        events[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("refresh"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("i"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("preferred"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        events[3] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("finished"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("zwlr_output_mode_v1"),
            Version = 3,
            MethodCount = 1,
            Methods = requests,
            EventCount = 4,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)ZwlrOutputModeV1, false);
        Interfaces.Add("zwlr_output_mode_v1", (IntPtr)ZwlrOutputModeV1);
    }


    /// <summary>
    /// Interface: zwlr_output_configuration_v1
    /// Version: 4
    /// Requests: 5, Events: 3
    /// </summary>
    [ModuleInitializer]
    public static void CreateZwlrOutputConfigurationV1Interface()
    {
        // Request signatures
        var requests = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 5);
        requests[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("enable_head"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("no"),
            Types = (WlInterface**)CreateTypesArray([ZwlrOutputConfigurationHeadV1, ZwlrOutputHeadV1])
        };
        requests[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("disable_head"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("o"),
            Types = (WlInterface**)CreateTypesArray([ZwlrOutputHeadV1])
        };
        requests[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("apply"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        requests[3] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("test"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        requests[4] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("destroy"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        // Event signatures
        var events = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 3);
        events[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("succeeded"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        events[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("failed"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        events[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("cancelled"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("zwlr_output_configuration_v1"),
            Version = 4,
            MethodCount = 5,
            Methods = requests,
            EventCount = 3,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)ZwlrOutputConfigurationV1, false);
        Interfaces.Add("zwlr_output_configuration_v1", (IntPtr)ZwlrOutputConfigurationV1);
    }


    /// <summary>
    /// Interface: zwlr_output_configuration_head_v1
    /// Version: 4
    /// Requests: 6, Events: 0
    /// </summary>
    [ModuleInitializer]
    public static void CreateZwlrOutputConfigurationHeadV1Interface()
    {
        // Request signatures
        var requests = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 6);
        requests[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_mode"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("o"),
            Types = (WlInterface**)CreateTypesArray([ZwlrOutputModeV1])
        };
        requests[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_custom_mode"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("iii"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };
        requests[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_position"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("ii"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };
        requests[3] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_transform"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("i"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        requests[4] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_scale"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("f"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        requests[5] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_adaptive_sync"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("zwlr_output_configuration_head_v1"),
            Version = 4,
            MethodCount = 6,
            Methods = requests,
            EventCount = 0,
            Events = (WlMessage*)IntPtr.Zero
        };

        Marshal.StructureToPtr(iface, (IntPtr)ZwlrOutputConfigurationHeadV1, false);
        Interfaces.Add("zwlr_output_configuration_head_v1", (IntPtr)ZwlrOutputConfigurationHeadV1);
    }

}
