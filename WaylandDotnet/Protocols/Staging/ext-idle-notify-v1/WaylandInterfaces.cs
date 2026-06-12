namespace WaylandDotnet.Internal;

#nullable enable
#pragma warning disable CA2255
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

/// <summary> Native Wayland interface descriptors for this protocol. </summary>
public static unsafe partial class WaylandInterfaces
{
    /// <summary> Native interface descriptor for ext_idle_notifier_v1. </summary>
    public static WlInterface* ExtIdleNotifierV1 = AllocateInterface();
    /// <summary> Native interface descriptor for ext_idle_notification_v1. </summary>
    public static WlInterface* ExtIdleNotificationV1 = AllocateInterface();


    /// <summary>
    /// Interface: ext_idle_notifier_v1
    /// Version: 2
    /// Requests: 3, Events: 0
    /// </summary>
    [ModuleInitializer]
    public static void CreateExtIdleNotifierV1Interface()
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
            Name = Utf8StringMarshaller.ConvertToUnmanaged("get_idle_notification"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("nuo"),
            Types = (WlInterface**)CreateTypesArray([ExtIdleNotificationV1, (WlInterface*)IntPtr.Zero, WlSeat])
        };
        requests[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("get_input_idle_notification"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("nuo"),
            Types = (WlInterface**)CreateTypesArray([ExtIdleNotificationV1, (WlInterface*)IntPtr.Zero, WlSeat])
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("ext_idle_notifier_v1"),
            Version = 2,
            MethodCount = 3,
            Methods = requests,
            EventCount = 0,
            Events = (WlMessage*)IntPtr.Zero
        };

        Marshal.StructureToPtr(iface, (IntPtr)ExtIdleNotifierV1, false);
        Interfaces.Add("ext_idle_notifier_v1", (IntPtr)ExtIdleNotifierV1);
    }


    /// <summary>
    /// Interface: ext_idle_notification_v1
    /// Version: 2
    /// Requests: 1, Events: 2
    /// </summary>
    [ModuleInitializer]
    public static void CreateExtIdleNotificationV1Interface()
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
            Name = Utf8StringMarshaller.ConvertToUnmanaged("idled"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        events[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("resumed"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("ext_idle_notification_v1"),
            Version = 2,
            MethodCount = 1,
            Methods = requests,
            EventCount = 2,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)ExtIdleNotificationV1, false);
        Interfaces.Add("ext_idle_notification_v1", (IntPtr)ExtIdleNotificationV1);
    }

}