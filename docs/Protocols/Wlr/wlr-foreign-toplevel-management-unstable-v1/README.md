# Foreign Toplevel Management

##### [WaylandDotnet](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet) ![](../../../assets/arrow.svg ':class=breadcrumb-arrow') [Wlr](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/Wlr) ![](../../../assets/arrow.svg ':class=breadcrumb-arrow') [WlrForeignToplevelManagementUnstableV1](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/Wlr/wlr-foreign-toplevel-management-unstable-v1/)

---

<h2 class="decleration interface">
    <a href="?id=ZwlrForeignToplevelManagerV1" id="ZwlrForeignToplevelManagerV1">
        <span class="codicon codicon-symbol-interface"></span>
        ZwlrForeignToplevelManagerV1
    </a>
    <span class="pill">version 3</span>
</h2>

List and control opened apps


The purpose of this protocol is to enable the creation of taskbars
and docks by providing them with a list of opened applications and
letting them request certain actions on them, like maximizing, etc.

After a client binds the zwlr_foreign_toplevel_manager_v1, each opened
toplevel window will be sent via the toplevel event



[Test](#WlDisplay)

<h3 class="decleration request" title="Stop request">
    <a href="?id=ZwlrForeignToplevelManagerV1_Stop" id="ZwlrForeignToplevelManagerV1_Stop">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrForeignToplevelManagerV1.<span class="method">Stop</span>
    </a>
</h3>

```csharp
void Stop()
```


**Stop sending events**

Indicates the client no longer wishes to receive events for new toplevels.
However the compositor may emit further toplevel_created events, until
the finished event is emitted.

The client must not send any more requests after this one.

<h3 class="decleration event" title="Toplevel event">
    <a href="?id=OnZwlrForeignToplevelManagerV1_Toplevel" id="OnZwlrForeignToplevelManagerV1_Toplevel">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrForeignToplevelManagerV1.<span class="event">OnToplevel</span>
    </a>
</h3>

```csharp
void ToplevelHandler(ZwlrForeignToplevelHandleV1 toplevel)
```

| Argument | Type | Description |
| --- | --- | --- |
| toplevel | new_id |  |

**A toplevel has been created**

This event is emitted whenever a new toplevel window is created. It
is emitted for all toplevels, regardless of the app that has created
them.

All initial details of the toplevel(title, app_id, states, etc.) will
be sent immediately after this event via the corresponding events in
zwlr_foreign_toplevel_handle_v1.

<h3 class="decleration event" title="Finished event">
    <a href="?id=OnZwlrForeignToplevelManagerV1_Finished" id="OnZwlrForeignToplevelManagerV1_Finished">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrForeignToplevelManagerV1.<span class="event">OnFinished</span>
    </a>
</h3>

```csharp
void FinishedHandler()
```


**The compositor has finished with the toplevel manager**

This event indicates that the compositor is done sending events to the
zwlr_foreign_toplevel_manager_v1. The server will destroy the object
immediately after sending this request, so it will become invalid and
the client should free any resources associated with it.

<h2 class="decleration interface">
    <a href="?id=ZwlrForeignToplevelHandleV1" id="ZwlrForeignToplevelHandleV1">
        <span class="codicon codicon-symbol-interface"></span>
        ZwlrForeignToplevelHandleV1
    </a>
    <span class="pill">version 3</span>
</h2>

An opened toplevel


A zwlr_foreign_toplevel_handle_v1 object represents an opened toplevel
window. Each app may have multiple opened toplevels.

Each toplevel has a list of outputs it is visible on, conveyed to the
client with the output_enter and output_leave events.



[Test](#WlDisplay)

<h3 class="decleration request" title="SetMaximized request">
    <a href="?id=ZwlrForeignToplevelHandleV1_SetMaximized" id="ZwlrForeignToplevelHandleV1_SetMaximized">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrForeignToplevelHandleV1.<span class="method">SetMaximized</span>
    </a>
</h3>

```csharp
void SetMaximized()
```


**Requests that the toplevel be maximized**

Requests that the toplevel be maximized. If the maximized state actually
changes, this will be indicated by the state event.


[Test](#WlDisplay)

<h3 class="decleration request" title="UnsetMaximized request">
    <a href="?id=ZwlrForeignToplevelHandleV1_UnsetMaximized" id="ZwlrForeignToplevelHandleV1_UnsetMaximized">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrForeignToplevelHandleV1.<span class="method">UnsetMaximized</span>
    </a>
</h3>

```csharp
void UnsetMaximized()
```


**Requests that the toplevel be unmaximized**

Requests that the toplevel be unmaximized. If the maximized state actually
changes, this will be indicated by the state event.


[Test](#WlDisplay)

<h3 class="decleration request" title="SetMinimized request">
    <a href="?id=ZwlrForeignToplevelHandleV1_SetMinimized" id="ZwlrForeignToplevelHandleV1_SetMinimized">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrForeignToplevelHandleV1.<span class="method">SetMinimized</span>
    </a>
</h3>

```csharp
void SetMinimized()
```


**Requests that the toplevel be minimized**

Requests that the toplevel be minimized. If the minimized state actually
changes, this will be indicated by the state event.


[Test](#WlDisplay)

<h3 class="decleration request" title="UnsetMinimized request">
    <a href="?id=ZwlrForeignToplevelHandleV1_UnsetMinimized" id="ZwlrForeignToplevelHandleV1_UnsetMinimized">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrForeignToplevelHandleV1.<span class="method">UnsetMinimized</span>
    </a>
</h3>

```csharp
void UnsetMinimized()
```


**Requests that the toplevel be unminimized**

Requests that the toplevel be unminimized. If the minimized state actually
changes, this will be indicated by the state event.


[Test](#WlDisplay)

<h3 class="decleration request" title="Activate request">
    <a href="?id=ZwlrForeignToplevelHandleV1_Activate" id="ZwlrForeignToplevelHandleV1_Activate">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrForeignToplevelHandleV1.<span class="method">Activate</span>
    </a>
</h3>

```csharp
void Activate(WlSeat seat)
```

| Argument | Type | Description |
| --- | --- | --- |
| seat | object |  |

**Activate the toplevel**

Request that this toplevel be activated on the given seat.
There is no guarantee the toplevel will be actually activated.


[Test](#WlDisplay)

<h3 class="decleration request" title="Close request">
    <a href="?id=ZwlrForeignToplevelHandleV1_Close" id="ZwlrForeignToplevelHandleV1_Close">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrForeignToplevelHandleV1.<span class="method">Close</span>
    </a>
</h3>

```csharp
void Close()
```


**Request that the toplevel be closed**

Send a request to the toplevel to close itself. The compositor would
typically use a shell-specific method to carry out this request, for
example by sending the xdg_toplevel.close event. However, this gives
no guarantees the toplevel will actually be destroyed. If and when
this happens, the zwlr_foreign_toplevel_handle_v1.closed event will
be emitted.


[Test](#WlDisplay)

<h3 class="decleration request" title="SetRectangle request">
    <a href="?id=ZwlrForeignToplevelHandleV1_SetRectangle" id="ZwlrForeignToplevelHandleV1_SetRectangle">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrForeignToplevelHandleV1.<span class="method">SetRectangle</span>
    </a>
</h3>

```csharp
void SetRectangle(WlSurface surface, int x, int y, int width, int height)
```

| Argument | Type | Description |
| --- | --- | --- |
| surface | object |  |
| x | int |  |
| y | int |  |
| width | int |  |
| height | int |  |

**The rectangle which represents the toplevel**

The rectangle of the surface specified in this request corresponds to
the place where the app using this protocol represents the given toplevel.
It can be used by the compositor as a hint for some operations, e.g
minimizing. The client is however not required to set this, in which
case the compositor is free to decide some default value.

If the client specifies more than one rectangle, only the last one is
considered.

The dimensions are given in surface-local coordinates.
Setting width=height=0 removes the already-set rectangle.


[Test](#WlDisplay)

<h3 class="decleration request" title="Destroy request">
    <a href="?id=ZwlrForeignToplevelHandleV1_Destroy" id="ZwlrForeignToplevelHandleV1_Destroy">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrForeignToplevelHandleV1.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the zwlr_foreign_toplevel_handle_v1 object**

Destroys the zwlr_foreign_toplevel_handle_v1 object.

This request should be called either when the client does not want to
use the toplevel anymore or after the closed event to finalize the
destruction of the object.


[Test](#WlDisplay)

<h3 class="decleration request" title="SetFullscreen request">
    <a href="?id=ZwlrForeignToplevelHandleV1_SetFullscreen" id="ZwlrForeignToplevelHandleV1_SetFullscreen">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrForeignToplevelHandleV1.<span class="method">SetFullscreen</span>
    </a>
    <span class="pill">since 2</span>
</h3>

```csharp
void SetFullscreen(WlOutput? output)
```

| Argument | Type | Description |
| --- | --- | --- |
| output | object |  |

**Request that the toplevel be fullscreened**

Requests that the toplevel be fullscreened on the given output. If the
fullscreen state and/or the outputs the toplevel is visible on actually
change, this will be indicated by the state and output_enter/leave
events.

The output parameter is only a hint to the compositor. Also, if output
is NULL, the compositor should decide which output the toplevel will be
fullscreened on, if at all.


[Test](#WlDisplay)

<h3 class="decleration request" title="UnsetFullscreen request">
    <a href="?id=ZwlrForeignToplevelHandleV1_UnsetFullscreen" id="ZwlrForeignToplevelHandleV1_UnsetFullscreen">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrForeignToplevelHandleV1.<span class="method">UnsetFullscreen</span>
    </a>
    <span class="pill">since 2</span>
</h3>

```csharp
void UnsetFullscreen()
```


**Request that the toplevel be unfullscreened**

Requests that the toplevel be unfullscreened. If the fullscreen state
actually changes, this will be indicated by the state event.

<h3 class="decleration event" title="Title event">
    <a href="?id=OnZwlrForeignToplevelHandleV1_Title" id="OnZwlrForeignToplevelHandleV1_Title">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrForeignToplevelHandleV1.<span class="event">OnTitle</span>
    </a>
</h3>

```csharp
void TitleHandler(string title)
```

| Argument | Type | Description |
| --- | --- | --- |
| title | string |  |

**Title change**

This event is emitted whenever the title of the toplevel changes.

<h3 class="decleration event" title="AppId event">
    <a href="?id=OnZwlrForeignToplevelHandleV1_AppId" id="OnZwlrForeignToplevelHandleV1_AppId">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrForeignToplevelHandleV1.<span class="event">OnAppId</span>
    </a>
</h3>

```csharp
void AppIdHandler(string appId)
```

| Argument | Type | Description |
| --- | --- | --- |
| app_id | string |  |

**App-id change**

This event is emitted whenever the app-id of the toplevel changes.

<h3 class="decleration event" title="OutputEnter event">
    <a href="?id=OnZwlrForeignToplevelHandleV1_OutputEnter" id="OnZwlrForeignToplevelHandleV1_OutputEnter">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrForeignToplevelHandleV1.<span class="event">OnOutputEnter</span>
    </a>
</h3>

```csharp
void OutputEnterHandler(WlOutput output)
```

| Argument | Type | Description |
| --- | --- | --- |
| output | object |  |

**Toplevel entered an output**

This event is emitted whenever the toplevel becomes visible on
the given output. A toplevel may be visible on multiple outputs.

<h3 class="decleration event" title="OutputLeave event">
    <a href="?id=OnZwlrForeignToplevelHandleV1_OutputLeave" id="OnZwlrForeignToplevelHandleV1_OutputLeave">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrForeignToplevelHandleV1.<span class="event">OnOutputLeave</span>
    </a>
</h3>

```csharp
void OutputLeaveHandler(WlOutput output)
```

| Argument | Type | Description |
| --- | --- | --- |
| output | object |  |

**Toplevel left an output**

This event is emitted whenever the toplevel stops being visible on
the given output. It is guaranteed that an entered-output event
with the same output has been emitted before this event.

<h3 class="decleration event" title="State event">
    <a href="?id=OnZwlrForeignToplevelHandleV1_State" id="OnZwlrForeignToplevelHandleV1_State">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrForeignToplevelHandleV1.<span class="event">OnState</span>
    </a>
</h3>

```csharp
void StateHandler(byte[] state)
```

| Argument | Type | Description |
| --- | --- | --- |
| state | array |  |

**The toplevel state changed**

This event is emitted immediately after the zlw_foreign_toplevel_handle_v1
is created and each time the toplevel state changes, either because of a
compositor action or because of a request in this protocol.

<h3 class="decleration event" title="Done event">
    <a href="?id=OnZwlrForeignToplevelHandleV1_Done" id="OnZwlrForeignToplevelHandleV1_Done">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrForeignToplevelHandleV1.<span class="event">OnDone</span>
    </a>
</h3>

```csharp
void DoneHandler()
```


**All information about the toplevel has been sent**

This event is sent after all changes in the toplevel state have been
sent.

This allows changes to the zwlr_foreign_toplevel_handle_v1 properties
to be seen as atomic, even if they happen via multiple events.

<h3 class="decleration event" title="Closed event">
    <a href="?id=OnZwlrForeignToplevelHandleV1_Closed" id="OnZwlrForeignToplevelHandleV1_Closed">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrForeignToplevelHandleV1.<span class="event">OnClosed</span>
    </a>
</h3>

```csharp
void ClosedHandler()
```


**This toplevel has been destroyed**

This event means the toplevel has been destroyed. It is guaranteed there
won't be any more events for this zwlr_foreign_toplevel_handle_v1. The
toplevel itself becomes inert so any requests will be ignored except the
destroy request.

<h3 class="decleration event" title="Parent event">
    <a href="?id=OnZwlrForeignToplevelHandleV1_Parent" id="OnZwlrForeignToplevelHandleV1_Parent">
        <span class="codicon codicon-symbol-event event"></span>
        ZwlrForeignToplevelHandleV1.<span class="event">OnParent</span>
    </a>
    <span class="pill">since 3</span>
</h3>

```csharp
void ParentHandler(ZwlrForeignToplevelHandleV1? parent)
```

| Argument | Type | Description |
| --- | --- | --- |
| parent | object |  |

**Parent change**

This event is emitted whenever the parent of the toplevel changes.

No event is emitted when the parent handle is destroyed by the client.

<h3 class="decleration enum" title="State enum">
    <a href="?id=State" id="State">
        <span class="codicon codicon-symbol-enum enum"></span>
        ZwlrForeignToplevelHandleV1.<span class="enum">State</span>
    </a>
</h3>

```csharp
public enum State
```

Types of states on the toplevel


The different states that a toplevel can have. These have the same meaning
as the states with the same names defined in xdg-toplevel


| Value | Integer | Description |
| --- | --- | --- |
| Maximized | 0 | The toplevel is maximized |
| Minimized | 1 | The toplevel is minimized |
| Activated | 2 | The toplevel is active |
| Fullscreen | 3 | The toplevel is fullscreen |
<h3 class="decleration enum" title="Error enum">
    <a href="?id=Error" id="Error">
        <span class="codicon codicon-symbol-enum enum"></span>
        ZwlrForeignToplevelHandleV1.<span class="enum">Error</span>
    </a>
</h3>

```csharp
public enum Error
```

| Value | Integer | Description |
| --- | --- | --- |
| InvalidRectangle | 0 | The provided rectangle is invalid |
