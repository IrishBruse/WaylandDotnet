# River Window Management

##### [WaylandDotnet](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet) ![](../../../assets/arrow.svg ':class=breadcrumb-arrow') [River](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/River) ![](../../../assets/arrow.svg ':class=breadcrumb-arrow') [RiverWindowManagementV1](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/River/river-window-management-v1/)

---

<h2 class="decleration interface">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowmanagerv1" id="riverwindowmanagerv1">
        <span class="codicon codicon-symbol-interface"></span>
        RiverWindowManagerV1
    </a>
    <span class="pill">version 4</span>
</h2>

Window manager global interface


This global interface should only be advertised to the window manager
process. Only one window management client may be active at a time. The
compositor should use the unavailable event if necessary to enforce this.

There are two disjoint categories of state managed by this protocol:

Window management state influences the communication between the
compositor and individual windows (e.g. xdg_toplevels). Window management
state includes window dimensions, fullscreen state, keyboard focus,
keyboard bindings, and more.

Rendering state only affects the rendered output of the compositor and
does not influence communication between the compositor and individual
windows. Rendering state includes the position and rendering order of
windows, shell surfaces, decoration surfaces, borders, and more.

Window management state may only be modified by the window manager as part
of a manage sequence. A manage sequence is started with the manage_start
event and ended with the manage_finish request. It is a protocol error to
modify window management state outside of a manage sequence.

A manage sequence is always followed by at least one render sequence. A
render sequence is started with the render_start event and ended with the
render_finish request.

Rendering state may be modified by the window manager during a manage
sequence or a render sequence. Regardless of when the rendering state is
modified, it is applied with the next render_finish request. It is a
protocol error to modify rendering state outside of a manage or render
sequence.

The server will start a manage sequence by sending new state and the
manage_start event as soon as possible whenever there is a change in state
that must be communicated with the window manager.

If the window manager client needs to ensure a manage sequence is started
due to a state change the compositor is not aware of, it may send the
manage_dirty request.

The server will start a render sequence by sending new state and the
render_start event as soon as possible whenever there is a change in
window dimensions that must be communicated with the window manager.
Multiple render sequences may be made consecutively without a manage
sequence in between, for example if a window independently changes its own
dimensions.

To summarize, the main loop of this protocol is as follows:

1. The server sends events indicating all changes since the last
manage sequence followed by the manage_start event.

2. The client sends requests modifying window management state or
rendering state (as defined above) followed by the manage_finish
request.

3. The server sends new state to windows and waits for responses.

4. The server sends new window dimensions to the client followed by the
render_start event.

5. The client sends requests modifying rendering state (as defined above)
followed by the render_finish request.

6. If window dimensions change, loop back to step 4.
If state that requires a manage sequence changes or if the client makes
a manage_dirty request, loop back to step 1.

For the purposes of frame perfection, the server may delay rendering new
state committed by the windows in step 3 until after step 5 is finished.

It is a protocol error for the client to make a manage_finish or
render_finish request that violates this ordering.


<h3 class="decleration request" title="Stop request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowmanagerv1_stop" id="riverwindowmanagerv1_stop">
        <span class="codicon codicon-symbol-method method"></span>
        RiverWindowManagerV1.<span class="method">Stop</span>
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
for a river_window_manager_v1.finished event before destroying this
object.

<h3 class="decleration request" title="Destroy request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowmanagerv1_destroy" id="riverwindowmanagerv1_destroy">
        <span class="codicon codicon-symbol-method method"></span>
        RiverWindowManagerV1.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the river_window_manager_v1 object**

This request should be called after the finished event has been received
to complete destruction of the object.

If a client wishes to destroy this object it should send a
river_window_manager_v1.stop request and wait for a
river_window_manager_v1.finished event. Once the finished event is
received it is safe to destroy this object and any other objects created
through this interface.

<h3 class="decleration request" title="ManageFinish request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowmanagerv1_managefinish" id="riverwindowmanagerv1_managefinish">
        <span class="codicon codicon-symbol-method method"></span>
        RiverWindowManagerV1.<span class="method">ManageFinish</span>
    </a>
</h3>

```csharp
void ManageFinish()
```


**Finish a manage sequence**

This request indicates that the client has made all changes to window
management state it wishes to include in the current manage sequence and
that the server should atomically send these state changes to the
windows and continue with the manage sequence.

After sending this request, it is a protocol error for the client to
make further changes to window management state until the next
manage_start event is received.

See the description of the river_window_manager_v1 interface for a
complete overview of the manage/render sequence loop.

<h3 class="decleration request" title="ManageDirty request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowmanagerv1_managedirty" id="riverwindowmanagerv1_managedirty">
        <span class="codicon codicon-symbol-method method"></span>
        RiverWindowManagerV1.<span class="method">ManageDirty</span>
    </a>
</h3>

```csharp
void ManageDirty()
```


**Ensure a manage sequence is started**

This request ensures a manage sequence is started and that a
manage_start event is sent by the server. If this request is made during
an ongoing manage sequence, a new manage sequence will be started as
soon as the current one is completed.

The client may want to use this request due to an internal state change
that the compositor is not aware of (e.g. a dbus event) which should
affect window management or rendering state.

<h3 class="decleration request" title="RenderFinish request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowmanagerv1_renderfinish" id="riverwindowmanagerv1_renderfinish">
        <span class="codicon codicon-symbol-method method"></span>
        RiverWindowManagerV1.<span class="method">RenderFinish</span>
    </a>
</h3>

```csharp
void RenderFinish()
```


**Finish a render sequence**

This request indicates that the client has made all changes to rendering
state it wishes to include in the current manage sequence and that the
server should atomically apply and display these state changes to the
user.

After sending this request, it is a protocol error for the client to
make further changes to rendering state until the next manage_start or
render_start event is received, whichever comes first.

See the description of the river_window_manager_v1 interface for a
complete overview of the manage/render sequence loop.

<h3 class="decleration request" title="GetShellSurface request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowmanagerv1_getshellsurface" id="riverwindowmanagerv1_getshellsurface">
        <span class="codicon codicon-symbol-method method"></span>
        RiverWindowManagerV1.<span class="method">GetShellSurface</span>
    </a>
</h3>

```csharp
RiverShellSurfaceV1 GetShellSurface(WlSurface surface)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id | New river shell surface |
| surface | object | Base surface |

**Assign the river_shell_surface_v1 surface role**

Create a new shell surface for window manager UI and assign the
river_shell_surface_v1 role to the surface.

Providing a wl_surface which already has a role or already has a buffer
attached or committed is a protocol error.

<h3 class="decleration request" title="ExitSession request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowmanagerv1_exitsession" id="riverwindowmanagerv1_exitsession">
        <span class="codicon codicon-symbol-method method"></span>
        RiverWindowManagerV1.<span class="method">ExitSession</span>
    </a>
    <span class="pill">since 4</span>
</h3>

```csharp
void ExitSession()
```


**Exit the Wayland session**

End the current Wayland session and exit the compositor.
All Wayland clients running in the current session, including
the window manager, will be disconnected.

Window managers should only make this request if the user explicitly
asks to exit the Wayland session, not for example on normal window
manager termination.

<h3 class="decleration event" title="Unavailable event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverwindowmanagerv1_unavailable" id="onriverwindowmanagerv1_unavailable">
        <span class="codicon codicon-symbol-event event"></span>
        RiverWindowManagerV1.<span class="event">OnUnavailable</span>
    </a>
</h3>

```csharp
void UnavailableHandler()
```


**Window management unavailable**

This event indicates that window management is not available to the
client, perhaps due to another window management client already running.
The circumstances causing this event to be sent are compositor policy.

If sent, this event is guaranteed to be the first and only event sent by
the server.

The server will send no further events on this object. The client should
destroy this object and all objects created through this interface.

<h3 class="decleration event" title="Finished event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverwindowmanagerv1_finished" id="onriverwindowmanagerv1_finished">
        <span class="codicon codicon-symbol-event event"></span>
        RiverWindowManagerV1.<span class="event">OnFinished</span>
    </a>
</h3>

```csharp
void FinishedHandler()
```


**The server has finished with the window manager**

This event indicates that the server will send no further events on this
object. The client should destroy the object. See
river_window_manager_v1.destroy for more information.

<h3 class="decleration event" title="ManageStart event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverwindowmanagerv1_managestart" id="onriverwindowmanagerv1_managestart">
        <span class="codicon codicon-symbol-event event"></span>
        RiverWindowManagerV1.<span class="event">OnManageStart</span>
    </a>
</h3>

```csharp
void ManageStartHandler()
```


**Start a manage sequence**

This event indicates that the server has sent events indicating all
state changes since the last manage sequence.

In response to this event, the client should make requests modifying
window management state as it chooses. Then, the client must make the
manage_finish request.

See the description of the river_window_manager_v1 interface for a
complete overview of the manage/render sequence loop.

<h3 class="decleration event" title="RenderStart event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverwindowmanagerv1_renderstart" id="onriverwindowmanagerv1_renderstart">
        <span class="codicon codicon-symbol-event event"></span>
        RiverWindowManagerV1.<span class="event">OnRenderStart</span>
    </a>
</h3>

```csharp
void RenderStartHandler()
```


**Start a render sequence**

This event indicates that the server has sent all river_node_v1.position
and river_window_v1.dimensions events necessary.

In response to this event, the client should make requests modifying
rendering state as it chooses. Then, the client must make the
render_finish request.

See the description of the river_window_manager_v1 interface for a
complete overview of the manage/render sequence loop.

<h3 class="decleration event" title="SessionLocked event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverwindowmanagerv1_sessionlocked" id="onriverwindowmanagerv1_sessionlocked">
        <span class="codicon codicon-symbol-event event"></span>
        RiverWindowManagerV1.<span class="event">OnSessionLocked</span>
    </a>
</h3>

```csharp
void SessionLockedHandler()
```


**The session has been locked**

This event indicates that the session has been locked.

The window manager may wish to restrict which key bindings are available
while locked or otherwise use this information.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

<h3 class="decleration event" title="SessionUnlocked event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverwindowmanagerv1_sessionunlocked" id="onriverwindowmanagerv1_sessionunlocked">
        <span class="codicon codicon-symbol-event event"></span>
        RiverWindowManagerV1.<span class="event">OnSessionUnlocked</span>
    </a>
</h3>

```csharp
void SessionUnlockedHandler()
```


**The session has been unlocked**

This event indicates that the session has been unlocked.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

<h3 class="decleration event" title="Window event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverwindowmanagerv1_window" id="onriverwindowmanagerv1_window">
        <span class="codicon codicon-symbol-event event"></span>
        RiverWindowManagerV1.<span class="event">OnWindow</span>
    </a>
</h3>

```csharp
void WindowHandler(RiverWindowV1 id)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id | New window |

**New window**

A new window has been created.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

<h3 class="decleration event" title="Output event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverwindowmanagerv1_output" id="onriverwindowmanagerv1_output">
        <span class="codicon codicon-symbol-event event"></span>
        RiverWindowManagerV1.<span class="event">OnOutput</span>
    </a>
</h3>

```csharp
void OutputHandler(RiverOutputV1 id)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id | New output |

**New output**

A new logical output has been created, perhaps due to a new physical
monitor being plugged in or perhaps due to a change in configuration.

This event will be followed by river_output_v1.position and dimensions
events as well as a manage_start event after all other new state has
been sent by the server.

<h3 class="decleration event" title="Seat event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverwindowmanagerv1_seat" id="onriverwindowmanagerv1_seat">
        <span class="codicon codicon-symbol-event event"></span>
        RiverWindowManagerV1.<span class="event">OnSeat</span>
    </a>
</h3>

```csharp
void SeatHandler(RiverSeatV1 id)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id | New seat |

**New seat**

A new seat has been created.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

<h3 class="decleration enum" title="Error enum">
    <a href="#/Protocols/River/river-window-management-v1/?id=error" id="error">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverWindowManagerV1.<span class="enum">Error</span>
    </a>
</h3>

```csharp
public enum Error
```

| Value | Integer | Description |
| --- | --- | --- |
| SequenceOrder | 0 | Request violates manage/render sequence ordering |
| Role | 1 | Given wl_surface already has a role |
| Unresponsive | 2 | Window manager unresponsive |
<h2 class="decleration interface">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowv1" id="riverwindowv1">
        <span class="codicon codicon-symbol-interface"></span>
        RiverWindowV1
    </a>
    <span class="pill">version 4</span>
</h2>

A logical window


This represents a logical window. For example, a window may correspond to
an xdg_toplevel or Xwayland window.

A newly created window will not be displayed until the window manager
makes a propose_dimensions or fullscreen request as part of a manage
sequence, the server replies with a dimensions event as part of a render
sequence, and that render sequence is finished.


<h3 class="decleration request" title="Destroy request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowv1_destroy" id="riverwindowv1_destroy">
        <span class="codicon codicon-symbol-method method"></span>
        RiverWindowV1.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the window object**

This request indicates that the client will no longer use the window
object and that it may be safely destroyed.

This request should be made after the river_window_v1.closed event or
river_window_manager_v1.finished is received to complete destruction of
the window.

<h3 class="decleration request" title="Close request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowv1_close" id="riverwindowv1_close">
        <span class="codicon codicon-symbol-method method"></span>
        RiverWindowV1.<span class="method">Close</span>
    </a>
</h3>

```csharp
void Close()
```


**Request that the window be closed**

Request that the window be closed. The window may ignore this request or
only close after some delay, perhaps opening a dialog asking the user to
save their work or similar.

The server will send a river_window_v1.closed event if/when the window
has been closed.

This request modifies window management state and may only be made as
part of a manage sequence, see the river_window_manager_v1 description.

<h3 class="decleration request" title="GetNode request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowv1_getnode" id="riverwindowv1_getnode">
        <span class="codicon codicon-symbol-method method"></span>
        RiverWindowV1.<span class="method">GetNode</span>
    </a>
</h3>

```csharp
RiverNodeV1 GetNode()
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id | New node |

**Get the window's render list node**

Get the node in the render list corresponding to the window.

It is a protocol error to make this request more than once for a single
window.

<h3 class="decleration request" title="ProposeDimensions request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowv1_proposedimensions" id="riverwindowv1_proposedimensions">
        <span class="codicon codicon-symbol-method method"></span>
        RiverWindowV1.<span class="method">ProposeDimensions</span>
    </a>
</h3>

```csharp
void ProposeDimensions(int width, int height)
```

| Argument | Type | Description |
| --- | --- | --- |
| width | int | Proposed content width |
| height | int | Proposed content height |

**Propose window dimensions**

This request proposes dimensions for the window in the compositor's
logical coordinate space.

The width and height must be greater than or equal to zero. If the width
or height is zero the window will be allowed to decide its own
dimensions.

The window may not take the exact dimensions proposed. The actual
dimensions taken by the window will be sent in a subsequent
river_window_v1.dimensions event. For example, a terminal emulator may
only allow dimensions that are multiple of the cell size.

When a propose_dimensions request is made, the server must send a
dimensions event in response as soon as possible. It may not be possible
to send a dimensions event in the very next render sequence if, for
example, the window takes too long to respond to the proposed
dimensions. In this case, the server will send the dimensions event in a
future render sequence.

Note that the dimensions of a river_window_v1 refer to the dimensions of
the window content and are unaffected by the presence of borders or
decoration surfaces.

This request modifies window management state and may only be made as
part of a manage sequence, see the river_window_manager_v1 description.

<h3 class="decleration request" title="Hide request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowv1_hide" id="riverwindowv1_hide">
        <span class="codicon codicon-symbol-method method"></span>
        RiverWindowV1.<span class="method">Hide</span>
    </a>
</h3>

```csharp
void Hide()
```


**Request that the window be hidden**

Request that the window be hidden. Has no effect if the window is
already hidden. Hides any window borders and decorations as well.

Newly created windows are considered shown unless explicitly hidden with
the hide request.

This request modifies rendering state and may only be made as part of a
render sequence, see the river_window_manager_v1 description.

<h3 class="decleration request" title="Show request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowv1_show" id="riverwindowv1_show">
        <span class="codicon codicon-symbol-method method"></span>
        RiverWindowV1.<span class="method">Show</span>
    </a>
</h3>

```csharp
void Show()
```


**Request that the window be shown**

Request that the window be shown. Has no effect if the window is not
hidden. Does not guarantee that the window is visible as it may be
completely obscured by other windows placed above it for example.

Newly created windows are considered shown unless explicitly hidden with
the hide request.

This request modifies rendering state and may only be made as part of a
render sequence, see the river_window_manager_v1 description.

<h3 class="decleration request" title="UseCsd request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowv1_usecsd" id="riverwindowv1_usecsd">
        <span class="codicon codicon-symbol-method method"></span>
        RiverWindowV1.<span class="method">UseCsd</span>
    </a>
</h3>

```csharp
void UseCsd()
```


**Tell the client to use CSD**

Tell the client to use client side decoration and draw its own title
bar, borders, etc.

This is the default if neither this request nor the use_ssd request is
ever made.

This request modifies window management state and may only be made as
part of a manage sequence, see the river_window_manager_v1 description.

<h3 class="decleration request" title="UseSsd request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowv1_usessd" id="riverwindowv1_usessd">
        <span class="codicon codicon-symbol-method method"></span>
        RiverWindowV1.<span class="method">UseSsd</span>
    </a>
</h3>

```csharp
void UseSsd()
```


**Tell the client to use SSD**

Tell the client to use server side decoration and not draw any client
side decorations.

This request will have no effect if the client only supports client side
decoration, see the decoration_hint event.

This request modifies window management state and may only be made as
part of a manage sequence, see the river_window_manager_v1 description.

<h3 class="decleration request" title="SetBorders request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowv1_setborders" id="riverwindowv1_setborders">
        <span class="codicon codicon-symbol-method method"></span>
        RiverWindowV1.<span class="method">SetBorders</span>
    </a>
</h3>

```csharp
void SetBorders(uint edges, int width, uint r, uint g, uint b, uint a)
```

| Argument | Type | Description |
| --- | --- | --- |
| edges | uint | Border edges |
| width | int | Border width |
| r | uint | 32-bit red value |
| g | uint | 32-bit green value |
| b | uint | 32-bit blue value |
| a | uint | 32-bit alpha value |

**Set window borders**

This request decorates the window with borders drawn by the compositor
on the specified edges of the window. Borders are drawn above the window
content.

Corners are drawn only between borders on adjacent edges. If e.g. the
left edge has a border and the top edge does not, the border drawn on
the left edge will not extend vertically beyond the top edge of the
window.

Borders are not drawn while the window is fullscreen.

The color is defined by four 32-bit RGBA values. Unless specified in
another protocol extension, the RGBA values use pre-multiplied alpha.

Setting the edges to none or the width to 0 disables the borders.
Setting a negative width is a protocol error.

This request completely overrides all previous set_borders requests.
Only the most recent set_borders request has an effect.

Note that the position/dimensions of a river_window_v1 refer to the
position/dimensions of the window content and are unaffected by the
presence of borders or decoration surfaces.

This request modifies rendering state and may only be made as part of a
render sequence, see the river_window_manager_v1 description.

<h3 class="decleration request" title="SetTiled request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowv1_settiled" id="riverwindowv1_settiled">
        <span class="codicon codicon-symbol-method method"></span>
        RiverWindowV1.<span class="method">SetTiled</span>
    </a>
</h3>

```csharp
void SetTiled(uint edges)
```

| Argument | Type | Description |
| --- | --- | --- |
| edges | uint | Tiled edges |

**Set window tiled state**

Inform the window that it is part of a tiled layout and adjacent to
other elements in the tiled layout on the given edges.

The window should use this information to change the style of its client
side decorations and avoid drawing e.g. drop shadows outside of the
window dimensions on the tiled edges.

Setting the edges argument to none informs the window that it is not
part of a tiled layout. If this request is never made, the window is
informed that it is not part of a tiled layout.

This request modifies window management state and may only be made as
part of a manage sequence, see the river_window_manager_v1 description.

<h3 class="decleration request" title="GetDecorationAbove request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowv1_getdecorationabove" id="riverwindowv1_getdecorationabove">
        <span class="codicon codicon-symbol-method method"></span>
        RiverWindowV1.<span class="method">GetDecorationAbove</span>
    </a>
</h3>

```csharp
RiverDecorationV1 GetDecorationAbove(WlSurface surface)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id | New decoration surface |
| surface | object | Base surface |

**Create a decoration above the window in z-order**

Create a decoration surface and assign the river_decoration_v1 role to
the surface. The created decoration is placed above the window in
rendering order, see the description of river_decoration_v1.

Providing a wl_surface which already has a role or already has a buffer
attached or committed is a protocol error.

<h3 class="decleration request" title="GetDecorationBelow request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowv1_getdecorationbelow" id="riverwindowv1_getdecorationbelow">
        <span class="codicon codicon-symbol-method method"></span>
        RiverWindowV1.<span class="method">GetDecorationBelow</span>
    </a>
</h3>

```csharp
RiverDecorationV1 GetDecorationBelow(WlSurface surface)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id | New decoration surface |
| surface | object | Base surface |

**Create a decoration below the window in z-order**

Create a decoration surface and assign the river_decoration_v1 role to
the surface. The created decoration is placed below the window in
rendering order, see the description of river_decoration_v1.

Providing a wl_surface which already has a role or already has a buffer
attached or committed is a protocol error.

<h3 class="decleration request" title="InformResizeStart request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowv1_informresizestart" id="riverwindowv1_informresizestart">
        <span class="codicon codicon-symbol-method method"></span>
        RiverWindowV1.<span class="method">InformResizeStart</span>
    </a>
</h3>

```csharp
void InformResizeStart()
```


**Inform the window it is being resized**

Inform the window that it is being resized. The window manager should
use this request to inform windows that are the target of an interactive
resize for example.

The window manager remains responsible for handling the position and
dimensions of the window while it is resizing.

This request modifies window management state and may only be made as
part of a manage sequence, see the river_window_manager_v1 description.

<h3 class="decleration request" title="InformResizeEnd request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowv1_informresizeend" id="riverwindowv1_informresizeend">
        <span class="codicon codicon-symbol-method method"></span>
        RiverWindowV1.<span class="method">InformResizeEnd</span>
    </a>
</h3>

```csharp
void InformResizeEnd()
```


**Inform the window it no longer being resized**

Inform the window that it is no longer being resized. The window manager
should use this request to inform windows that are the target of an
interactive resize that the interactive resize has ended for example.

This request modifies window management state and may only be made as
part of a manage sequence, see the river_window_manager_v1 description.

<h3 class="decleration request" title="SetCapabilities request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowv1_setcapabilities" id="riverwindowv1_setcapabilities">
        <span class="codicon codicon-symbol-method method"></span>
        RiverWindowV1.<span class="method">SetCapabilities</span>
    </a>
</h3>

```csharp
void SetCapabilities(uint caps)
```

| Argument | Type | Description |
| --- | --- | --- |
| caps | uint | Supported capabilities |

**Inform window of supported capabilities**

This request informs the window of the capabilities supported by the
window manager. If the window manager, for example, ignores requests to
be maximized from the window it should not tell the window that it
supports the maximize capability.

The window might use this information to, for example, only show a
maximize button if the window manager supports the maximize capability.

The window manager client should use this request to set capabilities
for all new windows. If this request is never made, the compositor will
inform windows that all capabilities are supported.

This request modifies window management state and may only be made as
part of a manage sequence, see the river_window_manager_v1 description.

<h3 class="decleration request" title="InformMaximized request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowv1_informmaximized" id="riverwindowv1_informmaximized">
        <span class="codicon codicon-symbol-method method"></span>
        RiverWindowV1.<span class="method">InformMaximized</span>
    </a>
</h3>

```csharp
void InformMaximized()
```


**Inform the window that it is maximized**

Inform the window that it is maximized. The window might use this
information to adapt the style of its client-side window decorations for
example.

The window manager remains responsible for handling the position and
dimensions of the window while it is maximized.

This request modifies window management state and may only be made as
part of a manage sequence, see the river_window_manager_v1 description.

<h3 class="decleration request" title="InformUnmaximized request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowv1_informunmaximized" id="riverwindowv1_informunmaximized">
        <span class="codicon codicon-symbol-method method"></span>
        RiverWindowV1.<span class="method">InformUnmaximized</span>
    </a>
</h3>

```csharp
void InformUnmaximized()
```


**Inform the window that it is unmaximized**

Inform the window that it is unmaximized. The window might use this
information to adapt the style of its client-side window decorations for
example.

This request modifies window management state and may only be made as
part of a manage sequence, see the river_window_manager_v1 description.

<h3 class="decleration request" title="InformFullscreen request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowv1_informfullscreen" id="riverwindowv1_informfullscreen">
        <span class="codicon codicon-symbol-method method"></span>
        RiverWindowV1.<span class="method">InformFullscreen</span>
    </a>
</h3>

```csharp
void InformFullscreen()
```


**Inform the window that it is fullscreen**

Inform the window that it is fullscreen. The window might use this
information to adapt the style of its client-side window decorations for
example.

This request does not affect the size/position of the window or cause it
to become the only window rendered, see the river_window_v1.fullscreen
and exit_fullscreen requests for that.

This request modifies window management state and may only be made as
part of a manage sequence, see the river_window_manager_v1 description.

<h3 class="decleration request" title="InformNotFullscreen request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowv1_informnotfullscreen" id="riverwindowv1_informnotfullscreen">
        <span class="codicon codicon-symbol-method method"></span>
        RiverWindowV1.<span class="method">InformNotFullscreen</span>
    </a>
</h3>

```csharp
void InformNotFullscreen()
```


**Inform the window that it is not fullscreen**

Inform the window that it is not fullscreen. The window might use this
information to adapt the style of its client-side window decorations for
example.

This request does not affect the size/position of the window or cause it
to become the only window rendered, see the river_window_v1.fullscreen
and exit_fullscreen requests for that.

This request modifies window management state and may only be made as
part of a manage sequence, see the river_window_manager_v1 description.

<h3 class="decleration request" title="Fullscreen request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowv1_fullscreen" id="riverwindowv1_fullscreen">
        <span class="codicon codicon-symbol-method method"></span>
        RiverWindowV1.<span class="method">Fullscreen</span>
    </a>
</h3>

```csharp
void Fullscreen(RiverOutputV1 output)
```

| Argument | Type | Description |
| --- | --- | --- |
| output | object | Fullscreen output |

**Make the window fullscreen**

Make the window fullscreen on the given output. If multiple windows are
fullscreen on the same output at the same time only the "top" window in
rendering order shall be displayed.

All river_shell_surface_v1 objects above the top fullscreen window in
the rendering order will continue to be rendered.

The compositor will handle the position and dimensions of the window
while it is fullscreen. The set_position and propose_dimensions requests
shall not affect the current position and dimensions of a fullscreen
window.

When a fullscreen request is made, the server must send a dimensions
event in response as soon as possible. It may not be possible to send a
dimensions event in the very next render sequence if, for example, the
window takes too long to respond. In this case, the server will send the
dimensions event in a future render sequence.

The compositor will clip window content, decoration surfaces, and
borders to the given output's dimensions while the window is fullscreen.
The effects of set_clip_box and set_content_clip_box are ignored while
the window is fullscreen.

If the output on which a window is currently fullscreen is removed, the
windowing state is modified as if there were an exit_fullscreen request
made in the same manage sequence as the river_output_v1.removed event.

This request does not inform the window that it is fullscreen, see the
river_window_v1.inform_fullscreen and inform_not_fullscreen requests.

This request modifies window management state and may only be made as
part of a manage sequence, see the river_window_manager_v1 description.

<h3 class="decleration request" title="ExitFullscreen request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowv1_exitfullscreen" id="riverwindowv1_exitfullscreen">
        <span class="codicon codicon-symbol-method method"></span>
        RiverWindowV1.<span class="method">ExitFullscreen</span>
    </a>
</h3>

```csharp
void ExitFullscreen()
```


**Make the window not fullscreen**

Make the window not fullscreen.

The position and dimensions are undefined after this request is made
until a manage sequence in which the window manager makes the
propose_dimensions and set_position requests is completed.

The window manager should make propose_dimensions and set_position
requests in the same manage sequence as the exit_fullscreen request for
frame perfection.

This request does not inform the window that it is fullscreen, see the
river_window_v1.inform_fullscreen and inform_not_fullscreen requests.

This request modifies window management state and may only be made as
part of a manage sequence, see the river_window_manager_v1 description.

<h3 class="decleration request" title="SetClipBox request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowv1_setclipbox" id="riverwindowv1_setclipbox">
        <span class="codicon codicon-symbol-method method"></span>
        RiverWindowV1.<span class="method">SetClipBox</span>
    </a>
    <span class="pill">since 2</span>
</h3>

```csharp
void SetClipBox(int x, int y, int width, int height)
```

| Argument | Type | Description |
| --- | --- | --- |
| x | int | X relative to top left window corner |
| y | int | Y relative to top left window corner |
| width | int | Clip box width |
| height | int | Clip box height |

**Clip the window to a given box**

Clip the window, including borders and decoration surfaces, to the box
specified by the x, y, width, and height arguments. The x/y position of
the box is relative to the top left corner of the window.

The width and height arguments must be greater than or equal to 0.

Setting a clip box with 0 width or height disables clipping.

The clip box is ignored while the window is fullscreen.

Both set_clip_box and set_content_clip_box may be enabled simultaneously.

This request modifies rendering state and may only be made as part of a
render sequence, see the river_window_manager_v1 description.

<h3 class="decleration request" title="SetContentClipBox request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowv1_setcontentclipbox" id="riverwindowv1_setcontentclipbox">
        <span class="codicon codicon-symbol-method method"></span>
        RiverWindowV1.<span class="method">SetContentClipBox</span>
    </a>
    <span class="pill">since 3</span>
</h3>

```csharp
void SetContentClipBox(int x, int y, int width, int height)
```

| Argument | Type | Description |
| --- | --- | --- |
| x | int | X relative to top left window corner |
| y | int | Y relative to top left window corner |
| width | int | Clip box width |
| height | int | Clip box height |

**Clip the window content to a given box**

Clip the content of the window, excluding borders and decoration
surfaces, to the box specified by the x, y, width, and height arguments.
The x/y position of the box is relative to the top left corner of the
window.

Borders drawn by the compositor (see set_borders) are placed around the
intersection of the window content (as defined by the dimensions event)
and the content clip box when content clipping is enabled.

The width and height arguments must be greater than or equal to 0.

Setting a box with 0 width or height disables content clipping.

The content clip box is ignored while the window is fullscreen.

Both set_clip_box and set_content_clip_box may be enabled simultaneously.

This request modifies rendering state and may only be made as part of a
render sequence, see the river_window_manager_v1 description.

<h3 class="decleration request" title="SetDimensionBounds request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverwindowv1_setdimensionbounds" id="riverwindowv1_setdimensionbounds">
        <span class="codicon codicon-symbol-method method"></span>
        RiverWindowV1.<span class="method">SetDimensionBounds</span>
    </a>
    <span class="pill">since 4</span>
</h3>

```csharp
void SetDimensionBounds(int maxWidth, int maxHeight)
```

| Argument | Type | Description |
| --- | --- | --- |
| max_width | int | Maximum width |
| max_height | int | Maximum height |

**Recommend maximum dimensions to the window**

Recommend that the window keep its dimensions within a given
maximum width/height. This recommendation is only a hint and the window
may ignore it.

Setting the width and height to 0 indicates that there are no bounds
and is equivalent to having never made this request.

Setting width or height to a negative value is a protocol error.

The server should communicate this hint to an xdg_toplevel window with
the xdg_toplevel.configure_bounds event for example.

This request modifies window management state and may only be made as
part of a manage sequence, see the river_window_manager_v1 description.

<h3 class="decleration event" title="Closed event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverwindowv1_closed" id="onriverwindowv1_closed">
        <span class="codicon codicon-symbol-event event"></span>
        RiverWindowV1.<span class="event">OnClosed</span>
    </a>
</h3>

```csharp
void ClosedHandler()
```


**The window has been closed**

The window has been closed by the server, perhaps due to an
xdg_toplevel.close request or similar.

The server will send no further events on this object and ignore any
request other than river_window_v1.destroy made after this event is
sent. The client should destroy this object with the
river_window_v1.destroy request to free up resources.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

<h3 class="decleration event" title="DimensionsHint event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverwindowv1_dimensionshint" id="onriverwindowv1_dimensionshint">
        <span class="codicon codicon-symbol-event event"></span>
        RiverWindowV1.<span class="event">OnDimensionsHint</span>
    </a>
</h3>

```csharp
void DimensionsHintHandler(int minWidth, int minHeight, int maxWidth, int maxHeight)
```

| Argument | Type | Description |
| --- | --- | --- |
| min_width | int | Minimum width |
| min_height | int | Minimum height |
| max_width | int | Maximum width |
| max_height | int | Maximum height |

**The window's preferred min/max dimensions**

This event informs the window manager of the window's preferred min/max
dimensions. These preferences are a hint, and the window manager is free
to propose dimensions outside of these bounds.

All min/max width/height values must be strictly greater than or equal
to 0. A value of 0 indicates that the window has no preference for that
value.

The min_width/min_height must be strictly less than or equal to the
max_width/max_height.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

<h3 class="decleration event" title="Dimensions event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverwindowv1_dimensions" id="onriverwindowv1_dimensions">
        <span class="codicon codicon-symbol-event event"></span>
        RiverWindowV1.<span class="event">OnDimensions</span>
    </a>
</h3>

```csharp
void DimensionsHandler(int width, int height)
```

| Argument | Type | Description |
| --- | --- | --- |
| width | int | Window content width |
| height | int | Window content height |

**Window dimensions**

This event indicates the dimensions of the window in the compositor's
logical coordinate space. The width and height must be strictly greater
than zero.

Note that the dimensions of a river_window_v1 refer to the dimensions of
the window content and are unaffected by the presence of borders or
decoration surfaces.

This event is sent as part of a render sequence before the render_start
event.

It may be sent due to a propose_dimensions or fullscreen request in a
previous manage sequence or because a window independently decides to
change its dimensions.

The window will not be displayed until the first dimensions event is
received and the render sequence is finished.

<h3 class="decleration event" title="AppId event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverwindowv1_appid" id="onriverwindowv1_appid">
        <span class="codicon codicon-symbol-event event"></span>
        RiverWindowV1.<span class="event">OnAppId</span>
    </a>
</h3>

```csharp
void AppIdHandler(string? appId)
```

| Argument | Type | Description |
| --- | --- | --- |
| app_id | string | Window application ID |

**The window set an application ID**

The window set an application ID.

The app_id argument will be null if the window has never set an
application ID or if the window cleared its application ID. (Xwayland
windows may do this for example, though xdg-toplevels may not.)

This event will be followed by a manage_start event after all other new
state has been sent by the server.

<h3 class="decleration event" title="Title event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverwindowv1_title" id="onriverwindowv1_title">
        <span class="codicon codicon-symbol-event event"></span>
        RiverWindowV1.<span class="event">OnTitle</span>
    </a>
</h3>

```csharp
void TitleHandler(string? title)
```

| Argument | Type | Description |
| --- | --- | --- |
| title | string | Window title |

**The window set a title**

The window set a title.

The title argument will be null if the window has never set a title or
if the window cleared its title. (Xwayland windows may do this for
example, though xdg-toplevels may not.)

This event will be followed by a manage_start event after all other new
state has been sent by the server.

<h3 class="decleration event" title="Parent event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverwindowv1_parent" id="onriverwindowv1_parent">
        <span class="codicon codicon-symbol-event event"></span>
        RiverWindowV1.<span class="event">OnParent</span>
    </a>
</h3>

```csharp
void ParentHandler(RiverWindowV1? parent)
```

| Argument | Type | Description |
| --- | --- | --- |
| parent | object | Parent window, if any |

**The window set a parent**

The window set a parent window. If this event is never received or if
the parent argument is null then the window has no parent.

A surface with a parent set might be a dialog, file picker, or similar
for the parent window.

Child windows should generally be rendered directly above their parent.

The compositor must guarantee that there are no loops in the window
tree: a parent must not be the descendant of one of its children.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

<h3 class="decleration event" title="DecorationHint event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverwindowv1_decorationhint" id="onriverwindowv1_decorationhint">
        <span class="codicon codicon-symbol-event event"></span>
        RiverWindowV1.<span class="event">OnDecorationHint</span>
    </a>
</h3>

```csharp
void DecorationHintHandler(uint hint)
```

| Argument | Type | Description |
| --- | --- | --- |
| hint | uint | Decoration hint |

**Supported/preferred decoration style**

Information from the window about the supported and preferred client
side/server side decoration options.

This event may be sent multiple times over the lifetime of the window if
the window changes its preferences.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

<h3 class="decleration event" title="PointerMoveRequested event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverwindowv1_pointermoverequested" id="onriverwindowv1_pointermoverequested">
        <span class="codicon codicon-symbol-event event"></span>
        RiverWindowV1.<span class="event">OnPointerMoveRequested</span>
    </a>
</h3>

```csharp
void PointerMoveRequestedHandler(RiverSeatV1 seat)
```

| Argument | Type | Description |
| --- | --- | --- |
| seat | object | Requested seat |

**Window requested interactive pointer move**

This event informs the window manager that the window has requested to
be interactively moved using the pointer. The seat argument indicates the
seat for the move.

The xdg-shell protocol for example allows windows to request that an
interactive move be started, perhaps when a client-side rendered
titlebar is dragged.

The window manager may use the river_seat_v1.op_start_pointer request to
interactively move the window or ignore this event entirely.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

<h3 class="decleration event" title="PointerResizeRequested event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverwindowv1_pointerresizerequested" id="onriverwindowv1_pointerresizerequested">
        <span class="codicon codicon-symbol-event event"></span>
        RiverWindowV1.<span class="event">OnPointerResizeRequested</span>
    </a>
</h3>

```csharp
void PointerResizeRequestedHandler(RiverSeatV1 seat, uint edges)
```

| Argument | Type | Description |
| --- | --- | --- |
| seat | object | Requested seat |
| edges | uint | Requested edges |

**Window requested interactive pointer resize**

This event informs the window manager that the window has requested to
be interactively resized using the pointer. The seat argument indicates
the seat for the resize.

The edges argument indicates which edges the window has requested to be
resized from. The edges argument will never be none and will never have
both top and bottom or both left and right edges set.

The xdg-shell protocol for example allows windows to request that an
interactive resize be started, perhaps when the corner of client-side
rendered decorations is dragged.

The window manager may use the river_seat_v1.op_start_pointer request to
interactively resize the window or ignore this event entirely.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

<h3 class="decleration event" title="ShowWindowMenuRequested event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverwindowv1_showwindowmenurequested" id="onriverwindowv1_showwindowmenurequested">
        <span class="codicon codicon-symbol-event event"></span>
        RiverWindowV1.<span class="event">OnShowWindowMenuRequested</span>
    </a>
</h3>

```csharp
void ShowWindowMenuRequestedHandler(int x, int y)
```

| Argument | Type | Description |
| --- | --- | --- |
| x | int | X offset from top left corner |
| y | int | Y offset from top left corner |

**Window requested that the window menu be shown**

The xdg-shell protocol for example allows windows to request that a
window menu be shown, for example when the user right clicks on client
side window decorations.

A window menu might include options to maximize or minimize the window.

The window manager is free to ignore this request and decide what the
window menu contains if it does choose to show one.

The x and y arguments indicate where the window requested that the
window menu be shown.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

<h3 class="decleration event" title="MaximizeRequested event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverwindowv1_maximizerequested" id="onriverwindowv1_maximizerequested">
        <span class="codicon codicon-symbol-event event"></span>
        RiverWindowV1.<span class="event">OnMaximizeRequested</span>
    </a>
</h3>

```csharp
void MaximizeRequestedHandler()
```


**The window requested to be maximized**

The xdg-shell protocol for example allows windows to request to be
maximized.

The window manager is free to honor this request using
river_window_v1.inform_maximize or ignore it.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

<h3 class="decleration event" title="UnmaximizeRequested event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverwindowv1_unmaximizerequested" id="onriverwindowv1_unmaximizerequested">
        <span class="codicon codicon-symbol-event event"></span>
        RiverWindowV1.<span class="event">OnUnmaximizeRequested</span>
    </a>
</h3>

```csharp
void UnmaximizeRequestedHandler()
```


**The window requested to be unmaximized**

The xdg-shell protocol for example allows windows to request to be
unmaximized.

The window manager is free to honor this request using
river_window_v1.inform_unmaximized or ignore it.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

<h3 class="decleration event" title="FullscreenRequested event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverwindowv1_fullscreenrequested" id="onriverwindowv1_fullscreenrequested">
        <span class="codicon codicon-symbol-event event"></span>
        RiverWindowV1.<span class="event">OnFullscreenRequested</span>
    </a>
</h3>

```csharp
void FullscreenRequestedHandler(RiverOutputV1? output)
```

| Argument | Type | Description |
| --- | --- | --- |
| output | object | Fullscreen output requested |

**The window requested to be fullscreen**

The xdg-shell protocol for example allows windows to request that they
be made fullscreen and allows them to provide an optional output hint.

If the output argument is null, the window has no preference and the
window manager should choose an output.

The window manager is free to honor this request using
river_window_v1.fullscreen or ignore it.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

<h3 class="decleration event" title="ExitFullscreenRequested event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverwindowv1_exitfullscreenrequested" id="onriverwindowv1_exitfullscreenrequested">
        <span class="codicon codicon-symbol-event event"></span>
        RiverWindowV1.<span class="event">OnExitFullscreenRequested</span>
    </a>
</h3>

```csharp
void ExitFullscreenRequestedHandler()
```


**The window requested to exit fullscreen**

The xdg-shell protocol for example allows windows to request to exit
fullscreen.

The window manager is free to honor this request using
river_window_v1.exit_fullscreen or ignore it.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

<h3 class="decleration event" title="MinimizeRequested event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverwindowv1_minimizerequested" id="onriverwindowv1_minimizerequested">
        <span class="codicon codicon-symbol-event event"></span>
        RiverWindowV1.<span class="event">OnMinimizeRequested</span>
    </a>
</h3>

```csharp
void MinimizeRequestedHandler()
```


**The window requested to be minimized**

The xdg-shell protocol for example allows windows to request to be
minimized.

The window manager is free to ignore this request, hide the window, or
do whatever else it chooses.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

<h3 class="decleration event" title="UnreliablePid event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverwindowv1_unreliablepid" id="onriverwindowv1_unreliablepid">
        <span class="codicon codicon-symbol-event event"></span>
        RiverWindowV1.<span class="event">OnUnreliablePid</span>
    </a>
    <span class="pill">since 2</span>
</h3>

```csharp
void UnreliablePidHandler(int unreliablePid)
```

| Argument | Type | Description |
| --- | --- | --- |
| unreliable_pid | int | Unreliable PID |

**Unreliable PID of the window's creator**

This event gives an unreliable PID of the process that created the
window. Obtaining this information is inherently racy due to PID reuse.
Therefore, this PID must not be used for anything security sensitive.

Note also that a single process may create multiple windows, so there is
not necessarily a 1-to-1 mapping from PID to window. Multiple windows
may have the same PID.

This event is sent once when the river_window_v1 is created and never
sent again.

<h3 class="decleration event" title="PresentationHint event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverwindowv1_presentationhint" id="onriverwindowv1_presentationhint">
        <span class="codicon codicon-symbol-event event"></span>
        RiverWindowV1.<span class="event">OnPresentationHint</span>
    </a>
    <span class="pill">since 4</span>
</h3>

```csharp
void PresentationHintHandler(uint hint)
```

| Argument | Type | Description |
| --- | --- | --- |
| hint | uint | Presentation hint |

**Presentation hint set by the window**

This event communicates the window's preferred presentation mode.

This event will be followed by a render_start event after all other new
state has been sent by the server.

<h3 class="decleration event" title="Identifier event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverwindowv1_identifier" id="onriverwindowv1_identifier">
        <span class="codicon codicon-symbol-event event"></span>
        RiverWindowV1.<span class="event">OnIdentifier</span>
    </a>
    <span class="pill">since 4</span>
</h3>

```csharp
void IdentifierHandler(string identifier)
```

| Argument | Type | Description |
| --- | --- | --- |
| identifier | string | Unique identifier |

**Unique window identifier**

The identifier is a string that contains up to 32 printable ASCII bytes.
The identifier must not be an empty string.

It is compositor policy how the identifier is generated, but the following
properties must be upheld:

1. The identifier must uniquely identify the window. Two windows must not
share the same identifier.

2. The identifier must not be reused. This avoids races around window
creation/destruction when identifiers are used in out-of-band IPC.

If the compositor implements the ext-foreign-toplevel-list-v1 protocol,
the river_window_v1.identifier event must match the corresponding
ext_foreign_toplevel_handle_v1.identifier event.

This event is sent once when the river_window_v1 is created and never
sent again.

<h3 class="decleration enum" title="Error enum">
    <a href="#/Protocols/River/river-window-management-v1/?id=error" id="error">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverWindowV1.<span class="enum">Error</span>
    </a>
</h3>

```csharp
public enum Error
```

| Value | Integer | Description |
| --- | --- | --- |
| NodeExists | 0 | Window already has a node object |
| InvalidDimensions | 1 | Proposed dimensions out of bounds |
| InvalidBorder | 2 | Invalid arg to set_borders |
| InvalidClipBox | 3 | Invalid arg to set_clip_box |
<h3 class="decleration enum" title="DecorationHint enum">
    <a href="#/Protocols/River/river-window-management-v1/?id=decorationhint" id="decorationhint">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverWindowV1.<span class="enum">DecorationHint</span>
    </a>
</h3>

```csharp
public enum DecorationHint
```

| Value | Integer | Description |
| --- | --- | --- |
| OnlySupportsCsd | 0 | Only supports client side decoration |
| PrefersCsd | 1 | Client side decoration preferred, both CSD and SSD supported |
| PrefersSsd | 2 | Server side decoration preferred, both CSD and SSD supported |
| NoPreference | 3 | No preference, both CSD and SSD supported |
<h3 class="decleration enum" title="Edges enum">
    <a href="#/Protocols/River/river-window-management-v1/?id=edges" id="edges">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverWindowV1.<span class="enum">Edges</span>
    </a>
</h3>

```csharp
public enum EdgesFlag
```

| Value | Integer | Description |
| --- | --- | --- |
| None | 0 |  |
| Top | 1 |  |
| Bottom | 2 |  |
| Left | 4 |  |
| Right | 8 |  |
<h3 class="decleration enum" title="Capabilities enum">
    <a href="#/Protocols/River/river-window-management-v1/?id=capabilities" id="capabilities">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverWindowV1.<span class="enum">Capabilities</span>
    </a>
</h3>

```csharp
public enum CapabilitiesFlag
```

| Value | Integer | Description |
| --- | --- | --- |
| WindowMenu | 1 |  |
| Maximize | 2 |  |
| Fullscreen | 4 |  |
| Minimize | 8 |  |
<h2 class="decleration interface">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverdecorationv1" id="riverdecorationv1">
        <span class="codicon codicon-symbol-interface"></span>
        RiverDecorationV1
    </a>
    <span class="pill">version 4</span>
</h2>

A window decoration


The rendering order of windows with decorations is follows:

1. Decorations created with get_decoration_below at the bottom
2. Window content
3. Borders configured with river_window_v1.set_borders
4. Decorations created with get_decoration_above at the top

The relative ordering of decoration surfaces above/below a window is
undefined by this protocol and left up to the compositor.


<h3 class="decleration request" title="Destroy request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverdecorationv1_destroy" id="riverdecorationv1_destroy">
        <span class="codicon codicon-symbol-method method"></span>
        RiverDecorationV1.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the decoration object**

This request indicates that the client will no longer use the decoration
object and that it may be safely destroyed.

<h3 class="decleration request" title="SetOffset request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverdecorationv1_setoffset" id="riverdecorationv1_setoffset">
        <span class="codicon codicon-symbol-method method"></span>
        RiverDecorationV1.<span class="method">SetOffset</span>
    </a>
</h3>

```csharp
void SetOffset(int x, int y)
```

| Argument | Type | Description |
| --- | --- | --- |
| x | int | X relative to top left window corner |
| y | int | Y relative to top left window corner |

**Set offset from the window's top left corner**

This request sets the offset of the decoration surface from the top left
corner of the window.

If this request is never sent, the x and y offsets are undefined by this
protocol and left up to the compositor.

This request modifies rendering state and may only be made as part of a
render sequence, see the river_window_manager_v1 description.

<h3 class="decleration request" title="SyncNextCommit request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverdecorationv1_syncnextcommit" id="riverdecorationv1_syncnextcommit">
        <span class="codicon codicon-symbol-method method"></span>
        RiverDecorationV1.<span class="method">SyncNextCommit</span>
    </a>
</h3>

```csharp
void SyncNextCommit()
```


**Sync next commit with other rendering state**

Synchronize application of the next wl_surface.commit request on the
decoration surface with rest of the state atomically applied with the
next river_window_manager_v1.render_finish request.

The client must make a wl_surface.commit request on the decoration
surface after this request and before the render_finish request, failure
to do so is a protocol error.

This request modifies rendering state and may only be made as part of a
render sequence, see the river_window_manager_v1 description.

<h3 class="decleration enum" title="Error enum">
    <a href="#/Protocols/River/river-window-management-v1/?id=error" id="error">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverDecorationV1.<span class="enum">Error</span>
    </a>
</h3>

```csharp
public enum Error
```

| Value | Integer | Description |
| --- | --- | --- |
| NoCommit | 0 | Failed to commit the surface before the window manager commit |
<h2 class="decleration interface">
    <a href="#/Protocols/River/river-window-management-v1/?id=rivershellsurfacev1" id="rivershellsurfacev1">
        <span class="codicon codicon-symbol-interface"></span>
        RiverShellSurfaceV1
    </a>
    <span class="pill">version 4</span>
</h2>

A surface for window manager UI


The window manager might use a shell surface to display a status bar,
background image, desktop notifications, launcher, desktop menu, or
whatever else it wants.


<h3 class="decleration request" title="Destroy request">
    <a href="#/Protocols/River/river-window-management-v1/?id=rivershellsurfacev1_destroy" id="rivershellsurfacev1_destroy">
        <span class="codicon codicon-symbol-method method"></span>
        RiverShellSurfaceV1.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the shell surface object**

This request indicates that the client will no longer use the shell
surface object and that it may be safely destroyed.

<h3 class="decleration request" title="GetNode request">
    <a href="#/Protocols/River/river-window-management-v1/?id=rivershellsurfacev1_getnode" id="rivershellsurfacev1_getnode">
        <span class="codicon codicon-symbol-method method"></span>
        RiverShellSurfaceV1.<span class="method">GetNode</span>
    </a>
</h3>

```csharp
RiverNodeV1 GetNode()
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id | New node |

**Get the shell surface's render list node**

Get the node in the render list corresponding to the shell surface.

It is a protocol error to make this request more than once for a single
shell surface.

<h3 class="decleration request" title="SyncNextCommit request">
    <a href="#/Protocols/River/river-window-management-v1/?id=rivershellsurfacev1_syncnextcommit" id="rivershellsurfacev1_syncnextcommit">
        <span class="codicon codicon-symbol-method method"></span>
        RiverShellSurfaceV1.<span class="method">SyncNextCommit</span>
    </a>
</h3>

```csharp
void SyncNextCommit()
```


**Sync next surface commit to window manager commit**

Synchronize application of the next wl_surface.commit request on the
shell surface with rest of the rendering state atomically applied with
the next river_window_manager_v1.render_finish request.

The client must make a wl_surface.commit request on the shell surface
after this request and before the render_finish request, failure to do
so is a protocol error.

This request modifies rendering state and may only be made as part of a
render sequence, see the river_window_manager_v1 description.

<h3 class="decleration enum" title="Error enum">
    <a href="#/Protocols/River/river-window-management-v1/?id=error" id="error">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverShellSurfaceV1.<span class="enum">Error</span>
    </a>
</h3>

```csharp
public enum Error
```

| Value | Integer | Description |
| --- | --- | --- |
| NodeExists | 0 | Shell surface already has a node object |
| NoCommit | 1 | Failed to commit the surface before the window manager commit |
<h2 class="decleration interface">
    <a href="#/Protocols/River/river-window-management-v1/?id=rivernodev1" id="rivernodev1">
        <span class="codicon codicon-symbol-interface"></span>
        RiverNodeV1
    </a>
    <span class="pill">version 4</span>
</h2>

A node in the render list


The render list is a list of nodes that determines the rendering order of
the compositor. Nodes may correspond to windows or shell surfaces. The
relative ordering of nodes may be changed with the place_above and
place_below requests, changing the rendering order.

The initial position of a node in the render list is undefined, the window
manager client must use the place_above or place_below request to
guarantee a specific rendering order.


<h3 class="decleration request" title="Destroy request">
    <a href="#/Protocols/River/river-window-management-v1/?id=rivernodev1_destroy" id="rivernodev1_destroy">
        <span class="codicon codicon-symbol-method method"></span>
        RiverNodeV1.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the decoration object**

This request indicates that the client will no longer use the node
object and that it may be safely destroyed.

<h3 class="decleration request" title="SetPosition request">
    <a href="#/Protocols/River/river-window-management-v1/?id=rivernodev1_setposition" id="rivernodev1_setposition">
        <span class="codicon codicon-symbol-method method"></span>
        RiverNodeV1.<span class="method">SetPosition</span>
    </a>
</h3>

```csharp
void SetPosition(int x, int y)
```

| Argument | Type | Description |
| --- | --- | --- |
| x | int | Global x coordinate |
| y | int | Global y coordinate |

**Set absolute position of the node**

Set the absolute position of the node in the compositor's logical
coordinate space. The x and y coordinates may be positive or negative.

Note that the position of a river_window_v1 refers to the position of
the window content and is unaffected by the presence of borders or
decoration surfaces.

If this request is never sent, the position of the node is undefined by
this protocol and left up to the compositor.

This request modifies rendering state and may only be made as part of a
render sequence, see the river_window_manager_v1 description.

<h3 class="decleration request" title="PlaceTop request">
    <a href="#/Protocols/River/river-window-management-v1/?id=rivernodev1_placetop" id="rivernodev1_placetop">
        <span class="codicon codicon-symbol-method method"></span>
        RiverNodeV1.<span class="method">PlaceTop</span>
    </a>
</h3>

```csharp
void PlaceTop()
```


**Place node above all other nodes**

This request places the node above all other nodes in the compositor's
render list.

This request modifies rendering state and may only be made as part of a
render sequence, see the river_window_manager_v1 description.

<h3 class="decleration request" title="PlaceBottom request">
    <a href="#/Protocols/River/river-window-management-v1/?id=rivernodev1_placebottom" id="rivernodev1_placebottom">
        <span class="codicon codicon-symbol-method method"></span>
        RiverNodeV1.<span class="method">PlaceBottom</span>
    </a>
</h3>

```csharp
void PlaceBottom()
```


**Place node below all other nodes**

This request places the node below all other nodes in the compositor's
render list.

This request modifies rendering state and may only be made as part of a
render sequence, see the river_window_manager_v1 description.

<h3 class="decleration request" title="PlaceAbove request">
    <a href="#/Protocols/River/river-window-management-v1/?id=rivernodev1_placeabove" id="rivernodev1_placeabove">
        <span class="codicon codicon-symbol-method method"></span>
        RiverNodeV1.<span class="method">PlaceAbove</span>
    </a>
</h3>

```csharp
void PlaceAbove(RiverNodeV1 other)
```

| Argument | Type | Description |
| --- | --- | --- |
| other | object | Other node |

**Place node above another node**

This request places the node directly above another node in the
compositor's render list.

Attempting to place a node above itself has no effect.

This request modifies rendering state and may only be made as part of a
render sequence, see the river_window_manager_v1 description.

<h3 class="decleration request" title="PlaceBelow request">
    <a href="#/Protocols/River/river-window-management-v1/?id=rivernodev1_placebelow" id="rivernodev1_placebelow">
        <span class="codicon codicon-symbol-method method"></span>
        RiverNodeV1.<span class="method">PlaceBelow</span>
    </a>
</h3>

```csharp
void PlaceBelow(RiverNodeV1 other)
```

| Argument | Type | Description |
| --- | --- | --- |
| other | object | Other node |

**Place node below another node**

This request places the node directly below another node in the
compositor's render list.

Attempting to place a node below itself has no effect.

This request modifies rendering state and may only be made as part of a
render sequence, see the river_window_manager_v1 description.

<h2 class="decleration interface">
    <a href="#/Protocols/River/river-window-management-v1/?id=riveroutputv1" id="riveroutputv1">
        <span class="codicon codicon-symbol-interface"></span>
        RiverOutputV1
    </a>
    <span class="pill">version 4</span>
</h2>

A logical output


An area in the compositor's logical coordinate space that should be
treated as a single output for window management purposes. This area may
correspond to a single physical output or multiple physical outputs in the
case of mirroring or tiled monitors depending on the hardware and
compositor configuration.


<h3 class="decleration request" title="Destroy request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riveroutputv1_destroy" id="riveroutputv1_destroy">
        <span class="codicon codicon-symbol-method method"></span>
        RiverOutputV1.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the output object**

This request indicates that the client will no longer use the output
object and that it may be safely destroyed.

This request should be made after the river_output_v1.removed event is
received to complete destruction of the output.

<h3 class="decleration request" title="SetPresentationMode request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riveroutputv1_setpresentationmode" id="riveroutputv1_setpresentationmode">
        <span class="codicon codicon-symbol-method method"></span>
        RiverOutputV1.<span class="method">SetPresentationMode</span>
    </a>
    <span class="pill">since 4</span>
</h3>

```csharp
void SetPresentationMode(uint mode)
```

| Argument | Type | Description |
| --- | --- | --- |
| mode | uint | Preferred presentation mode |

**Set the preferred presentation mode**

Set the preferred presentation mode of the output. The compositor should
always respect the preference of the window manager if possible. If this
request is never made, the preferred presentation mode is vsync.

This request modifies rendering state and may only be made as part of a
render sequence, see the river_window_manager_v1 description.

<h3 class="decleration event" title="Removed event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriveroutputv1_removed" id="onriveroutputv1_removed">
        <span class="codicon codicon-symbol-event event"></span>
        RiverOutputV1.<span class="event">OnRemoved</span>
    </a>
</h3>

```csharp
void RemovedHandler()
```


**The output is removed**

This event indicates that the logical output is no longer conceptually
part of window management space.

The server will send no further events on this object and ignore any
request (other than river_output_v1.destroy) made after this event is
sent. The client should destroy this object with the
river_output_v1.destroy request to free up resources.

This event may be sent because a corresponding physical output has been
physically unplugged or because some output configuration has changed.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

<h3 class="decleration event" title="WlOutput event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriveroutputv1_wloutput" id="onriveroutputv1_wloutput">
        <span class="codicon codicon-symbol-event event"></span>
        RiverOutputV1.<span class="event">OnWlOutput</span>
    </a>
</h3>

```csharp
void WlOutputHandler(uint name)
```

| Argument | Type | Description |
| --- | --- | --- |
| name | uint | Name of the wl_output global |

**Corresponding wl_output**

The wl_output object corresponding to the river_output_v1. The argument
is the global name of the wl_output advertised with wl_registry.global.

It is guaranteed that the corresponding wl_output is advertised before
this event is sent.

This event is sent exactly once. The wl_output associated with a
river_output_v1 cannot change. It is guaranteed that there is a 1-to-1
mapping between wl_output and river_output_v1 objects.

The global_remove event for the corresponding wl_output may be sent
before the river_output_v1.remove event. This is due to the fact that
river_output_v1 state changes are synced to the river window management
manage sequence while changes to globals are not.

Rationale: The window manager may need information provided by the
wl_output interface such as the name/description. It also may need the
wl_output object to start screencopy for example.

<h3 class="decleration event" title="Position event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriveroutputv1_position" id="onriveroutputv1_position">
        <span class="codicon codicon-symbol-event event"></span>
        RiverOutputV1.<span class="event">OnPosition</span>
    </a>
</h3>

```csharp
void PositionHandler(int x, int y)
```

| Argument | Type | Description |
| --- | --- | --- |
| x | int | Global x coordinate |
| y | int | Global y coordinate |

**Output position**

This event indicates the position of the output in the compositor's
logical coordinate space. The x and y coordinates may be positive or
negative.

This event is sent once when the river_output_v1 is created and again
whenever the position changes.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

The server must guarantee that the position and dimensions events do not
cause the areas of multiple logical outputs to overlap when the
corresponding manage_start event is received.

<h3 class="decleration event" title="Dimensions event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriveroutputv1_dimensions" id="onriveroutputv1_dimensions">
        <span class="codicon codicon-symbol-event event"></span>
        RiverOutputV1.<span class="event">OnDimensions</span>
    </a>
</h3>

```csharp
void DimensionsHandler(int width, int height)
```

| Argument | Type | Description |
| --- | --- | --- |
| width | int | Output width |
| height | int | Output height |

**Output dimensions**

This event indicates the dimensions of the output in the compositor's
logical coordinate space. The width and height will always be strictly
greater than zero.

This event is sent once when the river_output_v1 is created and again
whenever the dimensions change.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

The server must guarantee that the position and dimensions events do not
cause the areas of multiple logical outputs to overlap when the
corresponding manage_start event is received.

<h3 class="decleration enum" title="Error enum">
    <a href="#/Protocols/River/river-window-management-v1/?id=error" id="error">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverOutputV1.<span class="enum">Error</span>
    </a>
</h3>

```csharp
public enum Error
```

| Value | Integer | Description |
| --- | --- | --- |
| InvalidPresentationMode | 0 | Invalid presentation mode enum value |
<h3 class="decleration enum" title="PresentationMode enum">
    <a href="#/Protocols/River/river-window-management-v1/?id=presentationmode" id="presentationmode">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverOutputV1.<span class="enum">PresentationMode</span>
    </a>
</h3>

```csharp
public enum PresentationMode
```

| Value | Integer | Description |
| --- | --- | --- |
| Vsync | 0 |  |
| Async | 1 |  |
<h2 class="decleration interface">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverseatv1" id="riverseatv1">
        <span class="codicon codicon-symbol-interface"></span>
        RiverSeatV1
    </a>
    <span class="pill">version 4</span>
</h2>

A window management seat


This object represents a single user's collection of input devices. It
allows the window manager to route keyboard input to windows, get
high-level information about pointer input, define pointer bindings, etc.

For keyboard bindings, see the river-xkb-bindings-v1 protocol.

Since version 4: The cursor surface/shape set by the window manager on the
wl_pointer of this seat is used when no client has pointer focus, for
example during a pointer operation. Since the window manager is allowed to
set cursor surface/shape even when it does not have pointer focus, the
compositor must ignore the serial argument of wl_pointer.set_cursor and
wp_cursor_shape_device_v1.set_shape requests made by the window manager.

The most recent cursor surface/shape set by the window manager is
remembered by the compositor and restored whenever no client has pointer
focus. If the window manager never sets a cursor surface/shape, the
"default" shape is used.


<h3 class="decleration request" title="Destroy request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverseatv1_destroy" id="riverseatv1_destroy">
        <span class="codicon codicon-symbol-method method"></span>
        RiverSeatV1.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the seat object**

This request indicates that the client will no longer use the seat
object and that it may be safely destroyed.

This request should be made after the river_seat_v1.removed event is
received to complete destruction of the seat.

<h3 class="decleration request" title="FocusWindow request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverseatv1_focuswindow" id="riverseatv1_focuswindow">
        <span class="codicon codicon-symbol-method method"></span>
        RiverSeatV1.<span class="method">FocusWindow</span>
    </a>
</h3>

```csharp
void FocusWindow(RiverWindowV1 window)
```

| Argument | Type | Description |
| --- | --- | --- |
| window | object | Window to focus |

**Give keyboard focus to a window**

Request that the compositor send keyboard input to the given window.

This request modifies window management state and may only be made as
part of a manage sequence, see the river_window_manager_v1 description.

<h3 class="decleration request" title="FocusShellSurface request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverseatv1_focusshellsurface" id="riverseatv1_focusshellsurface">
        <span class="codicon codicon-symbol-method method"></span>
        RiverSeatV1.<span class="method">FocusShellSurface</span>
    </a>
</h3>

```csharp
void FocusShellSurface(RiverShellSurfaceV1 shellSurface)
```

| Argument | Type | Description |
| --- | --- | --- |
| shell_surface | object | Shell surface to focus |

**Give keyboard focus to a shell_surface**

Request that the compositor send keyboard input to the given shell
surface.

This request modifies window management state and may only be made as
part of a manage sequence, see the river_window_manager_v1 description.

<h3 class="decleration request" title="ClearFocus request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverseatv1_clearfocus" id="riverseatv1_clearfocus">
        <span class="codicon codicon-symbol-method method"></span>
        RiverSeatV1.<span class="method">ClearFocus</span>
    </a>
</h3>

```csharp
void ClearFocus()
```


**Clear keyboard focus**

Request that the compositor not send keyboard input to any client.

This request modifies window management state and may only be made as
part of a manage sequence, see the river_window_manager_v1 description.

<h3 class="decleration request" title="OpStartPointer request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverseatv1_opstartpointer" id="riverseatv1_opstartpointer">
        <span class="codicon codicon-symbol-method method"></span>
        RiverSeatV1.<span class="method">OpStartPointer</span>
    </a>
</h3>

```csharp
void OpStartPointer()
```


**Start an interactive pointer operation**

Start an interactive pointer operation. During the operation, op_delta
events will be sent based on pointer input.

When all pointer buttons are released, the op_release event is sent.

The pointer operation continues until the op_end request is made during
a manage sequence and that manage sequence is finished.

The window manager may use this operation to implement interactive
move/resize of windows by setting the position of windows and proposing
dimensions based off of the op_delta events.

This request is ignored if an operation is already in progress.

The compositor must ensure that no client has pointer focus from this
seat during the pointer operation. This means that the window manager
has control over the pointer's cursor surface/shape during the pointer
operation. See the river_seat_v1 description.

This request modifies window management state and may only be made as
part of a manage sequence, see the river_window_manager_v1 description.

<h3 class="decleration request" title="OpEnd request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverseatv1_opend" id="riverseatv1_opend">
        <span class="codicon codicon-symbol-method method"></span>
        RiverSeatV1.<span class="method">OpEnd</span>
    </a>
</h3>

```csharp
void OpEnd()
```


**End an interactive operation**

End an interactive operation.

This request is ignored if there is no operation in progress.

This request modifies window management state and may only be made as
part of a manage sequence, see the river_window_manager_v1 description.

<h3 class="decleration request" title="GetPointerBinding request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverseatv1_getpointerbinding" id="riverseatv1_getpointerbinding">
        <span class="codicon codicon-symbol-method method"></span>
        RiverSeatV1.<span class="method">GetPointerBinding</span>
    </a>
</h3>

```csharp
RiverPointerBindingV1 GetPointerBinding(uint button, uint modifiers)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id | New pointer binding |
| button | uint | A Linux input event code |
| modifiers | uint | Keyboard modifiers |

**Define a new pointer binding**

Define a pointer binding in terms of a pointer button, keyboard
modifiers, and other configurable properties.

The button argument is a Linux input event code defined in the
linux/input-event-codes.h header file (e.g. BTN_RIGHT).

The new pointer binding is not enabled until initial configuration is
completed and the enable request is made during a manage sequence.

<h3 class="decleration request" title="SetXcursorTheme request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverseatv1_setxcursortheme" id="riverseatv1_setxcursortheme">
        <span class="codicon codicon-symbol-method method"></span>
        RiverSeatV1.<span class="method">SetXcursorTheme</span>
    </a>
    <span class="pill">since 2</span>
</h3>

```csharp
void SetXcursorTheme(string name, uint size)
```

| Argument | Type | Description |
| --- | --- | --- |
| name | string | Xcursor theme name |
| size | uint | Cursor size |

**Set the xcursor theme for the seat**

Set the XCursor theme for the seat. This theme is used for cursors
rendered by the compositor, but not necessarily for cursors rendered by
clients.

Note: The window manager may also wish to set the XCURSOR_THEME and
XCURSOR_SIZE environment variable for programs it starts.

<h3 class="decleration request" title="PointerWarp request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverseatv1_pointerwarp" id="riverseatv1_pointerwarp">
        <span class="codicon codicon-symbol-method method"></span>
        RiverSeatV1.<span class="method">PointerWarp</span>
    </a>
    <span class="pill">since 3</span>
</h3>

```csharp
void PointerWarp(int x, int y)
```

| Argument | Type | Description |
| --- | --- | --- |
| x | int | Global x coordinate |
| y | int | Global y coordinate |

**Warp the pointer to a given position**

Warp the pointer to the given position in the compositor's logical
coordinate space.

If the given position is outside the bounds of all outputs, the pointer
will be warped to the closest point inside an output instead.

This request modifies window management state and may only be made as
part of a manage sequence, see the river_window_manager_v1 description.

<h3 class="decleration event" title="Removed event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverseatv1_removed" id="onriverseatv1_removed">
        <span class="codicon codicon-symbol-event event"></span>
        RiverSeatV1.<span class="event">OnRemoved</span>
    </a>
</h3>

```csharp
void RemovedHandler()
```


**The seat is removed**

This event indicates that seat is no longer in use and should be
destroyed.

The server will send no further events on this object and ignore any
request (other than river_seat_v1.destroy) made after this event is
sent.  The client should destroy this object with the
river_seat_v1.destroy request to free up resources.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

<h3 class="decleration event" title="WlSeat event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverseatv1_wlseat" id="onriverseatv1_wlseat">
        <span class="codicon codicon-symbol-event event"></span>
        RiverSeatV1.<span class="event">OnWlSeat</span>
    </a>
</h3>

```csharp
void WlSeatHandler(uint name)
```

| Argument | Type | Description |
| --- | --- | --- |
| name | uint | Name of the wl_seat global |

**Corresponding wl_seat**

The wl_seat object corresponding to the river_seat_v1. The argument is
the global name of the wl_seat advertised with wl_registry.global.

It is guaranteed that the corresponding wl_seat is advertised before
this event is sent.

This event is sent exactly once. The wl_seat associated with a
river_seat_v1 cannot change. It is guaranteed that there is a 1-to-1
mapping between wl_seat and river_seat_v1 objects.

The global_remove event for the corresponding wl_seat may be sent before
the river_seat_v1.remove event. This is due to the fact that
river_seat_v1 state changes are synced to the river window management
manage sequence while changes to globals are not.

Rationale: The window manager may want to trigger window management
state changes based on normal input events received by its shell
surfaces for example.

<h3 class="decleration event" title="PointerEnter event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverseatv1_pointerenter" id="onriverseatv1_pointerenter">
        <span class="codicon codicon-symbol-event event"></span>
        RiverSeatV1.<span class="event">OnPointerEnter</span>
    </a>
</h3>

```csharp
void PointerEnterHandler(RiverWindowV1 window)
```

| Argument | Type | Description |
| --- | --- | --- |
| window | object | Window entered |

**Pointer entered a window**

The seat's pointer entered the given window's area.

The area of a window is defined to include the area defined by the
window dimensions, borders configured using river_window_v1.set_borders,
and the input regions of decoration surfaces. In particular, it does not
include input regions of surfaces belonging to the window that extend
outside the window dimensions.

The pointer of a seat may only enter a single window at a time. When the
pointer moves between windows, the pointer_leave event for the old
window must be sent before the pointer_enter event for the new window.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

<h3 class="decleration event" title="PointerLeave event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverseatv1_pointerleave" id="onriverseatv1_pointerleave">
        <span class="codicon codicon-symbol-event event"></span>
        RiverSeatV1.<span class="event">OnPointerLeave</span>
    </a>
</h3>

```csharp
void PointerLeaveHandler()
```


**Pointer left the entered window**

The seat's pointer left the window for which pointer_enter was most
recently sent. See pointer_enter for details.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

<h3 class="decleration event" title="WindowInteraction event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverseatv1_windowinteraction" id="onriverseatv1_windowinteraction">
        <span class="codicon codicon-symbol-event event"></span>
        RiverSeatV1.<span class="event">OnWindowInteraction</span>
    </a>
</h3>

```csharp
void WindowInteractionHandler(RiverWindowV1 window)
```

| Argument | Type | Description |
| --- | --- | --- |
| window | object | Window interacted with |

**A window has been interacted with**

A window has been interacted with beyond the pointer merely passing over
it. This event might be sent due to a pointer button press or due to a
touch/tablet tool interaction with the window.

There are no guarantees regarding how this event is sent in relation to
the pointer_enter and pointer_leave events as the interaction may use
touch or tablet tool input.

Rationale: this event gives window managers necessary information to
determine when to send keyboard focus, raise a window that already has
keyboard focus, etc. Rather than expose all pointer, touch, and tablet
events to window managers, a policy over mechanism approach is taken.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

<h3 class="decleration event" title="ShellSurfaceInteraction event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverseatv1_shellsurfaceinteraction" id="onriverseatv1_shellsurfaceinteraction">
        <span class="codicon codicon-symbol-event event"></span>
        RiverSeatV1.<span class="event">OnShellSurfaceInteraction</span>
    </a>
</h3>

```csharp
void ShellSurfaceInteractionHandler(RiverShellSurfaceV1 shellSurface)
```

| Argument | Type | Description |
| --- | --- | --- |
| shell_surface | object | Shell surface interacted with |

**A shell surface has been interacted with**

A shell surface has been interacted with beyond the pointer merely
passing over it. This event might be sent due to a pointer button press
or due to a touch/tablet tool interaction with the shell_surface.

There are no guarantees regarding how this event is sent in relation to
the pointer_enter and pointer_leave events as the interaction may use
touch or tablet tool input.

Rationale: While the shell surface does receive all wl_pointer,
wl_touch, etc. input events for the surface directly, these events do
not necessarily trigger a manage sequence and therefore do not allow the
window manager to update focus or perform other actions in response to
the input in a race-free way.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

<h3 class="decleration event" title="OpDelta event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverseatv1_opdelta" id="onriverseatv1_opdelta">
        <span class="codicon codicon-symbol-event event"></span>
        RiverSeatV1.<span class="event">OnOpDelta</span>
    </a>
</h3>

```csharp
void OpDeltaHandler(int dx, int dy)
```

| Argument | Type | Description |
| --- | --- | --- |
| dx | int | Total change in x |
| dy | int | Total change in y |

**Total cumulative motion since op start**

This event indicates the total change in position since the start of the
operation of the pointer/touch point/etc.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

<h3 class="decleration event" title="OpRelease event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverseatv1_oprelease" id="onriverseatv1_oprelease">
        <span class="codicon codicon-symbol-event event"></span>
        RiverSeatV1.<span class="event">OnOpRelease</span>
    </a>
</h3>

```csharp
void OpReleaseHandler()
```


**Operation input has been released**

The input driving the current interactive operation has been released.
For a pointer op for example, all pointer buttons have been released.

Depending on the op type, op_delta events may continue to be sent until
the op is ended with the op_end request.

This event is sent at most once during an interactive operation.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

<h3 class="decleration event" title="PointerPosition event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverseatv1_pointerposition" id="onriverseatv1_pointerposition">
        <span class="codicon codicon-symbol-event event"></span>
        RiverSeatV1.<span class="event">OnPointerPosition</span>
    </a>
    <span class="pill">since 2</span>
</h3>

```csharp
void PointerPositionHandler(int x, int y)
```

| Argument | Type | Description |
| --- | --- | --- |
| x | int | Global x coordinate |
| y | int | Global y coordinate |

**The current position of the pointer**

The current position of the pointer in the compositor's logical
coordinate space.

This state is special in that a change in pointer position alone must
not cause the compositor to start a manage sequence.

Assuming the seat has a pointer, this event must be sent in every manage
sequence unless there is no change in x/y position since the last time this
event was sent.

<h3 class="decleration enum" title="Modifiers enum">
    <a href="#/Protocols/River/river-window-management-v1/?id=modifiers" id="modifiers">
        <span class="codicon codicon-symbol-enum enum"></span>
        RiverSeatV1.<span class="enum">Modifiers</span>
    </a>
</h3>

```csharp
public enum ModifiersFlag
```

A set of keyboard modifiers


This enum is used to describe the keyboard modifiers that must be held
down to trigger a key binding or pointer binding.

Note that river and wlroots use the values 2 and 16 for capslock and
numlock internally. It doesn't make sense to use locked modifiers for
bindings however so these values are not included in this enum.


| Value | Integer | Description |
| --- | --- | --- |
| None | 0 |  |
| Shift | 1 |  |
| Ctrl | 4 |  |
| Mod1 | 8 | Commonly called alt |
| Mod3 | 32 |  |
| Mod4 | 64 | Commonly called super or logo |
| Mod5 | 128 |  |
<h2 class="decleration interface">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverpointerbindingv1" id="riverpointerbindingv1">
        <span class="codicon codicon-symbol-interface"></span>
        RiverPointerBindingV1
    </a>
    <span class="pill">version 4</span>
</h2>

Configure a pointer binding, receive trigger events


This object allows the window manager to configure a pointer binding and
receive events when the binding is triggered.

The new pointer binding is not enabled until the enable request is made
during a manage sequence.

Normally, all pointer button events are sent to the surface with pointer
focus by the compositor. Pointer button events that trigger a pointer
binding are not sent to the surface with pointer focus.

If multiple pointer bindings would be triggered by a single physical
pointer event on the compositor side, it is compositor policy which
pointer binding(s) will receive press/release events or if all of the
matched pointer bindings receive press/release events.


<h3 class="decleration request" title="Destroy request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverpointerbindingv1_destroy" id="riverpointerbindingv1_destroy">
        <span class="codicon codicon-symbol-method method"></span>
        RiverPointerBindingV1.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the pointer binding object**

This request indicates that the client will no longer use the pointer
binding object and that it may be safely destroyed.

<h3 class="decleration request" title="Enable request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverpointerbindingv1_enable" id="riverpointerbindingv1_enable">
        <span class="codicon codicon-symbol-method method"></span>
        RiverPointerBindingV1.<span class="method">Enable</span>
    </a>
</h3>

```csharp
void Enable()
```


**Enable the pointer binding**

This request should be made after all initial configuration has been
completed and the window manager wishes the pointer binding to be able
to be triggered.

This request modifies window management state and may only be made as
part of a manage sequence, see the river_window_manager_v1 description.

<h3 class="decleration request" title="Disable request">
    <a href="#/Protocols/River/river-window-management-v1/?id=riverpointerbindingv1_disable" id="riverpointerbindingv1_disable">
        <span class="codicon codicon-symbol-method method"></span>
        RiverPointerBindingV1.<span class="method">Disable</span>
    </a>
</h3>

```csharp
void Disable()
```


**Disable the pointer binding**

This request may be used to temporarily disable the pointer binding. It
may be later re-enabled with the enable request.

This request modifies window management state and may only be made as
part of a manage sequence, see the river_window_manager_v1 description.

<h3 class="decleration event" title="Pressed event">
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverpointerbindingv1_pressed" id="onriverpointerbindingv1_pressed">
        <span class="codicon codicon-symbol-event event"></span>
        RiverPointerBindingV1.<span class="event">OnPressed</span>
    </a>
</h3>

```csharp
void PressedHandler()
```


**The bound pointer button has been pressed**

This event indicates that the pointer button triggering the binding has
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
    <a href="#/Protocols/River/river-window-management-v1/?id=onriverpointerbindingv1_released" id="onriverpointerbindingv1_released">
        <span class="codicon codicon-symbol-event event"></span>
        RiverPointerBindingV1.<span class="event">OnReleased</span>
    </a>
</h3>

```csharp
void ReleasedHandler()
```


**The bound pointer button has been released**

This event indicates that the pointer button triggering the binding has
been released.

Releasing the modifiers for the binding without releasing the pointer
button does not trigger the release event. This event is sent when the
pointer button is released, even if the modifiers have changed since the
pressed event.

This event will be followed by a manage_start event after all other new
state has been sent by the server.

The compositor should wait for the manage sequence to complete before
processing further input events. This allows the window manager client
to, for example, modify key bindings and keyboard focus without racing
against future input events. The window manager should of course respond
as soon as possible as the capacity of the compositor to buffer incoming
input events is finite.

