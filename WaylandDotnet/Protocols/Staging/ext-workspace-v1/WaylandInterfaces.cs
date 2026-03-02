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
    public static WlInterface* ExtWorkspaceManagerV1 = AllocateInterface();
    public static WlInterface* ExtWorkspaceGroupHandleV1 = AllocateInterface();
    public static WlInterface* ExtWorkspaceHandleV1 = AllocateInterface();


    /// <summary>
    /// Interface: ext_workspace_manager_v1
    /// Version: 1
    /// Requests: 2, Events: 4
    /// </summary>
    [ModuleInitializer]
    public static void CreateExtWorkspaceManagerV1Interface()
    {
        // Request signatures
        var requests = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 2);
        requests[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("commit"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        requests[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("stop"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        // Event signatures
        var events = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 4);
        events[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("workspace_group"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("n"),
            Types = (WlInterface**)CreateTypesArray([ExtWorkspaceGroupHandleV1])
        };
        events[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("workspace"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("n"),
            Types = (WlInterface**)CreateTypesArray([ExtWorkspaceHandleV1])
        };
        events[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("done"),
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
            Name = Utf8StringMarshaller.ConvertToUnmanaged("ext_workspace_manager_v1"),
            Version = 1,
            MethodCount = 2,
            Methods = requests,
            EventCount = 4,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)ExtWorkspaceManagerV1, false);
        Interfaces.Add("ext_workspace_manager_v1", (IntPtr)ExtWorkspaceManagerV1);
    }


    /// <summary>
    /// Interface: ext_workspace_group_handle_v1
    /// Version: 1
    /// Requests: 2, Events: 6
    /// </summary>
    [ModuleInitializer]
    public static void CreateExtWorkspaceGroupHandleV1Interface()
    {
        // Request signatures
        var requests = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 2);
        requests[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("create_workspace"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("s"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        requests[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("destroy"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        // Event signatures
        var events = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 6);
        events[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("capabilities"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("output_enter"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("o"),
            Types = (WlInterface**)CreateTypesArray([WlOutput])
        };
        events[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("output_leave"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("o"),
            Types = (WlInterface**)CreateTypesArray([WlOutput])
        };
        events[3] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("workspace_enter"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("o"),
            Types = (WlInterface**)CreateTypesArray([ExtWorkspaceHandleV1])
        };
        events[4] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("workspace_leave"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("o"),
            Types = (WlInterface**)CreateTypesArray([ExtWorkspaceHandleV1])
        };
        events[5] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("removed"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("ext_workspace_group_handle_v1"),
            Version = 1,
            MethodCount = 2,
            Methods = requests,
            EventCount = 6,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)ExtWorkspaceGroupHandleV1, false);
        Interfaces.Add("ext_workspace_group_handle_v1", (IntPtr)ExtWorkspaceGroupHandleV1);
    }


    /// <summary>
    /// Interface: ext_workspace_handle_v1
    /// Version: 1
    /// Requests: 5, Events: 6
    /// </summary>
    [ModuleInitializer]
    public static void CreateExtWorkspaceHandleV1Interface()
    {
        // Request signatures
        var requests = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 5);
        requests[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("destroy"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        requests[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("activate"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        requests[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("deactivate"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        requests[3] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("assign"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("o"),
            Types = (WlInterface**)CreateTypesArray([ExtWorkspaceGroupHandleV1])
        };
        requests[4] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("remove"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        // Event signatures
        var events = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 6);
        events[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("id"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("s"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("name"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("s"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("coordinates"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("a"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[3] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("state"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[4] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("capabilities"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[5] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("removed"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("ext_workspace_handle_v1"),
            Version = 1,
            MethodCount = 5,
            Methods = requests,
            EventCount = 6,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)ExtWorkspaceHandleV1, false);
        Interfaces.Add("ext_workspace_handle_v1", (IntPtr)ExtWorkspaceHandleV1);
    }

}
