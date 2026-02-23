# Viewporter

##### [WaylandDotnet](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet) ![](../../assets/arrow.svg ':class=breadcrumb-arrow') [Stable](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/Stable) ![](../../assets/arrow.svg ':class=breadcrumb-arrow') [Viewporter](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/Stable/viewporter/)

---

<h2 class="decleration interface">
    <a href="?id=WpViewporter" id="WpViewporter">
        <span class="codicon codicon-symbol-interface"></span>
        WpViewporter
    </a>
    <span class="pill">version 1</span>
</h2>

Surface cropping and scaling


The global interface exposing surface cropping and scaling
capabilities is used to instantiate an interface extension for a
wl_surface object. This extended interface will then allow
cropping and scaling the surface contents, effectively
disconnecting the direct relationship between the buffer and the
surface size.


<h3 class="decleration request" title="Destroy request">
    <a href="?id=WpViewporter_Destroy" id="WpViewporter_Destroy">
        <span class="codicon codicon-symbol-method method"></span>
        WpViewporter.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Unbind from the cropping and scaling interface**

Informs the server that the client will not be using this
protocol object anymore. This does not affect any other objects,
wp_viewport objects included.

<h3 class="decleration request" title="GetViewport request">
    <a href="?id=WpViewporter_GetViewport" id="WpViewporter_GetViewport">
        <span class="codicon codicon-symbol-method method"></span>
        WpViewporter.<span class="method">GetViewport</span>
    </a>
</h3>

```csharp
WpViewport GetViewport(WlSurface surface)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id | The new viewport interface id |
| surface | object | The surface |

**Extend surface interface for crop and scale**

Instantiate an interface extension for the given wl_surface to
crop and scale its content. If the given wl_surface already has
a wp_viewport object associated, the viewport_exists
protocol error is raised.

<h2 class="decleration interface">
    <a href="?id=WpViewport" id="WpViewport">
        <span class="codicon codicon-symbol-interface"></span>
        WpViewport
    </a>
    <span class="pill">version 1</span>
</h2>

Crop and scale interface to a wl_surface


An additional interface to a wl_surface object, which allows the
client to specify the cropping and scaling of the surface
contents.

This interface works with two concepts: the source rectangle (src_x,
src_y, src_width, src_height), and the destination size (dst_width,
dst_height). The contents of the source rectangle are scaled to the
destination size, and content outside the source rectangle is ignored.
This state is double-buffered, see wl_surface.commit.

The two parts of crop and scale state are independent: the source
rectangle, and the destination size. Initially both are unset, that
is, no scaling is applied. The whole of the current wl_buffer is
used as the source, and the surface size is as defined in
wl_surface.attach.

If the destination size is set, it causes the surface size to become
dst_width, dst_height. The source (rectangle) is scaled to exactly
this size. This overrides whatever the attached wl_buffer size is,
unless the wl_buffer is NULL. If the wl_buffer is NULL, the surface
has no content and therefore no size. Otherwise, the size is always
at least 1x1 in surface local coordinates.

If the source rectangle is set, it defines what area of the wl_buffer is
taken as the source. If the source rectangle is set and the destination
size is not set, then src_width and src_height must be integers, and the
surface size becomes the source rectangle size. This results in cropping
without scaling. If src_width or src_height are not integers and
destination size is not set, the bad_size protocol error is raised when
the surface state is applied.

The coordinate transformations from buffer pixel coordinates up to
the surface-local coordinates happen in the following order:
1. buffer_transform (wl_surface.set_buffer_transform)
2. buffer_scale (wl_surface.set_buffer_scale)
3. crop and scale (wp_viewport.set*)
This means, that the source rectangle coordinates of crop and scale
are given in the coordinates after the buffer transform and scale,
i.e. in the coordinates that would be the surface-local coordinates
if the crop and scale was not applied.

If src_x or src_y are negative, the bad_value protocol error is raised.
Otherwise, if the source rectangle is partially or completely outside of
the non-NULL wl_buffer, then the out_of_buffer protocol error is raised
when the surface state is applied. A NULL wl_buffer does not raise the
out_of_buffer error.

If the wl_surface associated with the wp_viewport is destroyed,
all wp_viewport requests except 'destroy' raise the protocol error
no_surface.

If the wp_viewport object is destroyed, the crop and scale
state is removed from the wl_surface. The change will be applied
on the next wl_surface.commit.


<h3 class="decleration request" title="Destroy request">
    <a href="?id=WpViewport_Destroy" id="WpViewport_Destroy">
        <span class="codicon codicon-symbol-method method"></span>
        WpViewport.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Remove scaling and cropping from the surface**

The associated wl_surface's crop and scale state is removed.
The change is applied on the next wl_surface.commit.

<h3 class="decleration request" title="SetSource request">
    <a href="?id=WpViewport_SetSource" id="WpViewport_SetSource">
        <span class="codicon codicon-symbol-method method"></span>
        WpViewport.<span class="method">SetSource</span>
    </a>
</h3>

```csharp
void SetSource(WlFixed x, WlFixed y, WlFixed width, WlFixed height)
```

| Argument | Type | Description |
| --- | --- | --- |
| x | fixed | Source rectangle x |
| y | fixed | Source rectangle y |
| width | fixed | Source rectangle width |
| height | fixed | Source rectangle height |

**Set the source rectangle for cropping**

Set the source rectangle of the associated wl_surface. See
wp_viewport for the description, and relation to the wl_buffer
size.

If all of x, y, width and height are -1.0, the source rectangle is
unset instead. Any other set of values where width or height are zero
or negative, or x or y are negative, raise the bad_value protocol
error.

The crop and scale state is double-buffered, see wl_surface.commit.

<h3 class="decleration request" title="SetDestination request">
    <a href="?id=WpViewport_SetDestination" id="WpViewport_SetDestination">
        <span class="codicon codicon-symbol-method method"></span>
        WpViewport.<span class="method">SetDestination</span>
    </a>
</h3>

```csharp
void SetDestination(int width, int height)
```

| Argument | Type | Description |
| --- | --- | --- |
| width | int | Surface width |
| height | int | Surface height |

**Set the surface size for scaling**

Set the destination size of the associated wl_surface. See
wp_viewport for the description, and relation to the wl_buffer
size.

If width is -1 and height is -1, the destination size is unset
instead. Any other pair of values for width and height that
contains zero or negative values raises the bad_value protocol
error.

The crop and scale state is double-buffered, see wl_surface.commit.

