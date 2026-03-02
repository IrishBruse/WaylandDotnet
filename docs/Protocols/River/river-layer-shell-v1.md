# River Layer Shell

##### [WaylandDotnet](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet) ![](../../assets/arrow.svg ':class=breadcrumb-arrow') [River](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/River) ![](../../assets/arrow.svg ':class=breadcrumb-arrow') [RiverLayerShellV1](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/River/river-layer-shell-v1/)

---

<h2 class="decleration interface">
    <a href="?id=RiverLayerShellV1" id="RiverLayerShellV1">
        <span class="codicon codicon-symbol-interface"></span>
        RiverLayerShellV1
    </a>
    <span class="pill">version 1</span>
</h2>

River layer shell global interface


This global interface should only be advertised to the client if the
river_window_manager_v1 global is also advertised. Binding this interface
indicates that the window manager supports layer shell.

If the window manager does not bind this interface, the compositor should
not allow clients to map layer surfaces. This can be achieved by
closing layer surfaces immediately.


<h3 class="decleration request" title="Destroy request">
    <a href="?id=RiverLayerShellV1_Destroy" id="RiverLayerShellV1_Destroy">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLayerShellV1.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the river_layer_shell_v1 object**

This request indicates that the client will no longer use the
river_layer_shell_v1 object.

<h3 class="decleration request" title="GetOutput request">
    <a href="?id=RiverLayerShellV1_GetOutput" id="RiverLayerShellV1_GetOutput">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLayerShellV1.<span class="method">GetOutput</span>
    </a>
</h3>

```csharp
RiverLayerShellOutputV1 GetOutput(RiverOutputV1 output)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id |  |
| output | object |  |

**Get layer shell output state**

It is a protocol error to make this request more than once for a given
river_output_v1 object.

<h3 class="decleration request" title="GetSeat request">
    <a href="?id=RiverLayerShellV1_GetSeat" id="RiverLayerShellV1_GetSeat">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLayerShellV1.<span class="method">GetSeat</span>
    </a>
</h3>

```csharp
RiverLayerShellSeatV1 GetSeat(RiverSeatV1 seat)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id |  |
| seat | object |  |

**Get layer shell seat state**

It is a protocol error to make this request more than once for a given
river_seat_v1 object.

<h3 class="decleration enum" title="Error enum">
    <a href="?id=Error" id="Error">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverLayerShellV1.<span class="enum">Error</span>
    </a>
</h3>

```csharp
public enum Error
```

| Value | Integer | Description |
| --- | --- | --- |
| ObjectAlreadyCreated | 0 | The layer_shell_output/seat object was already created. |
<h2 class="decleration interface">
    <a href="?id=RiverLayerShellOutputV1" id="RiverLayerShellOutputV1">
        <span class="codicon codicon-symbol-interface"></span>
        RiverLayerShellOutputV1
    </a>
    <span class="pill">version 1</span>
</h2>

Layer shell output state


The lifetime of this object is tied to the corresponding river_output_v1.
This object is made inert when the river_output_v1.removed event is sent
and should be destroyed.


<h3 class="decleration request" title="Destroy request">
    <a href="?id=RiverLayerShellOutputV1_Destroy" id="RiverLayerShellOutputV1_Destroy">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLayerShellOutputV1.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the object**

This request indicates that the client will no longer use the
river_layer_shell_output_v1 object and that it may be safely destroyed.

This request should be made after the river_output_v1.removed event is
received to complete destruction of the output.

<h3 class="decleration request" title="SetDefault request">
    <a href="?id=RiverLayerShellOutputV1_SetDefault" id="RiverLayerShellOutputV1_SetDefault">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLayerShellOutputV1.<span class="method">SetDefault</span>
    </a>
</h3>

```csharp
void SetDefault()
```


**Set default output for layer surfaces**

Mark this output as the default for new layer surfaces which do not
request a specific output themselves. This request overrides any
previous set_default request on any river_layer_shell_output_v1 object.

If no set_default request is made or if the default output is destroyed,
the default output is undefined until the next set_default request.

This request modifies window management state and may only be made as
part of a manage sequence, see the river_window_manager_v1 description.

<h3 class="decleration event" title="NonExclusiveArea event">
    <a href="?id=OnRiverLayerShellOutputV1_NonExclusiveArea" id="OnRiverLayerShellOutputV1_NonExclusiveArea">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLayerShellOutputV1.<span class="event">OnNonExclusiveArea</span>
    </a>
</h3>

```csharp
void NonExclusiveAreaHandler(int x, int y, int width, int height)
```

| Argument | Type | Description |
| --- | --- | --- |
| x | int |  |
| y | int |  |
| width | int |  |
| height | int |  |

**Area left after subtracting exclusive zones**

This event indicates the area of the output remaining after subtracting
the exclusive zones of layer surfaces. Exclusive zones are a hint, the
window manager is free to ignore this area hint if it wishes.

The x and y values are in the global coordinate space, not relative to
the position of the output.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

<h2 class="decleration interface">
    <a href="?id=RiverLayerShellSeatV1" id="RiverLayerShellSeatV1">
        <span class="codicon codicon-symbol-interface"></span>
        RiverLayerShellSeatV1
    </a>
    <span class="pill">version 1</span>
</h2>

Layer shell seat state


The lifetime of this object is tied to the corresponding river_seat_v1.
This object is made inert when the river_seat_v1.removed event is sent and
should be destroyed.


<h3 class="decleration request" title="Destroy request">
    <a href="?id=RiverLayerShellSeatV1_Destroy" id="RiverLayerShellSeatV1_Destroy">
        <span class="codicon codicon-symbol-method method"></span>
        RiverLayerShellSeatV1.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the object**

This request indicates that the client will no longer use the
river_layer_shell_seat_v1 object and that it may be safely destroyed.

This request should be made after the river_seat_v1.removed event is
received to complete destruction of the seat.

<h3 class="decleration event" title="FocusExclusive event">
    <a href="?id=OnRiverLayerShellSeatV1_FocusExclusive" id="OnRiverLayerShellSeatV1_FocusExclusive">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLayerShellSeatV1.<span class="event">OnFocusExclusive</span>
    </a>
</h3>

```csharp
void FocusExclusiveHandler()
```


****

A layer shell surface will be given exclusive keyboard focus at the end
of the manage sequence in which this event is sent. The window manager
may want to update window decorations or similar to indicate that no
window is focused.

Until the focus_non_exclusive or focus_none event is sent, all window
manager requests to change focus are ignored.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

<h3 class="decleration event" title="FocusNonExclusive event">
    <a href="?id=OnRiverLayerShellSeatV1_FocusNonExclusive" id="OnRiverLayerShellSeatV1_FocusNonExclusive">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLayerShellSeatV1.<span class="event">OnFocusNonExclusive</span>
    </a>
</h3>

```csharp
void FocusNonExclusiveHandler()
```


****

A layer shell surface will be given non-exclusive keyboard focus at the
end of the manage sequence in which this event is sent. The window
manager may want to update window decorations or similar to indicate
that no window is focused.

The window manager continues to control focus and may choose to focus a
different window/shell surface at any time. If the window manager sets
focus during the same manage sequence in which this event is sent, the
layer surface will not be focused.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

<h3 class="decleration event" title="FocusNone event">
    <a href="?id=OnRiverLayerShellSeatV1_FocusNone" id="OnRiverLayerShellSeatV1_FocusNone">
        <span class="codicon codicon-symbol-event event"></span>
        RiverLayerShellSeatV1.<span class="event">OnFocusNone</span>
    </a>
</h3>

```csharp
void FocusNoneHandler()
```


****

No layer shell surface will have keyboard focus at the end of the manage
sequence in which this event is sent. The window manager may want to
return focus to whichever window last had focus, for example.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

