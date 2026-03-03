# Output Management

##### [WaylandDotnet](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet) ![](../../../assets/arrow.svg ':class=breadcrumb-arrow') [Wlr](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/Wlr) ![](../../../assets/arrow.svg ':class=breadcrumb-arrow') [WlrOutputManagementUnstableV1](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/Wlr/wlr-output-management-unstable-v1/)

---

<h2 class="decleration interface">
    <a href="/?id=ZwlrOutputManagerV1" id="ZwlrOutputManagerV1">
        <span class="codicon codicon-symbol-interface"></span>
        ZwlrOutputManagerV1
    </a>
    <span class="pill">version 4</span>
</h2>

Output device configuration manager


This interface is a manager that allows reading and writing the current
output device configuration.

Output devices that display pixels (e.g. a physical monitor or a virtual
output in a window) are represented as heads. Heads cannot be created nor
destroyed by the client, but they can be enabled or disabled and their
properties can be changed. Each head may have one or more available modes.

Whenever a head appears (e.g. a monitor is plugged in), it will be
advertised via the head event. Immediately after the output manager is
bound, all current heads are advertised.

Whenever a head's properties change, the relevant wlr_output_head events
will be sent. Not all head properties will be sent: only properties that
have changed need to.

Whenever a head disappears (e.g. a monitor is unplugged), a
wlr_output_head.finished event will be sent.

After one or more heads appear, change or disappear, the done event will
be sent. It carries a serial which can be used in a create_configuration
request to update heads properties.

The information obtained from this protocol should only be used for output
configuration purposes. This protocol is not designed to be a generic
output property advertisement protocol for regular clients. Instead,
protocols such as xdg-output should be used.


<h3 class="decleration request" title="CreateConfiguration request">
    <a href="/?id=ZwlrOutputManagerV1_CreateConfiguration" id="ZwlrOutputManagerV1_CreateConfiguration">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrOutputManagerV1.<span class="method">CreateConfiguration</span>
    </a>
</h3>

```csharp
ZwlrOutputConfigurationV1 CreateConfiguration(uint serial)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id |  |
| serial | uint |  |

**Create a new output configuration object**

Create a new output configuration object. This allows to update head
properties.

<h3 class="decleration request" title="Stop request">
    <a href="/?id=ZwlrOutputManagerV1_Stop" id="ZwlrOutputManagerV1_Stop">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrOutputManagerV1.<span class="method">Stop</span>
    </a>
</h3>

```csharp
void Stop()
```


**Stop sending events**

Indicates the client no longer wishes to receive events for output
configuration changes. However the compositor may emit further events,
until the finished event is emitted.

The client must not send any more requests after this one.

<h3 class="decleration event" title="Head event">
    <a href="/?id=OnZwlrOutputManagerV1_Head" id="OnZwlrOutputManagerV1_Head">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrOutputManagerV1.<span class="event">OnHead</span>
    </a>
</h3>

```csharp
void HeadHandler(ZwlrOutputHeadV1 head)
```

| Argument | Type | Description |
| --- | --- | --- |
| head | new_id |  |

**Introduce a new head**

This event introduces a new head. This happens whenever a new head
appears (e.g. a monitor is plugged in) or after the output manager is
bound.

<h3 class="decleration event" title="Done event">
    <a href="/?id=OnZwlrOutputManagerV1_Done" id="OnZwlrOutputManagerV1_Done">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrOutputManagerV1.<span class="event">OnDone</span>
    </a>
</h3>

```csharp
void DoneHandler(uint serial)
```

| Argument | Type | Description |
| --- | --- | --- |
| serial | uint | Current configuration serial |

**Sent all information about current configuration**

This event is sent after all information has been sent after binding to
the output manager object and after any subsequent changes. This applies
to child head and mode objects as well. In other words, this event is
sent whenever a head or mode is created or destroyed and whenever one of
their properties has been changed. Not all state is re-sent each time
the current configuration changes: only the actual changes are sent.

This allows changes to the output configuration to be seen as atomic,
even if they happen via multiple events.

A serial is sent to be used in a future create_configuration request.

<h3 class="decleration event" title="Finished event">
    <a href="/?id=OnZwlrOutputManagerV1_Finished" id="OnZwlrOutputManagerV1_Finished">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrOutputManagerV1.<span class="event">OnFinished</span>
    </a>
</h3>

```csharp
void FinishedHandler()
```


**The compositor has finished with the manager**

This event indicates that the compositor is done sending manager events.
The compositor will destroy the object immediately after sending this
event, so it will become invalid and the client should release any
resources associated with it.

<h2 class="decleration interface">
    <a href="/?id=ZwlrOutputHeadV1" id="ZwlrOutputHeadV1">
        <span class="codicon codicon-symbol-interface"></span>
        ZwlrOutputHeadV1
    </a>
    <span class="pill">version 4</span>
</h2>

Output device


A head is an output device. The difference between a wl_output object and
a head is that heads are advertised even if they are turned off. A head
object only advertises properties and cannot be used directly to change
them.

A head has some read-only properties: modes, name, description and
physical_size. These cannot be changed by clients.

Other properties can be updated via a wlr_output_configuration object.

Properties sent via this interface are applied atomically via the
wlr_output_manager.done event. No guarantees are made regarding the order
in which properties are sent.


<h3 class="decleration request" title="Release request">
    <a href="/?id=ZwlrOutputHeadV1_Release" id="ZwlrOutputHeadV1_Release">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrOutputHeadV1.<span class="method">Release</span>
    </a>
    <span class="pill">since 3</span>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Release()
```


**Destroy the head object**

This request indicates that the client will no longer use this head
object.

<h3 class="decleration event" title="Name event">
    <a href="/?id=OnZwlrOutputHeadV1_Name" id="OnZwlrOutputHeadV1_Name">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrOutputHeadV1.<span class="event">OnName</span>
    </a>
</h3>

```csharp
void NameHandler(string name)
```

| Argument | Type | Description |
| --- | --- | --- |
| name | string |  |

**Head name**

This event describes the head name.

The naming convention is compositor defined, but limited to alphanumeric
characters and dashes (-). Each name is unique among all wlr_output_head
objects, but if a wlr_output_head object is destroyed the same name may
be reused later. The names will also remain consistent across sessions
with the same hardware and software configuration.

Examples of names include 'HDMI-A-1', 'WL-1', 'X11-1', etc. However, do
not assume that the name is a reflection of an underlying DRM
connector, X11 connection, etc.

If this head matches a wl_output, the wl_output.name event must report
the same name.

The name event is sent after a wlr_output_head object is created. This
event is only sent once per object, and the name does not change over
the lifetime of the wlr_output_head object.

<h3 class="decleration event" title="Description event">
    <a href="/?id=OnZwlrOutputHeadV1_Description" id="OnZwlrOutputHeadV1_Description">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrOutputHeadV1.<span class="event">OnDescription</span>
    </a>
</h3>

```csharp
void DescriptionHandler(string description)
```

| Argument | Type | Description |
| --- | --- | --- |
| description | string |  |

**Head description**

This event describes a human-readable description of the head.

The description is a UTF-8 string with no convention defined for its
contents. Examples might include 'Foocorp 11" Display' or 'Virtual X11
output via :1'. However, do not assume that the name is a reflection of
the make, model, serial of the underlying DRM connector or the display
name of the underlying X11 connection, etc.

If this head matches a wl_output, the wl_output.description event must
report the same name.

The description event is sent after a wlr_output_head object is created.
This event is only sent once per object, and the description does not
change over the lifetime of the wlr_output_head object.

<h3 class="decleration event" title="PhysicalSize event">
    <a href="/?id=OnZwlrOutputHeadV1_PhysicalSize" id="OnZwlrOutputHeadV1_PhysicalSize">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrOutputHeadV1.<span class="event">OnPhysicalSize</span>
    </a>
</h3>

```csharp
void PhysicalSizeHandler(int width, int height)
```

| Argument | Type | Description |
| --- | --- | --- |
| width | int | Width in millimeters of the output |
| height | int | Height in millimeters of the output |

**Head physical size**

This event describes the physical size of the head. This event is only
sent if the head has a physical size (e.g. is not a projector or a
virtual device).

The physical size event is sent after a wlr_output_head object is created. This
event is only sent once per object, and the physical size does not change over
the lifetime of the wlr_output_head object.

<h3 class="decleration event" title="Mode event">
    <a href="/?id=OnZwlrOutputHeadV1_Mode" id="OnZwlrOutputHeadV1_Mode">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrOutputHeadV1.<span class="event">OnMode</span>
    </a>
</h3>

```csharp
void ModeHandler(ZwlrOutputModeV1 mode)
```

| Argument | Type | Description |
| --- | --- | --- |
| mode | new_id |  |

**Introduce a mode**

This event introduces a mode for this head. It is sent once per
supported mode.

<h3 class="decleration event" title="Enabled event">
    <a href="/?id=OnZwlrOutputHeadV1_Enabled" id="OnZwlrOutputHeadV1_Enabled">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrOutputHeadV1.<span class="event">OnEnabled</span>
    </a>
</h3>

```csharp
void EnabledHandler(int enabled)
```

| Argument | Type | Description |
| --- | --- | --- |
| enabled | int | Zero if disabled, non-zero if enabled |

**Head is enabled or disabled**

This event describes whether the head is enabled. A disabled head is not
mapped to a region of the global compositor space.

When a head is disabled, some properties (current_mode, position,
transform and scale) are irrelevant.

<h3 class="decleration event" title="CurrentMode event">
    <a href="/?id=OnZwlrOutputHeadV1_CurrentMode" id="OnZwlrOutputHeadV1_CurrentMode">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrOutputHeadV1.<span class="event">OnCurrentMode</span>
    </a>
</h3>

```csharp
void CurrentModeHandler(ZwlrOutputModeV1 mode)
```

| Argument | Type | Description |
| --- | --- | --- |
| mode | object |  |

**Current mode**

This event describes the mode currently in use for this head. It is only
sent if the output is enabled.

<h3 class="decleration event" title="Position event">
    <a href="/?id=OnZwlrOutputHeadV1_Position" id="OnZwlrOutputHeadV1_Position">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrOutputHeadV1.<span class="event">OnPosition</span>
    </a>
</h3>

```csharp
void PositionHandler(int x, int y)
```

| Argument | Type | Description |
| --- | --- | --- |
| x | int | X position within the global compositor space |
| y | int | Y position within the global compositor space |

**Current position**

This events describes the position of the head in the global compositor
space. It is only sent if the output is enabled.

<h3 class="decleration event" title="Transform event">
    <a href="/?id=OnZwlrOutputHeadV1_Transform" id="OnZwlrOutputHeadV1_Transform">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrOutputHeadV1.<span class="event">OnTransform</span>
    </a>
</h3>

```csharp
void TransformHandler(int transform)
```

| Argument | Type | Description |
| --- | --- | --- |
| transform | int |  |

**Current transformation**

This event describes the transformation currently applied to the head.
It is only sent if the output is enabled.

<h3 class="decleration event" title="Scale event">
    <a href="/?id=OnZwlrOutputHeadV1_Scale" id="OnZwlrOutputHeadV1_Scale">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrOutputHeadV1.<span class="event">OnScale</span>
    </a>
</h3>

```csharp
void ScaleHandler(WlFixed scale)
```

| Argument | Type | Description |
| --- | --- | --- |
| scale | fixed |  |

**Current scale**

This events describes the scale of the head in the global compositor
space. It is only sent if the output is enabled.

<h3 class="decleration event" title="Finished event">
    <a href="/?id=OnZwlrOutputHeadV1_Finished" id="OnZwlrOutputHeadV1_Finished">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrOutputHeadV1.<span class="event">OnFinished</span>
    </a>
</h3>

```csharp
void FinishedHandler()
```


**The head has disappeared**

This event indicates that the head is no longer available. The head
object becomes inert. Clients should send a destroy request and release
any resources associated with it.

<h3 class="decleration event" title="Make event">
    <a href="/?id=OnZwlrOutputHeadV1_Make" id="OnZwlrOutputHeadV1_Make">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrOutputHeadV1.<span class="event">OnMake</span>
    </a>
    <span class="pill">since 2</span>
</h3>

```csharp
void MakeHandler(string make)
```

| Argument | Type | Description |
| --- | --- | --- |
| make | string |  |

**Head manufacturer**

This event describes the manufacturer of the head.

Together with the model and serial_number events the purpose is to
allow clients to recognize heads from previous sessions and for example
load head-specific configurations back.

It is not guaranteed this event will be ever sent. A reason for that
can be that the compositor does not have information about the make of
the head or the definition of a make is not sensible in the current
setup, for example in a virtual session. Clients can still try to
identify the head by available information from other events but should
be aware that there is an increased risk of false positives.

If sent, the make event is sent after a wlr_output_head object is
created and only sent once per object. The make does not change over
the lifetime of the wlr_output_head object.

It is not recommended to display the make string in UI to users. For
that the string provided by the description event should be preferred.

<h3 class="decleration event" title="Model event">
    <a href="/?id=OnZwlrOutputHeadV1_Model" id="OnZwlrOutputHeadV1_Model">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrOutputHeadV1.<span class="event">OnModel</span>
    </a>
    <span class="pill">since 2</span>
</h3>

```csharp
void ModelHandler(string model)
```

| Argument | Type | Description |
| --- | --- | --- |
| model | string |  |

**Head model**

This event describes the model of the head.

Together with the make and serial_number events the purpose is to
allow clients to recognize heads from previous sessions and for example
load head-specific configurations back.

It is not guaranteed this event will be ever sent. A reason for that
can be that the compositor does not have information about the model of
the head or the definition of a model is not sensible in the current
setup, for example in a virtual session. Clients can still try to
identify the head by available information from other events but should
be aware that there is an increased risk of false positives.

If sent, the model event is sent after a wlr_output_head object is
created and only sent once per object. The model does not change over
the lifetime of the wlr_output_head object.

It is not recommended to display the model string in UI to users. For
that the string provided by the description event should be preferred.

<h3 class="decleration event" title="SerialNumber event">
    <a href="/?id=OnZwlrOutputHeadV1_SerialNumber" id="OnZwlrOutputHeadV1_SerialNumber">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrOutputHeadV1.<span class="event">OnSerialNumber</span>
    </a>
    <span class="pill">since 2</span>
</h3>

```csharp
void SerialNumberHandler(string serialNumber)
```

| Argument | Type | Description |
| --- | --- | --- |
| serial_number | string |  |

**Head serial number**

This event describes the serial number of the head.

Together with the make and model events the purpose is to allow clients
to recognize heads from previous sessions and for example load head-
specific configurations back.

It is not guaranteed this event will be ever sent. A reason for that
can be that the compositor does not have information about the serial
number of the head or the definition of a serial number is not sensible
in the current setup. Clients can still try to identify the head by
available information from other events but should be aware that there
is an increased risk of false positives.

If sent, the serial number event is sent after a wlr_output_head object
is created and only sent once per object. The serial number does not
change over the lifetime of the wlr_output_head object.

It is not recommended to display the serial_number string in UI to
users. For that the string provided by the description event should be
preferred.

<h3 class="decleration event" title="AdaptiveSync event">
    <a href="/?id=OnZwlrOutputHeadV1_AdaptiveSync" id="OnZwlrOutputHeadV1_AdaptiveSync">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrOutputHeadV1.<span class="event">OnAdaptiveSync</span>
    </a>
    <span class="pill">since 4</span>
</h3>

```csharp
void AdaptiveSyncHandler(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| state | uint |  |

**Current adaptive sync state**

This event describes whether adaptive sync is currently enabled for
the head or not. Adaptive sync is also known as Variable Refresh
Rate or VRR.

<h3 class="decleration enum" title="AdaptiveSyncState enum">
    <a href="/?id=AdaptiveSyncState" id="AdaptiveSyncState">
        <span class="codicon codicon-symbol-enum enum"></span>
        ZwlrOutputHeadV1.<span class="enum">AdaptiveSyncState</span>
    </a>
</h3>

```csharp
public enum AdaptiveSyncState
```

| Value | Integer | Description |
| --- | --- | --- |
| Disabled | 0 | Adaptive sync is disabled |
| Enabled | 1 | Adaptive sync is enabled |
<h2 class="decleration interface">
    <a href="/?id=ZwlrOutputModeV1" id="ZwlrOutputModeV1">
        <span class="codicon codicon-symbol-interface"></span>
        ZwlrOutputModeV1
    </a>
    <span class="pill">version 3</span>
</h2>

Output mode


This object describes an output mode.

Some heads don't support output modes, in which case modes won't be
advertised.

Properties sent via this interface are applied atomically via the
wlr_output_manager.done event. No guarantees are made regarding the order
in which properties are sent.


<h3 class="decleration request" title="Release request">
    <a href="/?id=ZwlrOutputModeV1_Release" id="ZwlrOutputModeV1_Release">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrOutputModeV1.<span class="method">Release</span>
    </a>
    <span class="pill">since 3</span>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Release()
```


**Destroy the mode object**

This request indicates that the client will no longer use this mode
object.

<h3 class="decleration event" title="Size event">
    <a href="/?id=OnZwlrOutputModeV1_Size" id="OnZwlrOutputModeV1_Size">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrOutputModeV1.<span class="event">OnSize</span>
    </a>
</h3>

```csharp
void SizeHandler(int width, int height)
```

| Argument | Type | Description |
| --- | --- | --- |
| width | int | Width of the mode in hardware units |
| height | int | Height of the mode in hardware units |

**Mode size**

This event describes the mode size. The size is given in physical
hardware units of the output device. This is not necessarily the same as
the output size in the global compositor space. For instance, the output
may be scaled or transformed.

<h3 class="decleration event" title="Refresh event">
    <a href="/?id=OnZwlrOutputModeV1_Refresh" id="OnZwlrOutputModeV1_Refresh">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrOutputModeV1.<span class="event">OnRefresh</span>
    </a>
</h3>

```csharp
void RefreshHandler(int refresh)
```

| Argument | Type | Description |
| --- | --- | --- |
| refresh | int | Vertical refresh rate in mHz |

**Mode refresh rate**

This event describes the mode's fixed vertical refresh rate. It is only
sent if the mode has a fixed refresh rate.

<h3 class="decleration event" title="Preferred event">
    <a href="/?id=OnZwlrOutputModeV1_Preferred" id="OnZwlrOutputModeV1_Preferred">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrOutputModeV1.<span class="event">OnPreferred</span>
    </a>
</h3>

```csharp
void PreferredHandler()
```


**Mode is preferred**

This event advertises this mode as preferred.

<h3 class="decleration event" title="Finished event">
    <a href="/?id=OnZwlrOutputModeV1_Finished" id="OnZwlrOutputModeV1_Finished">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrOutputModeV1.<span class="event">OnFinished</span>
    </a>
</h3>

```csharp
void FinishedHandler()
```


**The mode has disappeared**

This event indicates that the mode is no longer available. The mode
object becomes inert. Clients should send a destroy request and release
any resources associated with it.

<h2 class="decleration interface">
    <a href="/?id=ZwlrOutputConfigurationV1" id="ZwlrOutputConfigurationV1">
        <span class="codicon codicon-symbol-interface"></span>
        ZwlrOutputConfigurationV1
    </a>
    <span class="pill">version 4</span>
</h2>

Output configuration


This object is used by the client to describe a full output configuration.

First, the client needs to setup the output configuration. Each head can
be either enabled (and configured) or disabled. It is a protocol error to
send two enable_head or disable_head requests with the same head. It is a
protocol error to omit a head in a configuration.

Then, the client can apply or test the configuration. The compositor will
then reply with a succeeded, failed or cancelled event. Finally the client
should destroy the configuration object.


<h3 class="decleration request" title="EnableHead request">
    <a href="/?id=ZwlrOutputConfigurationV1_EnableHead" id="ZwlrOutputConfigurationV1_EnableHead">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrOutputConfigurationV1.<span class="method">EnableHead</span>
    </a>
</h3>

```csharp
ZwlrOutputConfigurationHeadV1 EnableHead(ZwlrOutputHeadV1 head)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id | A new object to configure the head |
| head | object | The head to be enabled |

**Enable and configure a head**

Enable a head. This request creates a head configuration object that can
be used to change the head's properties.

<h3 class="decleration request" title="DisableHead request">
    <a href="/?id=ZwlrOutputConfigurationV1_DisableHead" id="ZwlrOutputConfigurationV1_DisableHead">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrOutputConfigurationV1.<span class="method">DisableHead</span>
    </a>
</h3>

```csharp
void DisableHead(ZwlrOutputHeadV1 head)
```

| Argument | Type | Description |
| --- | --- | --- |
| head | object | The head to be disabled |

**Disable a head**

Disable a head.

<h3 class="decleration request" title="Apply request">
    <a href="/?id=ZwlrOutputConfigurationV1_Apply" id="ZwlrOutputConfigurationV1_Apply">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrOutputConfigurationV1.<span class="method">Apply</span>
    </a>
</h3>

```csharp
void Apply()
```


**Apply the configuration**

Apply the new output configuration.

In case the configuration is successfully applied, there is no guarantee
that the new output state matches completely the requested
configuration. For instance, a compositor might round the scale if it
doesn't support fractional scaling.

After this request has been sent, the compositor must respond with an
succeeded, failed or cancelled event. Sending a request that isn't the
destructor is a protocol error.

<h3 class="decleration request" title="Test request">
    <a href="/?id=ZwlrOutputConfigurationV1_Test" id="ZwlrOutputConfigurationV1_Test">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrOutputConfigurationV1.<span class="method">Test</span>
    </a>
</h3>

```csharp
void Test()
```


**Test the configuration**

Test the new output configuration. The configuration won't be applied,
but will only be validated.

Even if the compositor succeeds to test a configuration, applying it may
fail.

After this request has been sent, the compositor must respond with an
succeeded, failed or cancelled event. Sending a request that isn't the
destructor is a protocol error.

<h3 class="decleration request" title="Destroy request">
    <a href="/?id=ZwlrOutputConfigurationV1_Destroy" id="ZwlrOutputConfigurationV1_Destroy">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrOutputConfigurationV1.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the output configuration**

Using this request a client can tell the compositor that it is not going
to use the configuration object anymore. Any changes to the outputs
that have not been applied will be discarded.

This request also destroys wlr_output_configuration_head objects created
via this object.

<h3 class="decleration event" title="Succeeded event">
    <a href="/?id=OnZwlrOutputConfigurationV1_Succeeded" id="OnZwlrOutputConfigurationV1_Succeeded">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrOutputConfigurationV1.<span class="event">OnSucceeded</span>
    </a>
</h3>

```csharp
void SucceededHandler()
```


**Configuration changes succeeded**

Sent after the compositor has successfully applied the changes or
tested them.

Upon receiving this event, the client should destroy this object.

If the current configuration has changed, events to describe the changes
will be sent followed by a wlr_output_manager.done event.

<h3 class="decleration event" title="Failed event">
    <a href="/?id=OnZwlrOutputConfigurationV1_Failed" id="OnZwlrOutputConfigurationV1_Failed">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrOutputConfigurationV1.<span class="event">OnFailed</span>
    </a>
</h3>

```csharp
void FailedHandler()
```


**Configuration changes failed**

Sent if the compositor rejects the changes or failed to apply them. The
compositor should revert any changes made by the apply request that
triggered this event.

Upon receiving this event, the client should destroy this object.

<h3 class="decleration event" title="Cancelled event">
    <a href="/?id=OnZwlrOutputConfigurationV1_Cancelled" id="OnZwlrOutputConfigurationV1_Cancelled">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrOutputConfigurationV1.<span class="event">OnCancelled</span>
    </a>
</h3>

```csharp
void CancelledHandler()
```


**Configuration has been cancelled**

Sent if the compositor cancels the configuration because the state of an
output changed and the client has outdated information (e.g. after an
output has been hotplugged).

The client can create a new configuration with a newer serial and try
again.

Upon receiving this event, the client should destroy this object.

<h3 class="decleration enum" title="Error enum">
    <a href="/?id=Error" id="Error">
        <span class="codicon codicon-symbol-enum enum"></span>
        ZwlrOutputConfigurationV1.<span class="enum">Error</span>
    </a>
</h3>

```csharp
public enum Error
```

| Value | Integer | Description |
| --- | --- | --- |
| AlreadyConfiguredHead | 1 | Head has been configured twice |
| UnconfiguredHead | 2 | Head has not been configured |
| AlreadyUsed | 3 | Request sent after configuration has been applied or tested |
<h2 class="decleration interface">
    <a href="/?id=ZwlrOutputConfigurationHeadV1" id="ZwlrOutputConfigurationHeadV1">
        <span class="codicon codicon-symbol-interface"></span>
        ZwlrOutputConfigurationHeadV1
    </a>
    <span class="pill">version 4</span>
</h2>

Head configuration


This object is used by the client to update a single head's configuration.

It is a protocol error to set the same property twice.


<h3 class="decleration request" title="SetMode request">
    <a href="/?id=ZwlrOutputConfigurationHeadV1_SetMode" id="ZwlrOutputConfigurationHeadV1_SetMode">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrOutputConfigurationHeadV1.<span class="method">SetMode</span>
    </a>
</h3>

```csharp
void SetMode(ZwlrOutputModeV1 mode)
```

| Argument | Type | Description |
| --- | --- | --- |
| mode | object |  |

**Set the mode**

This request sets the head's mode.

<h3 class="decleration request" title="SetCustomMode request">
    <a href="/?id=ZwlrOutputConfigurationHeadV1_SetCustomMode" id="ZwlrOutputConfigurationHeadV1_SetCustomMode">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrOutputConfigurationHeadV1.<span class="method">SetCustomMode</span>
    </a>
</h3>

```csharp
void SetCustomMode(int width, int height, int refresh)
```

| Argument | Type | Description |
| --- | --- | --- |
| width | int | Width of the mode in hardware units |
| height | int | Height of the mode in hardware units |
| refresh | int | Vertical refresh rate in mHz or zero |

**Set a custom mode**

This request assigns a custom mode to the head. The size is given in
physical hardware units of the output device. If set to zero, the
refresh rate is unspecified.

It is a protocol error to set both a mode and a custom mode.

<h3 class="decleration request" title="SetPosition request">
    <a href="/?id=ZwlrOutputConfigurationHeadV1_SetPosition" id="ZwlrOutputConfigurationHeadV1_SetPosition">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrOutputConfigurationHeadV1.<span class="method">SetPosition</span>
    </a>
</h3>

```csharp
void SetPosition(int x, int y)
```

| Argument | Type | Description |
| --- | --- | --- |
| x | int | X position in the global compositor space |
| y | int | Y position in the global compositor space |

**Set the position**

This request sets the head's position in the global compositor space.

<h3 class="decleration request" title="SetTransform request">
    <a href="/?id=ZwlrOutputConfigurationHeadV1_SetTransform" id="ZwlrOutputConfigurationHeadV1_SetTransform">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrOutputConfigurationHeadV1.<span class="method">SetTransform</span>
    </a>
</h3>

```csharp
void SetTransform(int transform)
```

| Argument | Type | Description |
| --- | --- | --- |
| transform | int |  |

**Set the transform**

This request sets the head's transform.

<h3 class="decleration request" title="SetScale request">
    <a href="/?id=ZwlrOutputConfigurationHeadV1_SetScale" id="ZwlrOutputConfigurationHeadV1_SetScale">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrOutputConfigurationHeadV1.<span class="method">SetScale</span>
    </a>
</h3>

```csharp
void SetScale(WlFixed scale)
```

| Argument | Type | Description |
| --- | --- | --- |
| scale | fixed |  |

**Set the scale**

This request sets the head's scale.

<h3 class="decleration request" title="SetAdaptiveSync request">
    <a href="/?id=ZwlrOutputConfigurationHeadV1_SetAdaptiveSync" id="ZwlrOutputConfigurationHeadV1_SetAdaptiveSync">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrOutputConfigurationHeadV1.<span class="method">SetAdaptiveSync</span>
    </a>
    <span class="pill">since 4</span>
</h3>

```csharp
void SetAdaptiveSync(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| state | uint |  |

**Enable/disable adaptive sync**

This request enables/disables adaptive sync. Adaptive sync is also
known as Variable Refresh Rate or VRR.

<h3 class="decleration enum" title="Error enum">
    <a href="/?id=Error" id="Error">
        <span class="codicon codicon-symbol-enum enum"></span>
        ZwlrOutputConfigurationHeadV1.<span class="enum">Error</span>
    </a>
</h3>

```csharp
public enum Error
```

| Value | Integer | Description |
| --- | --- | --- |
| AlreadySet | 1 | Property has already been set |
| InvalidMode | 2 | Mode doesn't belong to head |
| InvalidCustomMode | 3 | Mode is invalid |
| InvalidTransform | 4 | Transform value outside enum |
| InvalidScale | 5 | Scale negative or zero |
| InvalidAdaptiveSyncState | 6 | Invalid enum value used in the set_adaptive_sync request |
