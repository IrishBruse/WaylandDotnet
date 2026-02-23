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
    public static WlInterface* WpPresentation = AllocateInterface();
    public static WlInterface* WpPresentationFeedback = AllocateInterface();

    /// <summary>
    /// Interface: wp_presentation
    /// Version: 2
    /// Requests: 2, Events: 1
    /// </summary>
    [ModuleInitializer]
    public static void CreateWpPresentationInterface()
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
            Name = Utf8StringMarshaller.ConvertToUnmanaged("feedback"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("on"),
            Types = (WlInterface**)CreateTypesArray([WlSurface, WpPresentationFeedback])
        };

        // Event signatures
        var events = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 1);
        events[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("clock_id"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("wp_presentation"),
            Version = 2,
            MethodCount = 2,
            Methods = requests,
            EventCount = 1,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)WpPresentation, false);
        Interfaces.Add("wp_presentation", (IntPtr)WpPresentation);
    }

    /// <summary>
    /// Interface: wp_presentation_feedback
    /// Version: 2
    /// Requests: 0, Events: 3
    /// </summary>
    [ModuleInitializer]
    public static void CreateWpPresentationFeedbackInterface()
    {
        // Event signatures
        var events = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 3);
        events[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("sync_output"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("o"),
            Types = (WlInterface**)CreateTypesArray([WlOutput])
        };
        events[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("presented"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("uuuuuuu"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };
        events[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("discarded"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("wp_presentation_feedback"),
            Version = 2,
            MethodCount = 0,
            Methods = (WlMessage*)IntPtr.Zero,
            EventCount = 3,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)WpPresentationFeedback, false);
        Interfaces.Add("wp_presentation_feedback", (IntPtr)WpPresentationFeedback);
    }

}
