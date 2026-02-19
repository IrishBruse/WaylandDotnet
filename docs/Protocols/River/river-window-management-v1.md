# River Window Management

##### [WaylandDotnet](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet) ![](../../assets/arrow.svg ':class=breadcrumb-arrow') [River](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/River)

---

<h2 class='decleration interface'>
    <a href='?id=RiverWindowManagerV1' id='RiverWindowManagerV1'>
        <span class='codicon codicon-symbol-interface'></span>
        RiverWindowManagerV1
    </a>
    <span class='pill'>version 3</span>
</h2>

Window manager global interface


This global interface should only be advertised to the window manager
process. Only one window management client may be active at a time. The
compositor should use the unavailable event if necessary to enforce this.

There are two disjoint categories of state managed by this protocol:

Window management state influences the communication between the server
and individual window clients (e.g. xdg_toplevels). Window management
state includes window dimensions, fullscreen state, keyboard focus,
keyboard bindings, and more.

Rendering state only affects the rendered output of the compositor and
does not influence communication between the server and individual window
clients. Rendering state includes the position and rendering order of
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


<h3 class="decleration request">
    <a href="?id=RiverWindowManagerV1_Stop" id="RiverWindowManagerV1_Stop">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverWindowManagerV1_Destroy" id="RiverWindowManagerV1_Destroy">
        <span class='codicon codicon-symbol-method method'></span>
        RiverWindowManagerV1.<span class="method">Destroy</span>
    </a>
    <span class='pill destructor'>Type: destructor</span>
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

<h3 class="decleration request">
    <a href="?id=RiverWindowManagerV1_ManageFinish" id="RiverWindowManagerV1_ManageFinish">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverWindowManagerV1_ManageDirty" id="RiverWindowManagerV1_ManageDirty">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverWindowManagerV1_RenderFinish" id="RiverWindowManagerV1_RenderFinish">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverWindowManagerV1_GetShellSurface" id="RiverWindowManagerV1_GetShellSurface">
        <span class='codicon codicon-symbol-method method'></span>
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

<h2 class='decleration interface'>
    <a href='?id=RiverWindowV1' id='RiverWindowV1'>
        <span class='codicon codicon-symbol-interface'></span>
        RiverWindowV1
    </a>
    <span class='pill'>version 3</span>
</h2>

A logical window


This represents a logical window. For example, a window may correspond to
an xdg_toplevel or Xwayland window.

A newly created window will not be displayed until the window manager
makes a propose_dimensions or fullscreen request as part of a manage
sequence, the server replies with a dimensions event as part of a render
sequence, and that render sequence is finished.


<h3 class="decleration request">
    <a href="?id=RiverWindowV1_Destroy" id="RiverWindowV1_Destroy">
        <span class='codicon codicon-symbol-method method'></span>
        RiverWindowV1.<span class="method">Destroy</span>
    </a>
    <span class='pill destructor'>Type: destructor</span>
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

<h3 class="decleration request">
    <a href="?id=RiverWindowV1_Close" id="RiverWindowV1_Close">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverWindowV1_GetNode" id="RiverWindowV1_GetNode">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverWindowV1_ProposeDimensions" id="RiverWindowV1_ProposeDimensions">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverWindowV1_Hide" id="RiverWindowV1_Hide">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverWindowV1_Show" id="RiverWindowV1_Show">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverWindowV1_UseCsd" id="RiverWindowV1_UseCsd">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverWindowV1_UseSsd" id="RiverWindowV1_UseSsd">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverWindowV1_SetBorders" id="RiverWindowV1_SetBorders">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverWindowV1_SetTiled" id="RiverWindowV1_SetTiled">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverWindowV1_GetDecorationAbove" id="RiverWindowV1_GetDecorationAbove">
        <span class='codicon codicon-symbol-method method'></span>
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

**Create a decoration surface above the window**

Create a decoration surface and assign the river_decoration_v1 role to
the surface. The created decoration is placed above the window in
rendering order, see the description of river_decoration_v1.

Providing a wl_surface which already has a role or already has a buffer
attached or committed is a protocol error.

<h3 class="decleration request">
    <a href="?id=RiverWindowV1_GetDecorationBelow" id="RiverWindowV1_GetDecorationBelow">
        <span class='codicon codicon-symbol-method method'></span>
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

**Create a decoration surface below the window**

Create a decoration surface and assign the river_decoration_v1 role to
the surface. The created decoration is placed below the window in
rendering order, see the description of river_decoration_v1.

Providing a wl_surface which already has a role or already has a buffer
attached or committed is a protocol error.

<h3 class="decleration request">
    <a href="?id=RiverWindowV1_InformResizeStart" id="RiverWindowV1_InformResizeStart">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverWindowV1_InformResizeEnd" id="RiverWindowV1_InformResizeEnd">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverWindowV1_SetCapabilities" id="RiverWindowV1_SetCapabilities">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverWindowV1_InformMaximized" id="RiverWindowV1_InformMaximized">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverWindowV1_InformUnmaximized" id="RiverWindowV1_InformUnmaximized">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverWindowV1_InformFullscreen" id="RiverWindowV1_InformFullscreen">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverWindowV1_InformNotFullscreen" id="RiverWindowV1_InformNotFullscreen">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverWindowV1_Fullscreen" id="RiverWindowV1_Fullscreen">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverWindowV1_ExitFullscreen" id="RiverWindowV1_ExitFullscreen">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverWindowV1_SetClipBox" id="RiverWindowV1_SetClipBox">
        <span class='codicon codicon-symbol-method method'></span>
        RiverWindowV1.<span class="method">SetClipBox</span>
    </a>
    <span class='pill'>since 2</span>
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

<h3 class="decleration request">
    <a href="?id=RiverWindowV1_SetContentClipBox" id="RiverWindowV1_SetContentClipBox">
        <span class='codicon codicon-symbol-method method'></span>
        RiverWindowV1.<span class="method">SetContentClipBox</span>
    </a>
    <span class='pill'>since 3</span>
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

<h2 class='decleration interface'>
    <a href='?id=RiverDecorationV1' id='RiverDecorationV1'>
        <span class='codicon codicon-symbol-interface'></span>
        RiverDecorationV1
    </a>
    <span class='pill'>version 3</span>
</h2>

A window decoration


The rendering order of windows with decorations is follows:

1. Decorations created with get_decoration_below at the bottom
2. Window content
3. Borders configured with river_window_v1.set_borders
4. Decorations created with get_decoration_above at the top

The relative ordering of decoration surfaces above/below a window is
undefined by this protocol and left up to the compositor.


<h3 class="decleration request">
    <a href="?id=RiverDecorationV1_Destroy" id="RiverDecorationV1_Destroy">
        <span class='codicon codicon-symbol-method method'></span>
        RiverDecorationV1.<span class="method">Destroy</span>
    </a>
    <span class='pill destructor'>Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the decoration object**

This request indicates that the client will no longer use the decoration
object and that it may be safely destroyed.

<h3 class="decleration request">
    <a href="?id=RiverDecorationV1_SetOffset" id="RiverDecorationV1_SetOffset">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverDecorationV1_SyncNextCommit" id="RiverDecorationV1_SyncNextCommit">
        <span class='codicon codicon-symbol-method method'></span>
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

<h2 class='decleration interface'>
    <a href='?id=RiverShellSurfaceV1' id='RiverShellSurfaceV1'>
        <span class='codicon codicon-symbol-interface'></span>
        RiverShellSurfaceV1
    </a>
    <span class='pill'>version 3</span>
</h2>

A surface for window manager UI


The window manager might use a shell surface to display a status bar,
background image, desktop notifications, launcher, desktop menu, or
whatever else it wants.


<h3 class="decleration request">
    <a href="?id=RiverShellSurfaceV1_Destroy" id="RiverShellSurfaceV1_Destroy">
        <span class='codicon codicon-symbol-method method'></span>
        RiverShellSurfaceV1.<span class="method">Destroy</span>
    </a>
    <span class='pill destructor'>Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the shell surface object**

This request indicates that the client will no longer use the shell
surface object and that it may be safely destroyed.

<h3 class="decleration request">
    <a href="?id=RiverShellSurfaceV1_GetNode" id="RiverShellSurfaceV1_GetNode">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverShellSurfaceV1_SyncNextCommit" id="RiverShellSurfaceV1_SyncNextCommit">
        <span class='codicon codicon-symbol-method method'></span>
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

<h2 class='decleration interface'>
    <a href='?id=RiverNodeV1' id='RiverNodeV1'>
        <span class='codicon codicon-symbol-interface'></span>
        RiverNodeV1
    </a>
    <span class='pill'>version 3</span>
</h2>

A node in the render list


The render list is a list of nodes that determines the rendering order of
the compositor. Nodes may correspond to windows or shell surfaces. The
relative ordering of nodes may be changed with the place_above and
place_below requests, changing the rendering order.

The initial position of a node in the render list is undefined, the window
manager client must use the place_above or place_below request to
guarantee a specific rendering order.


<h3 class="decleration request">
    <a href="?id=RiverNodeV1_Destroy" id="RiverNodeV1_Destroy">
        <span class='codicon codicon-symbol-method method'></span>
        RiverNodeV1.<span class="method">Destroy</span>
    </a>
    <span class='pill destructor'>Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the decoration object**

This request indicates that the client will no longer use the node
object and that it may be safely destroyed.

<h3 class="decleration request">
    <a href="?id=RiverNodeV1_SetPosition" id="RiverNodeV1_SetPosition">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverNodeV1_PlaceTop" id="RiverNodeV1_PlaceTop">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverNodeV1_PlaceBottom" id="RiverNodeV1_PlaceBottom">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverNodeV1_PlaceAbove" id="RiverNodeV1_PlaceAbove">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverNodeV1_PlaceBelow" id="RiverNodeV1_PlaceBelow">
        <span class='codicon codicon-symbol-method method'></span>
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

<h2 class='decleration interface'>
    <a href='?id=RiverOutputV1' id='RiverOutputV1'>
        <span class='codicon codicon-symbol-interface'></span>
        RiverOutputV1
    </a>
    <span class='pill'>version 3</span>
</h2>

A logical output


An area in the compositor's logical coordinate space that should be
treated as a single output for window management purposes. This area may
correspond to a single physical output or multiple physical outputs in the
case of mirroring or tiled monitors depending on the hardware and
compositor configuration.


<h3 class="decleration request">
    <a href="?id=RiverOutputV1_Destroy" id="RiverOutputV1_Destroy">
        <span class='codicon codicon-symbol-method method'></span>
        RiverOutputV1.<span class="method">Destroy</span>
    </a>
    <span class='pill destructor'>Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the output object**

This request indicates that the client will no longer use the output
object and that it may be safely destroyed.

This request should be made after the river_output_v1.removed event is
received to complete destruction of the output.

<h2 class='decleration interface'>
    <a href='?id=RiverSeatV1' id='RiverSeatV1'>
        <span class='codicon codicon-symbol-interface'></span>
        RiverSeatV1
    </a>
    <span class='pill'>version 3</span>
</h2>

A window management seat


This object represents a single user's collection of input devices. It
allows the window manager to route keyboard input to windows, get
high-level information about pointer input, define keyboard and pointer
bindings, etc.

TODO:
- touch input
- tablet input


<h3 class="decleration request">
    <a href="?id=RiverSeatV1_Destroy" id="RiverSeatV1_Destroy">
        <span class='codicon codicon-symbol-method method'></span>
        RiverSeatV1.<span class="method">Destroy</span>
    </a>
    <span class='pill destructor'>Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the seat object**

This request indicates that the client will no longer use the seat
object and that it may be safely destroyed.

This request should be made after the river_seat_v1.removed event is
received to complete destruction of the seat.

<h3 class="decleration request">
    <a href="?id=RiverSeatV1_FocusWindow" id="RiverSeatV1_FocusWindow">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverSeatV1_FocusShellSurface" id="RiverSeatV1_FocusShellSurface">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverSeatV1_ClearFocus" id="RiverSeatV1_ClearFocus">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverSeatV1_OpStartPointer" id="RiverSeatV1_OpStartPointer">
        <span class='codicon codicon-symbol-method method'></span>
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

This request modifies window management state and may only be made as
part of a manage sequence, see the river_window_manager_v1 description.

<h3 class="decleration request">
    <a href="?id=RiverSeatV1_OpEnd" id="RiverSeatV1_OpEnd">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverSeatV1_GetPointerBinding" id="RiverSeatV1_GetPointerBinding">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverSeatV1_SetXcursorTheme" id="RiverSeatV1_SetXcursorTheme">
        <span class='codicon codicon-symbol-method method'></span>
        RiverSeatV1.<span class="method">SetXcursorTheme</span>
    </a>
    <span class='pill'>since 2</span>
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

<h3 class="decleration request">
    <a href="?id=RiverSeatV1_PointerWarp" id="RiverSeatV1_PointerWarp">
        <span class='codicon codicon-symbol-method method'></span>
        RiverSeatV1.<span class="method">PointerWarp</span>
    </a>
    <span class='pill'>since 3</span>
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

<h2 class='decleration interface'>
    <a href='?id=RiverPointerBindingV1' id='RiverPointerBindingV1'>
        <span class='codicon codicon-symbol-interface'></span>
        RiverPointerBindingV1
    </a>
    <span class='pill'>version 3</span>
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


<h3 class="decleration request">
    <a href="?id=RiverPointerBindingV1_Destroy" id="RiverPointerBindingV1_Destroy">
        <span class='codicon codicon-symbol-method method'></span>
        RiverPointerBindingV1.<span class="method">Destroy</span>
    </a>
    <span class='pill destructor'>Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the pointer binding object**

This request indicates that the client will no longer use the pointer
binding object and that it may be safely destroyed.

<h3 class="decleration request">
    <a href="?id=RiverPointerBindingV1_Enable" id="RiverPointerBindingV1_Enable">
        <span class='codicon codicon-symbol-method method'></span>
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

<h3 class="decleration request">
    <a href="?id=RiverPointerBindingV1_Disable" id="RiverPointerBindingV1_Disable">
        <span class='codicon codicon-symbol-method method'></span>
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

