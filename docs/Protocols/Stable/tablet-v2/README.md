# Tablet

##### [WaylandDotnet](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet) ![](../../../assets/arrow.svg ':class=breadcrumb-arrow') [Stable](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/Stable) ![](../../../assets/arrow.svg ':class=breadcrumb-arrow') [TabletV2](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/Stable/tablet-v2/)

---

<h2 class="decleration interface">
    <a href="#/Protocols/Stable/tablet-v2/?id=zwptabletmanagerv2" id="zwptabletmanagerv2">
        <span class="codicon codicon-symbol-interface"></span>
        ZwpTabletManagerV2
    </a>
    <span class="pill">version 2</span>
</h2>

Controller object for graphic tablet devices


An object that provides access to the graphics tablets available on this
system. All tablets are associated with a seat, to get access to the
actual tablets, use zwp_tablet_manager_v2.get_tablet_seat.


<h3 class="decleration request" title="GetTabletSeat request">
    <a href="#/Protocols/Stable/tablet-v2/?id=zwptabletmanagerv2_gettabletseat" id="zwptabletmanagerv2_gettabletseat">
        <span class="codicon codicon-symbol-method method"></span>
        ZwpTabletManagerV2.<span class="method">GetTabletSeat</span>
    </a>
</h3>

```csharp
ZwpTabletSeatV2 GetTabletSeat(WlSeat seat)
```

| Argument | Type | Description |
| --- | --- | --- |
| tablet_seat | new_id |  |
| seat | object | The wl_seat object to retrieve the tablets for |

**Get the tablet seat**

Get the zwp_tablet_seat_v2 object for the given seat. This object
provides access to all graphics tablets in this seat.

<h3 class="decleration request" title="Destroy request">
    <a href="#/Protocols/Stable/tablet-v2/?id=zwptabletmanagerv2_destroy" id="zwptabletmanagerv2_destroy">
        <span class="codicon codicon-symbol-method method"></span>
        ZwpTabletManagerV2.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Release the memory for the tablet manager object**

Destroy the zwp_tablet_manager_v2 object. Objects created from this
object are unaffected and should be destroyed separately.

<h2 class="decleration interface">
    <a href="#/Protocols/Stable/tablet-v2/?id=zwptabletseatv2" id="zwptabletseatv2">
        <span class="codicon codicon-symbol-interface"></span>
        ZwpTabletSeatV2
    </a>
    <span class="pill">version 2</span>
</h2>

Controller object for graphic tablet devices of a seat


An object that provides access to the graphics tablets available on this
seat. After binding to this interface, the compositor sends a set of
zwp_tablet_seat_v2.tablet_added and zwp_tablet_seat_v2.tool_added events.


<h3 class="decleration request" title="Destroy request">
    <a href="#/Protocols/Stable/tablet-v2/?id=zwptabletseatv2_destroy" id="zwptabletseatv2_destroy">
        <span class="codicon codicon-symbol-method method"></span>
        ZwpTabletSeatV2.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Release the memory for the tablet seat object**

Destroy the zwp_tablet_seat_v2 object. Objects created from this
object are unaffected and should be destroyed separately.

<h3 class="decleration event" title="TabletAdded event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletseatv2_tabletadded" id="onzwptabletseatv2_tabletadded">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletSeatV2.<span class="event">OnTabletAdded</span>
    </a>
</h3>

```csharp
void TabletAddedHandler(ZwpTabletV2 id)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id | The newly added graphics tablet |

**New device notification**

This event is sent whenever a new tablet becomes available on this
seat. This event only provides the object id of the tablet, any
static information about the tablet (device name, vid/pid, etc.) is
sent through the zwp_tablet_v2 interface.

<h3 class="decleration event" title="ToolAdded event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletseatv2_tooladded" id="onzwptabletseatv2_tooladded">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletSeatV2.<span class="event">OnToolAdded</span>
    </a>
</h3>

```csharp
void ToolAddedHandler(ZwpTabletToolV2 id)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id | The newly added tablet tool |

**A new tool has been used with a tablet**

This event is sent whenever a tool that has not previously been used
with a tablet comes into use. This event only provides the object id
of the tool; any static information about the tool (capabilities,
type, etc.) is sent through the zwp_tablet_tool_v2 interface.

<h3 class="decleration event" title="PadAdded event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletseatv2_padadded" id="onzwptabletseatv2_padadded">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletSeatV2.<span class="event">OnPadAdded</span>
    </a>
</h3>

```csharp
void PadAddedHandler(ZwpTabletPadV2 id)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id | The newly added pad |

**New pad notification**

This event is sent whenever a new pad is known to the system. Typically,
pads are physically attached to tablets and a pad_added event is
sent immediately after the zwp_tablet_seat_v2.tablet_added.
However, some standalone pad devices logically attach to tablets at
runtime, and the client must wait for zwp_tablet_pad_v2.enter to know
the tablet a pad is attached to.

This event only provides the object id of the pad. All further
features (buttons, strips, rings) are sent through the zwp_tablet_pad_v2
interface.

<h2 class="decleration interface">
    <a href="#/Protocols/Stable/tablet-v2/?id=zwptablettoolv2" id="zwptablettoolv2">
        <span class="codicon codicon-symbol-interface"></span>
        ZwpTabletToolV2
    </a>
    <span class="pill">version 2</span>
</h2>

A physical tablet tool


An object that represents a physical tool that has been, or is
currently in use with a tablet in this seat. Each zwp_tablet_tool_v2
object stays valid until the client destroys it; the compositor
reuses the zwp_tablet_tool_v2 object to indicate that the object's
respective physical tool has come into proximity of a tablet again.

A zwp_tablet_tool_v2 object's relation to a physical tool depends on the
tablet's ability to report serial numbers. If the tablet supports
this capability, then the object represents a specific physical tool
and can be identified even when used on multiple tablets.

A tablet tool has a number of static characteristics, e.g. tool type,
hardware_serial and capabilities. These capabilities are sent in an
event sequence after the zwp_tablet_seat_v2.tool_added event before any
actual events from this tool. This initial event sequence is
terminated by a zwp_tablet_tool_v2.done event.

Tablet tool events are grouped by zwp_tablet_tool_v2.frame events.
Any events received before a zwp_tablet_tool_v2.frame event should be
considered part of the same hardware state change.


<h3 class="decleration request" title="SetCursor request">
    <a href="#/Protocols/Stable/tablet-v2/?id=zwptablettoolv2_setcursor" id="zwptablettoolv2_setcursor">
        <span class="codicon codicon-symbol-method method"></span>
        ZwpTabletToolV2.<span class="method">SetCursor</span>
    </a>
</h3>

```csharp
void SetCursor(uint serial, WlSurface? surface, int hotspotX, int hotspotY)
```

| Argument | Type | Description |
| --- | --- | --- |
| serial | uint | Serial of the proximity_in event |
| surface | object |  |
| hotspot_x | int | Surface-local x coordinate |
| hotspot_y | int | Surface-local y coordinate |

**Set the tablet tool's surface**

Sets the surface of the cursor used for this tool on the given
tablet. This request only takes effect if the tool is in proximity
of one of the requesting client's surfaces or the surface parameter
is the current pointer surface. If there was a previous surface set
with this request it is replaced. If surface is NULL, the cursor
image is hidden.

The parameters hotspot_x and hotspot_y define the position of the
pointer surface relative to the pointer location. Its top-left corner
is always at (x, y) - (hotspot_x, hotspot_y), where (x, y) are the
coordinates of the pointer location, in surface-local coordinates.

On surface.attach requests to the pointer surface, hotspot_x and
hotspot_y are decremented by the x and y parameters passed to the
request. Attach must be confirmed by wl_surface.commit as usual.

The hotspot can also be updated by passing the currently set pointer
surface to this request with new values for hotspot_x and hotspot_y.

The current and pending input regions of the wl_surface are cleared,
and wl_surface.set_input_region is ignored until the wl_surface is no
longer used as the cursor. When the use as a cursor ends, the current
and pending input regions become undefined, and the wl_surface is
unmapped.

This request gives the surface the role of a zwp_tablet_tool_v2 cursor. A
surface may only ever be used as the cursor surface for one
zwp_tablet_tool_v2. If the surface already has another role or has
previously been used as cursor surface for a different tool, a
protocol error is raised.

<h3 class="decleration request" title="Destroy request">
    <a href="#/Protocols/Stable/tablet-v2/?id=zwptablettoolv2_destroy" id="zwptablettoolv2_destroy">
        <span class="codicon codicon-symbol-method method"></span>
        ZwpTabletToolV2.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the tool object**

This destroys the client's resource for this tool object.

<h3 class="decleration event" title="Type event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptablettoolv2_type" id="onzwptablettoolv2_type">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletToolV2.<span class="event">OnType</span>
    </a>
</h3>

```csharp
void TypeHandler(uint toolType)
```

| Argument | Type | Description |
| --- | --- | --- |
| tool_type | uint | The physical tool type |

**Tool type**

The tool type is the high-level type of the tool and usually decides
the interaction expected from this tool.

This event is sent in the initial burst of events before the
zwp_tablet_tool_v2.done event.

<h3 class="decleration event" title="HardwareSerial event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptablettoolv2_hardwareserial" id="onzwptablettoolv2_hardwareserial">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletToolV2.<span class="event">OnHardwareSerial</span>
    </a>
</h3>

```csharp
void HardwareSerialHandler(uint hardwareSerialHi, uint hardwareSerialLo)
```

| Argument | Type | Description |
| --- | --- | --- |
| hardware_serial_hi | uint | The unique serial number of the tool, most significant bits |
| hardware_serial_lo | uint | The unique serial number of the tool, least significant bits |

**Unique hardware serial number of the tool**

If the physical tool can be identified by a unique 64-bit serial
number, this event notifies the client of this serial number.

If multiple tablets are available in the same seat and the tool is
uniquely identifiable by the serial number, that tool may move
between tablets.

Otherwise, if the tool has no serial number and this event is
missing, the tool is tied to the tablet it first comes into
proximity with. Even if the physical tool is used on multiple
tablets, separate zwp_tablet_tool_v2 objects will be created, one per
tablet.

This event is sent in the initial burst of events before the
zwp_tablet_tool_v2.done event.

<h3 class="decleration event" title="HardwareIdWacom event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptablettoolv2_hardwareidwacom" id="onzwptablettoolv2_hardwareidwacom">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletToolV2.<span class="event">OnHardwareIdWacom</span>
    </a>
</h3>

```csharp
void HardwareIdWacomHandler(uint hardwareIdHi, uint hardwareIdLo)
```

| Argument | Type | Description |
| --- | --- | --- |
| hardware_id_hi | uint | The hardware id, most significant bits |
| hardware_id_lo | uint | The hardware id, least significant bits |

**Hardware id notification in Wacom's format**

This event notifies the client of a hardware id available on this tool.

The hardware id is a device-specific 64-bit id that provides extra
information about the tool in use, beyond the wl_tool.type
enumeration. The format of the id is specific to tablets made by
Wacom Inc. For example, the hardware id of a Wacom Grip
Pen (a stylus) is 0x802.

This event is sent in the initial burst of events before the
zwp_tablet_tool_v2.done event.

<h3 class="decleration event" title="Capability event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptablettoolv2_capability" id="onzwptablettoolv2_capability">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletToolV2.<span class="event">OnCapability</span>
    </a>
</h3>

```csharp
void CapabilityHandler(uint capability)
```

| Argument | Type | Description |
| --- | --- | --- |
| capability | uint | The capability |

**Tool capability notification**

This event notifies the client of any capabilities of this tool,
beyond the main set of x/y axes and tip up/down detection.

One event is sent for each extra capability available on this tool.

This event is sent in the initial burst of events before the
zwp_tablet_tool_v2.done event.

<h3 class="decleration event" title="Done event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptablettoolv2_done" id="onzwptablettoolv2_done">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletToolV2.<span class="event">OnDone</span>
    </a>
</h3>

```csharp
void DoneHandler()
```


**Tool description events sequence complete**

This event signals the end of the initial burst of descriptive
events. A client may consider the static description of the tool to
be complete and finalize initialization of the tool.

<h3 class="decleration event" title="Removed event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptablettoolv2_removed" id="onzwptablettoolv2_removed">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletToolV2.<span class="event">OnRemoved</span>
    </a>
</h3>

```csharp
void RemovedHandler()
```


**Tool removed**

This event is sent when the tool is removed from the system and will
send no further events. Should the physical tool come back into
proximity later, a new zwp_tablet_tool_v2 object will be created.

It is compositor-dependent when a tool is removed. A compositor may
remove a tool on proximity out, tablet removal or any other reason.
A compositor may also keep a tool alive until shutdown.

If the tool is currently in proximity, a proximity_out event will be
sent before the removed event. See zwp_tablet_tool_v2.proximity_out for
the handling of any buttons logically down.

When this event is received, the client must zwp_tablet_tool_v2.destroy
the object.

<h3 class="decleration event" title="ProximityIn event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptablettoolv2_proximityin" id="onzwptablettoolv2_proximityin">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletToolV2.<span class="event">OnProximityIn</span>
    </a>
</h3>

```csharp
void ProximityInHandler(uint serial, ZwpTabletV2 tablet, WlSurface surface)
```

| Argument | Type | Description |
| --- | --- | --- |
| serial | uint |  |
| tablet | object | The tablet the tool is in proximity of |
| surface | object | The current surface the tablet tool is over |

**Proximity in event**

Notification that this tool is focused on a certain surface.

This event can be received when the tool has moved from one surface to
another, or when the tool has come back into proximity above the
surface.

If any button is logically down when the tool comes into proximity,
the respective button event is sent after the proximity_in event but
within the same frame as the proximity_in event.

<h3 class="decleration event" title="ProximityOut event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptablettoolv2_proximityout" id="onzwptablettoolv2_proximityout">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletToolV2.<span class="event">OnProximityOut</span>
    </a>
</h3>

```csharp
void ProximityOutHandler()
```


**Proximity out event**

Notification that this tool has either left proximity, or is no
longer focused on a certain surface.

When the tablet tool leaves proximity of the tablet, button release
events are sent for each button that was held down at the time of
leaving proximity. These events are sent before the proximity_out
event but within the same zwp_tablet_v2.frame.

If the tool stays within proximity of the tablet, but the focus
changes from one surface to another, a button release event may not
be sent until the button is actually released or the tool leaves the
proximity of the tablet.

<h3 class="decleration event" title="Down event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptablettoolv2_down" id="onzwptablettoolv2_down">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletToolV2.<span class="event">OnDown</span>
    </a>
</h3>

```csharp
void DownHandler(uint serial)
```

| Argument | Type | Description |
| --- | --- | --- |
| serial | uint |  |

**Tablet tool is making contact**

Sent whenever the tablet tool comes in contact with the surface of the
tablet.

If the tool is already in contact with the tablet when entering the
input region, the client owning said region will receive a
zwp_tablet_v2.proximity_in event, followed by a zwp_tablet_v2.down
event and a zwp_tablet_v2.frame event.

Note that this event describes logical contact, not physical
contact. On some devices, a compositor may not consider a tool in
logical contact until a minimum physical pressure threshold is
exceeded.

<h3 class="decleration event" title="Up event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptablettoolv2_up" id="onzwptablettoolv2_up">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletToolV2.<span class="event">OnUp</span>
    </a>
</h3>

```csharp
void UpHandler()
```


**Tablet tool is no longer making contact**

Sent whenever the tablet tool stops making contact with the surface of
the tablet, or when the tablet tool moves out of the input region
and the compositor grab (if any) is dismissed.

If the tablet tool moves out of the input region while in contact
with the surface of the tablet and the compositor does not have an
ongoing grab on the surface, the client owning said region will
receive a zwp_tablet_v2.up event, followed by a zwp_tablet_v2.proximity_out
event and a zwp_tablet_v2.frame event. If the compositor has an ongoing
grab on this device, this event sequence is sent whenever the grab
is dismissed in the future.

Note that this event describes logical contact, not physical
contact. On some devices, a compositor may not consider a tool out
of logical contact until physical pressure falls below a specific
threshold.

<h3 class="decleration event" title="Motion event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptablettoolv2_motion" id="onzwptablettoolv2_motion">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletToolV2.<span class="event">OnMotion</span>
    </a>
</h3>

```csharp
void MotionHandler(WlFixed x, WlFixed y)
```

| Argument | Type | Description |
| --- | --- | --- |
| x | fixed | Surface-local x coordinate |
| y | fixed | Surface-local y coordinate |

**Motion event**

Sent whenever a tablet tool moves.

<h3 class="decleration event" title="Pressure event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptablettoolv2_pressure" id="onzwptablettoolv2_pressure">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletToolV2.<span class="event">OnPressure</span>
    </a>
</h3>

```csharp
void PressureHandler(uint pressure)
```

| Argument | Type | Description |
| --- | --- | --- |
| pressure | uint | The current pressure value |

**Pressure change event**

Sent whenever the pressure axis on a tool changes. The value of this
event is normalized to a value between 0 and 65535.

Note that pressure may be nonzero even when a tool is not in logical
contact. See the down and up events for more details.

<h3 class="decleration event" title="Distance event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptablettoolv2_distance" id="onzwptablettoolv2_distance">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletToolV2.<span class="event">OnDistance</span>
    </a>
</h3>

```csharp
void DistanceHandler(uint distance)
```

| Argument | Type | Description |
| --- | --- | --- |
| distance | uint | The current distance value |

**Distance change event**

Sent whenever the distance axis on a tool changes. The value of this
event is normalized to a value between 0 and 65535.

Note that distance may be nonzero even when a tool is not in logical
contact. See the down and up events for more details.

<h3 class="decleration event" title="Tilt event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptablettoolv2_tilt" id="onzwptablettoolv2_tilt">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletToolV2.<span class="event">OnTilt</span>
    </a>
</h3>

```csharp
void TiltHandler(WlFixed tiltX, WlFixed tiltY)
```

| Argument | Type | Description |
| --- | --- | --- |
| tilt_x | fixed | The current value of the X tilt axis |
| tilt_y | fixed | The current value of the Y tilt axis |

**Tilt change event**

Sent whenever one or both of the tilt axes on a tool change. Each tilt
value is in degrees, relative to the z-axis of the tablet.
The angle is positive when the top of a tool tilts along the
positive x or y axis.

<h3 class="decleration event" title="Rotation event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptablettoolv2_rotation" id="onzwptablettoolv2_rotation">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletToolV2.<span class="event">OnRotation</span>
    </a>
</h3>

```csharp
void RotationHandler(WlFixed degrees)
```

| Argument | Type | Description |
| --- | --- | --- |
| degrees | fixed | The current rotation of the Z axis |

**Z-rotation change event**

Sent whenever the z-rotation axis on the tool changes. The
rotation value is in degrees clockwise from the tool's
logical neutral position.

<h3 class="decleration event" title="Slider event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptablettoolv2_slider" id="onzwptablettoolv2_slider">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletToolV2.<span class="event">OnSlider</span>
    </a>
</h3>

```csharp
void SliderHandler(int position)
```

| Argument | Type | Description |
| --- | --- | --- |
| position | int | The current position of slider |

**Slider position change event**

Sent whenever the slider position on the tool changes. The
value is normalized between -65535 and 65535, with 0 as the logical
neutral position of the slider.

The slider is available on e.g. the Wacom Airbrush tool.

<h3 class="decleration event" title="Wheel event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptablettoolv2_wheel" id="onzwptablettoolv2_wheel">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletToolV2.<span class="event">OnWheel</span>
    </a>
</h3>

```csharp
void WheelHandler(WlFixed degrees, int clicks)
```

| Argument | Type | Description |
| --- | --- | --- |
| degrees | fixed | The wheel delta in degrees |
| clicks | int | The wheel delta in discrete clicks |

**Wheel delta event**

Sent whenever the wheel on the tool emits an event. This event
contains two values for the same axis change. The degrees value is
in the same orientation as the wl_pointer.vertical_scroll axis. The
clicks value is in discrete logical clicks of the mouse wheel. This
value may be zero if the movement of the wheel was less
than one logical click.

Clients should choose either value and avoid mixing degrees and
clicks. The compositor may accumulate values smaller than a logical
click and emulate click events when a certain threshold is met.
Thus, zwp_tablet_tool_v2.wheel events with non-zero clicks values may
have different degrees values.

<h3 class="decleration event" title="Button event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptablettoolv2_button" id="onzwptablettoolv2_button">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletToolV2.<span class="event">OnButton</span>
    </a>
</h3>

```csharp
void ButtonHandler(uint serial, uint button, uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| serial | uint |  |
| button | uint | The button whose state has changed |
| state | uint | Whether the button was pressed or released |

**Button event**

Sent whenever a button on the tool is pressed or released.

If a button is held down when the tool moves in or out of proximity,
button events are generated by the compositor. See
zwp_tablet_tool_v2.proximity_in and zwp_tablet_tool_v2.proximity_out for
details.

<h3 class="decleration event" title="Frame event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptablettoolv2_frame" id="onzwptablettoolv2_frame">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletToolV2.<span class="event">OnFrame</span>
    </a>
</h3>

```csharp
void FrameHandler(uint time)
```

| Argument | Type | Description |
| --- | --- | --- |
| time | uint | The time of the event with millisecond granularity |

**Frame event**

Marks the end of a series of axis and/or button updates from the
tablet. The Wayland protocol requires axis updates to be sent
sequentially, however all events within a frame should be considered
one hardware event.

<h3 class="decleration enum" title="Type enum">
    <a href="#/Protocols/Stable/tablet-v2/?id=type" id="type">
        <span class="codicon codicon-symbol-enum enum"></span>
        ZwpTabletToolV2.<span class="enum">Type</span>
    </a>
</h3>

```csharp
public enum Type
```

A physical tool type


Describes the physical type of a tool. The physical type of a tool
generally defines its base usage.

The mouse tool represents a mouse-shaped tool that is not a relative
device but bound to the tablet's surface, providing absolute
coordinates.

The lens tool is a mouse-shaped tool with an attached lens to
provide precision focus.


| Value | Integer | Description |
| --- | --- | --- |
| Pen | 0x140 | Pen |
| Eraser | 0x141 | Eraser |
| Brush | 0x142 | Brush |
| Pencil | 0x143 | Pencil |
| Airbrush | 0x144 | Airbrush |
| Finger | 0x145 | Finger |
| Mouse | 0x146 | Mouse |
| Lens | 0x147 | Lens |
<h3 class="decleration enum" title="Capability enum">
    <a href="#/Protocols/Stable/tablet-v2/?id=capability" id="capability">
        <span class="codicon codicon-symbol-enum enum"></span>
        ZwpTabletToolV2.<span class="enum">Capability</span>
    </a>
</h3>

```csharp
public enum Capability
```

Capability flags for a tool


Describes extra capabilities on a tablet.

Any tool must provide x and y values, extra axes are
device-specific.


| Value | Integer | Description |
| --- | --- | --- |
| Tilt | 1 | Tilt axes |
| Pressure | 2 | Pressure axis |
| Distance | 3 | Distance axis |
| Rotation | 4 | Z-rotation axis |
| Slider | 5 | Slider axis |
| Wheel | 6 | Wheel axis |
<h3 class="decleration enum" title="ButtonState enum">
    <a href="#/Protocols/Stable/tablet-v2/?id=buttonstate" id="buttonstate">
        <span class="codicon codicon-symbol-enum enum"></span>
        ZwpTabletToolV2.<span class="enum">ButtonState</span>
    </a>
</h3>

```csharp
public enum ButtonState
```

Physical button state


Describes the physical state of a button that produced the button event.


| Value | Integer | Description |
| --- | --- | --- |
| Released | 0 | Button is not pressed |
| Pressed | 1 | Button is pressed |
<h3 class="decleration enum" title="Error enum">
    <a href="#/Protocols/Stable/tablet-v2/?id=error" id="error">
        <span class="codicon codicon-symbol-enum enum"></span>
        ZwpTabletToolV2.<span class="enum">Error</span>
    </a>
</h3>

```csharp
public enum Error
```

| Value | Integer | Description |
| --- | --- | --- |
| Role | 0 | Given wl_surface has another role |
<h2 class="decleration interface">
    <a href="#/Protocols/Stable/tablet-v2/?id=zwptabletv2" id="zwptabletv2">
        <span class="codicon codicon-symbol-interface"></span>
        ZwpTabletV2
    </a>
    <span class="pill">version 2</span>
</h2>

Graphics tablet device


The zwp_tablet_v2 interface represents one graphics tablet device. The
tablet interface itself does not generate events; all events are
generated by zwp_tablet_tool_v2 objects when in proximity above a tablet.

A tablet has a number of static characteristics, e.g. device name and
pid/vid. These capabilities are sent in an event sequence after the
zwp_tablet_seat_v2.tablet_added event. This initial event sequence is
terminated by a zwp_tablet_v2.done event.


<h3 class="decleration request" title="Destroy request">
    <a href="#/Protocols/Stable/tablet-v2/?id=zwptabletv2_destroy" id="zwptabletv2_destroy">
        <span class="codicon codicon-symbol-method method"></span>
        ZwpTabletV2.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the tablet object**

This destroys the client's resource for this tablet object.

<h3 class="decleration event" title="Name event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletv2_name" id="onzwptabletv2_name">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletV2.<span class="event">OnName</span>
    </a>
</h3>

```csharp
void NameHandler(string name)
```

| Argument | Type | Description |
| --- | --- | --- |
| name | string | The device name |

**Tablet device name**

A descriptive name for the tablet device.

If the device has no descriptive name, this event is not sent.

This event is sent in the initial burst of events before the
zwp_tablet_v2.done event.

<h3 class="decleration event" title="Id event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletv2_id" id="onzwptabletv2_id">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletV2.<span class="event">OnId</span>
    </a>
</h3>

```csharp
void IdHandler(uint vid, uint pid)
```

| Argument | Type | Description |
| --- | --- | --- |
| vid | uint | Vendor id |
| pid | uint | Product id |

**Tablet device vendor/product id**

The vendor and product IDs for the tablet device.

The interpretation of the id depends on the zwp_tablet_v2.bustype.
Prior to version v2 of this protocol, the id was implied to be a USB
vendor and product ID. If no zwp_tablet_v2.bustype is sent, the ID
is to be interpreted as USB vendor and product ID.

If the device has no vendor/product ID, this event is not sent.
This can happen for virtual devices or non-USB devices, for instance.

This event is sent in the initial burst of events before the
zwp_tablet_v2.done event.

<h3 class="decleration event" title="Path event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletv2_path" id="onzwptabletv2_path">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletV2.<span class="event">OnPath</span>
    </a>
</h3>

```csharp
void PathHandler(string path)
```

| Argument | Type | Description |
| --- | --- | --- |
| path | string | Path to local device |

**Path to the device**

A system-specific device path that indicates which device is behind
this zwp_tablet_v2. This information may be used to gather additional
information about the device, e.g. through libwacom.

A device may have more than one device path. If so, multiple
zwp_tablet_v2.path events are sent. A device may be emulated and not
have a device path, and in that case this event will not be sent.

The format of the path is unspecified, it may be a device node, a
sysfs path, or some other identifier. It is up to the client to
identify the string provided.

This event is sent in the initial burst of events before the
zwp_tablet_v2.done event.

<h3 class="decleration event" title="Done event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletv2_done" id="onzwptabletv2_done">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletV2.<span class="event">OnDone</span>
    </a>
</h3>

```csharp
void DoneHandler()
```


**Tablet description events sequence complete**

This event is sent immediately to signal the end of the initial
burst of descriptive events. A client may consider the static
description of the tablet to be complete and finalize initialization
of the tablet.

<h3 class="decleration event" title="Removed event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletv2_removed" id="onzwptabletv2_removed">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletV2.<span class="event">OnRemoved</span>
    </a>
</h3>

```csharp
void RemovedHandler()
```


**Tablet removed event**

Sent when the tablet has been removed from the system. When a tablet
is removed, some tools may be removed.

When this event is received, the client must zwp_tablet_v2.destroy
the object.

<h3 class="decleration event" title="Bustype event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletv2_bustype" id="onzwptabletv2_bustype">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletV2.<span class="event">OnBustype</span>
    </a>
    <span class="pill">since 2</span>
</h3>

```csharp
void BustypeHandler(uint bustype)
```

| Argument | Type | Description |
| --- | --- | --- |
| bustype | uint | Bus type |

**Tablet device bus type**

The bustype argument is one of the BUS_ defines in the Linux kernel's
linux/input.h

If the device has no known bustype or the bustype cannot be
queried, this event is not sent.

This event is sent in the initial burst of events before the
zwp_tablet_v2.done event.

<h3 class="decleration enum" title="Bustype enum">
    <a href="#/Protocols/Stable/tablet-v2/?id=bustype" id="bustype">
        <span class="codicon codicon-symbol-enum enum"></span>
        ZwpTabletV2.<span class="enum">Bustype</span>
    </a>
</h3>

```csharp
public enum Bustype
```

Bus type 


Describes the bus types this tablet is connected to.


| Value | Integer | Description |
| --- | --- | --- |
| Usb | 3 | USB |
| Bluetooth | 5 | Bluetooth |
| Virtual | 6 | Virtual |
| Serial | 17 | Serial |
| I2c | 24 | I2C |
<h2 class="decleration interface">
    <a href="#/Protocols/Stable/tablet-v2/?id=zwptabletpadringv2" id="zwptabletpadringv2">
        <span class="codicon codicon-symbol-interface"></span>
        ZwpTabletPadRingV2
    </a>
    <span class="pill">version 2</span>
</h2>

Pad ring


A circular interaction area, such as the touch ring on the Wacom Intuos
Pro series tablets.

Events on a ring are logically grouped by the zwp_tablet_pad_ring_v2.frame
event.


<h3 class="decleration request" title="SetFeedback request">
    <a href="#/Protocols/Stable/tablet-v2/?id=zwptabletpadringv2_setfeedback" id="zwptabletpadringv2_setfeedback">
        <span class="codicon codicon-symbol-method method"></span>
        ZwpTabletPadRingV2.<span class="method">SetFeedback</span>
    </a>
</h3>

```csharp
void SetFeedback(string description, uint serial)
```

| Argument | Type | Description |
| --- | --- | --- |
| description | string | Ring description |
| serial | uint | Serial of the mode switch event |

**Set compositor feedback**

Request that the compositor use the provided feedback string
associated with this ring. This request should be issued immediately
after a zwp_tablet_pad_group_v2.mode_switch event from the corresponding
group is received, or whenever the ring is mapped to a different
action. See zwp_tablet_pad_group_v2.mode_switch for more details.

Clients are encouraged to provide context-aware descriptions for
the actions associated with the ring; compositors may use this
information to offer visual feedback about the button layout
(eg. on-screen displays).

The provided string 'description' is a UTF-8 encoded string to be
associated with this ring, and is considered user-visible; general
internationalization rules apply.

The serial argument will be that of the last
zwp_tablet_pad_group_v2.mode_switch event received for the group of this
ring. Requests providing other serials than the most recent one will be
ignored.

<h3 class="decleration request" title="Destroy request">
    <a href="#/Protocols/Stable/tablet-v2/?id=zwptabletpadringv2_destroy" id="zwptabletpadringv2_destroy">
        <span class="codicon codicon-symbol-method method"></span>
        ZwpTabletPadRingV2.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the ring object**

This destroys the client's resource for this ring object.

<h3 class="decleration event" title="Source event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletpadringv2_source" id="onzwptabletpadringv2_source">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletPadRingV2.<span class="event">OnSource</span>
    </a>
</h3>

```csharp
void SourceHandler(uint source)
```

| Argument | Type | Description |
| --- | --- | --- |
| source | uint | The event source |

**Ring event source**

Source information for ring events.

This event does not occur on its own. It is sent before a
zwp_tablet_pad_ring_v2.frame event and carries the source information
for all events within that frame.

The source specifies how this event was generated. If the source is
zwp_tablet_pad_ring_v2.source.finger, a zwp_tablet_pad_ring_v2.stop event
will be sent when the user lifts the finger off the device.

This event is optional. If the source is unknown for an interaction,
no event is sent.

<h3 class="decleration event" title="Angle event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletpadringv2_angle" id="onzwptabletpadringv2_angle">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletPadRingV2.<span class="event">OnAngle</span>
    </a>
</h3>

```csharp
void AngleHandler(WlFixed degrees)
```

| Argument | Type | Description |
| --- | --- | --- |
| degrees | fixed | The current angle in degrees |

**Angle changed**

Sent whenever the angle on a ring changes.

The angle is provided in degrees clockwise from the logical
north of the ring in the pad's current rotation.

<h3 class="decleration event" title="Stop event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletpadringv2_stop" id="onzwptabletpadringv2_stop">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletPadRingV2.<span class="event">OnStop</span>
    </a>
</h3>

```csharp
void StopHandler()
```


**Interaction stopped**

Stop notification for ring events.

For some zwp_tablet_pad_ring_v2.source types, a zwp_tablet_pad_ring_v2.stop
event is sent to notify a client that the interaction with the ring
has terminated. This enables the client to implement kinetic scrolling.
See the zwp_tablet_pad_ring_v2.source documentation for information on
when this event may be generated.

Any zwp_tablet_pad_ring_v2.angle events with the same source after this
event should be considered as the start of a new interaction.

<h3 class="decleration event" title="Frame event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletpadringv2_frame" id="onzwptabletpadringv2_frame">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletPadRingV2.<span class="event">OnFrame</span>
    </a>
</h3>

```csharp
void FrameHandler(uint time)
```

| Argument | Type | Description |
| --- | --- | --- |
| time | uint | Timestamp with millisecond granularity |

**End of a ring event sequence**

Indicates the end of a set of ring events that logically belong
together. A client is expected to accumulate the data in all events
within the frame before proceeding.

All zwp_tablet_pad_ring_v2 events before a zwp_tablet_pad_ring_v2.frame event belong
logically together. For example, on termination of a finger interaction
on a ring the compositor will send a zwp_tablet_pad_ring_v2.source event,
a zwp_tablet_pad_ring_v2.stop event and a zwp_tablet_pad_ring_v2.frame event.

A zwp_tablet_pad_ring_v2.frame event is sent for every logical event
group, even if the group only contains a single zwp_tablet_pad_ring_v2
event. Specifically, a client may get a sequence: angle, frame,
angle, frame, etc.

<h3 class="decleration enum" title="Source enum">
    <a href="#/Protocols/Stable/tablet-v2/?id=source" id="source">
        <span class="codicon codicon-symbol-enum enum"></span>
        ZwpTabletPadRingV2.<span class="enum">Source</span>
    </a>
</h3>

```csharp
public enum Source
```

Ring axis source


Describes the source types for ring events. This indicates to the
client how a ring event was physically generated; a client may
adjust the user interface accordingly. For example, events
from a "finger" source may trigger kinetic scrolling.


| Value | Integer | Description |
| --- | --- | --- |
| Finger | 1 | Finger |
<h2 class="decleration interface">
    <a href="#/Protocols/Stable/tablet-v2/?id=zwptabletpadstripv2" id="zwptabletpadstripv2">
        <span class="codicon codicon-symbol-interface"></span>
        ZwpTabletPadStripV2
    </a>
    <span class="pill">version 2</span>
</h2>

Pad strip


A linear interaction area, such as the strips found in Wacom Cintiq
models.

Events on a strip are logically grouped by the zwp_tablet_pad_strip_v2.frame
event.


<h3 class="decleration request" title="SetFeedback request">
    <a href="#/Protocols/Stable/tablet-v2/?id=zwptabletpadstripv2_setfeedback" id="zwptabletpadstripv2_setfeedback">
        <span class="codicon codicon-symbol-method method"></span>
        ZwpTabletPadStripV2.<span class="method">SetFeedback</span>
    </a>
</h3>

```csharp
void SetFeedback(string description, uint serial)
```

| Argument | Type | Description |
| --- | --- | --- |
| description | string | Strip description |
| serial | uint | Serial of the mode switch event |

**Set compositor feedback**

Requests the compositor to use the provided feedback string
associated with this strip. This request should be issued immediately
after a zwp_tablet_pad_group_v2.mode_switch event from the corresponding
group is received, or whenever the strip is mapped to a different
action. See zwp_tablet_pad_group_v2.mode_switch for more details.

Clients are encouraged to provide context-aware descriptions for
the actions associated with the strip, and compositors may use this
information to offer visual feedback about the button layout
(eg. on-screen displays).

The provided string 'description' is a UTF-8 encoded string to be
associated with this ring, and is considered user-visible; general
internationalization rules apply.

The serial argument will be that of the last
zwp_tablet_pad_group_v2.mode_switch event received for the group of this
strip. Requests providing other serials than the most recent one will be
ignored.

<h3 class="decleration request" title="Destroy request">
    <a href="#/Protocols/Stable/tablet-v2/?id=zwptabletpadstripv2_destroy" id="zwptabletpadstripv2_destroy">
        <span class="codicon codicon-symbol-method method"></span>
        ZwpTabletPadStripV2.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the strip object**

This destroys the client's resource for this strip object.

<h3 class="decleration event" title="Source event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletpadstripv2_source" id="onzwptabletpadstripv2_source">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletPadStripV2.<span class="event">OnSource</span>
    </a>
</h3>

```csharp
void SourceHandler(uint source)
```

| Argument | Type | Description |
| --- | --- | --- |
| source | uint | The event source |

**Strip event source**

Source information for strip events.

This event does not occur on its own. It is sent before a
zwp_tablet_pad_strip_v2.frame event and carries the source information
for all events within that frame.

The source specifies how this event was generated. If the source is
zwp_tablet_pad_strip_v2.source.finger, a zwp_tablet_pad_strip_v2.stop event
will be sent when the user lifts their finger off the device.

This event is optional. If the source is unknown for an interaction,
no event is sent.

<h3 class="decleration event" title="Position event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletpadstripv2_position" id="onzwptabletpadstripv2_position">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletPadStripV2.<span class="event">OnPosition</span>
    </a>
</h3>

```csharp
void PositionHandler(uint position)
```

| Argument | Type | Description |
| --- | --- | --- |
| position | uint | The current position |

**Position changed**

Sent whenever the position on a strip changes.

The position is normalized to a range of [0, 65535], the 0-value
represents the top-most and/or left-most position of the strip in
the pad's current rotation.

<h3 class="decleration event" title="Stop event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletpadstripv2_stop" id="onzwptabletpadstripv2_stop">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletPadStripV2.<span class="event">OnStop</span>
    </a>
</h3>

```csharp
void StopHandler()
```


**Interaction stopped**

Stop notification for strip events.

For some zwp_tablet_pad_strip_v2.source types, a zwp_tablet_pad_strip_v2.stop
event is sent to notify a client that the interaction with the strip
has terminated. This enables the client to implement kinetic
scrolling. See the zwp_tablet_pad_strip_v2.source documentation for
information on when this event may be generated.

Any zwp_tablet_pad_strip_v2.position events with the same source after this
event should be considered as the start of a new interaction.

<h3 class="decleration event" title="Frame event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletpadstripv2_frame" id="onzwptabletpadstripv2_frame">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletPadStripV2.<span class="event">OnFrame</span>
    </a>
</h3>

```csharp
void FrameHandler(uint time)
```

| Argument | Type | Description |
| --- | --- | --- |
| time | uint | Timestamp with millisecond granularity |

**End of a strip event sequence**

Indicates the end of a set of events that represent one logical
hardware strip event. A client is expected to accumulate the data
in all events within the frame before proceeding.

All zwp_tablet_pad_strip_v2 events before a zwp_tablet_pad_strip_v2.frame event belong
logically together. For example, on termination of a finger interaction
on a strip the compositor will send a zwp_tablet_pad_strip_v2.source event,
a zwp_tablet_pad_strip_v2.stop event and a zwp_tablet_pad_strip_v2.frame
event.

A zwp_tablet_pad_strip_v2.frame event is sent for every logical event
group, even if the group only contains a single zwp_tablet_pad_strip_v2
event. Specifically, a client may get a sequence: position, frame,
position, frame, etc.

<h3 class="decleration enum" title="Source enum">
    <a href="#/Protocols/Stable/tablet-v2/?id=source" id="source">
        <span class="codicon codicon-symbol-enum enum"></span>
        ZwpTabletPadStripV2.<span class="enum">Source</span>
    </a>
</h3>

```csharp
public enum Source
```

Strip axis source


Describes the source types for strip events. This indicates to the
client how a strip event was physically generated; a client may
adjust the user interface accordingly. For example, events
from a "finger" source may trigger kinetic scrolling.


| Value | Integer | Description |
| --- | --- | --- |
| Finger | 1 | Finger |
<h2 class="decleration interface">
    <a href="#/Protocols/Stable/tablet-v2/?id=zwptabletpadgroupv2" id="zwptabletpadgroupv2">
        <span class="codicon codicon-symbol-interface"></span>
        ZwpTabletPadGroupV2
    </a>
    <span class="pill">version 2</span>
</h2>

A set of buttons, rings and strips


A pad group describes a distinct (sub)set of buttons, rings and strips
present in the tablet. The criteria of this grouping is usually positional,
eg. if a tablet has buttons on the left and right side, 2 groups will be
presented. The physical arrangement of groups is undisclosed and may
change on the fly.

Pad groups will announce their features during pad initialization. Between
the corresponding zwp_tablet_pad_v2.group event and zwp_tablet_pad_group_v2.done, the
pad group will announce the buttons, rings and strips contained in it,
plus the number of supported modes.

Modes are a mechanism to allow multiple groups of actions for every element
in the pad group. The number of groups and available modes in each is
persistent across device plugs. The current mode is user-switchable, it
will be announced through the zwp_tablet_pad_group_v2.mode_switch event both
whenever it is switched, and after zwp_tablet_pad_v2.enter.

The current mode logically applies to all elements in the pad group,
although it is at clients' discretion whether to actually perform different
actions, and/or issue the respective .set_feedback requests to notify the
compositor. See the zwp_tablet_pad_group_v2.mode_switch event for more details.


<h3 class="decleration request" title="Destroy request">
    <a href="#/Protocols/Stable/tablet-v2/?id=zwptabletpadgroupv2_destroy" id="zwptabletpadgroupv2_destroy">
        <span class="codicon codicon-symbol-method method"></span>
        ZwpTabletPadGroupV2.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the pad object**

Destroy the zwp_tablet_pad_group_v2 object. Objects created from this object
are unaffected and should be destroyed separately.

<h3 class="decleration event" title="Buttons event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletpadgroupv2_buttons" id="onzwptabletpadgroupv2_buttons">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletPadGroupV2.<span class="event">OnButtons</span>
    </a>
</h3>

```csharp
void ButtonsHandler(byte[] buttons)
```

| Argument | Type | Description |
| --- | --- | --- |
| buttons | array | Buttons in this group |

**Buttons announced**

Sent on zwp_tablet_pad_group_v2 initialization to announce the available
buttons in the group. Button indices start at 0, a button may only be
in one group at a time.

This event is first sent in the initial burst of events before the
zwp_tablet_pad_group_v2.done event.

Some buttons are reserved by the compositor. These buttons may not be
assigned to any zwp_tablet_pad_group_v2. Compositors may broadcast this
event in the case of changes to the mapping of these reserved buttons.
If the compositor happens to reserve all buttons in a group, this event
will be sent with an empty array.

<h3 class="decleration event" title="Ring event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletpadgroupv2_ring" id="onzwptabletpadgroupv2_ring">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletPadGroupV2.<span class="event">OnRing</span>
    </a>
</h3>

```csharp
void RingHandler(ZwpTabletPadRingV2 ring)
```

| Argument | Type | Description |
| --- | --- | --- |
| ring | new_id |  |

**Ring announced**

Sent on zwp_tablet_pad_group_v2 initialization to announce available rings.
One event is sent for each ring available on this pad group.

This event is sent in the initial burst of events before the
zwp_tablet_pad_group_v2.done event.

<h3 class="decleration event" title="Strip event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletpadgroupv2_strip" id="onzwptabletpadgroupv2_strip">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletPadGroupV2.<span class="event">OnStrip</span>
    </a>
</h3>

```csharp
void StripHandler(ZwpTabletPadStripV2 strip)
```

| Argument | Type | Description |
| --- | --- | --- |
| strip | new_id |  |

**Strip announced**

Sent on zwp_tablet_pad_v2 initialization to announce available strips.
One event is sent for each strip available on this pad group.

This event is sent in the initial burst of events before the
zwp_tablet_pad_group_v2.done event.

<h3 class="decleration event" title="Modes event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletpadgroupv2_modes" id="onzwptabletpadgroupv2_modes">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletPadGroupV2.<span class="event">OnModes</span>
    </a>
</h3>

```csharp
void ModesHandler(uint modes)
```

| Argument | Type | Description |
| --- | --- | --- |
| modes | uint | The number of modes |

**Mode-switch ability announced**

Sent on zwp_tablet_pad_group_v2 initialization to announce that the pad
group may switch between modes. A client may use a mode to store a
specific configuration for buttons, rings and strips and use the
zwp_tablet_pad_group_v2.mode_switch event to toggle between these
configurations. Mode indices start at 0.

Switching modes is compositor-dependent. See the
zwp_tablet_pad_group_v2.mode_switch event for more details.

This event is sent in the initial burst of events before the
zwp_tablet_pad_group_v2.done event. This event is only sent when
more than one mode is available.

<h3 class="decleration event" title="Done event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletpadgroupv2_done" id="onzwptabletpadgroupv2_done">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletPadGroupV2.<span class="event">OnDone</span>
    </a>
</h3>

```csharp
void DoneHandler()
```


**Tablet group description events sequence complete**

This event is sent immediately to signal the end of the initial
burst of descriptive events. A client may consider the static
description of the tablet to be complete and finalize initialization
of the tablet group.

<h3 class="decleration event" title="ModeSwitch event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletpadgroupv2_modeswitch" id="onzwptabletpadgroupv2_modeswitch">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletPadGroupV2.<span class="event">OnModeSwitch</span>
    </a>
</h3>

```csharp
void ModeSwitchHandler(uint time, uint serial, uint mode)
```

| Argument | Type | Description |
| --- | --- | --- |
| time | uint | The time of the event with millisecond granularity |
| serial | uint |  |
| mode | uint | The new mode of the pad |

**Mode switch event**

Notification that the mode was switched.

A mode applies to all buttons, rings, strips and dials in a group
simultaneously, but a client is not required to assign different actions
for each mode. For example, a client may have mode-specific button
mappings but map the ring to vertical scrolling in all modes. Mode
indices start at 0.

Switching modes is compositor-dependent. The compositor may provide
visual cues to the user about the mode, e.g. by toggling LEDs on
the tablet device. Mode-switching may be software-controlled or
controlled by one or more physical buttons. For example, on a Wacom
Intuos Pro, the button inside the ring may be assigned to switch
between modes.

The compositor will also send this event after zwp_tablet_pad_v2.enter on
each group in order to notify of the current mode. Groups that only
feature one mode will use mode=0 when emitting this event.

If a button action in the new mode differs from the action in the
previous mode, the client should immediately issue a
zwp_tablet_pad_v2.set_feedback request for each changed button.

If a ring, strip or dial action in the new mode differs from the action
in the previous mode, the client should immediately issue a
zwp_tablet_ring_v2.set_feedback, zwp_tablet_strip_v2.set_feedback or
zwp_tablet_dial_v2.set_feedback request for each changed ring, strip or dial.

<h3 class="decleration event" title="Dial event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletpadgroupv2_dial" id="onzwptabletpadgroupv2_dial">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletPadGroupV2.<span class="event">OnDial</span>
    </a>
    <span class="pill">since 2</span>
</h3>

```csharp
void DialHandler(ZwpTabletPadDialV2 dial)
```

| Argument | Type | Description |
| --- | --- | --- |
| dial | new_id |  |

**Dial announced**

Sent on zwp_tablet_pad_v2 initialization to announce available dials.
One event is sent for each dial available on this pad group.

This event is sent in the initial burst of events before the
zwp_tablet_pad_group_v2.done event.

<h2 class="decleration interface">
    <a href="#/Protocols/Stable/tablet-v2/?id=zwptabletpadv2" id="zwptabletpadv2">
        <span class="codicon codicon-symbol-interface"></span>
        ZwpTabletPadV2
    </a>
    <span class="pill">version 2</span>
</h2>

A set of buttons, rings, strips and dials


A pad device is a set of buttons, rings, strips and dials
usually physically present on the tablet device itself. Some
exceptions exist where the pad device is physically detached, e.g. the
Wacom ExpressKey Remote.

Pad devices have no axes that control the cursor and are generally
auxiliary devices to the tool devices used on the tablet surface.

A pad device has a number of static characteristics, e.g. the number
of rings. These capabilities are sent in an event sequence after the
zwp_tablet_seat_v2.pad_added event before any actual events from this pad.
This initial event sequence is terminated by a zwp_tablet_pad_v2.done
event.

All pad features (buttons, rings, strips and dials) are logically divided into
groups and all pads have at least one group. The available groups are
notified through the zwp_tablet_pad_v2.group event; the compositor will
emit one event per group before emitting zwp_tablet_pad_v2.done.

Groups may have multiple modes. Modes allow clients to map multiple
actions to a single pad feature. Only one mode can be active per group,
although different groups may have different active modes.


<h3 class="decleration request" title="SetFeedback request">
    <a href="#/Protocols/Stable/tablet-v2/?id=zwptabletpadv2_setfeedback" id="zwptabletpadv2_setfeedback">
        <span class="codicon codicon-symbol-method method"></span>
        ZwpTabletPadV2.<span class="method">SetFeedback</span>
    </a>
</h3>

```csharp
void SetFeedback(uint button, string description, uint serial)
```

| Argument | Type | Description |
| --- | --- | --- |
| button | uint | Button index |
| description | string | Button description |
| serial | uint | Serial of the mode switch event |

**Set compositor feedback**

Requests the compositor to use the provided feedback string
associated with this button. This request should be issued immediately
after a zwp_tablet_pad_group_v2.mode_switch event from the corresponding
group is received, or whenever a button is mapped to a different
action. See zwp_tablet_pad_group_v2.mode_switch for more details.

Clients are encouraged to provide context-aware descriptions for
the actions associated with each button, and compositors may use
this information to offer visual feedback on the button layout
(e.g. on-screen displays).

Button indices start at 0. Setting the feedback string on a button
that is reserved by the compositor (i.e. not belonging to any
zwp_tablet_pad_group_v2) does not generate an error but the compositor
is free to ignore the request.

The provided string 'description' is a UTF-8 encoded string to be
associated with this ring, and is considered user-visible; general
internationalization rules apply.

The serial argument will be that of the last
zwp_tablet_pad_group_v2.mode_switch event received for the group of this
button. Requests providing other serials than the most recent one will
be ignored.

<h3 class="decleration request" title="Destroy request">
    <a href="#/Protocols/Stable/tablet-v2/?id=zwptabletpadv2_destroy" id="zwptabletpadv2_destroy">
        <span class="codicon codicon-symbol-method method"></span>
        ZwpTabletPadV2.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the pad object**

Destroy the zwp_tablet_pad_v2 object. Objects created from this object
are unaffected and should be destroyed separately.

<h3 class="decleration event" title="Group event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletpadv2_group" id="onzwptabletpadv2_group">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletPadV2.<span class="event">OnGroup</span>
    </a>
</h3>

```csharp
void GroupHandler(ZwpTabletPadGroupV2 padGroup)
```

| Argument | Type | Description |
| --- | --- | --- |
| pad_group | new_id |  |

**Group announced**

Sent on zwp_tablet_pad_v2 initialization to announce available groups.
One event is sent for each pad group available.

This event is sent in the initial burst of events before the
zwp_tablet_pad_v2.done event. At least one group will be announced.

<h3 class="decleration event" title="Path event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletpadv2_path" id="onzwptabletpadv2_path">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletPadV2.<span class="event">OnPath</span>
    </a>
</h3>

```csharp
void PathHandler(string path)
```

| Argument | Type | Description |
| --- | --- | --- |
| path | string | Path to local device |

**Path to the device**

A system-specific device path that indicates which device is behind
this zwp_tablet_pad_v2. This information may be used to gather additional
information about the device, e.g. through libwacom.

The format of the path is unspecified, it may be a device node, a
sysfs path, or some other identifier. It is up to the client to
identify the string provided.

This event is sent in the initial burst of events before the
zwp_tablet_pad_v2.done event.

<h3 class="decleration event" title="Buttons event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletpadv2_buttons" id="onzwptabletpadv2_buttons">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletPadV2.<span class="event">OnButtons</span>
    </a>
</h3>

```csharp
void ButtonsHandler(uint buttons)
```

| Argument | Type | Description |
| --- | --- | --- |
| buttons | uint | The number of buttons |

**Buttons announced**

Sent on zwp_tablet_pad_v2 initialization to announce the available
buttons.

This event is sent in the initial burst of events before the
zwp_tablet_pad_v2.done event. This event is only sent when at least one
button is available.

<h3 class="decleration event" title="Done event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletpadv2_done" id="onzwptabletpadv2_done">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletPadV2.<span class="event">OnDone</span>
    </a>
</h3>

```csharp
void DoneHandler()
```


**Pad description event sequence complete**

This event signals the end of the initial burst of descriptive
events. A client may consider the static description of the pad to
be complete and finalize initialization of the pad.

<h3 class="decleration event" title="Button event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletpadv2_button" id="onzwptabletpadv2_button">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletPadV2.<span class="event">OnButton</span>
    </a>
</h3>

```csharp
void ButtonHandler(uint time, uint button, uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| time | uint | The time of the event with millisecond granularity |
| button | uint | The index of the button that changed state |
| state | uint |  |

**Physical button state**

Sent whenever the physical state of a button changes.

<h3 class="decleration event" title="Enter event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletpadv2_enter" id="onzwptabletpadv2_enter">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletPadV2.<span class="event">OnEnter</span>
    </a>
</h3>

```csharp
void EnterHandler(uint serial, ZwpTabletV2 tablet, WlSurface surface)
```

| Argument | Type | Description |
| --- | --- | --- |
| serial | uint | Serial number of the enter event |
| tablet | object | The tablet the pad is attached to |
| surface | object | Surface the pad is focused on |

**Enter event**

Notification that this pad is focused on the specified surface.

<h3 class="decleration event" title="Leave event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletpadv2_leave" id="onzwptabletpadv2_leave">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletPadV2.<span class="event">OnLeave</span>
    </a>
</h3>

```csharp
void LeaveHandler(uint serial, WlSurface surface)
```

| Argument | Type | Description |
| --- | --- | --- |
| serial | uint | Serial number of the leave event |
| surface | object | Surface the pad is no longer focused on |

**Leave event**

Notification that this pad is no longer focused on the specified
surface.

<h3 class="decleration event" title="Removed event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletpadv2_removed" id="onzwptabletpadv2_removed">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletPadV2.<span class="event">OnRemoved</span>
    </a>
</h3>

```csharp
void RemovedHandler()
```


**Pad removed event**

Sent when the pad has been removed from the system. When a tablet
is removed its pad(s) will be removed too.

When this event is received, the client must destroy all rings, strips
and groups that were offered by this pad, and issue zwp_tablet_pad_v2.destroy
the pad itself.

<h3 class="decleration enum" title="ButtonState enum">
    <a href="#/Protocols/Stable/tablet-v2/?id=buttonstate" id="buttonstate">
        <span class="codicon codicon-symbol-enum enum"></span>
        ZwpTabletPadV2.<span class="enum">ButtonState</span>
    </a>
</h3>

```csharp
public enum ButtonState
```

Physical button state


Describes the physical state of a button that caused the button
event.


| Value | Integer | Description |
| --- | --- | --- |
| Released | 0 | The button is not pressed |
| Pressed | 1 | The button is pressed |
<h2 class="decleration interface">
    <a href="#/Protocols/Stable/tablet-v2/?id=zwptabletpaddialv2" id="zwptabletpaddialv2">
        <span class="codicon codicon-symbol-interface"></span>
        ZwpTabletPadDialV2
    </a>
    <span class="pill">version 2</span>
</h2>

Pad dial


A rotary control, e.g. a dial or a wheel.

Events on a dial are logically grouped by the zwp_tablet_pad_dial_v2.frame
event.


<h3 class="decleration request" title="SetFeedback request">
    <a href="#/Protocols/Stable/tablet-v2/?id=zwptabletpaddialv2_setfeedback" id="zwptabletpaddialv2_setfeedback">
        <span class="codicon codicon-symbol-method method"></span>
        ZwpTabletPadDialV2.<span class="method">SetFeedback</span>
    </a>
</h3>

```csharp
void SetFeedback(string description, uint serial)
```

| Argument | Type | Description |
| --- | --- | --- |
| description | string | Dial description |
| serial | uint | Serial of the mode switch event |

**Set compositor feedback**

Requests the compositor to use the provided feedback string
associated with this dial. This request should be issued immediately
after a zwp_tablet_pad_group_v2.mode_switch event from the corresponding
group is received, or whenever the dial is mapped to a different
action. See zwp_tablet_pad_group_v2.mode_switch for more details.

Clients are encouraged to provide context-aware descriptions for
the actions associated with the dial, and compositors may use this
information to offer visual feedback about the button layout
(eg. on-screen displays).

The provided string 'description' is a UTF-8 encoded string to be
associated with this ring, and is considered user-visible; general
internationalization rules apply.

The serial argument will be that of the last
zwp_tablet_pad_group_v2.mode_switch event received for the group of this
dial. Requests providing other serials than the most recent one will be
ignored.

<h3 class="decleration request" title="Destroy request">
    <a href="#/Protocols/Stable/tablet-v2/?id=zwptabletpaddialv2_destroy" id="zwptabletpaddialv2_destroy">
        <span class="codicon codicon-symbol-method method"></span>
        ZwpTabletPadDialV2.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the dial object**

This destroys the client's resource for this dial object.

<h3 class="decleration event" title="Delta event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletpaddialv2_delta" id="onzwptabletpaddialv2_delta">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletPadDialV2.<span class="event">OnDelta</span>
    </a>
</h3>

```csharp
void DeltaHandler(int value120)
```

| Argument | Type | Description |
| --- | --- | --- |
| value120 | int | Rotation distance as fraction of 120 |

**Delta movement**

Sent whenever the position on a dial changes.

This event carries the wheel delta as multiples or fractions
of 120 with each multiple of 120 representing one logical wheel detent.
For example, an axis_value120 of 30 is one quarter of
a logical wheel step in the positive direction, a value120 of
-240 are two logical wheel steps in the negative direction within the
same hardware event. See the wl_pointer.axis_value120 for more details.

The value120 must not be zero.

<h3 class="decleration event" title="Frame event">
    <a href="#/Protocols/Stable/tablet-v2/?id=onzwptabletpaddialv2_frame" id="onzwptabletpaddialv2_frame">
        <span class="codicon codicon-symbol-event event"></span>
        ZwpTabletPadDialV2.<span class="event">OnFrame</span>
    </a>
</h3>

```csharp
void FrameHandler(uint time)
```

| Argument | Type | Description |
| --- | --- | --- |
| time | uint | Timestamp with millisecond granularity |

**End of a dial event sequence**

Indicates the end of a set of events that represent one logical
hardware dial event. A client is expected to accumulate the data
in all events within the frame before proceeding.

All zwp_tablet_pad_dial_v2 events before a zwp_tablet_pad_dial_v2.frame event belong
logically together.

A zwp_tablet_pad_dial_v2.frame event is sent for every logical event
group, even if the group only contains a single zwp_tablet_pad_dial_v2
event. Specifically, a client may get a sequence: delta, frame,
delta, frame, etc.

