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
    public static WlInterface* ZwpLinuxDmabufV1 = AllocateInterface();
    public static WlInterface* ZwpLinuxBufferParamsV1 = AllocateInterface();
    public static WlInterface* ZwpLinuxDmabufFeedbackV1 = AllocateInterface();

    /// <summary>
    /// Interface: zwp_linux_dmabuf_v1
    /// Version: 5
    /// Requests: 4, Events: 2
    /// </summary>
    [ModuleInitializer]
    public static void CreateZwpLinuxDmabufV1Interface()
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
            Name = Utf8StringMarshaller.ConvertToUnmanaged("create_params"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("n"),
            Types = (WlInterface**)CreateTypesArray([ZwpLinuxBufferParamsV1])
        };
        requests[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("get_default_feedback"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("n"),
            Types = (WlInterface**)CreateTypesArray([ZwpLinuxDmabufFeedbackV1])
        };
        requests[3] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("get_surface_feedback"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("no"),
            Types = (WlInterface**)CreateTypesArray([ZwpLinuxDmabufFeedbackV1, WlSurface])
        };

        // Event signatures
        var events = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 2);
        events[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("format"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("modifier"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("uuu"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("zwp_linux_dmabuf_v1"),
            Version = 5,
            MethodCount = 4,
            Methods = requests,
            EventCount = 2,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)ZwpLinuxDmabufV1, false);
        Interfaces.Add("zwp_linux_dmabuf_v1", (IntPtr)ZwpLinuxDmabufV1);
    }

    /// <summary>
    /// Interface: zwp_linux_buffer_params_v1
    /// Version: 5
    /// Requests: 4, Events: 2
    /// </summary>
    [ModuleInitializer]
    public static void CreateZwpLinuxBufferParamsV1Interface()
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
            Name = Utf8StringMarshaller.ConvertToUnmanaged("add"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("huuuuu"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };
        requests[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("create"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("iiuu"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };
        requests[3] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("create_immed"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("niiuu"),
            Types = (WlInterface**)CreateTypesArray([WlBuffer, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };

        // Event signatures
        var events = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 2);
        events[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("created"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("n"),
            Types = (WlInterface**)CreateTypesArray([WlBuffer])
        };
        events[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("failed"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("zwp_linux_buffer_params_v1"),
            Version = 5,
            MethodCount = 4,
            Methods = requests,
            EventCount = 2,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)ZwpLinuxBufferParamsV1, false);
        Interfaces.Add("zwp_linux_buffer_params_v1", (IntPtr)ZwpLinuxBufferParamsV1);
    }

    /// <summary>
    /// Interface: zwp_linux_dmabuf_feedback_v1
    /// Version: 5
    /// Requests: 1, Events: 7
    /// </summary>
    [ModuleInitializer]
    public static void CreateZwpLinuxDmabufFeedbackV1Interface()
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
            Name = Utf8StringMarshaller.ConvertToUnmanaged("done"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        events[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("format_table"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("hu"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };
        events[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("main_device"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("a"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[3] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("tranche_done"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        events[4] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("tranche_target_device"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("a"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[5] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("tranche_formats"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("a"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[6] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("tranche_flags"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("zwp_linux_dmabuf_feedback_v1"),
            Version = 5,
            MethodCount = 1,
            Methods = requests,
            EventCount = 7,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)ZwpLinuxDmabufFeedbackV1, false);
        Interfaces.Add("zwp_linux_dmabuf_feedback_v1", (IntPtr)ZwpLinuxDmabufFeedbackV1);
    }

}
