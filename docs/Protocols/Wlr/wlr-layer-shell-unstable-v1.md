# Layer Shell

##### [WaylandDotnet](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet) ![](../../assets/arrow.svg ':class=breadcrumb-arrow') [Wlr](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/Wlr)

---

<h2 class="decleration interface">
    <a href="?id=ZwlrLayerShellV1" id="ZwlrLayerShellV1">
        <span class="codicon codicon-symbol-interface"></span>
        ZwlrLayerShellV1
    </a>
    <span class="pill">version 5</span>
</h2>

Create surfaces that are layers of the desktop


Clients can use this interface to assign the surface_layer role to
wl_surfaces. Such surfaces are assigned to a "layer" of the output and
rendered with a defined z-depth respective to each other. They may also be
anchored to the edges and corners of a screen and specify input handling
semantics. This interface should be suitable for the implementation of
many desktop shell components, and a broad number of other applications
that interact with the desktop.


<h3 class="decleration request">
    <a href="?id=ZwlrLayerShellV1_GetLayerSurface" id="ZwlrLayerShellV1_GetLayerSurface">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrLayerShellV1.<span class="method">GetLayerSurface</span>
    </a>
</h3>

```csharp
ZwlrLayerSurfaceV1 GetLayerSurface(WlSurface surface, WlOutput? output, uint layer, string _namespace)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id |  |
| surface | object |  |
| output | object |  |
| layer | uint | Layer to add this surface to |
| namespace | string | Namespace for the layer surface |

**Create a layer_surface from a surface**

Create a layer surface for an existing surface. This assigns the role of
layer_surface, or raises a protocol error if another role is already
assigned.

Creating a layer surface from a wl_surface which has a buffer attached
or committed is a client error, and any attempts by a client to attach
or manipulate a buffer prior to the first layer_surface.configure call
must also be treated as errors.

After creating a layer_surface object and setting it up, the client
must perform an initial commit without any buffer attached.
The compositor will reply with a layer_surface.configure event.
The client must acknowledge it and is then allowed to attach a buffer
to map the surface.

You may pass NULL for output to allow the compositor to decide which
output to use. Generally this will be the one that the user most
recently interacted with.

Clients can specify a namespace that defines the purpose of the layer
surface.

<h3 class="decleration request">
    <a href="?id=ZwlrLayerShellV1_Destroy" id="ZwlrLayerShellV1_Destroy">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrLayerShellV1.<span class="method">Destroy</span>
    </a>
    <span class="pill">since 3</span>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the layer_shell object**

This request indicates that the client will not use the layer_shell
object any more. Objects that have been created through this instance
are not affected.

<h2 class="decleration interface">
    <a href="?id=ZwlrLayerSurfaceV1" id="ZwlrLayerSurfaceV1">
        <span class="codicon codicon-symbol-interface"></span>
        ZwlrLayerSurfaceV1
    </a>
    <span class="pill">version 5</span>
</h2>

Layer metadata interface


An interface that may be implemented by a wl_surface, for surfaces that
are designed to be rendered as a layer of a stacked desktop-like
environment.

Layer surface state (layer, size, anchor, exclusive zone,
margin, interactivity) is double-buffered, and will be applied at the
time wl_surface.commit of the corresponding wl_surface is called.

Attaching a null buffer to a layer surface unmaps it.

Unmapping a layer_surface means that the surface cannot be shown by the
compositor until it is explicitly mapped again. The layer_surface
returns to the state it had right after layer_shell.get_layer_surface.
The client can re-map the surface by performing a commit without any
buffer attached, waiting for a configure event and handling it as usual.


<h3 class="decleration request">
    <a href="?id=ZwlrLayerSurfaceV1_SetSize" id="ZwlrLayerSurfaceV1_SetSize">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrLayerSurfaceV1.<span class="method">SetSize</span>
    </a>
</h3>

```csharp
void SetSize(uint width, uint height)
```

| Argument | Type | Description |
| --- | --- | --- |
| width | uint |  |
| height | uint |  |

**Sets the size of the surface**

Sets the size of the surface in surface-local coordinates. The
compositor will display the surface centered with respect to its
anchors.

If you pass 0 for either value, the compositor will assign it and
inform you of the assignment in the configure event. You must set your
anchor to opposite edges in the dimensions you omit; not doing so is a
protocol error. Both values are 0 by default.

Size is double-buffered, see wl_surface.commit.

<h3 class="decleration request">
    <a href="?id=ZwlrLayerSurfaceV1_SetAnchor" id="ZwlrLayerSurfaceV1_SetAnchor">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrLayerSurfaceV1.<span class="method">SetAnchor</span>
    </a>
</h3>

```csharp
void SetAnchor(uint anchor)
```

| Argument | Type | Description |
| --- | --- | --- |
| anchor | uint |  |

**Configures the anchor point of the surface**

Requests that the compositor anchor the surface to the specified edges
and corners. If two orthogonal edges are specified (e.g. 'top' and
'left'), then the anchor point will be the intersection of the edges
(e.g. the top left corner of the output); otherwise the anchor point
will be centered on that edge, or in the center if none is specified.

Anchor is double-buffered, see wl_surface.commit.

<h3 class="decleration request">
    <a href="?id=ZwlrLayerSurfaceV1_SetExclusiveZone" id="ZwlrLayerSurfaceV1_SetExclusiveZone">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrLayerSurfaceV1.<span class="method">SetExclusiveZone</span>
    </a>
</h3>

```csharp
void SetExclusiveZone(int zone)
```

| Argument | Type | Description |
| --- | --- | --- |
| zone | int |  |

**Configures the exclusive geometry of this surface**

Requests that the compositor avoids occluding an area with other
surfaces. The compositor's use of this information is
implementation-dependent - do not assume that this region will not
actually be occluded.

A positive value is only meaningful if the surface is anchored to one
edge or an edge and both perpendicular edges. If the surface is not
anchored, anchored to only two perpendicular edges (a corner), anchored
to only two parallel edges or anchored to all edges, a positive value
will be treated the same as zero.

A positive zone is the distance from the edge in surface-local
coordinates to consider exclusive.

Surfaces that do not wish to have an exclusive zone may instead specify
how they should interact with surfaces that do. If set to zero, the
surface indicates that it would like to be moved to avoid occluding
surfaces with a positive exclusive zone. If set to -1, the surface
indicates that it would not like to be moved to accommodate for other
surfaces, and the compositor should extend it all the way to the edges
it is anchored to.

For example, a panel might set its exclusive zone to 10, so that
maximized shell surfaces are not shown on top of it. A notification
might set its exclusive zone to 0, so that it is moved to avoid
occluding the panel, but shell surfaces are shown underneath it. A
wallpaper or lock screen might set their exclusive zone to -1, so that
they stretch below or over the panel.

The default value is 0.

Exclusive zone is double-buffered, see wl_surface.commit.

<h3 class="decleration request">
    <a href="?id=ZwlrLayerSurfaceV1_SetMargin" id="ZwlrLayerSurfaceV1_SetMargin">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrLayerSurfaceV1.<span class="method">SetMargin</span>
    </a>
</h3>

```csharp
void SetMargin(int top, int right, int bottom, int left)
```

| Argument | Type | Description |
| --- | --- | --- |
| top | int |  |
| right | int |  |
| bottom | int |  |
| left | int |  |

**Sets a margin from the anchor point**

Requests that the surface be placed some distance away from the anchor
point on the output, in surface-local coordinates. Setting this value
for edges you are not anchored to has no effect.

The exclusive zone includes the margin.

Margin is double-buffered, see wl_surface.commit.

<h3 class="decleration request">
    <a href="?id=ZwlrLayerSurfaceV1_SetKeyboardInteractivity" id="ZwlrLayerSurfaceV1_SetKeyboardInteractivity">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrLayerSurfaceV1.<span class="method">SetKeyboardInteractivity</span>
    </a>
</h3>

```csharp
void SetKeyboardInteractivity(uint keyboardInteractivity)
```

| Argument | Type | Description |
| --- | --- | --- |
| keyboard_interactivity | uint |  |

**Requests keyboard events**

Set how keyboard events are delivered to this surface. By default,
layer shell surfaces do not receive keyboard events; this request can
be used to change this.

This setting is inherited by child surfaces set by the get_popup
request.

Layer surfaces receive pointer, touch, and tablet events normally. If
you do not want to receive them, set the input region on your surface
to an empty region.

Keyboard interactivity is double-buffered, see wl_surface.commit.

<h3 class="decleration request">
    <a href="?id=ZwlrLayerSurfaceV1_GetPopup" id="ZwlrLayerSurfaceV1_GetPopup">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrLayerSurfaceV1.<span class="method">GetPopup</span>
    </a>
</h3>

```csharp
void GetPopup(XdgPopup popup)
```

| Argument | Type | Description |
| --- | --- | --- |
| popup | object |  |

**Assign this layer_surface as an xdg_popup parent**

This assigns an xdg_popup's parent to this layer_surface.  This popup
should have been created via xdg_surface::get_popup with the parent set
to NULL, and this request must be invoked before committing the popup's
initial state.

See the documentation of xdg_popup for more details about what an
xdg_popup is and how it is used.

<h3 class="decleration request">
    <a href="?id=ZwlrLayerSurfaceV1_AckConfigure" id="ZwlrLayerSurfaceV1_AckConfigure">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrLayerSurfaceV1.<span class="method">AckConfigure</span>
    </a>
</h3>

```csharp
void AckConfigure(uint serial)
```

| Argument | Type | Description |
| --- | --- | --- |
| serial | uint | The serial from the configure event |

**Ack a configure event**

When a configure event is received, if a client commits the
surface in response to the configure event, then the client
must make an ack_configure request sometime before the commit
request, passing along the serial of the configure event.

If the client receives multiple configure events before it
can respond to one, it only has to ack the last configure event.

A client is not required to commit immediately after sending
an ack_configure request - it may even ack_configure several times
before its next surface commit.

A client may send multiple ack_configure requests before committing, but
only the last request sent before a commit indicates which configure
event the client really is responding to.

<h3 class="decleration request">
    <a href="?id=ZwlrLayerSurfaceV1_Destroy" id="ZwlrLayerSurfaceV1_Destroy">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrLayerSurfaceV1.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the layer_surface**

This request destroys the layer surface.

<h3 class="decleration request">
    <a href="?id=ZwlrLayerSurfaceV1_SetLayer" id="ZwlrLayerSurfaceV1_SetLayer">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrLayerSurfaceV1.<span class="method">SetLayer</span>
    </a>
    <span class="pill">since 2</span>
</h3>

```csharp
void SetLayer(uint layer)
```

| Argument | Type | Description |
| --- | --- | --- |
| layer | uint | Layer to move this surface to |

**Change the layer of the surface**

Change the layer that the surface is rendered on.

Layer is double-buffered, see wl_surface.commit.

<h3 class="decleration request">
    <a href="?id=ZwlrLayerSurfaceV1_SetExclusiveEdge" id="ZwlrLayerSurfaceV1_SetExclusiveEdge">
        <span class="codicon codicon-symbol-method method"></span>
        ZwlrLayerSurfaceV1.<span class="method">SetExclusiveEdge</span>
    </a>
    <span class="pill">since 5</span>
</h3>

```csharp
void SetExclusiveEdge(uint edge)
```

| Argument | Type | Description |
| --- | --- | --- |
| edge | uint |  |

**Set the edge the exclusive zone will be applied to**

Requests an edge for the exclusive zone to apply. The exclusive
edge will be automatically deduced from anchor points when possible,
but when the surface is anchored to a corner, it will be necessary
to set it explicitly to disambiguate, as it is not possible to deduce
which one of the two corner edges should be used.

The edge must be one the surface is anchored to, otherwise the
invalid_exclusive_edge protocol error will be raised.

