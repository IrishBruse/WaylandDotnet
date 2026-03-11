# River XKB Bindings

##### [WaylandDotnet](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet) ![](../../../assets/arrow.svg ':class=breadcrumb-arrow') [River](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/River) ![](../../../assets/arrow.svg ':class=breadcrumb-arrow') [RiverXkbBindingsV1](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/River/river-xkb-bindings-v1/)

---

<h2 class="decleration interface">
    <a href="?id=RiverXkbBindingsV1" id="RiverXkbBindingsV1">
        <span class="codicon codicon-symbol-interface"></span>
        RiverXkbBindingsV1
    </a>
    <span class="pill">version 2</span>
</h2>

Xkbcommon bindings global interface


This global interface should only be advertised to the client if the
river_window_manager_v1 global is also advertised.



[Test](#WlDisplay)

<h3 class="decleration request" title="Destroy request">
    <a href="?id=RiverXkbBindingsV1_Destroy" id="RiverXkbBindingsV1_Destroy">
        <span class="codicon codicon-symbol-method method"></span>
        RiverXkbBindingsV1.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the river_xkb_bindings_v1 object**

This request indicates that the client will no longer use the
river_xkb_bindings_v1 object.


[Test](#WlDisplay)

<h3 class="decleration request" title="GetXkbBinding request">
    <a href="?id=RiverXkbBindingsV1_GetXkbBinding" id="RiverXkbBindingsV1_GetXkbBinding">
        <span class="codicon codicon-symbol-method method"></span>
        RiverXkbBindingsV1.<span class="method">GetXkbBinding</span>
    </a>
</h3>

```csharp
RiverXkbBindingV1 GetXkbBinding(RiverSeatV1 seat, uint keysym, uint modifiers)
```

| Argument | Type | Description |
| --- | --- | --- |
| seat | object |  |
| id | new_id |  |
| keysym | uint | An xkbcommon keysym |
| modifiers | uint |  |

**Define a new xkbcommon key binding**

Define a key binding for the given seat in terms of an xkbcommon keysym
and other configurable properties.

The new key binding is not enabled until initial configuration is
completed and the enable request is made during a manage sequence.


[Test](#WlDisplay)

<h3 class="decleration request" title="GetSeat request">
    <a href="?id=RiverXkbBindingsV1_GetSeat" id="RiverXkbBindingsV1_GetSeat">
        <span class="codicon codicon-symbol-method method"></span>
        RiverXkbBindingsV1.<span class="method">GetSeat</span>
    </a>
    <span class="pill">since 2</span>
</h3>

```csharp
RiverXkbBindingsSeatV1 GetSeat(RiverSeatV1 seat)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id |  |
| seat | object |  |

**Manage seat-specific state**

Create an object to manage seat-specific xkb bindings state.

It is a protocol error to make this request more than once for a given
river_seat_v1 object.

<h3 class="decleration enum" title="Error enum">
    <a href="?id=Error" id="Error">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverXkbBindingsV1.<span class="enum">Error</span>
    </a>
</h3>

```csharp
public enum Error
```

| Value | Integer | Description |
| --- | --- | --- |
| ObjectAlreadyCreated | 0 |  |
<h2 class="decleration interface">
    <a href="?id=RiverXkbBindingV1" id="RiverXkbBindingV1">
        <span class="codicon codicon-symbol-interface"></span>
        RiverXkbBindingV1
    </a>
    <span class="pill">version 2</span>
</h2>

Configure a xkb key binding, receive trigger events


This object allows the window manager to configure a xkbcommon key binding
and receive events when the key binding is triggered.

The new key binding is not enabled until the enable request is made during
a manage sequence.

Normally, all key events are sent to the surface with keyboard focus by
the compositor. Key events that trigger a key binding are not sent to the
surface with keyboard focus.

If multiple key bindings would be triggered by a single physical key event
on the compositor side, it is compositor policy which key binding(s) will
receive press/release events or if all of the matched key bindings receive
press/release events.

Key bindings might be matched by the same physical key event due to shared
keysym and modifiers. The layout override feature may also cause the same
physical key event to trigger two key bindings with different keysyms and
different layout overrides configured.



[Test](#WlDisplay)

<h3 class="decleration request" title="Destroy request">
    <a href="?id=RiverXkbBindingV1_Destroy" id="RiverXkbBindingV1_Destroy">
        <span class="codicon codicon-symbol-method method"></span>
        RiverXkbBindingV1.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the xkb binding object**

This request indicates that the client will no longer use the xkb key
binding object and that it may be safely destroyed.


[Test](#WlDisplay)

<h3 class="decleration request" title="SetLayoutOverride request">
    <a href="?id=RiverXkbBindingV1_SetLayoutOverride" id="RiverXkbBindingV1_SetLayoutOverride">
        <span class="codicon codicon-symbol-method method"></span>
        RiverXkbBindingV1.<span class="method">SetLayoutOverride</span>
    </a>
</h3>

```csharp
void SetLayoutOverride(uint layout)
```

| Argument | Type | Description |
| --- | --- | --- |
| layout | uint | 0-indexed xkbcommon layout |

**Override currently active xkb layout**

Specify an xkb layout that should be used to translate key events for
the purpose of triggering this key binding irrespective of the currently
active xkb layout.

The layout argument is a 0-indexed xkbcommon layout number for the
keyboard that generated the key event.

If this request is never made, the currently active xkb layout of the
keyboard that generated the key event will be used.

This request modifies window management state and may only be made as
part of a manage sequence, see the river_window_manager_v1 description.


[Test](#WlDisplay)

<h3 class="decleration request" title="Enable request">
    <a href="?id=RiverXkbBindingV1_Enable" id="RiverXkbBindingV1_Enable">
        <span class="codicon codicon-symbol-method method"></span>
        RiverXkbBindingV1.<span class="method">Enable</span>
    </a>
</h3>

```csharp
void Enable()
```


**Enable the key binding**

This request should be made after all initial configuration has been
completed and the window manager wishes the key binding to be able to be
triggered.

This request modifies window management state and may only be made as
part of a manage sequence, see the river_window_manager_v1 description.


[Test](#WlDisplay)

<h3 class="decleration request" title="Disable request">
    <a href="?id=RiverXkbBindingV1_Disable" id="RiverXkbBindingV1_Disable">
        <span class="codicon codicon-symbol-method method"></span>
        RiverXkbBindingV1.<span class="method">Disable</span>
    </a>
</h3>

```csharp
void Disable()
```


**Disable the key binding**

This request may be used to temporarily disable the key binding. It may
be later re-enabled with the enable request.

This request modifies window management state and may only be made as
part of a manage sequence, see the river_window_manager_v1 description.

<h3 class="decleration event" title="Pressed event">
    <a href="?id=OnRiverXkbBindingV1_Pressed" id="OnRiverXkbBindingV1_Pressed">
        <span class="codicon codicon-symbol-event event"></span>
        RiverXkbBindingV1.<span class="event">OnPressed</span>
    </a>
</h3>

```csharp
void PressedHandler()
```


**The key triggering the binding has been pressed**

This event indicates that the physical key triggering the binding has
been pressed.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

The compositor should wait for the manage sequence to complete before
processing further input events. This allows the window manager client
to, for example, modify key bindings and keyboard focus without racing
against future input events. The window manager should of course respond
as soon as possible as the capacity of the compositor to buffer incoming
input events is finite.

<h3 class="decleration event" title="Released event">
    <a href="?id=OnRiverXkbBindingV1_Released" id="OnRiverXkbBindingV1_Released">
        <span class="codicon codicon-symbol-event event"></span>
        RiverXkbBindingV1.<span class="event">OnReleased</span>
    </a>
</h3>

```csharp
void ReleasedHandler()
```


**The key triggering the binding has been released**

This event indicates that the physical key triggering the binding has
been released.

Releasing the modifiers for the binding without releasing the "main"
physical key that produces the bound keysym does not trigger the release
event. This event is sent when the "main" key is released, even if the
modifiers have changed since the pressed event.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

The compositor should wait for the manage sequence to complete before
processing further input events. This allows the window manager client
to, for example, modify key bindings and keyboard focus without racing
against future input events. The window manager should of course respond
as soon as possible as the capacity of the compositor to buffer incoming
input events is finite.

<h3 class="decleration event" title="StopRepeat event">
    <a href="?id=OnRiverXkbBindingV1_StopRepeat" id="OnRiverXkbBindingV1_StopRepeat">
        <span class="codicon codicon-symbol-event event"></span>
        RiverXkbBindingV1.<span class="event">OnStopRepeat</span>
    </a>
    <span class="pill">since 2</span>
</h3>

```csharp
void StopRepeatHandler()
```


**Repeating should be stopped**

This event indicates that repeating should be stopped for the binding if
the window manager has been repeating some action since the pressed
event.

This event is generally sent when some other (possible unbound) key is
pressed after the pressed event is sent and before the released event
is sent for this binding.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

<h2 class="decleration interface">
    <a href="?id=RiverXkbBindingsSeatV1" id="RiverXkbBindingsSeatV1">
        <span class="codicon codicon-symbol-interface"></span>
        RiverXkbBindingsSeatV1
    </a>
    <span class="pill">version 2</span>
</h2>

Xkb bindings seat


This object manages xkb bindings state associated with a specific seat.



[Test](#WlDisplay)

<h3 class="decleration request" title="Destroy request">
    <a href="?id=RiverXkbBindingsSeatV1_Destroy" id="RiverXkbBindingsSeatV1_Destroy">
        <span class="codicon codicon-symbol-method method"></span>
        RiverXkbBindingsSeatV1.<span class="method">Destroy</span>
    </a>
    <span class="pill">since 2</span>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the object**

This request indicates that the client will no longer use the object and
that it may be safely destroyed.


[Test](#WlDisplay)

<h3 class="decleration request" title="EnsureNextKeyEaten request">
    <a href="?id=RiverXkbBindingsSeatV1_EnsureNextKeyEaten" id="RiverXkbBindingsSeatV1_EnsureNextKeyEaten">
        <span class="codicon codicon-symbol-method method"></span>
        RiverXkbBindingsSeatV1.<span class="method">EnsureNextKeyEaten</span>
    </a>
    <span class="pill">since 2</span>
</h3>

```csharp
void EnsureNextKeyEaten()
```


**Ensure the next key press event is eaten**

Ensure that the next non-modifier key press and corresponding release
events for this seat are not sent to the currently focused surface.

If the next non-modifier key press triggers a binding, the
pressed/released events are sent to the river_xkb_binding_v1 object as
usual.

If the next non-modifier key press does not trigger a binding, the
ate_unbound_key event is sent instead.

Rationale: the window manager may wish to implement "chorded"
keybindings where triggering a binding activates a "submap" with a
different set of keybindings. Without a way to eat the next key
press event, there is no good way for the window manager to know that it
should error out and exit the submap when a key not bound in the submap
is pressed.

This request modifies window management state and may only be made as
part of a manage sequence, see the river_window_manager_v1 description.


[Test](#WlDisplay)

<h3 class="decleration request" title="CancelEnsureNextKeyEaten request">
    <a href="?id=RiverXkbBindingsSeatV1_CancelEnsureNextKeyEaten" id="RiverXkbBindingsSeatV1_CancelEnsureNextKeyEaten">
        <span class="codicon codicon-symbol-method method"></span>
        RiverXkbBindingsSeatV1.<span class="method">CancelEnsureNextKeyEaten</span>
    </a>
    <span class="pill">since 2</span>
</h3>

```csharp
void CancelEnsureNextKeyEaten()
```


**Cancel an ensure_next_key_eaten request**

This requests cancels the effect of the latest ensure_next_key_eaten
request if no key has been eaten due to the request yet. This request
has no effect if a key has already been eaten or no
ensure_next_key_eaten was made.

Rationale: the window manager may wish cancel an uncompleted "chorded"
keybinding after a timeout of a few seconds. Note that since this
timeout use-case requires the window manager to trigger a manage sequence
with the river_window_manager_v1.manage_dirty request it is possible that
the ate_unbound_key key event may be sent before the window manager has
a chance to make the cancel_ensure_next_key_eaten request.

This request modifies window management state and may only be made as
part of a manage sequence, see the river_window_manager_v1 description.

<h3 class="decleration event" title="AteUnboundKey event">
    <a href="?id=OnRiverXkbBindingsSeatV1_AteUnboundKey" id="OnRiverXkbBindingsSeatV1_AteUnboundKey">
        <span class="codicon codicon-symbol-event event"></span>
        RiverXkbBindingsSeatV1.<span class="event">OnAteUnboundKey</span>
    </a>
    <span class="pill">since 2</span>
</h3>

```csharp
void AteUnboundKeyHandler()
```


**An unbound key press event was eaten**

An unbound key press event was eaten due to the ensure_next_key_eaten
request.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

