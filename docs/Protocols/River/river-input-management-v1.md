# River Input Management

##### [WaylandDotnet](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet) ![](../../assets/arrow.svg ':class=breadcrumb-arrow') [River](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/River) ![](../../assets/arrow.svg ':class=breadcrumb-arrow') [RiverInputManagementV1](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/River/river-input-management-v1/)

---

<h2 class="decleration interface">
    <a href="?id=RiverInputManagerV1" id="RiverInputManagerV1">
        <span class="codicon codicon-symbol-interface"></span>
        RiverInputManagerV1
    </a>
    <span class="pill">version 1</span>
</h2>

Input manager global interface


Input manager global interface.


<h3 class="decleration request" title="Stop request">
    <a href="?id=RiverInputManagerV1_Stop" id="RiverInputManagerV1_Stop">
        <span class="codicon codicon-symbol-method method"></span>
        RiverInputManagerV1.<span class="method">Stop</span>
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
for a river_input_manager_v1.finished event before destroying this
object.

<h3 class="decleration request" title="Destroy request">
    <a href="?id=RiverInputManagerV1_Destroy" id="RiverInputManagerV1_Destroy">
        <span class="codicon codicon-symbol-method method"></span>
        RiverInputManagerV1.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the river_input_manager_v1 object**

This request should be called after the finished event has been received
to complete destruction of the object.

It is a protocol error to make this request before the finished event
has been received.

If a client wishes to destroy this object it should send a
river_input_manager_v1.stop request and wait for a
river_input_manager_v1.finished event. Once the finished event is
received it is safe to destroy this object and any other objects created
through this interface.

<h3 class="decleration request" title="CreateSeat request">
    <a href="?id=RiverInputManagerV1_CreateSeat" id="RiverInputManagerV1_CreateSeat">
        <span class="codicon codicon-symbol-method method"></span>
        RiverInputManagerV1.<span class="method">CreateSeat</span>
    </a>
</h3>

```csharp
void CreateSeat(string name)
```

| Argument | Type | Description |
| --- | --- | --- |
| name | string |  |

**Create a new seat**

Create a new seat with the given name. Has no effect if a seat with the
given name already exists.

The default seat with name "default" always exists and does not need to
be explicitly created.

<h3 class="decleration request" title="DestroySeat request">
    <a href="?id=RiverInputManagerV1_DestroySeat" id="RiverInputManagerV1_DestroySeat">
        <span class="codicon codicon-symbol-method method"></span>
        RiverInputManagerV1.<span class="method">DestroySeat</span>
    </a>
</h3>

```csharp
void DestroySeat(string name)
```

| Argument | Type | Description |
| --- | --- | --- |
| name | string |  |

**Destroy a seat**

Destroy the seat with the given name. Has no effect if a seat with the
given name does not exist.

The default seat with name "default" cannot be destroyed and attempting
to destroy it will have no effect.

Any input devices assigned to the destroyed seat at the time of
destruction are assigned to the default seat.

<h3 class="decleration event" title="Finished event">
    <a href="?id=OnRiverInputManagerV1_Finished" id="OnRiverInputManagerV1_Finished">
        <span class="codicon codicon-symbol-event event"></span>
        RiverInputManagerV1.<span class="event">OnFinished</span>
    </a>
</h3>

```csharp
void FinishedHandler()
```


**The server has finished with the input manager**

This event indicates that the server will send no further events on this
object. The client should destroy the object. See
river_input_manager_v1.destroy for more information.

<h3 class="decleration event" title="InputDevice event">
    <a href="?id=OnRiverInputManagerV1_InputDevice" id="OnRiverInputManagerV1_InputDevice">
        <span class="codicon codicon-symbol-event event"></span>
        RiverInputManagerV1.<span class="event">OnInputDevice</span>
    </a>
</h3>

```csharp
void InputDeviceHandler(RiverInputDeviceV1 id)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id |  |

**New input device**

A new input device has been created.

<h2 class="decleration interface">
    <a href="?id=RiverInputDeviceV1" id="RiverInputDeviceV1">
        <span class="codicon codicon-symbol-interface"></span>
        RiverInputDeviceV1
    </a>
    <span class="pill">version 1</span>
</h2>

An input device


An input device represents a physical keyboard, mouse, touchscreen, or
drawing tablet tool. It is assigned to exactly one seat at a time.
By default, all input devices are assigned to the default seat.


<h3 class="decleration request" title="Destroy request">
    <a href="?id=RiverInputDeviceV1_Destroy" id="RiverInputDeviceV1_Destroy">
        <span class="codicon codicon-symbol-method method"></span>
        RiverInputDeviceV1.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the input device object**

This request indicates that the client will no longer use the input
device object and that it may be safely destroyed.

<h3 class="decleration request" title="AssignToSeat request">
    <a href="?id=RiverInputDeviceV1_AssignToSeat" id="RiverInputDeviceV1_AssignToSeat">
        <span class="codicon codicon-symbol-method method"></span>
        RiverInputDeviceV1.<span class="method">AssignToSeat</span>
    </a>
</h3>

```csharp
void AssignToSeat(string name)
```

| Argument | Type | Description |
| --- | --- | --- |
| name | string | Name of the seat |

**Assign the input device to a seat**

Assign the input device to a seat. All input devices not explicitly
assigned to a seat are considered assigned to the default seat.

Has no effect if a seat with the given name does not exist.

<h3 class="decleration request" title="SetRepeatInfo request">
    <a href="?id=RiverInputDeviceV1_SetRepeatInfo" id="RiverInputDeviceV1_SetRepeatInfo">
        <span class="codicon codicon-symbol-method method"></span>
        RiverInputDeviceV1.<span class="method">SetRepeatInfo</span>
    </a>
</h3>

```csharp
void SetRepeatInfo(int rate, int delay)
```

| Argument | Type | Description |
| --- | --- | --- |
| rate | int | Rate in key repeats per second |
| delay | int | Delay in milliseconds |

**Set keyboard repeat rate and delay**

Set repeat rate and delay for a keyboard input device. Has no effect if
the device is not a keyboard.

Negative values for either rate or delay are illegal. A rate of zero
will disable any repeating (regardless of the value of delay).

<h3 class="decleration request" title="SetScrollFactor request">
    <a href="?id=RiverInputDeviceV1_SetScrollFactor" id="RiverInputDeviceV1_SetScrollFactor">
        <span class="codicon codicon-symbol-method method"></span>
        RiverInputDeviceV1.<span class="method">SetScrollFactor</span>
    </a>
</h3>

```csharp
void SetScrollFactor(WlFixed factor)
```

| Argument | Type | Description |
| --- | --- | --- |
| factor | fixed |  |

**Set scroll factor**

Set the scroll factor for a pointer input device. Has no effect if the
device is not a pointer.

For example, a factor of 0.5 will make scrolling twice as slow while a
factor of 3.0 will make scrolling 3 times as fast.

Setting a scroll factor less than 0 is a protocol error.

<h3 class="decleration request" title="MapToOutput request">
    <a href="?id=RiverInputDeviceV1_MapToOutput" id="RiverInputDeviceV1_MapToOutput">
        <span class="codicon codicon-symbol-method method"></span>
        RiverInputDeviceV1.<span class="method">MapToOutput</span>
    </a>
</h3>

```csharp
void MapToOutput(WlOutput? output)
```

| Argument | Type | Description |
| --- | --- | --- |
| output | object |  |

**Map input device to the given output**

Map the input device to the given output. Has no effect if the device is
not a pointer, touch, or tablet device.

If mapped to both an output and a rectangle, the rectangle has priority.

Passing null clears an existing mapping.

<h3 class="decleration request" title="MapToRectangle request">
    <a href="?id=RiverInputDeviceV1_MapToRectangle" id="RiverInputDeviceV1_MapToRectangle">
        <span class="codicon codicon-symbol-method method"></span>
        RiverInputDeviceV1.<span class="method">MapToRectangle</span>
    </a>
</h3>

```csharp
void MapToRectangle(int x, int y, int width, int height)
```

| Argument | Type | Description |
| --- | --- | --- |
| x | int |  |
| y | int |  |
| width | int |  |
| height | int |  |

**Map input device to the given rectangle**

Map the input device to the given rectangle in the global compositor
coordinate space. Has no effect if the device is not a pointer, touch,
or tablet device.

If mapped to both an output and a rectangle, the rectangle has priority.

Width and height must be greater than or equal to 0.

Passing 0 for width or height clears an existing mapping.

<h3 class="decleration event" title="Removed event">
    <a href="?id=OnRiverInputDeviceV1_Removed" id="OnRiverInputDeviceV1_Removed">
        <span class="codicon codicon-symbol-event event"></span>
        RiverInputDeviceV1.<span class="event">OnRemoved</span>
    </a>
</h3>

```csharp
void RemovedHandler()
```


**The input device is removed**

This event indicates that the input device has been removed.

The server will send no further events on this object and ignore any
request (other than river_input_device_v1.destroy) made after this event is
sent. The client should destroy this object with the
river_input_device_v1.destroy request to free up resources.

<h3 class="decleration event" title="Type event">
    <a href="?id=OnRiverInputDeviceV1_Type" id="OnRiverInputDeviceV1_Type">
        <span class="codicon codicon-symbol-event event"></span>
        RiverInputDeviceV1.<span class="event">OnType</span>
    </a>
</h3>

```csharp
void TypeHandler(uint type)
```

| Argument | Type | Description |
| --- | --- | --- |
| type | uint |  |

**The type of the input device**

The type of the input device. This event is sent once when the
river_input_device_v1 object is created. The device type cannot
change during the lifetime of the object.

<h3 class="decleration event" title="Name event">
    <a href="?id=OnRiverInputDeviceV1_Name" id="OnRiverInputDeviceV1_Name">
        <span class="codicon codicon-symbol-event event"></span>
        RiverInputDeviceV1.<span class="event">OnName</span>
    </a>
</h3>

```csharp
void NameHandler(string name)
```

| Argument | Type | Description |
| --- | --- | --- |
| name | string |  |

**The name of the input device**

The name of the input device. This event is sent once when the
river_input_device_v1 object is created. The device name cannot
change during the lifetime of the object.

