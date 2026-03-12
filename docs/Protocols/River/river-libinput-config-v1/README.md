# River LibInput Config

##### [WaylandDotnet](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet) ![](../../../assets/arrow.svg ':class=breadcrumb-arrow') [River](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/River) ![](../../../assets/arrow.svg ':class=breadcrumb-arrow') [RiverLibinputConfigV1](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/River/river-libinput-config-v1/)

---

<h2 class="decleration interface">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=riverlibinputconfigv1" id="riverlibinputconfigv1">
        <span class="codicon codicon-symbol-interface"></span>
        RiverLibinputConfigV1
    </a>
    <span class="pill">version 1</span>
</h2>

Libinput config global interface


Global interface for configuring libinput devices. This global should
only be advertised if river_input_manager_v1 is advertised as well.


<h3 class="decleration request" title="Stop request">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=riverlibinputconfigv1_stop" id="riverlibinputconfigv1_stop">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLibinputConfigV1.<span class="method">Stop</span>
    </a>
</h3>

```csharp
void Stop()
```


**Stop sending events**

This request indicates that the client no longer wishes to receive
events on this object.

The Wayland protocol is asynchronous, which means the server may send
further events until the stop request is processed. The client must wait
for a river_libinput_config_v1.finished event before destroying this
object.

<h3 class="decleration request" title="Destroy request">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=riverlibinputconfigv1_destroy" id="riverlibinputconfigv1_destroy">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLibinputConfigV1.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the river_libinput_config_v1 object**

This request should be called after the finished event has been received
to complete destruction of the object.

It is a protocol error to make this request before the finished event
has been received.

If a client wishes to destroy this object it should send a
river_libinput_config_v1.stop request and wait for a
river_libinput_config_v1.finished event. Once the finished event is
received it is safe to destroy this object and any other objects created
through this interface.

<h3 class="decleration request" title="CreateAccelConfig request">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=riverlibinputconfigv1_createaccelconfig" id="riverlibinputconfigv1_createaccelconfig">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLibinputConfigV1.<span class="method">CreateAccelConfig</span>
    </a>
</h3>

```csharp
RiverLibinputAccelConfigV1 CreateAccelConfig(uint profile)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id |  |
| profile | uint |  |

**Create a acceleration config**

Create a acceleration config which can be applied
with river_libinput_device_v1.apply_accel_config.

<h3 class="decleration event" title="Finished event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputconfigv1_finished" id="onriverlibinputconfigv1_finished">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputConfigV1.<span class="event">OnFinished</span>
    </a>
</h3>

```csharp
void FinishedHandler()
```


**The server has finished with the object**

This event indicates that the server will send no further events on this
object. The client should destroy the object. See
river_libinput_config_v1.destroy for more information.

<h3 class="decleration event" title="LibinputDevice event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputconfigv1_libinputdevice" id="onriverlibinputconfigv1_libinputdevice">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputConfigV1.<span class="event">OnLibinputDevice</span>
    </a>
</h3>

```csharp
void LibinputDeviceHandler(RiverLibinputDeviceV1 id)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id |  |

**New libinput device**

A new libinput device has been created. Not every river_input_device_v1
is necessarily a libinput device as well.

<h3 class="decleration enum" title="Error enum">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=error" id="error">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverLibinputConfigV1.<span class="enum">Error</span>
    </a>
</h3>

```csharp
public enum Error
```

| Value | Integer | Description |
| --- | --- | --- |
| InvalidArg | 0 | Invalid enum value or similar |
| InvalidDestroy | 1 |  |
<h2 class="decleration interface">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=riverlibinputdevicev1" id="riverlibinputdevicev1">
        <span class="codicon codicon-symbol-interface"></span>
        RiverLibinputDeviceV1
    </a>
    <span class="pill">version 1</span>
</h2>

A libinput device


In general, *_support events will be sent exactly once directly after the
river_libinput_device_v1 is created. *_default events will be sent after
*_support events if the config option is supported, and *_current events
willl be sent after the *_default events and again whenever the config
option is changed.


<h3 class="decleration request" title="Destroy request">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=riverlibinputdevicev1_destroy" id="riverlibinputdevicev1_destroy">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLibinputDeviceV1.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the libinput device object**

This request indicates that the client will no longer use the input
device object and that it may be safely destroyed.

<h3 class="decleration request" title="SetSendEvents request">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=riverlibinputdevicev1_setsendevents" id="riverlibinputdevicev1_setsendevents">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLibinputDeviceV1.<span class="method">SetSendEvents</span>
    </a>
</h3>

```csharp
RiverLibinputResultV1 SetSendEvents(uint mode)
```

| Argument | Type | Description |
| --- | --- | --- |
| result | new_id |  |
| mode | uint |  |

**Set send events mode**

Set the send events mode for the device.

<h3 class="decleration request" title="SetTap request">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=riverlibinputdevicev1_settap" id="riverlibinputdevicev1_settap">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLibinputDeviceV1.<span class="method">SetTap</span>
    </a>
</h3>

```csharp
RiverLibinputResultV1 SetTap(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| result | new_id |  |
| state | uint |  |

**Enable/disable tap-to-click**

Configure tap-to-click on this device, with a default mapping of
1, 2, 3 finger tap mapping to left, right, middle click, respectively.

<h3 class="decleration request" title="SetTapButtonMap request">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=riverlibinputdevicev1_settapbuttonmap" id="riverlibinputdevicev1_settapbuttonmap">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLibinputDeviceV1.<span class="method">SetTapButtonMap</span>
    </a>
</h3>

```csharp
RiverLibinputResultV1 SetTapButtonMap(uint buttonMap)
```

| Argument | Type | Description |
| --- | --- | --- |
| result | new_id |  |
| button_map | uint |  |

**Set tap-to-click button map**

Set the finger number to button number mapping for tap-to-click. The
default mapping on most devices is to have a 1, 2 and 3 finger tap to
map to the left, right and middle button, respectively.

<h3 class="decleration request" title="SetDrag request">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=riverlibinputdevicev1_setdrag" id="riverlibinputdevicev1_setdrag">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLibinputDeviceV1.<span class="method">SetDrag</span>
    </a>
</h3>

```csharp
RiverLibinputResultV1 SetDrag(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| result | new_id |  |
| state | uint |  |

**Set tap-and-drag state**

Configure tap-and-drag functionality on the device.

<h3 class="decleration request" title="SetDragLock request">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=riverlibinputdevicev1_setdraglock" id="riverlibinputdevicev1_setdraglock">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLibinputDeviceV1.<span class="method">SetDragLock</span>
    </a>
</h3>

```csharp
RiverLibinputResultV1 SetDragLock(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| result | new_id |  |
| state | uint |  |

**Set drag lock state**

Configure drag-lock during tapping on this device. When enabled, a
finger may be lifted and put back on the touchpad and the drag process
continues. A timeout for lifting the finger is optional. When disabled,
lifting the finger during a tap-and-drag will immediately stop the drag.
See the libinput documentation for more details.

<h3 class="decleration request" title="SetThreeFingerDrag request">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=riverlibinputdevicev1_setthreefingerdrag" id="riverlibinputdevicev1_setthreefingerdrag">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLibinputDeviceV1.<span class="method">SetThreeFingerDrag</span>
    </a>
</h3>

```csharp
RiverLibinputResultV1 SetThreeFingerDrag(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| result | new_id |  |
| state | uint |  |

**Set three finger drag state**

Configure three finger drag functionality for the device.

<h3 class="decleration request" title="SetCalibrationMatrix request">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=riverlibinputdevicev1_setcalibrationmatrix" id="riverlibinputdevicev1_setcalibrationmatrix">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLibinputDeviceV1.<span class="method">SetCalibrationMatrix</span>
    </a>
</h3>

```csharp
RiverLibinputResultV1 SetCalibrationMatrix(byte[] matrix)
```

| Argument | Type | Description |
| --- | --- | --- |
| result | new_id |  |
| matrix | array | Array of 6 floats |

**Set calibration matrix**

Set calibration matrix.

<h3 class="decleration request" title="SetAccelProfile request">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=riverlibinputdevicev1_setaccelprofile" id="riverlibinputdevicev1_setaccelprofile">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLibinputDeviceV1.<span class="method">SetAccelProfile</span>
    </a>
</h3>

```csharp
RiverLibinputResultV1 SetAccelProfile(uint profile)
```

| Argument | Type | Description |
| --- | --- | --- |
| result | new_id |  |
| profile | uint |  |

**Set send events mode**

Set the acceleration profile.

<h3 class="decleration request" title="SetAccelSpeed request">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=riverlibinputdevicev1_setaccelspeed" id="riverlibinputdevicev1_setaccelspeed">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLibinputDeviceV1.<span class="method">SetAccelSpeed</span>
    </a>
</h3>

```csharp
RiverLibinputResultV1 SetAccelSpeed(byte[] speed)
```

| Argument | Type | Description |
| --- | --- | --- |
| result | new_id |  |
| speed | array | Double |

**Set acceleration speed**

Set the acceleration speed within a range of [-1, 1], where 0 is
the default acceleration for this device, -1 is the slowest acceleration
and 1 is the maximum acceleration available on this device.

<h3 class="decleration request" title="ApplyAccelConfig request">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=riverlibinputdevicev1_applyaccelconfig" id="riverlibinputdevicev1_applyaccelconfig">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLibinputDeviceV1.<span class="method">ApplyAccelConfig</span>
    </a>
</h3>

```csharp
RiverLibinputResultV1 ApplyAccelConfig(RiverLibinputAccelConfigV1 config)
```

| Argument | Type | Description |
| --- | --- | --- |
| result | new_id |  |
| config | object |  |

**Apply acceleration config**

Apply a pointer accleration config.

<h3 class="decleration request" title="SetNaturalScroll request">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=riverlibinputdevicev1_setnaturalscroll" id="riverlibinputdevicev1_setnaturalscroll">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLibinputDeviceV1.<span class="method">SetNaturalScroll</span>
    </a>
</h3>

```csharp
RiverLibinputResultV1 SetNaturalScroll(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| result | new_id |  |
| state | uint |  |

**Set natural scroll state**

Set natural scroll state.

<h3 class="decleration request" title="SetLeftHanded request">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=riverlibinputdevicev1_setlefthanded" id="riverlibinputdevicev1_setlefthanded">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLibinputDeviceV1.<span class="method">SetLeftHanded</span>
    </a>
</h3>

```csharp
RiverLibinputResultV1 SetLeftHanded(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| result | new_id |  |
| state | uint |  |

**Set left-handed mode state**

Set left-handed mode state.

<h3 class="decleration request" title="SetClickMethod request">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=riverlibinputdevicev1_setclickmethod" id="riverlibinputdevicev1_setclickmethod">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLibinputDeviceV1.<span class="method">SetClickMethod</span>
    </a>
</h3>

```csharp
RiverLibinputResultV1 SetClickMethod(uint method)
```

| Argument | Type | Description |
| --- | --- | --- |
| result | new_id |  |
| method | uint |  |

**Set click method**

Set click method.

<h3 class="decleration request" title="SetClickfingerButtonMap request">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=riverlibinputdevicev1_setclickfingerbuttonmap" id="riverlibinputdevicev1_setclickfingerbuttonmap">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLibinputDeviceV1.<span class="method">SetClickfingerButtonMap</span>
    </a>
</h3>

```csharp
RiverLibinputResultV1 SetClickfingerButtonMap(uint buttonMap)
```

| Argument | Type | Description |
| --- | --- | --- |
| result | new_id |  |
| button_map | uint |  |

**Set clickfinger button map**

Set clickfinger button map.
Supported if click_methods.clickfinger is supported.

<h3 class="decleration request" title="SetMiddleEmulation request">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=riverlibinputdevicev1_setmiddleemulation" id="riverlibinputdevicev1_setmiddleemulation">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLibinputDeviceV1.<span class="method">SetMiddleEmulation</span>
    </a>
</h3>

```csharp
RiverLibinputResultV1 SetMiddleEmulation(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| result | new_id |  |
| state | uint |  |

**Set middle mouse button emulation state**

Set middle mouse button emulation state.

<h3 class="decleration request" title="SetScrollMethod request">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=riverlibinputdevicev1_setscrollmethod" id="riverlibinputdevicev1_setscrollmethod">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLibinputDeviceV1.<span class="method">SetScrollMethod</span>
    </a>
</h3>

```csharp
RiverLibinputResultV1 SetScrollMethod(uint method)
```

| Argument | Type | Description |
| --- | --- | --- |
| result | new_id |  |
| method | uint |  |

**Set scroll method**

Set scroll method.

<h3 class="decleration request" title="SetScrollButton request">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=riverlibinputdevicev1_setscrollbutton" id="riverlibinputdevicev1_setscrollbutton">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLibinputDeviceV1.<span class="method">SetScrollButton</span>
    </a>
</h3>

```csharp
RiverLibinputResultV1 SetScrollButton(uint button)
```

| Argument | Type | Description |
| --- | --- | --- |
| result | new_id |  |
| button | uint |  |

**Set scroll button**

Set scroll button.
Supported if scroll_methods.on_button_down is supported.

<h3 class="decleration request" title="SetScrollButtonLock request">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=riverlibinputdevicev1_setscrollbuttonlock" id="riverlibinputdevicev1_setscrollbuttonlock">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLibinputDeviceV1.<span class="method">SetScrollButtonLock</span>
    </a>
</h3>

```csharp
RiverLibinputResultV1 SetScrollButtonLock(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| result | new_id |  |
| state | uint |  |

**Set scroll button lock state**

Set scroll button lock state.
Supported if scroll_methods.on_button_down is supported.

<h3 class="decleration request" title="SetDwt request">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=riverlibinputdevicev1_setdwt" id="riverlibinputdevicev1_setdwt">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLibinputDeviceV1.<span class="method">SetDwt</span>
    </a>
</h3>

```csharp
RiverLibinputResultV1 SetDwt(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| result | new_id |  |
| state | uint |  |

**Set disable-while-typing state**

Set disable-while-typing state.

<h3 class="decleration request" title="SetDwtp request">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=riverlibinputdevicev1_setdwtp" id="riverlibinputdevicev1_setdwtp">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLibinputDeviceV1.<span class="method">SetDwtp</span>
    </a>
</h3>

```csharp
RiverLibinputResultV1 SetDwtp(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| result | new_id |  |
| state | uint |  |

**Set disable-while-trackpointing state**

Set disable-while-trackpointing state.

<h3 class="decleration request" title="SetRotation request">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=riverlibinputdevicev1_setrotation" id="riverlibinputdevicev1_setrotation">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLibinputDeviceV1.<span class="method">SetRotation</span>
    </a>
</h3>

```csharp
RiverLibinputResultV1 SetRotation(uint angle)
```

| Argument | Type | Description |
| --- | --- | --- |
| result | new_id |  |
| angle | uint |  |

**Set rotation angle**

Set rotation angle in degrees clockwise off the logical neutral
position. Angle must be in the range [0-360).

<h3 class="decleration event" title="Removed event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_removed" id="onriverlibinputdevicev1_removed">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnRemoved</span>
    </a>
</h3>

```csharp
void RemovedHandler()
```


**The libinput device is removed**

This event indicates that the libinput device has been removed.

The server will send no further events on this object and ignore any
request (other than river_libinput_device_v1.destroy) made after this
event is sent. The client should destroy this object with the
river_libinput_device_v1.destroy request to free up resources.

<h3 class="decleration event" title="InputDevice event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_inputdevice" id="onriverlibinputdevicev1_inputdevice">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnInputDevice</span>
    </a>
</h3>

```csharp
void InputDeviceHandler(RiverInputDeviceV1 device)
```

| Argument | Type | Description |
| --- | --- | --- |
| device | object |  |

**Corresponding river input device**

The river_input_device_v1 corresponding to this libinput device.
This event will always be the first event sent on the
river_libinput_device_v1 object, and it will be sent exactly once.

<h3 class="decleration event" title="SendEventsSupport event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_sendeventssupport" id="onriverlibinputdevicev1_sendeventssupport">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnSendEventsSupport</span>
    </a>
</h3>

```csharp
void SendEventsSupportHandler(uint modes)
```

| Argument | Type | Description |
| --- | --- | --- |
| modes | uint |  |

**Supported send events modes**

Supported send events modes.

<h3 class="decleration event" title="SendEventsDefault event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_sendeventsdefault" id="onriverlibinputdevicev1_sendeventsdefault">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnSendEventsDefault</span>
    </a>
</h3>

```csharp
void SendEventsDefaultHandler(uint mode)
```

| Argument | Type | Description |
| --- | --- | --- |
| mode | uint |  |

**Default send events mode**

Default send events mode.

<h3 class="decleration event" title="SendEventsCurrent event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_sendeventscurrent" id="onriverlibinputdevicev1_sendeventscurrent">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnSendEventsCurrent</span>
    </a>
</h3>

```csharp
void SendEventsCurrentHandler(uint mode)
```

| Argument | Type | Description |
| --- | --- | --- |
| mode | uint |  |

**Current send events mode**

Current send events mode.

<h3 class="decleration event" title="TapSupport event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_tapsupport" id="onriverlibinputdevicev1_tapsupport">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnTapSupport</span>
    </a>
</h3>

```csharp
void TapSupportHandler(int fingerCount)
```

| Argument | Type | Description |
| --- | --- | --- |
| finger_count | int |  |

**Tap-to-click/drag support**

The number of fingers supported for tap-to-click/drag.
If finger_count is 0, tap-to-click and drag are unsupported.

<h3 class="decleration event" title="TapDefault event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_tapdefault" id="onriverlibinputdevicev1_tapdefault">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnTapDefault</span>
    </a>
</h3>

```csharp
void TapDefaultHandler(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| state | uint |  |

**Default tap-to-click state**

Default tap-to-click state.

<h3 class="decleration event" title="TapCurrent event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_tapcurrent" id="onriverlibinputdevicev1_tapcurrent">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnTapCurrent</span>
    </a>
</h3>

```csharp
void TapCurrentHandler(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| state | uint |  |

**Current tap-to-click state**

Current tap-to-click state.

<h3 class="decleration event" title="TapButtonMapDefault event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_tapbuttonmapdefault" id="onriverlibinputdevicev1_tapbuttonmapdefault">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnTapButtonMapDefault</span>
    </a>
</h3>

```csharp
void TapButtonMapDefaultHandler(uint buttonMap)
```

| Argument | Type | Description |
| --- | --- | --- |
| button_map | uint |  |

**Default tap-to-click button map**

Default tap-to-click button map.

<h3 class="decleration event" title="TapButtonMapCurrent event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_tapbuttonmapcurrent" id="onriverlibinputdevicev1_tapbuttonmapcurrent">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnTapButtonMapCurrent</span>
    </a>
</h3>

```csharp
void TapButtonMapCurrentHandler(uint buttonMap)
```

| Argument | Type | Description |
| --- | --- | --- |
| button_map | uint |  |

**Current tap-to-click button map**

Current tap-to-click button map.

<h3 class="decleration event" title="DragDefault event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_dragdefault" id="onriverlibinputdevicev1_dragdefault">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnDragDefault</span>
    </a>
</h3>

```csharp
void DragDefaultHandler(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| state | uint |  |

**Default tap-and-drag state**

Default tap-and-drag state.

<h3 class="decleration event" title="DragCurrent event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_dragcurrent" id="onriverlibinputdevicev1_dragcurrent">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnDragCurrent</span>
    </a>
</h3>

```csharp
void DragCurrentHandler(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| state | uint |  |

**Current tap-and-drag state**

Current tap-and-drag state.

<h3 class="decleration event" title="DragLockDefault event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_draglockdefault" id="onriverlibinputdevicev1_draglockdefault">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnDragLockDefault</span>
    </a>
</h3>

```csharp
void DragLockDefaultHandler(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| state | uint |  |

**Default drag lock state**

Default drag lock state.

<h3 class="decleration event" title="DragLockCurrent event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_draglockcurrent" id="onriverlibinputdevicev1_draglockcurrent">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnDragLockCurrent</span>
    </a>
</h3>

```csharp
void DragLockCurrentHandler(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| state | uint |  |

**Current drag lock state**

Current drag lock state.

<h3 class="decleration event" title="ThreeFingerDragSupport event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_threefingerdragsupport" id="onriverlibinputdevicev1_threefingerdragsupport">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnThreeFingerDragSupport</span>
    </a>
</h3>

```csharp
void ThreeFingerDragSupportHandler(int fingerCount)
```

| Argument | Type | Description |
| --- | --- | --- |
| finger_count | int |  |

**Three finger drag support**

The number of fingers supported for three/four finger drag.
If finger_count is less than 3, three finger drag is unsupported.

<h3 class="decleration event" title="ThreeFingerDragDefault event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_threefingerdragdefault" id="onriverlibinputdevicev1_threefingerdragdefault">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnThreeFingerDragDefault</span>
    </a>
</h3>

```csharp
void ThreeFingerDragDefaultHandler(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| state | uint |  |

**Default three finger drag state**

Default three finger drag state.

<h3 class="decleration event" title="ThreeFingerDragCurrent event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_threefingerdragcurrent" id="onriverlibinputdevicev1_threefingerdragcurrent">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnThreeFingerDragCurrent</span>
    </a>
</h3>

```csharp
void ThreeFingerDragCurrentHandler(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| state | uint |  |

**Current three finger drag state**

Current three finger drag state.

<h3 class="decleration event" title="CalibrationMatrixSupport event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_calibrationmatrixsupport" id="onriverlibinputdevicev1_calibrationmatrixsupport">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnCalibrationMatrixSupport</span>
    </a>
</h3>

```csharp
void CalibrationMatrixSupportHandler(int supported)
```

| Argument | Type | Description |
| --- | --- | --- |
| supported | int | Boolean |

**Support for a calibration matrix**

A calibration matrix is supported if the supported argument is non-zero.

<h3 class="decleration event" title="CalibrationMatrixDefault event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_calibrationmatrixdefault" id="onriverlibinputdevicev1_calibrationmatrixdefault">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnCalibrationMatrixDefault</span>
    </a>
</h3>

```csharp
void CalibrationMatrixDefaultHandler(byte[] matrix)
```

| Argument | Type | Description |
| --- | --- | --- |
| matrix | array | Array of 6 floats |

**Default calibration matrix**

Default calibration matrix.

<h3 class="decleration event" title="CalibrationMatrixCurrent event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_calibrationmatrixcurrent" id="onriverlibinputdevicev1_calibrationmatrixcurrent">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnCalibrationMatrixCurrent</span>
    </a>
</h3>

```csharp
void CalibrationMatrixCurrentHandler(byte[] matrix)
```

| Argument | Type | Description |
| --- | --- | --- |
| matrix | array | Array of 6 floats |

**Current calibration matrix**

Current calibration matrix.

<h3 class="decleration event" title="AccelProfilesSupport event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_accelprofilessupport" id="onriverlibinputdevicev1_accelprofilessupport">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnAccelProfilesSupport</span>
    </a>
</h3>

```csharp
void AccelProfilesSupportHandler(uint profiles)
```

| Argument | Type | Description |
| --- | --- | --- |
| profiles | uint |  |

**Supported acceleration profiles**

Supported acceleration profiles.

<h3 class="decleration event" title="AccelProfileDefault event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_accelprofiledefault" id="onriverlibinputdevicev1_accelprofiledefault">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnAccelProfileDefault</span>
    </a>
</h3>

```csharp
void AccelProfileDefaultHandler(uint profile)
```

| Argument | Type | Description |
| --- | --- | --- |
| profile | uint |  |

**Default acceleration profile**

Default acceleration profile.

<h3 class="decleration event" title="AccelProfileCurrent event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_accelprofilecurrent" id="onriverlibinputdevicev1_accelprofilecurrent">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnAccelProfileCurrent</span>
    </a>
</h3>

```csharp
void AccelProfileCurrentHandler(uint profile)
```

| Argument | Type | Description |
| --- | --- | --- |
| profile | uint |  |

**Current send events mode**

Current acceleration profile.

<h3 class="decleration event" title="AccelSpeedDefault event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_accelspeeddefault" id="onriverlibinputdevicev1_accelspeeddefault">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnAccelSpeedDefault</span>
    </a>
</h3>

```csharp
void AccelSpeedDefaultHandler(byte[] speed)
```

| Argument | Type | Description |
| --- | --- | --- |
| speed | array | Double |

**Default acceleration speed**

Default acceleration speed.

<h3 class="decleration event" title="AccelSpeedCurrent event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_accelspeedcurrent" id="onriverlibinputdevicev1_accelspeedcurrent">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnAccelSpeedCurrent</span>
    </a>
</h3>

```csharp
void AccelSpeedCurrentHandler(byte[] speed)
```

| Argument | Type | Description |
| --- | --- | --- |
| speed | array | Double |

**Current acceleration speed**

Current acceleration speed.

<h3 class="decleration event" title="NaturalScrollSupport event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_naturalscrollsupport" id="onriverlibinputdevicev1_naturalscrollsupport">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnNaturalScrollSupport</span>
    </a>
</h3>

```csharp
void NaturalScrollSupportHandler(int supported)
```

| Argument | Type | Description |
| --- | --- | --- |
| supported | int | Boolean |

**Support for natural scroll**

Natural scroll is supported if the supported argument is non-zero.

<h3 class="decleration event" title="NaturalScrollDefault event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_naturalscrolldefault" id="onriverlibinputdevicev1_naturalscrolldefault">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnNaturalScrollDefault</span>
    </a>
</h3>

```csharp
void NaturalScrollDefaultHandler(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| state | uint |  |

**Default natural scroll**

Default natural scroll.

<h3 class="decleration event" title="NaturalScrollCurrent event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_naturalscrollcurrent" id="onriverlibinputdevicev1_naturalscrollcurrent">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnNaturalScrollCurrent</span>
    </a>
</h3>

```csharp
void NaturalScrollCurrentHandler(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| state | uint |  |

**Current natural scroll state**

Current natural scroll.

<h3 class="decleration event" title="LeftHandedSupport event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_lefthandedsupport" id="onriverlibinputdevicev1_lefthandedsupport">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnLeftHandedSupport</span>
    </a>
</h3>

```csharp
void LeftHandedSupportHandler(int supported)
```

| Argument | Type | Description |
| --- | --- | --- |
| supported | int | Boolean |

**Support for left-handed mode**

Left-handed mode is supported if the supported argument is non-zero.

<h3 class="decleration event" title="LeftHandedDefault event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_lefthandeddefault" id="onriverlibinputdevicev1_lefthandeddefault">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnLeftHandedDefault</span>
    </a>
</h3>

```csharp
void LeftHandedDefaultHandler(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| state | uint |  |

**Default left-handed mode**

Default left-handed mode.

<h3 class="decleration event" title="LeftHandedCurrent event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_lefthandedcurrent" id="onriverlibinputdevicev1_lefthandedcurrent">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnLeftHandedCurrent</span>
    </a>
</h3>

```csharp
void LeftHandedCurrentHandler(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| state | uint |  |

**Current left-handed mode state**

Current left-handed mode.

<h3 class="decleration event" title="ClickMethodSupport event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_clickmethodsupport" id="onriverlibinputdevicev1_clickmethodsupport">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnClickMethodSupport</span>
    </a>
</h3>

```csharp
void ClickMethodSupportHandler(uint methods)
```

| Argument | Type | Description |
| --- | --- | --- |
| methods | uint |  |

**Supported click methods**

The click methods supported by the device.

<h3 class="decleration event" title="ClickMethodDefault event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_clickmethoddefault" id="onriverlibinputdevicev1_clickmethoddefault">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnClickMethodDefault</span>
    </a>
</h3>

```csharp
void ClickMethodDefaultHandler(uint method)
```

| Argument | Type | Description |
| --- | --- | --- |
| method | uint |  |

**Default click method**

Default click method.

<h3 class="decleration event" title="ClickMethodCurrent event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_clickmethodcurrent" id="onriverlibinputdevicev1_clickmethodcurrent">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnClickMethodCurrent</span>
    </a>
</h3>

```csharp
void ClickMethodCurrentHandler(uint method)
```

| Argument | Type | Description |
| --- | --- | --- |
| method | uint |  |

**Current click method**

Current click method.

<h3 class="decleration event" title="ClickfingerButtonMapDefault event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_clickfingerbuttonmapdefault" id="onriverlibinputdevicev1_clickfingerbuttonmapdefault">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnClickfingerButtonMapDefault</span>
    </a>
</h3>

```csharp
void ClickfingerButtonMapDefaultHandler(uint buttonMap)
```

| Argument | Type | Description |
| --- | --- | --- |
| button_map | uint |  |

**Default clickfinger button map**

Default clickfinger button map.
Supported if click_methods.clickfinger is supported.

<h3 class="decleration event" title="ClickfingerButtonMapCurrent event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_clickfingerbuttonmapcurrent" id="onriverlibinputdevicev1_clickfingerbuttonmapcurrent">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnClickfingerButtonMapCurrent</span>
    </a>
</h3>

```csharp
void ClickfingerButtonMapCurrentHandler(uint buttonMap)
```

| Argument | Type | Description |
| --- | --- | --- |
| button_map | uint |  |

**Current clickfinger button map**

Current clickfinger button map.
Supported if click_methods.clickfinger is supported.

<h3 class="decleration event" title="MiddleEmulationSupport event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_middleemulationsupport" id="onriverlibinputdevicev1_middleemulationsupport">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnMiddleEmulationSupport</span>
    </a>
</h3>

```csharp
void MiddleEmulationSupportHandler(int supported)
```

| Argument | Type | Description |
| --- | --- | --- |
| supported | int | Boolean |

**Support for middle mouse button emulation**

Middle mouse button emulation is supported if the supported argument is
non-zero.

<h3 class="decleration event" title="MiddleEmulationDefault event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_middleemulationdefault" id="onriverlibinputdevicev1_middleemulationdefault">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnMiddleEmulationDefault</span>
    </a>
</h3>

```csharp
void MiddleEmulationDefaultHandler(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| state | uint |  |

**Default middle mouse button emulation**

Default middle mouse button emulation.

<h3 class="decleration event" title="MiddleEmulationCurrent event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_middleemulationcurrent" id="onriverlibinputdevicev1_middleemulationcurrent">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnMiddleEmulationCurrent</span>
    </a>
</h3>

```csharp
void MiddleEmulationCurrentHandler(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| state | uint |  |

**Current middle mouse button emulation state**

Current middle mouse button emulation.

<h3 class="decleration event" title="ScrollMethodSupport event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_scrollmethodsupport" id="onriverlibinputdevicev1_scrollmethodsupport">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnScrollMethodSupport</span>
    </a>
</h3>

```csharp
void ScrollMethodSupportHandler(uint methods)
```

| Argument | Type | Description |
| --- | --- | --- |
| methods | uint |  |

**Supported scroll methods**

The scroll methods supported by the device.

<h3 class="decleration event" title="ScrollMethodDefault event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_scrollmethoddefault" id="onriverlibinputdevicev1_scrollmethoddefault">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnScrollMethodDefault</span>
    </a>
</h3>

```csharp
void ScrollMethodDefaultHandler(uint method)
```

| Argument | Type | Description |
| --- | --- | --- |
| method | uint |  |

**Default scroll method**

Default scroll method.

<h3 class="decleration event" title="ScrollMethodCurrent event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_scrollmethodcurrent" id="onriverlibinputdevicev1_scrollmethodcurrent">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnScrollMethodCurrent</span>
    </a>
</h3>

```csharp
void ScrollMethodCurrentHandler(uint method)
```

| Argument | Type | Description |
| --- | --- | --- |
| method | uint |  |

**Current scroll method**

Current scroll method.

<h3 class="decleration event" title="ScrollButtonDefault event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_scrollbuttondefault" id="onriverlibinputdevicev1_scrollbuttondefault">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnScrollButtonDefault</span>
    </a>
</h3>

```csharp
void ScrollButtonDefaultHandler(uint button)
```

| Argument | Type | Description |
| --- | --- | --- |
| button | uint |  |

**Default scroll button**

Default scroll button.
Supported if scroll_methods.on_button_down is supported.

<h3 class="decleration event" title="ScrollButtonCurrent event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_scrollbuttoncurrent" id="onriverlibinputdevicev1_scrollbuttoncurrent">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnScrollButtonCurrent</span>
    </a>
</h3>

```csharp
void ScrollButtonCurrentHandler(uint button)
```

| Argument | Type | Description |
| --- | --- | --- |
| button | uint |  |

**Current scroll button**

Current scroll button.
Supported if scroll_methods.on_button_down is supported.

<h3 class="decleration event" title="ScrollButtonLockDefault event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_scrollbuttonlockdefault" id="onriverlibinputdevicev1_scrollbuttonlockdefault">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnScrollButtonLockDefault</span>
    </a>
</h3>

```csharp
void ScrollButtonLockDefaultHandler(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| state | uint |  |

**Default scroll button lock state**

Default scroll button lock state.
Supported if scroll_methods.on_button_down is supported.

<h3 class="decleration event" title="ScrollButtonLockCurrent event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_scrollbuttonlockcurrent" id="onriverlibinputdevicev1_scrollbuttonlockcurrent">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnScrollButtonLockCurrent</span>
    </a>
</h3>

```csharp
void ScrollButtonLockCurrentHandler(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| state | uint |  |

**Current scroll button lock state**

Current scroll button lock state.
Supported if scroll_methods.on_button_down is supported.

<h3 class="decleration event" title="DwtSupport event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_dwtsupport" id="onriverlibinputdevicev1_dwtsupport">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnDwtSupport</span>
    </a>
</h3>

```csharp
void DwtSupportHandler(int supported)
```

| Argument | Type | Description |
| --- | --- | --- |
| supported | int | Boolean |

**Support for disable-while-typing**

Disable-while-typing is supported if the supported argument is
non-zero.

<h3 class="decleration event" title="DwtDefault event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_dwtdefault" id="onriverlibinputdevicev1_dwtdefault">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnDwtDefault</span>
    </a>
</h3>

```csharp
void DwtDefaultHandler(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| state | uint |  |

**Default disable-while-typing state**

Default disable-while-typing state.

<h3 class="decleration event" title="DwtCurrent event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_dwtcurrent" id="onriverlibinputdevicev1_dwtcurrent">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnDwtCurrent</span>
    </a>
</h3>

```csharp
void DwtCurrentHandler(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| state | uint |  |

**Current disable-while-typing state**

Current disable-while-typing state.

<h3 class="decleration event" title="DwtpSupport event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_dwtpsupport" id="onriverlibinputdevicev1_dwtpsupport">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnDwtpSupport</span>
    </a>
</h3>

```csharp
void DwtpSupportHandler(int supported)
```

| Argument | Type | Description |
| --- | --- | --- |
| supported | int | Boolean |

**Support for disable-while-trackpointing**

Disable-while-trackpointing is supported if the supported argument is
non-zero.

<h3 class="decleration event" title="DwtpDefault event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_dwtpdefault" id="onriverlibinputdevicev1_dwtpdefault">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnDwtpDefault</span>
    </a>
</h3>

```csharp
void DwtpDefaultHandler(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| state | uint |  |

**Default disable-while-trackpointing state**

Default disable-while-trackpointing state.

<h3 class="decleration event" title="DwtpCurrent event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_dwtpcurrent" id="onriverlibinputdevicev1_dwtpcurrent">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnDwtpCurrent</span>
    </a>
</h3>

```csharp
void DwtpCurrentHandler(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| state | uint |  |

**Current disable-while-trackpointing state**

Current disable-while-trackpointing state.

<h3 class="decleration event" title="RotationSupport event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_rotationsupport" id="onriverlibinputdevicev1_rotationsupport">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnRotationSupport</span>
    </a>
</h3>

```csharp
void RotationSupportHandler(int supported)
```

| Argument | Type | Description |
| --- | --- | --- |
| supported | int | Boolean |

**Support for rotation**

Rotation is supported if the supported argument is non-zero.

<h3 class="decleration event" title="RotationDefault event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_rotationdefault" id="onriverlibinputdevicev1_rotationdefault">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnRotationDefault</span>
    </a>
</h3>

```csharp
void RotationDefaultHandler(uint angle)
```

| Argument | Type | Description |
| --- | --- | --- |
| angle | uint |  |

**Default rotation angle**

Default rotation angle.

<h3 class="decleration event" title="RotationCurrent event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputdevicev1_rotationcurrent" id="onriverlibinputdevicev1_rotationcurrent">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputDeviceV1.<span class="event">OnRotationCurrent</span>
    </a>
</h3>

```csharp
void RotationCurrentHandler(uint angle)
```

| Argument | Type | Description |
| --- | --- | --- |
| angle | uint |  |

**Current rotation angle**

Current rotation angle.

<h3 class="decleration enum" title="Error enum">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=error" id="error">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverLibinputDeviceV1.<span class="enum">Error</span>
    </a>
</h3>

```csharp
public enum Error
```

| Value | Integer | Description |
| --- | --- | --- |
| InvalidArg | 0 | Invalid enum value or similar |
<h3 class="decleration enum" title="SendEventsModes enum">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=sendeventsmodes" id="sendeventsmodes">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverLibinputDeviceV1.<span class="enum">SendEventsModes</span>
    </a>
</h3>

```csharp
public enum SendEventsModesFlag
```

| Value | Integer | Description |
| --- | --- | --- |
| Enabled | 0 |  |
| Disabled | 1 |  |
| DisabledOnExternalMouse | 2 |  |
<h3 class="decleration enum" title="TapState enum">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=tapstate" id="tapstate">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverLibinputDeviceV1.<span class="enum">TapState</span>
    </a>
</h3>

```csharp
public enum TapState
```

| Value | Integer | Description |
| --- | --- | --- |
| Disabled | 0 |  |
| Enabled | 1 |  |
<h3 class="decleration enum" title="TapButtonMap enum">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=tapbuttonmap" id="tapbuttonmap">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverLibinputDeviceV1.<span class="enum">TapButtonMap</span>
    </a>
</h3>

```csharp
public enum TapButtonMap
```

| Value | Integer | Description |
| --- | --- | --- |
| Lrm | 0 | 1/2/3 finger tap maps to left/right/middle |
| Lmr | 1 | 1/2/3 finger tap maps to left/middle/right |
<h3 class="decleration enum" title="DragState enum">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=dragstate" id="dragstate">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverLibinputDeviceV1.<span class="enum">DragState</span>
    </a>
</h3>

```csharp
public enum DragState
```

| Value | Integer | Description |
| --- | --- | --- |
| Disabled | 0 |  |
| Enabled | 1 |  |
<h3 class="decleration enum" title="DragLockState enum">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=draglockstate" id="draglockstate">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverLibinputDeviceV1.<span class="enum">DragLockState</span>
    </a>
</h3>

```csharp
public enum DragLockState
```

| Value | Integer | Description |
| --- | --- | --- |
| Disabled | 0 |  |
| EnabledTimeout | 1 |  |
| EnabledSticky | 2 |  |
<h3 class="decleration enum" title="ThreeFingerDragState enum">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=threefingerdragstate" id="threefingerdragstate">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverLibinputDeviceV1.<span class="enum">ThreeFingerDragState</span>
    </a>
</h3>

```csharp
public enum ThreeFingerDragState
```

| Value | Integer | Description |
| --- | --- | --- |
| Disabled | 0 |  |
| Enabled3fg | 1 |  |
| Enabled4fg | 2 |  |
<h3 class="decleration enum" title="AccelProfile enum">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=accelprofile" id="accelprofile">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverLibinputDeviceV1.<span class="enum">AccelProfile</span>
    </a>
</h3>

```csharp
public enum AccelProfile
```

| Value | Integer | Description |
| --- | --- | --- |
| None | 0 |  |
| Flat | 1 |  |
| Adaptive | 2 |  |
| Custom | 4 |  |
<h3 class="decleration enum" title="AccelProfiles enum">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=accelprofiles" id="accelprofiles">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverLibinputDeviceV1.<span class="enum">AccelProfiles</span>
    </a>
</h3>

```csharp
public enum AccelProfilesFlag
```

| Value | Integer | Description |
| --- | --- | --- |
| None | 0 |  |
| Flat | 1 |  |
| Adaptive | 2 |  |
| Custom | 4 |  |
<h3 class="decleration enum" title="NaturalScrollState enum">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=naturalscrollstate" id="naturalscrollstate">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverLibinputDeviceV1.<span class="enum">NaturalScrollState</span>
    </a>
</h3>

```csharp
public enum NaturalScrollState
```

| Value | Integer | Description |
| --- | --- | --- |
| Disabled | 0 |  |
| Enabled | 1 |  |
<h3 class="decleration enum" title="LeftHandedState enum">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=lefthandedstate" id="lefthandedstate">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverLibinputDeviceV1.<span class="enum">LeftHandedState</span>
    </a>
</h3>

```csharp
public enum LeftHandedState
```

| Value | Integer | Description |
| --- | --- | --- |
| Disabled | 0 |  |
| Enabled | 1 |  |
<h3 class="decleration enum" title="ClickMethod enum">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=clickmethod" id="clickmethod">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverLibinputDeviceV1.<span class="enum">ClickMethod</span>
    </a>
</h3>

```csharp
public enum ClickMethod
```

| Value | Integer | Description |
| --- | --- | --- |
| None | 0 |  |
| ButtonAreas | 1 |  |
| Clickfinger | 2 |  |
<h3 class="decleration enum" title="ClickMethods enum">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=clickmethods" id="clickmethods">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverLibinputDeviceV1.<span class="enum">ClickMethods</span>
    </a>
</h3>

```csharp
public enum ClickMethodsFlag
```

| Value | Integer | Description |
| --- | --- | --- |
| None | 0 |  |
| ButtonAreas | 1 |  |
| Clickfinger | 2 |  |
<h3 class="decleration enum" title="ClickfingerButtonMap enum">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=clickfingerbuttonmap" id="clickfingerbuttonmap">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverLibinputDeviceV1.<span class="enum">ClickfingerButtonMap</span>
    </a>
</h3>

```csharp
public enum ClickfingerButtonMap
```

| Value | Integer | Description |
| --- | --- | --- |
| Lrm | 0 |  |
| Lmr | 1 |  |
<h3 class="decleration enum" title="MiddleEmulationState enum">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=middleemulationstate" id="middleemulationstate">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverLibinputDeviceV1.<span class="enum">MiddleEmulationState</span>
    </a>
</h3>

```csharp
public enum MiddleEmulationState
```

| Value | Integer | Description |
| --- | --- | --- |
| Disabled | 0 |  |
| Enabled | 1 |  |
<h3 class="decleration enum" title="ScrollMethod enum">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=scrollmethod" id="scrollmethod">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverLibinputDeviceV1.<span class="enum">ScrollMethod</span>
    </a>
</h3>

```csharp
public enum ScrollMethod
```

| Value | Integer | Description |
| --- | --- | --- |
| NoScroll | 0 |  |
| TwoFinger | 1 |  |
| Edge | 2 |  |
| OnButtonDown | 4 |  |
<h3 class="decleration enum" title="ScrollMethods enum">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=scrollmethods" id="scrollmethods">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverLibinputDeviceV1.<span class="enum">ScrollMethods</span>
    </a>
</h3>

```csharp
public enum ScrollMethodsFlag
```

| Value | Integer | Description |
| --- | --- | --- |
| NoScroll | 0 |  |
| TwoFinger | 1 |  |
| Edge | 2 |  |
| OnButtonDown | 4 |  |
<h3 class="decleration enum" title="ScrollButtonLockState enum">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=scrollbuttonlockstate" id="scrollbuttonlockstate">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverLibinputDeviceV1.<span class="enum">ScrollButtonLockState</span>
    </a>
</h3>

```csharp
public enum ScrollButtonLockState
```

| Value | Integer | Description |
| --- | --- | --- |
| Disabled | 0 |  |
| Enabled | 1 |  |
<h3 class="decleration enum" title="DwtState enum">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=dwtstate" id="dwtstate">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverLibinputDeviceV1.<span class="enum">DwtState</span>
    </a>
</h3>

```csharp
public enum DwtState
```

| Value | Integer | Description |
| --- | --- | --- |
| Disabled | 0 |  |
| Enabled | 1 |  |
<h3 class="decleration enum" title="DwtpState enum">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=dwtpstate" id="dwtpstate">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverLibinputDeviceV1.<span class="enum">DwtpState</span>
    </a>
</h3>

```csharp
public enum DwtpState
```

| Value | Integer | Description |
| --- | --- | --- |
| Disabled | 0 |  |
| Enabled | 1 |  |
<h2 class="decleration interface">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=riverlibinputaccelconfigv1" id="riverlibinputaccelconfigv1">
        <span class="codicon codicon-symbol-interface"></span>
        RiverLibinputAccelConfigV1
    </a>
    <span class="pill">version 1</span>
</h2>

Acceleration config


The result returned by libinput on setting configuration for a device.


<h3 class="decleration request" title="Destroy request">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=riverlibinputaccelconfigv1_destroy" id="riverlibinputaccelconfigv1_destroy">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLibinputAccelConfigV1.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the accel object**

This request indicates that the client will no longer use the accel
config object and that it may be safely destroyed.

<h3 class="decleration request" title="SetPoints request">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=riverlibinputaccelconfigv1_setpoints" id="riverlibinputaccelconfigv1_setpoints">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLibinputAccelConfigV1.<span class="method">SetPoints</span>
    </a>
</h3>

```csharp
RiverLibinputResultV1 SetPoints(uint type, byte[] step, byte[] points)
```

| Argument | Type | Description |
| --- | --- | --- |
| result | new_id |  |
| type | uint |  |
| step | array | Double |
| points | array | Array of doubles |

**Define custom acceleration function**

Defines the acceleration function for a given movement type
in an acceleration configuration with custom accel profile.

<h3 class="decleration enum" title="Error enum">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=error" id="error">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverLibinputAccelConfigV1.<span class="enum">Error</span>
    </a>
</h3>

```csharp
public enum Error
```

| Value | Integer | Description |
| --- | --- | --- |
| InvalidArg | 0 | Invalid enum value or similar |
<h3 class="decleration enum" title="AccelType enum">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=acceltype" id="acceltype">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverLibinputAccelConfigV1.<span class="enum">AccelType</span>
    </a>
</h3>

```csharp
public enum AccelType
```

| Value | Integer | Description |
| --- | --- | --- |
| Fallback | 0 |  |
| Motion | 1 |  |
| Scroll | 2 |  |
<h2 class="decleration interface">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=riverlibinputresultv1" id="riverlibinputresultv1">
        <span class="codicon codicon-symbol-interface"></span>
        RiverLibinputResultV1
    </a>
    <span class="pill">version 1</span>
</h2>

Config application result


The result returned by libinput on setting configuration for a device.


<h3 class="decleration event" title="Success event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputresultv1_success" id="onriverlibinputresultv1_success">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputResultV1.<span class="event">OnSuccess</span>
    </a>
</h3>

```csharp
void SuccessHandler()
```


**Config success**

The configuration was successfully applied to the device.

<h3 class="decleration event" title="Unsupported event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputresultv1_unsupported" id="onriverlibinputresultv1_unsupported">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputResultV1.<span class="event">OnUnsupported</span>
    </a>
</h3>

```csharp
void UnsupportedHandler()
```


**Config unsupported**

The configuration is unsupported by the device and was ignored.

<h3 class="decleration event" title="Invalid event">
    <a href="#/Protocols/River/river-libinput-config-v1/?id=onriverlibinputresultv1_invalid" id="onriverlibinputresultv1_invalid">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputResultV1.<span class="event">OnInvalid</span>
    </a>
</h3>

```csharp
void InvalidHandler()
```


**Config invalid**

The configuration is invalid and was ignored.

