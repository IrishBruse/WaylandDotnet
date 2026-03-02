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
    public static WlInterface* RiverLibinputConfigV1 = AllocateInterface();
    public static WlInterface* RiverLibinputDeviceV1 = AllocateInterface();
    public static WlInterface* RiverLibinputAccelConfigV1 = AllocateInterface();
    public static WlInterface* RiverLibinputResultV1 = AllocateInterface();


    /// <summary>
    /// Interface: river_libinput_config_v1
    /// Version: 1
    /// Requests: 3, Events: 2
    /// </summary>
    [ModuleInitializer]
    public static void CreateRiverLibinputConfigV1Interface()
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
            Name = Utf8StringMarshaller.ConvertToUnmanaged("create_accel_config"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("nu"),
            Types = (WlInterface**)CreateTypesArray([RiverLibinputAccelConfigV1, (WlInterface*)IntPtr.Zero])
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
            Name = Utf8StringMarshaller.ConvertToUnmanaged("libinput_device"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("n"),
            Types = (WlInterface**)CreateTypesArray([RiverLibinputDeviceV1])
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("river_libinput_config_v1"),
            Version = 1,
            MethodCount = 3,
            Methods = requests,
            EventCount = 2,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)RiverLibinputConfigV1, false);
        Interfaces.Add("river_libinput_config_v1", (IntPtr)RiverLibinputConfigV1);
    }


    /// <summary>
    /// Interface: river_libinput_device_v1
    /// Version: 1
    /// Requests: 22, Events: 55
    /// </summary>
    [ModuleInitializer]
    public static void CreateRiverLibinputDeviceV1Interface()
    {
        // Request signatures
        var requests = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 22);
        requests[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("destroy"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        requests[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_send_events"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("nu"),
            Types = (WlInterface**)CreateTypesArray([RiverLibinputResultV1, (WlInterface*)IntPtr.Zero])
        };
        requests[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_tap"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("nu"),
            Types = (WlInterface**)CreateTypesArray([RiverLibinputResultV1, (WlInterface*)IntPtr.Zero])
        };
        requests[3] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_tap_button_map"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("nu"),
            Types = (WlInterface**)CreateTypesArray([RiverLibinputResultV1, (WlInterface*)IntPtr.Zero])
        };
        requests[4] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_drag"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("nu"),
            Types = (WlInterface**)CreateTypesArray([RiverLibinputResultV1, (WlInterface*)IntPtr.Zero])
        };
        requests[5] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_drag_lock"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("nu"),
            Types = (WlInterface**)CreateTypesArray([RiverLibinputResultV1, (WlInterface*)IntPtr.Zero])
        };
        requests[6] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_three_finger_drag"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("nu"),
            Types = (WlInterface**)CreateTypesArray([RiverLibinputResultV1, (WlInterface*)IntPtr.Zero])
        };
        requests[7] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_calibration_matrix"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("na"),
            Types = (WlInterface**)CreateTypesArray([RiverLibinputResultV1, (WlInterface*)IntPtr.Zero])
        };
        requests[8] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_accel_profile"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("nu"),
            Types = (WlInterface**)CreateTypesArray([RiverLibinputResultV1, (WlInterface*)IntPtr.Zero])
        };
        requests[9] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_accel_speed"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("na"),
            Types = (WlInterface**)CreateTypesArray([RiverLibinputResultV1, (WlInterface*)IntPtr.Zero])
        };
        requests[10] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("apply_accel_config"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("no"),
            Types = (WlInterface**)CreateTypesArray([RiverLibinputResultV1, RiverLibinputAccelConfigV1])
        };
        requests[11] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_natural_scroll"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("nu"),
            Types = (WlInterface**)CreateTypesArray([RiverLibinputResultV1, (WlInterface*)IntPtr.Zero])
        };
        requests[12] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_left_handed"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("nu"),
            Types = (WlInterface**)CreateTypesArray([RiverLibinputResultV1, (WlInterface*)IntPtr.Zero])
        };
        requests[13] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_click_method"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("nu"),
            Types = (WlInterface**)CreateTypesArray([RiverLibinputResultV1, (WlInterface*)IntPtr.Zero])
        };
        requests[14] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_clickfinger_button_map"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("nu"),
            Types = (WlInterface**)CreateTypesArray([RiverLibinputResultV1, (WlInterface*)IntPtr.Zero])
        };
        requests[15] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_middle_emulation"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("nu"),
            Types = (WlInterface**)CreateTypesArray([RiverLibinputResultV1, (WlInterface*)IntPtr.Zero])
        };
        requests[16] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_scroll_method"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("nu"),
            Types = (WlInterface**)CreateTypesArray([RiverLibinputResultV1, (WlInterface*)IntPtr.Zero])
        };
        requests[17] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_scroll_button"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("nu"),
            Types = (WlInterface**)CreateTypesArray([RiverLibinputResultV1, (WlInterface*)IntPtr.Zero])
        };
        requests[18] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_scroll_button_lock"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("nu"),
            Types = (WlInterface**)CreateTypesArray([RiverLibinputResultV1, (WlInterface*)IntPtr.Zero])
        };
        requests[19] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_dwt"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("nu"),
            Types = (WlInterface**)CreateTypesArray([RiverLibinputResultV1, (WlInterface*)IntPtr.Zero])
        };
        requests[20] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_dwtp"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("nu"),
            Types = (WlInterface**)CreateTypesArray([RiverLibinputResultV1, (WlInterface*)IntPtr.Zero])
        };
        requests[21] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_rotation"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("nu"),
            Types = (WlInterface**)CreateTypesArray([RiverLibinputResultV1, (WlInterface*)IntPtr.Zero])
        };

        // Event signatures
        var events = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 55);
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
            Name = Utf8StringMarshaller.ConvertToUnmanaged("send_events_support"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[3] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("send_events_default"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[4] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("send_events_current"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[5] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("tap_support"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("i"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[6] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("tap_default"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[7] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("tap_current"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[8] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("tap_button_map_default"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[9] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("tap_button_map_current"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[10] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("drag_default"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[11] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("drag_current"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[12] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("drag_lock_default"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[13] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("drag_lock_current"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[14] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("three_finger_drag_support"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("i"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[15] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("three_finger_drag_default"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[16] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("three_finger_drag_current"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[17] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("calibration_matrix_support"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("i"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[18] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("calibration_matrix_default"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("a"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[19] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("calibration_matrix_current"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("a"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[20] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("accel_profiles_support"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[21] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("accel_profile_default"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[22] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("accel_profile_current"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[23] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("accel_speed_default"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("a"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[24] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("accel_speed_current"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("a"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[25] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("natural_scroll_support"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("i"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[26] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("natural_scroll_default"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[27] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("natural_scroll_current"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[28] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("left_handed_support"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("i"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[29] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("left_handed_default"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[30] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("left_handed_current"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[31] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("click_method_support"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[32] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("click_method_default"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[33] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("click_method_current"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[34] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("clickfinger_button_map_default"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[35] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("clickfinger_button_map_current"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[36] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("middle_emulation_support"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("i"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[37] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("middle_emulation_default"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[38] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("middle_emulation_current"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[39] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("scroll_method_support"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[40] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("scroll_method_default"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[41] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("scroll_method_current"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[42] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("scroll_button_default"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[43] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("scroll_button_current"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[44] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("scroll_button_lock_default"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[45] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("scroll_button_lock_current"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[46] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("dwt_support"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("i"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[47] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("dwt_default"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[48] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("dwt_current"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[49] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("dwtp_support"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("i"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[50] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("dwtp_default"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[51] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("dwtp_current"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[52] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("rotation_support"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("i"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[53] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("rotation_default"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };
        events[54] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("rotation_current"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("u"),
            Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("river_libinput_device_v1"),
            Version = 1,
            MethodCount = 22,
            Methods = requests,
            EventCount = 55,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)RiverLibinputDeviceV1, false);
        Interfaces.Add("river_libinput_device_v1", (IntPtr)RiverLibinputDeviceV1);
    }


    /// <summary>
    /// Interface: river_libinput_accel_config_v1
    /// Version: 1
    /// Requests: 2, Events: 0
    /// </summary>
    [ModuleInitializer]
    public static void CreateRiverLibinputAccelConfigV1Interface()
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
            Name = Utf8StringMarshaller.ConvertToUnmanaged("set_points"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged("nuaa"),
            Types = (WlInterface**)CreateTypesArray([RiverLibinputResultV1, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("river_libinput_accel_config_v1"),
            Version = 1,
            MethodCount = 2,
            Methods = requests,
            EventCount = 0,
            Events = (WlMessage*)IntPtr.Zero
        };

        Marshal.StructureToPtr(iface, (IntPtr)RiverLibinputAccelConfigV1, false);
        Interfaces.Add("river_libinput_accel_config_v1", (IntPtr)RiverLibinputAccelConfigV1);
    }


    /// <summary>
    /// Interface: river_libinput_result_v1
    /// Version: 1
    /// Requests: 0, Events: 3
    /// </summary>
    [ModuleInitializer]
    public static void CreateRiverLibinputResultV1Interface()
    {
        // Event signatures
        var events = (WlMessage*)Marshal.AllocHGlobal(sizeof(WlMessage) * 3);
        events[0] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("success"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        events[1] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("unsupported"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };
        events[2] = new WlMessage
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("invalid"),
            Signature = Utf8StringMarshaller.ConvertToUnmanaged(""),
            Types = (WlInterface**)IntPtr.Zero
        };

        var iface = new WlInterface
        {
            Name = Utf8StringMarshaller.ConvertToUnmanaged("river_libinput_result_v1"),
            Version = 1,
            MethodCount = 0,
            Methods = (WlMessage*)IntPtr.Zero,
            EventCount = 3,
            Events = events
        };

        Marshal.StructureToPtr(iface, (IntPtr)RiverLibinputResultV1, false);
        Interfaces.Add("river_libinput_result_v1", (IntPtr)RiverLibinputResultV1);
    }

}
