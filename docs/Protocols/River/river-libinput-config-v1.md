# River LibInput Config

##### [WaylandDotnet](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet) ![](../../assets/arrow.svg ':class=breadcrumb-arrow') [River](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/River) ![](../../assets/arrow.svg ':class=breadcrumb-arrow') [RiverLibinputConfigV1](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/River/river-libinput-config-v1/)

---

<h2 class="decleration interface">
    <a href="?id=RiverLibinputConfigV1" id="RiverLibinputConfigV1">
        <span class="codicon codicon-symbol-interface"></span>
        RiverLibinputConfigV1
    </a>
    <span class="pill">version 1</span>
</h2>

Libinput config global interface


Global interface for configuring libinput devices. This global should
only be advertised if river_input_manager_v1 is advertised as well.


<h3 class="decleration request" title="Stop request">
    <a href="?id=RiverLibinputConfigV1_Stop" id="RiverLibinputConfigV1_Stop">
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
    <a href="?id=RiverLibinputConfigV1_Destroy" id="RiverLibinputConfigV1_Destroy">
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
    <a href="?id=RiverLibinputConfigV1_CreateAccelConfig" id="RiverLibinputConfigV1_CreateAccelConfig">
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
    <a href="?id=OnRiverLibinputConfigV1_Finished" id="OnRiverLibinputConfigV1_Finished">
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
    <a href="?id=OnRiverLibinputConfigV1_LibinputDevice" id="OnRiverLibinputConfigV1_LibinputDevice">
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

<h2 class="decleration interface">
    <a href="?id=RiverLibinputDeviceV1" id="RiverLibinputDeviceV1">
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
    <a href="?id=RiverLibinputDeviceV1_Destroy" id="RiverLibinputDeviceV1_Destroy">
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
    <a href="?id=RiverLibinputDeviceV1_SetSendEvents" id="RiverLibinputDeviceV1_SetSendEvents">
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
    <a href="?id=RiverLibinputDeviceV1_SetTap" id="RiverLibinputDeviceV1_SetTap">
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
    <a href="?id=RiverLibinputDeviceV1_SetTapButtonMap" id="RiverLibinputDeviceV1_SetTapButtonMap">
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
    <a href="?id=RiverLibinputDeviceV1_SetDrag" id="RiverLibinputDeviceV1_SetDrag">
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
    <a href="?id=RiverLibinputDeviceV1_SetDragLock" id="RiverLibinputDeviceV1_SetDragLock">
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
    <a href="?id=RiverLibinputDeviceV1_SetThreeFingerDrag" id="RiverLibinputDeviceV1_SetThreeFingerDrag">
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
    <a href="?id=RiverLibinputDeviceV1_SetCalibrationMatrix" id="RiverLibinputDeviceV1_SetCalibrationMatrix">
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
    <a href="?id=RiverLibinputDeviceV1_SetAccelProfile" id="RiverLibinputDeviceV1_SetAccelProfile">
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
    <a href="?id=RiverLibinputDeviceV1_SetAccelSpeed" id="RiverLibinputDeviceV1_SetAccelSpeed">
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
    <a href="?id=RiverLibinputDeviceV1_ApplyAccelConfig" id="RiverLibinputDeviceV1_ApplyAccelConfig">
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
    <a href="?id=RiverLibinputDeviceV1_SetNaturalScroll" id="RiverLibinputDeviceV1_SetNaturalScroll">
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
    <a href="?id=RiverLibinputDeviceV1_SetLeftHanded" id="RiverLibinputDeviceV1_SetLeftHanded">
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
    <a href="?id=RiverLibinputDeviceV1_SetClickMethod" id="RiverLibinputDeviceV1_SetClickMethod">
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
    <a href="?id=RiverLibinputDeviceV1_SetClickfingerButtonMap" id="RiverLibinputDeviceV1_SetClickfingerButtonMap">
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
    <a href="?id=RiverLibinputDeviceV1_SetMiddleEmulation" id="RiverLibinputDeviceV1_SetMiddleEmulation">
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
    <a href="?id=RiverLibinputDeviceV1_SetScrollMethod" id="RiverLibinputDeviceV1_SetScrollMethod">
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
    <a href="?id=RiverLibinputDeviceV1_SetScrollButton" id="RiverLibinputDeviceV1_SetScrollButton">
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
    <a href="?id=RiverLibinputDeviceV1_SetScrollButtonLock" id="RiverLibinputDeviceV1_SetScrollButtonLock">
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
    <a href="?id=RiverLibinputDeviceV1_SetDwt" id="RiverLibinputDeviceV1_SetDwt">
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
    <a href="?id=RiverLibinputDeviceV1_SetDwtp" id="RiverLibinputDeviceV1_SetDwtp">
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
    <a href="?id=RiverLibinputDeviceV1_SetRotation" id="RiverLibinputDeviceV1_SetRotation">
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
    <a href="?id=OnRiverLibinputDeviceV1_Removed" id="OnRiverLibinputDeviceV1_Removed">
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
    <a href="?id=OnRiverLibinputDeviceV1_InputDevice" id="OnRiverLibinputDeviceV1_InputDevice">
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
    <a href="?id=OnRiverLibinputDeviceV1_SendEventsSupport" id="OnRiverLibinputDeviceV1_SendEventsSupport">
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
    <a href="?id=OnRiverLibinputDeviceV1_SendEventsDefault" id="OnRiverLibinputDeviceV1_SendEventsDefault">
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
    <a href="?id=OnRiverLibinputDeviceV1_SendEventsCurrent" id="OnRiverLibinputDeviceV1_SendEventsCurrent">
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
    <a href="?id=OnRiverLibinputDeviceV1_TapSupport" id="OnRiverLibinputDeviceV1_TapSupport">
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
    <a href="?id=OnRiverLibinputDeviceV1_TapDefault" id="OnRiverLibinputDeviceV1_TapDefault">
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
    <a href="?id=OnRiverLibinputDeviceV1_TapCurrent" id="OnRiverLibinputDeviceV1_TapCurrent">
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
    <a href="?id=OnRiverLibinputDeviceV1_TapButtonMapDefault" id="OnRiverLibinputDeviceV1_TapButtonMapDefault">
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
    <a href="?id=OnRiverLibinputDeviceV1_TapButtonMapCurrent" id="OnRiverLibinputDeviceV1_TapButtonMapCurrent">
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
    <a href="?id=OnRiverLibinputDeviceV1_DragDefault" id="OnRiverLibinputDeviceV1_DragDefault">
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
    <a href="?id=OnRiverLibinputDeviceV1_DragCurrent" id="OnRiverLibinputDeviceV1_DragCurrent">
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
    <a href="?id=OnRiverLibinputDeviceV1_DragLockDefault" id="OnRiverLibinputDeviceV1_DragLockDefault">
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
    <a href="?id=OnRiverLibinputDeviceV1_DragLockCurrent" id="OnRiverLibinputDeviceV1_DragLockCurrent">
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
    <a href="?id=OnRiverLibinputDeviceV1_ThreeFingerDragSupport" id="OnRiverLibinputDeviceV1_ThreeFingerDragSupport">
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
    <a href="?id=OnRiverLibinputDeviceV1_ThreeFingerDragDefault" id="OnRiverLibinputDeviceV1_ThreeFingerDragDefault">
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
    <a href="?id=OnRiverLibinputDeviceV1_ThreeFingerDragCurrent" id="OnRiverLibinputDeviceV1_ThreeFingerDragCurrent">
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
    <a href="?id=OnRiverLibinputDeviceV1_CalibrationMatrixSupport" id="OnRiverLibinputDeviceV1_CalibrationMatrixSupport">
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
    <a href="?id=OnRiverLibinputDeviceV1_CalibrationMatrixDefault" id="OnRiverLibinputDeviceV1_CalibrationMatrixDefault">
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
    <a href="?id=OnRiverLibinputDeviceV1_CalibrationMatrixCurrent" id="OnRiverLibinputDeviceV1_CalibrationMatrixCurrent">
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
    <a href="?id=OnRiverLibinputDeviceV1_AccelProfilesSupport" id="OnRiverLibinputDeviceV1_AccelProfilesSupport">
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
    <a href="?id=OnRiverLibinputDeviceV1_AccelProfileDefault" id="OnRiverLibinputDeviceV1_AccelProfileDefault">
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
    <a href="?id=OnRiverLibinputDeviceV1_AccelProfileCurrent" id="OnRiverLibinputDeviceV1_AccelProfileCurrent">
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
    <a href="?id=OnRiverLibinputDeviceV1_AccelSpeedDefault" id="OnRiverLibinputDeviceV1_AccelSpeedDefault">
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
    <a href="?id=OnRiverLibinputDeviceV1_AccelSpeedCurrent" id="OnRiverLibinputDeviceV1_AccelSpeedCurrent">
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
    <a href="?id=OnRiverLibinputDeviceV1_NaturalScrollSupport" id="OnRiverLibinputDeviceV1_NaturalScrollSupport">
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
    <a href="?id=OnRiverLibinputDeviceV1_NaturalScrollDefault" id="OnRiverLibinputDeviceV1_NaturalScrollDefault">
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
    <a href="?id=OnRiverLibinputDeviceV1_NaturalScrollCurrent" id="OnRiverLibinputDeviceV1_NaturalScrollCurrent">
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
    <a href="?id=OnRiverLibinputDeviceV1_LeftHandedSupport" id="OnRiverLibinputDeviceV1_LeftHandedSupport">
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
    <a href="?id=OnRiverLibinputDeviceV1_LeftHandedDefault" id="OnRiverLibinputDeviceV1_LeftHandedDefault">
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
    <a href="?id=OnRiverLibinputDeviceV1_LeftHandedCurrent" id="OnRiverLibinputDeviceV1_LeftHandedCurrent">
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
    <a href="?id=OnRiverLibinputDeviceV1_ClickMethodSupport" id="OnRiverLibinputDeviceV1_ClickMethodSupport">
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
    <a href="?id=OnRiverLibinputDeviceV1_ClickMethodDefault" id="OnRiverLibinputDeviceV1_ClickMethodDefault">
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
    <a href="?id=OnRiverLibinputDeviceV1_ClickMethodCurrent" id="OnRiverLibinputDeviceV1_ClickMethodCurrent">
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
    <a href="?id=OnRiverLibinputDeviceV1_ClickfingerButtonMapDefault" id="OnRiverLibinputDeviceV1_ClickfingerButtonMapDefault">
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
    <a href="?id=OnRiverLibinputDeviceV1_ClickfingerButtonMapCurrent" id="OnRiverLibinputDeviceV1_ClickfingerButtonMapCurrent">
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
    <a href="?id=OnRiverLibinputDeviceV1_MiddleEmulationSupport" id="OnRiverLibinputDeviceV1_MiddleEmulationSupport">
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
    <a href="?id=OnRiverLibinputDeviceV1_MiddleEmulationDefault" id="OnRiverLibinputDeviceV1_MiddleEmulationDefault">
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
    <a href="?id=OnRiverLibinputDeviceV1_MiddleEmulationCurrent" id="OnRiverLibinputDeviceV1_MiddleEmulationCurrent">
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
    <a href="?id=OnRiverLibinputDeviceV1_ScrollMethodSupport" id="OnRiverLibinputDeviceV1_ScrollMethodSupport">
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
    <a href="?id=OnRiverLibinputDeviceV1_ScrollMethodDefault" id="OnRiverLibinputDeviceV1_ScrollMethodDefault">
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
    <a href="?id=OnRiverLibinputDeviceV1_ScrollMethodCurrent" id="OnRiverLibinputDeviceV1_ScrollMethodCurrent">
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
    <a href="?id=OnRiverLibinputDeviceV1_ScrollButtonDefault" id="OnRiverLibinputDeviceV1_ScrollButtonDefault">
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
    <a href="?id=OnRiverLibinputDeviceV1_ScrollButtonCurrent" id="OnRiverLibinputDeviceV1_ScrollButtonCurrent">
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
    <a href="?id=OnRiverLibinputDeviceV1_ScrollButtonLockDefault" id="OnRiverLibinputDeviceV1_ScrollButtonLockDefault">
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
    <a href="?id=OnRiverLibinputDeviceV1_ScrollButtonLockCurrent" id="OnRiverLibinputDeviceV1_ScrollButtonLockCurrent">
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
    <a href="?id=OnRiverLibinputDeviceV1_DwtSupport" id="OnRiverLibinputDeviceV1_DwtSupport">
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
    <a href="?id=OnRiverLibinputDeviceV1_DwtDefault" id="OnRiverLibinputDeviceV1_DwtDefault">
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
    <a href="?id=OnRiverLibinputDeviceV1_DwtCurrent" id="OnRiverLibinputDeviceV1_DwtCurrent">
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
    <a href="?id=OnRiverLibinputDeviceV1_DwtpSupport" id="OnRiverLibinputDeviceV1_DwtpSupport">
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
    <a href="?id=OnRiverLibinputDeviceV1_DwtpDefault" id="OnRiverLibinputDeviceV1_DwtpDefault">
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
    <a href="?id=OnRiverLibinputDeviceV1_DwtpCurrent" id="OnRiverLibinputDeviceV1_DwtpCurrent">
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
    <a href="?id=OnRiverLibinputDeviceV1_RotationSupport" id="OnRiverLibinputDeviceV1_RotationSupport">
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
    <a href="?id=OnRiverLibinputDeviceV1_RotationDefault" id="OnRiverLibinputDeviceV1_RotationDefault">
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
    <a href="?id=OnRiverLibinputDeviceV1_RotationCurrent" id="OnRiverLibinputDeviceV1_RotationCurrent">
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

<h2 class="decleration interface">
    <a href="?id=RiverLibinputAccelConfigV1" id="RiverLibinputAccelConfigV1">
        <span class="codicon codicon-symbol-interface"></span>
        RiverLibinputAccelConfigV1
    </a>
    <span class="pill">version 1</span>
</h2>

Acceleration config


The result returned by libinput on setting configuration for a device.


<h3 class="decleration request" title="Destroy request">
    <a href="?id=RiverLibinputAccelConfigV1_Destroy" id="RiverLibinputAccelConfigV1_Destroy">
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
    <a href="?id=RiverLibinputAccelConfigV1_SetPoints" id="RiverLibinputAccelConfigV1_SetPoints">
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

<h2 class="decleration interface">
    <a href="?id=RiverLibinputResultV1" id="RiverLibinputResultV1">
        <span class="codicon codicon-symbol-interface"></span>
        RiverLibinputResultV1
    </a>
    <span class="pill">version 1</span>
</h2>

Config application result


The result returned by libinput on setting configuration for a device.


<h3 class="decleration event" title="Success event">
    <a href="?id=OnRiverLibinputResultV1_Success" id="OnRiverLibinputResultV1_Success">
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
    <a href="?id=OnRiverLibinputResultV1_Unsupported" id="OnRiverLibinputResultV1_Unsupported">
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
    <a href="?id=OnRiverLibinputResultV1_Invalid" id="OnRiverLibinputResultV1_Invalid">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLibinputResultV1.<span class="event">OnInvalid</span>
    </a>
</h3>

```csharp
void InvalidHandler()
```


**Config invalid**

The configuration is invalid and was ignored.

