# Wayland

##### [WaylandDotnet](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet) ![](../../assets/arrow.svg ':class=breadcrumb-arrow') [Core](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/Core) ![](../../assets/arrow.svg ':class=breadcrumb-arrow') [Wayland](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/Core/wayland/)

---

<h2 class="decleration interface">
    <a href="?id=WlDisplay" id="WlDisplay">
        <span class="codicon codicon-symbol-interface"></span>
        WlDisplay
    </a>
    <span class="pill">version 1</span>
</h2>

Core global object


The core global object.  This is a special singleton object.  It
is used for internal Wayland protocol features.


<h3 class="decleration request" title="Sync request">
    <a href="?id=WlDisplay_Sync" id="WlDisplay_Sync">
        <span class="codicon codicon-symbol-method method"></span>
        WlDisplay.<span class="method">Sync</span>
    </a>
</h3>

```csharp
WlCallback Sync()
```

| Argument | Type | Description |
| --- | --- | --- |
| callback | new_id | Callback object for the sync request |

**Asynchronous roundtrip**

The sync request asks the server to emit the 'done' event
on the returned wl_callback object.  Since requests are
handled in-order and events are delivered in-order, this can
be used as a barrier to ensure all previous requests and the
resulting events have been handled.

The object returned by this request will be destroyed by the
compositor after the callback is fired and as such the client must not
attempt to use it after that point.

The callback_data passed in the callback is undefined and should be ignored.

<h3 class="decleration request" title="GetRegistry request">
    <a href="?id=WlDisplay_GetRegistry" id="WlDisplay_GetRegistry">
        <span class="codicon codicon-symbol-method method"></span>
        WlDisplay.<span class="method">GetRegistry</span>
    </a>
</h3>

```csharp
WlRegistry GetRegistry()
```

| Argument | Type | Description |
| --- | --- | --- |
| registry | new_id | Global registry object |

**Get global registry object**

This request creates a registry object that allows the client
to list and bind the global objects available from the
compositor.

It should be noted that the server side resources consumed in
response to a get_registry request can only be released when the
client disconnects, not when the client side proxy is destroyed.
Therefore, clients should invoke get_registry as infrequently as
possible to avoid wasting memory.

<h3 class="decleration event" title="Error event">
    <a href="?id=OnWlDisplay_Error" id="OnWlDisplay_Error">
        <span class="codicon codicon-symbol-event event"></span>
        WlDisplay.<span class="event">OnError</span>
    </a>
</h3>

```csharp
void ErrorHandler(WaylandObject objectId, uint code, string message)
```

| Argument | Type | Description |
| --- | --- | --- |
| object_id | object | Object where the error occurred |
| code | uint | Error code |
| message | string | Error description |

**Fatal error event**

The error event is sent out when a fatal (non-recoverable)
error has occurred.  The object_id argument is the object
where the error occurred, most often in response to a request
to that object.  The code identifies the error and is defined
by the object interface.  As such, each interface defines its
own set of error codes.  The message is a brief description
of the error, for (debugging) convenience.

<h3 class="decleration event" title="DeleteId event">
    <a href="?id=OnWlDisplay_DeleteId" id="OnWlDisplay_DeleteId">
        <span class="codicon codicon-symbol-event event"></span>
        WlDisplay.<span class="event">OnDeleteId</span>
    </a>
</h3>

```csharp
void DeleteIdHandler(uint id)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | uint | Deleted object ID |

**Acknowledge object ID deletion**

This event is used internally by the object ID management
logic. When a client deletes an object that it had created,
the server will send this event to acknowledge that it has
seen the delete request. When the client receives this event,
it will know that it can safely reuse the object ID.

<h2 class="decleration interface">
    <a href="?id=WlRegistry" id="WlRegistry">
        <span class="codicon codicon-symbol-interface"></span>
        WlRegistry
    </a>
    <span class="pill">version 1</span>
</h2>

Global registry object


The singleton global registry object.  The server has a number of
global objects that are available to all clients.  These objects
typically represent an actual object in the server (for example,
an input device) or they are singleton objects that provide
extension functionality.

When a client creates a registry object, the registry object
will emit a global event for each global currently in the
registry.  Globals come and go as a result of device or
monitor hotplugs, reconfiguration or other events, and the
registry will send out global and global_remove events to
keep the client up to date with the changes.  To mark the end
of the initial burst of events, the client can use the
wl_display.sync request immediately after calling
wl_display.get_registry.

A client can bind to a global object by using the bind
request.  This creates a client-side handle that lets the object
emit events to the client and lets the client invoke requests on
the object.


<h3 class="decleration request" title="Bind request">
    <a href="?id=WlRegistry_Bind" id="WlRegistry_Bind">
        <span class="codicon codicon-symbol-method method"></span>
        WlRegistry.<span class="method">Bind</span>
    </a>
</h3>

```csharp
WaylandObject Bind(string interfaceName, uint version, uint name)
```

| Argument | Type | Description |
| --- | --- | --- |
| name | uint | Unique numeric name of the object |
| id | new_id | Bounded object |

**Bind an object to the display**

Binds a new, client-created object to the server using the
specified name as the identifier.

<h3 class="decleration event" title="Global event">
    <a href="?id=OnWlRegistry_Global" id="OnWlRegistry_Global">
        <span class="codicon codicon-symbol-event event"></span>
        WlRegistry.<span class="event">OnGlobal</span>
    </a>
</h3>

```csharp
void GlobalHandler(uint name, string _interface, uint version)
```

| Argument | Type | Description |
| --- | --- | --- |
| name | uint | Numeric name of the global object |
| interface | string | Interface implemented by the object |
| version | uint | Interface version |

**Announce global object**

Notify the client of global objects.

The event notifies the client that a global object with
the given name is now available, and it implements the
given version of the given interface.

<h3 class="decleration event" title="GlobalRemove event">
    <a href="?id=OnWlRegistry_GlobalRemove" id="OnWlRegistry_GlobalRemove">
        <span class="codicon codicon-symbol-event event"></span>
        WlRegistry.<span class="event">OnGlobalRemove</span>
    </a>
</h3>

```csharp
void GlobalRemoveHandler(uint name)
```

| Argument | Type | Description |
| --- | --- | --- |
| name | uint | Numeric name of the global object |

**Announce removal of global object**

Notify the client of removed global objects.

This event notifies the client that the global identified
by name is no longer available.  If the client bound to
the global using the bind request, the client should now
destroy that object.

The object remains valid and requests to the object will be
ignored until the client destroys it, to avoid races between
the global going away and a client sending a request to it.

<h2 class="decleration interface">
    <a href="?id=WlCallback" id="WlCallback">
        <span class="codicon codicon-symbol-interface"></span>
        WlCallback
    </a>
    <span class="pill">version 1</span>
</h2>

Callback object


Clients can handle the 'done' event to get notified when
the related request is done.

Note, because wl_callback objects are created from multiple independent
factory interfaces, the wl_callback interface is frozen at version 1.


<h3 class="decleration event" title="Done event">
    <a href="?id=OnWlCallback_Done" id="OnWlCallback_Done">
        <span class="codicon codicon-symbol-event event"></span>
        WlCallback.<span class="event">OnDone</span>
    </a>
</h3>

```csharp
void DoneHandler(uint callbackData)
```

| Argument | Type | Description |
| --- | --- | --- |
| callback_data | uint | Request-specific data for the callback |

**Done event**

Notify the client when the related request is done.

<h2 class="decleration interface">
    <a href="?id=WlCompositor" id="WlCompositor">
        <span class="codicon codicon-symbol-interface"></span>
        WlCompositor
    </a>
    <span class="pill">version 6</span>
</h2>

The compositor singleton


A compositor.  This object is a singleton global.  The
compositor is in charge of combining the contents of multiple
surfaces into one displayable output.


<h3 class="decleration request" title="CreateSurface request">
    <a href="?id=WlCompositor_CreateSurface" id="WlCompositor_CreateSurface">
        <span class="codicon codicon-symbol-method method"></span>
        WlCompositor.<span class="method">CreateSurface</span>
    </a>
</h3>

```csharp
WlSurface CreateSurface()
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id | The new surface |

**Create new surface**

Ask the compositor to create a new surface.

<h3 class="decleration request" title="CreateRegion request">
    <a href="?id=WlCompositor_CreateRegion" id="WlCompositor_CreateRegion">
        <span class="codicon codicon-symbol-method method"></span>
        WlCompositor.<span class="method">CreateRegion</span>
    </a>
</h3>

```csharp
WlRegion CreateRegion()
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id | The new region |

**Create new region**

Ask the compositor to create a new region.

<h2 class="decleration interface">
    <a href="?id=WlShmPool" id="WlShmPool">
        <span class="codicon codicon-symbol-interface"></span>
        WlShmPool
    </a>
    <span class="pill">version 2</span>
</h2>

A shared memory pool


The wl_shm_pool object encapsulates a piece of memory shared
between the compositor and client.  Through the wl_shm_pool
object, the client can allocate shared memory wl_buffer objects.
All objects created through the same pool share the same
underlying mapped memory. Reusing the mapped memory avoids the
setup/teardown overhead and is useful when interactively resizing
a surface or for many small buffers.


<h3 class="decleration request" title="CreateBuffer request">
    <a href="?id=WlShmPool_CreateBuffer" id="WlShmPool_CreateBuffer">
        <span class="codicon codicon-symbol-method method"></span>
        WlShmPool.<span class="method">CreateBuffer</span>
    </a>
</h3>

```csharp
WlBuffer CreateBuffer(int offset, int width, int height, int stride, uint format)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id | Buffer to create |
| offset | int | Buffer byte offset within the pool |
| width | int | Buffer width, in pixels |
| height | int | Buffer height, in pixels |
| stride | int | Number of bytes from the beginning of one row to the beginning of the next row |
| format | uint | Buffer pixel format |

**Create a buffer from the pool**

Create a wl_buffer object from the pool.

The buffer is created offset bytes into the pool and has
width and height as specified.  The stride argument specifies
the number of bytes from the beginning of one row to the beginning
of the next.  The format is the pixel format of the buffer and
must be one of those advertised through the wl_shm.format event.

A buffer will keep a reference to the pool it was created from
so it is valid to destroy the pool immediately after creating
a buffer from it.

<h3 class="decleration request" title="Destroy request">
    <a href="?id=WlShmPool_Destroy" id="WlShmPool_Destroy">
        <span class="codicon codicon-symbol-method method"></span>
        WlShmPool.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the pool**

Destroy the shared memory pool.

The mmapped memory will be released when all
buffers that have been created from this pool
are gone.

<h3 class="decleration request" title="Resize request">
    <a href="?id=WlShmPool_Resize" id="WlShmPool_Resize">
        <span class="codicon codicon-symbol-method method"></span>
        WlShmPool.<span class="method">Resize</span>
    </a>
</h3>

```csharp
void Resize(int size)
```

| Argument | Type | Description |
| --- | --- | --- |
| size | int | New size of the pool, in bytes |

**Change the size of the pool mapping**

This request will cause the server to remap the backing memory
for the pool from the file descriptor passed when the pool was
created, but using the new size.  This request can only be
used to make the pool bigger.

This request only changes the amount of bytes that are mmapped
by the server and does not touch the file corresponding to the
file descriptor passed at creation time. It is the client's
responsibility to ensure that the file is at least as big as
the new pool size.

<h2 class="decleration interface">
    <a href="?id=WlShm" id="WlShm">
        <span class="codicon codicon-symbol-interface"></span>
        WlShm
    </a>
    <span class="pill">version 2</span>
</h2>

Shared memory support


A singleton global object that provides support for shared
memory.

Clients can create wl_shm_pool objects using the create_pool
request.

On binding the wl_shm object one or more format events
are emitted to inform clients about the valid pixel formats
that can be used for buffers.


<h3 class="decleration request" title="CreatePool request">
    <a href="?id=WlShm_CreatePool" id="WlShm_CreatePool">
        <span class="codicon codicon-symbol-method method"></span>
        WlShm.<span class="method">CreatePool</span>
    </a>
</h3>

```csharp
WlShmPool CreatePool(int fd, int size)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id | Pool to create |
| fd | fd | File descriptor for the pool |
| size | int | Pool size, in bytes |

**Create a shm pool**

Create a new wl_shm_pool object.

The pool can be used to create shared memory based buffer
objects.  The server will mmap size bytes of the passed file
descriptor, to use as backing memory for the pool.

<h3 class="decleration request" title="Release request">
    <a href="?id=WlShm_Release" id="WlShm_Release">
        <span class="codicon codicon-symbol-method method"></span>
        WlShm.<span class="method">Release</span>
    </a>
    <span class="pill">since 2</span>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Release()
```


**Release the shm object**

Using this request a client can tell the server that it is not going to
use the shm object anymore.

Objects created via this interface remain unaffected.

<h3 class="decleration event" title="Format event">
    <a href="?id=OnWlShm_Format" id="OnWlShm_Format">
        <span class="codicon codicon-symbol-event event"></span>
        WlShm.<span class="event">OnFormat</span>
    </a>
</h3>

```csharp
void FormatHandler(uint format)
```

| Argument | Type | Description |
| --- | --- | --- |
| format | uint | Buffer pixel format |

**Pixel format description**

Informs the client about a valid pixel format that
can be used for buffers. Known formats include
argb8888 and xrgb8888.

<h2 class="decleration interface">
    <a href="?id=WlBuffer" id="WlBuffer">
        <span class="codicon codicon-symbol-interface"></span>
        WlBuffer
    </a>
    <span class="pill">version 1</span>
</h2>

Content for a wl_surface


A buffer provides the content for a wl_surface. Buffers are
created through factory interfaces such as wl_shm, wp_linux_buffer_params
(from the linux-dmabuf protocol extension) or similar. It has a width and
a height and can be attached to a wl_surface, but the mechanism by which a
client provides and updates the contents is defined by the buffer factory
interface.

Color channels are assumed to be electrical rather than optical (in other
words, encoded with a transfer function) unless otherwise specified. If
the buffer uses a format that has an alpha channel, the alpha channel is
assumed to be premultiplied into the electrical color channel values
(after transfer function encoding) unless otherwise specified.

Note, because wl_buffer objects are created from multiple independent
factory interfaces, the wl_buffer interface is frozen at version 1.


<h3 class="decleration request" title="Destroy request">
    <a href="?id=WlBuffer_Destroy" id="WlBuffer_Destroy">
        <span class="codicon codicon-symbol-method method"></span>
        WlBuffer.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy a buffer**

Destroy a buffer. If and how you need to release the backing
storage is defined by the buffer factory interface.

For possible side-effects to a surface, see wl_surface.attach.

<h3 class="decleration event" title="Release event">
    <a href="?id=OnWlBuffer_Release" id="OnWlBuffer_Release">
        <span class="codicon codicon-symbol-event event"></span>
        WlBuffer.<span class="event">OnRelease</span>
    </a>
</h3>

```csharp
void ReleaseHandler()
```


**Compositor releases buffer**

Sent when this wl_buffer is no longer used by the compositor.

For more information on when release events may or may not be sent,
and what consequences it has, please see the description of
wl_surface.attach.

If a client receives a release event before the frame callback
requested in the same wl_surface.commit that attaches this
wl_buffer to a surface, then the client is immediately free to
reuse the buffer and its backing storage, and does not need a
second buffer for the next surface content update. Typically
this is possible, when the compositor maintains a copy of the
wl_surface contents, e.g. as a GL texture. This is an important
optimization for GL(ES) compositors with wl_shm clients.

<h2 class="decleration interface">
    <a href="?id=WlDataOffer" id="WlDataOffer">
        <span class="codicon codicon-symbol-interface"></span>
        WlDataOffer
    </a>
    <span class="pill">version 3</span>
</h2>

Offer to transfer data


A wl_data_offer represents a piece of data offered for transfer
by another client (the source client).  It is used by the
copy-and-paste and drag-and-drop mechanisms.  The offer
describes the different mime types that the data can be
converted to and provides the mechanism for transferring the
data directly from the source client.


<h3 class="decleration request" title="Accept request">
    <a href="?id=WlDataOffer_Accept" id="WlDataOffer_Accept">
        <span class="codicon codicon-symbol-method method"></span>
        WlDataOffer.<span class="method">Accept</span>
    </a>
</h3>

```csharp
void Accept(uint serial, string? mimeType)
```

| Argument | Type | Description |
| --- | --- | --- |
| serial | uint | Serial number of the accept request |
| mime_type | string | Mime type accepted by the client |

**Accept one of the offered mime types**

Indicate that the client can accept the given mime type, or
NULL for not accepted.

For objects of version 2 or older, this request is used by the
client to give feedback whether the client can receive the given
mime type, or NULL if none is accepted; the feedback does not
determine whether the drag-and-drop operation succeeds or not.

For objects of version 3 or newer, this request determines the
final result of the drag-and-drop operation. If the end result
is that no mime types were accepted, the drag-and-drop operation
will be cancelled and the corresponding drag source will receive
wl_data_source.cancelled. Clients may still use this event in
conjunction with wl_data_source.action for feedback.

<h3 class="decleration request" title="Receive request">
    <a href="?id=WlDataOffer_Receive" id="WlDataOffer_Receive">
        <span class="codicon codicon-symbol-method method"></span>
        WlDataOffer.<span class="method">Receive</span>
    </a>
</h3>

```csharp
void Receive(string mimeType, int fd)
```

| Argument | Type | Description |
| --- | --- | --- |
| mime_type | string | Mime type desired by receiver |
| fd | fd | File descriptor for data transfer |

**Request that the data is transferred**

To transfer the offered data, the client issues this request
and indicates the mime type it wants to receive.  The transfer
happens through the passed file descriptor (typically created
with the pipe system call).  The source client writes the data
in the mime type representation requested and then closes the
file descriptor.

The receiving client reads from the read end of the pipe until
EOF and then closes its end, at which point the transfer is
complete.

This request may happen multiple times for different mime types,
both before and after wl_data_device.drop. Drag-and-drop destination
clients may preemptively fetch data or examine it more closely to
determine acceptance.

<h3 class="decleration request" title="Destroy request">
    <a href="?id=WlDataOffer_Destroy" id="WlDataOffer_Destroy">
        <span class="codicon codicon-symbol-method method"></span>
        WlDataOffer.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy data offer**

Destroy the data offer.

<h3 class="decleration request" title="Finish request">
    <a href="?id=WlDataOffer_Finish" id="WlDataOffer_Finish">
        <span class="codicon codicon-symbol-method method"></span>
        WlDataOffer.<span class="method">Finish</span>
    </a>
    <span class="pill">since 3</span>
</h3>

```csharp
void Finish()
```


**The offer will no longer be used**

Notifies the compositor that the drag destination successfully
finished the drag-and-drop operation.

Upon receiving this request, the compositor will emit
wl_data_source.dnd_finished on the drag source client.

It is a client error to perform other requests than
wl_data_offer.destroy after this one. It is also an error to perform
this request after a NULL mime type has been set in
wl_data_offer.accept or no action was received through
wl_data_offer.action.

If wl_data_offer.finish request is received for a non drag and drop
operation, the invalid_finish protocol error is raised.

<h3 class="decleration request" title="SetActions request">
    <a href="?id=WlDataOffer_SetActions" id="WlDataOffer_SetActions">
        <span class="codicon codicon-symbol-method method"></span>
        WlDataOffer.<span class="method">SetActions</span>
    </a>
    <span class="pill">since 3</span>
</h3>

```csharp
void SetActions(uint dndActions, uint preferredAction)
```

| Argument | Type | Description |
| --- | --- | --- |
| dnd_actions | uint | Actions supported by the destination client |
| preferred_action | uint | Action preferred by the destination client |

**Set the available/preferred drag-and-drop actions**

Sets the actions that the destination side client supports for
this operation. This request may trigger the emission of
wl_data_source.action and wl_data_offer.action events if the compositor
needs to change the selected action.

This request can be called multiple times throughout the
drag-and-drop operation, typically in response to wl_data_device.enter
or wl_data_device.motion events.

This request determines the final result of the drag-and-drop
operation. If the end result is that no action is accepted,
the drag source will receive wl_data_source.cancelled.

The dnd_actions argument must contain only values expressed in the
wl_data_device_manager.dnd_actions enum, and the preferred_action
argument must only contain one of those values set, otherwise it
will result in a protocol error.

While managing an "ask" action, the destination drag-and-drop client
may perform further wl_data_offer.receive requests, and is expected
to perform one last wl_data_offer.set_actions request with a preferred
action other than "ask" (and optionally wl_data_offer.accept) before
requesting wl_data_offer.finish, in order to convey the action selected
by the user. If the preferred action is not in the
wl_data_offer.source_actions mask, an error will be raised.

If the "ask" action is dismissed (e.g. user cancellation), the client
is expected to perform wl_data_offer.destroy right away.

This request can only be made on drag-and-drop offers, a protocol error
will be raised otherwise.

<h3 class="decleration event" title="Offer event">
    <a href="?id=OnWlDataOffer_Offer" id="OnWlDataOffer_Offer">
        <span class="codicon codicon-symbol-event event"></span>
        WlDataOffer.<span class="event">OnOffer</span>
    </a>
</h3>

```csharp
void OfferHandler(string mimeType)
```

| Argument | Type | Description |
| --- | --- | --- |
| mime_type | string | Offered mime type |

**Advertise offered mime type**

Sent immediately after creating the wl_data_offer object.  One
event per offered mime type.

<h3 class="decleration event" title="SourceActions event">
    <a href="?id=OnWlDataOffer_SourceActions" id="OnWlDataOffer_SourceActions">
        <span class="codicon codicon-symbol-event event"></span>
        WlDataOffer.<span class="event">OnSourceActions</span>
    </a>
    <span class="pill">since 3</span>
</h3>

```csharp
void SourceActionsHandler(uint sourceActions)
```

| Argument | Type | Description |
| --- | --- | --- |
| source_actions | uint | Actions offered by the data source |

**Notify the source-side available actions**

This event indicates the actions offered by the data source. It
will be sent immediately after creating the wl_data_offer object,
or anytime the source side changes its offered actions through
wl_data_source.set_actions.

<h3 class="decleration event" title="Action event">
    <a href="?id=OnWlDataOffer_Action" id="OnWlDataOffer_Action">
        <span class="codicon codicon-symbol-event event"></span>
        WlDataOffer.<span class="event">OnAction</span>
    </a>
    <span class="pill">since 3</span>
</h3>

```csharp
void ActionHandler(uint dndAction)
```

| Argument | Type | Description |
| --- | --- | --- |
| dnd_action | uint | Action selected by the compositor |

**Notify the selected action**

This event indicates the action selected by the compositor after
matching the source/destination side actions. Only one action (or
none) will be offered here.

This event can be emitted multiple times during the drag-and-drop
operation in response to destination side action changes through
wl_data_offer.set_actions.

This event will no longer be emitted after wl_data_device.drop
happened on the drag-and-drop destination, the client must
honor the last action received, or the last preferred one set
through wl_data_offer.set_actions when handling an "ask" action.

Compositors may also change the selected action on the fly, mainly
in response to keyboard modifier changes during the drag-and-drop
operation.

The most recent action received is always the valid one. Prior to
receiving wl_data_device.drop, the chosen action may change (e.g.
due to keyboard modifiers being pressed). At the time of receiving
wl_data_device.drop the drag-and-drop destination must honor the
last action received.

Action changes may still happen after wl_data_device.drop,
especially on "ask" actions, where the drag-and-drop destination
may choose another action afterwards. Action changes happening
at this stage are always the result of inter-client negotiation, the
compositor shall no longer be able to induce a different action.

Upon "ask" actions, it is expected that the drag-and-drop destination
may potentially choose a different action and/or mime type,
based on wl_data_offer.source_actions and finally chosen by the
user (e.g. popping up a menu with the available options). The
final wl_data_offer.set_actions and wl_data_offer.accept requests
must happen before the call to wl_data_offer.finish.

<h2 class="decleration interface">
    <a href="?id=WlDataSource" id="WlDataSource">
        <span class="codicon codicon-symbol-interface"></span>
        WlDataSource
    </a>
    <span class="pill">version 3</span>
</h2>

Offer to transfer data


The wl_data_source object is the source side of a wl_data_offer.
It is created by the source client in a data transfer and
provides a way to describe the offered data and a way to respond
to requests to transfer the data.


<h3 class="decleration request" title="Offer request">
    <a href="?id=WlDataSource_Offer" id="WlDataSource_Offer">
        <span class="codicon codicon-symbol-method method"></span>
        WlDataSource.<span class="method">Offer</span>
    </a>
</h3>

```csharp
void Offer(string mimeType)
```

| Argument | Type | Description |
| --- | --- | --- |
| mime_type | string | Mime type offered by the data source |

**Add an offered mime type**

This request adds a mime type to the set of mime types
advertised to targets.  Can be called several times to offer
multiple types.

<h3 class="decleration request" title="Destroy request">
    <a href="?id=WlDataSource_Destroy" id="WlDataSource_Destroy">
        <span class="codicon codicon-symbol-method method"></span>
        WlDataSource.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the data source**

Destroy the data source.

<h3 class="decleration request" title="SetActions request">
    <a href="?id=WlDataSource_SetActions" id="WlDataSource_SetActions">
        <span class="codicon codicon-symbol-method method"></span>
        WlDataSource.<span class="method">SetActions</span>
    </a>
    <span class="pill">since 3</span>
</h3>

```csharp
void SetActions(uint dndActions)
```

| Argument | Type | Description |
| --- | --- | --- |
| dnd_actions | uint | Actions supported by the data source |

**Set the available drag-and-drop actions**

Sets the actions that the source side client supports for this
operation. This request may trigger wl_data_source.action and
wl_data_offer.action events if the compositor needs to change the
selected action.

The dnd_actions argument must contain only values expressed in the
wl_data_device_manager.dnd_actions enum, otherwise it will result
in a protocol error.

This request must be made once only, and can only be made on sources
used in drag-and-drop, so it must be performed before
wl_data_device.start_drag. Attempting to use the source other than
for drag-and-drop will raise a protocol error.

<h3 class="decleration event" title="Target event">
    <a href="?id=OnWlDataSource_Target" id="OnWlDataSource_Target">
        <span class="codicon codicon-symbol-event event"></span>
        WlDataSource.<span class="event">OnTarget</span>
    </a>
</h3>

```csharp
void TargetHandler(string? mimeType)
```

| Argument | Type | Description |
| --- | --- | --- |
| mime_type | string | Mime type accepted by the target |

**A target accepts an offered mime type**

Sent when a target accepts pointer_focus or motion events.  If
a target does not accept any of the offered types, type is NULL.

Used for feedback during drag-and-drop.

<h3 class="decleration event" title="Send event">
    <a href="?id=OnWlDataSource_Send" id="OnWlDataSource_Send">
        <span class="codicon codicon-symbol-event event"></span>
        WlDataSource.<span class="event">OnSend</span>
    </a>
</h3>

```csharp
void SendHandler(string mimeType, int fd)
```

| Argument | Type | Description |
| --- | --- | --- |
| mime_type | string | Mime type for the data |
| fd | fd | File descriptor for the data |

**Send the data**

Request for data from the client.  Send the data as the
specified mime type over the passed file descriptor, then
close it.

<h3 class="decleration event" title="Cancelled event">
    <a href="?id=OnWlDataSource_Cancelled" id="OnWlDataSource_Cancelled">
        <span class="codicon codicon-symbol-event event"></span>
        WlDataSource.<span class="event">OnCancelled</span>
    </a>
</h3>

```csharp
void CancelledHandler()
```


**Selection was cancelled**

This data source is no longer valid. There are several reasons why
this could happen:

- The data source has been replaced by another data source.
- The drag-and-drop operation was performed, but the drop destination
did not accept any of the mime types offered through
wl_data_source.target.
- The drag-and-drop operation was performed, but the drop destination
did not select any of the actions present in the mask offered through
wl_data_source.action.
- The drag-and-drop operation was performed but didn't happen over a
surface.
- The compositor cancelled the drag-and-drop operation (e.g. compositor
dependent timeouts to avoid stale drag-and-drop transfers).

The client should clean up and destroy this data source.

For objects of version 2 or older, wl_data_source.cancelled will
only be emitted if the data source was replaced by another data
source.

<h3 class="decleration event" title="DndDropPerformed event">
    <a href="?id=OnWlDataSource_DndDropPerformed" id="OnWlDataSource_DndDropPerformed">
        <span class="codicon codicon-symbol-event event"></span>
        WlDataSource.<span class="event">OnDndDropPerformed</span>
    </a>
    <span class="pill">since 3</span>
</h3>

```csharp
void DndDropPerformedHandler()
```


**The drag-and-drop operation physically finished**

The user performed the drop action. This event does not indicate
acceptance, wl_data_source.cancelled may still be emitted afterwards
if the drop destination does not accept any mime type.

However, this event might however not be received if the compositor
cancelled the drag-and-drop operation before this event could happen.

Note that the data_source may still be used in the future and should
not be destroyed here.

<h3 class="decleration event" title="DndFinished event">
    <a href="?id=OnWlDataSource_DndFinished" id="OnWlDataSource_DndFinished">
        <span class="codicon codicon-symbol-event event"></span>
        WlDataSource.<span class="event">OnDndFinished</span>
    </a>
    <span class="pill">since 3</span>
</h3>

```csharp
void DndFinishedHandler()
```


**The drag-and-drop operation concluded**

The drop destination finished interoperating with this data
source, so the client is now free to destroy this data source and
free all associated data.

If the action used to perform the operation was "move", the
source can now delete the transferred data.

<h3 class="decleration event" title="Action event">
    <a href="?id=OnWlDataSource_Action" id="OnWlDataSource_Action">
        <span class="codicon codicon-symbol-event event"></span>
        WlDataSource.<span class="event">OnAction</span>
    </a>
    <span class="pill">since 3</span>
</h3>

```csharp
void ActionHandler(uint dndAction)
```

| Argument | Type | Description |
| --- | --- | --- |
| dnd_action | uint | Action selected by the compositor |

**Notify the selected action**

This event indicates the action selected by the compositor after
matching the source/destination side actions. Only one action (or
none) will be offered here.

This event can be emitted multiple times during the drag-and-drop
operation, mainly in response to destination side changes through
wl_data_offer.set_actions, and as the data device enters/leaves
surfaces.

It is only possible to receive this event after
wl_data_source.dnd_drop_performed if the drag-and-drop operation
ended in an "ask" action, in which case the final wl_data_source.action
event will happen immediately before wl_data_source.dnd_finished.

Compositors may also change the selected action on the fly, mainly
in response to keyboard modifier changes during the drag-and-drop
operation.

The most recent action received is always the valid one. The chosen
action may change alongside negotiation (e.g. an "ask" action can turn
into a "move" operation), so the effects of the final action must
always be applied in wl_data_offer.dnd_finished.

Clients can trigger cursor surface changes from this point, so
they reflect the current action.

<h2 class="decleration interface">
    <a href="?id=WlDataDevice" id="WlDataDevice">
        <span class="codicon codicon-symbol-interface"></span>
        WlDataDevice
    </a>
    <span class="pill">version 3</span>
</h2>

Data transfer device


There is one wl_data_device per seat which can be obtained
from the global wl_data_device_manager singleton.

A wl_data_device provides access to inter-client data transfer
mechanisms such as copy-and-paste and drag-and-drop.


<h3 class="decleration request" title="StartDrag request">
    <a href="?id=WlDataDevice_StartDrag" id="WlDataDevice_StartDrag">
        <span class="codicon codicon-symbol-method method"></span>
        WlDataDevice.<span class="method">StartDrag</span>
    </a>
</h3>

```csharp
void StartDrag(WlDataSource? source, WlSurface origin, WlSurface? icon, uint serial)
```

| Argument | Type | Description |
| --- | --- | --- |
| source | object | Data source for the eventual transfer |
| origin | object | Surface where the drag originates |
| icon | object | Drag-and-drop icon surface |
| serial | uint | Serial number of the implicit grab on the origin |

**Start drag-and-drop operation**

This request asks the compositor to start a drag-and-drop
operation on behalf of the client.

The source argument is the data source that provides the data
for the eventual data transfer. If source is NULL, enter, leave
and motion events are sent only to the client that initiated the
drag and the client is expected to handle the data passing
internally. If source is destroyed, the drag-and-drop session will be
cancelled.

The origin surface is the surface where the drag originates and
the client must have an active implicit grab that matches the
serial.

The icon surface is an optional (can be NULL) surface that
provides an icon to be moved around with the cursor.  Initially,
the top-left corner of the icon surface is placed at the cursor
hotspot, but subsequent wl_surface.offset requests can move the
relative position. Attach requests must be confirmed with
wl_surface.commit as usual. The icon surface is given the role of
a drag-and-drop icon. If the icon surface already has another role,
it raises a protocol error.

The input region is ignored for wl_surfaces with the role of a
drag-and-drop icon.

The given source may not be used in any further set_selection or
start_drag requests. Attempting to reuse a previously-used source
may send a used_source error.

<h3 class="decleration request" title="SetSelection request">
    <a href="?id=WlDataDevice_SetSelection" id="WlDataDevice_SetSelection">
        <span class="codicon codicon-symbol-method method"></span>
        WlDataDevice.<span class="method">SetSelection</span>
    </a>
</h3>

```csharp
void SetSelection(WlDataSource? source, uint serial)
```

| Argument | Type | Description |
| --- | --- | --- |
| source | object | Data source for the selection |
| serial | uint | Serial number of the event that triggered this request |

**Copy data to the selection**

This request asks the compositor to set the selection
to the data from the source on behalf of the client.

To unset the selection, set the source to NULL.

The given source may not be used in any further set_selection or
start_drag requests. Attempting to reuse a previously-used source
may send a used_source error.

<h3 class="decleration request" title="Release request">
    <a href="?id=WlDataDevice_Release" id="WlDataDevice_Release">
        <span class="codicon codicon-symbol-method method"></span>
        WlDataDevice.<span class="method">Release</span>
    </a>
    <span class="pill">since 2</span>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Release()
```


**Destroy data device**

This request destroys the data device.

<h3 class="decleration event" title="DataOffer event">
    <a href="?id=OnWlDataDevice_DataOffer" id="OnWlDataDevice_DataOffer">
        <span class="codicon codicon-symbol-event event"></span>
        WlDataDevice.<span class="event">OnDataOffer</span>
    </a>
</h3>

```csharp
void DataOfferHandler(WlDataOffer id)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id | The new data_offer object |

**Introduce a new wl_data_offer**

The data_offer event introduces a new wl_data_offer object,
which will subsequently be used in either the
data_device.enter event (for drag-and-drop) or the
data_device.selection event (for selections).  Immediately
following the data_device.data_offer event, the new data_offer
object will send out data_offer.offer events to describe the
mime types it offers.

<h3 class="decleration event" title="Enter event">
    <a href="?id=OnWlDataDevice_Enter" id="OnWlDataDevice_Enter">
        <span class="codicon codicon-symbol-event event"></span>
        WlDataDevice.<span class="event">OnEnter</span>
    </a>
</h3>

```csharp
void EnterHandler(uint serial, WlSurface surface, WlFixed x, WlFixed y, WlDataOffer? id)
```

| Argument | Type | Description |
| --- | --- | --- |
| serial | uint | Serial number of the enter event |
| surface | object | Client surface entered |
| x | fixed | Surface-local x coordinate |
| y | fixed | Surface-local y coordinate |
| id | object | Source data_offer object |

**Initiate drag-and-drop session**

This event is sent when an active drag-and-drop pointer enters
a surface owned by the client.  The position of the pointer at
enter time is provided by the x and y arguments, in surface-local
coordinates.

<h3 class="decleration event" title="Leave event">
    <a href="?id=OnWlDataDevice_Leave" id="OnWlDataDevice_Leave">
        <span class="codicon codicon-symbol-event event"></span>
        WlDataDevice.<span class="event">OnLeave</span>
    </a>
</h3>

```csharp
void LeaveHandler()
```


**End drag-and-drop session**

This event is sent when the drag-and-drop pointer leaves the
surface and the session ends.  The client must destroy the
wl_data_offer introduced at enter time at this point.

<h3 class="decleration event" title="Motion event">
    <a href="?id=OnWlDataDevice_Motion" id="OnWlDataDevice_Motion">
        <span class="codicon codicon-symbol-event event"></span>
        WlDataDevice.<span class="event">OnMotion</span>
    </a>
</h3>

```csharp
void MotionHandler(uint time, WlFixed x, WlFixed y)
```

| Argument | Type | Description |
| --- | --- | --- |
| time | uint | Timestamp with millisecond granularity |
| x | fixed | Surface-local x coordinate |
| y | fixed | Surface-local y coordinate |

**Drag-and-drop session motion**

This event is sent when the drag-and-drop pointer moves within
the currently focused surface. The new position of the pointer
is provided by the x and y arguments, in surface-local
coordinates.

<h3 class="decleration event" title="Drop event">
    <a href="?id=OnWlDataDevice_Drop" id="OnWlDataDevice_Drop">
        <span class="codicon codicon-symbol-event event"></span>
        WlDataDevice.<span class="event">OnDrop</span>
    </a>
</h3>

```csharp
void DropHandler()
```


**End drag-and-drop session successfully**

The event is sent when a drag-and-drop operation is ended
because the implicit grab is removed.

The drag-and-drop destination is expected to honor the last action
received through wl_data_offer.action, if the resulting action is
"copy" or "move", the destination can still perform
wl_data_offer.receive requests, and is expected to end all
transfers with a wl_data_offer.finish request.

If the resulting action is "ask", the action will not be considered
final. The drag-and-drop destination is expected to perform one last
wl_data_offer.set_actions request, or wl_data_offer.destroy in order
to cancel the operation.

<h3 class="decleration event" title="Selection event">
    <a href="?id=OnWlDataDevice_Selection" id="OnWlDataDevice_Selection">
        <span class="codicon codicon-symbol-event event"></span>
        WlDataDevice.<span class="event">OnSelection</span>
    </a>
</h3>

```csharp
void SelectionHandler(WlDataOffer? id)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | object | Selection data_offer object |

**Advertise new selection**

The selection event is sent out to notify the client of a new
wl_data_offer for the selection for this device.  The
data_device.data_offer and the data_offer.offer events are
sent out immediately before this event to introduce the data
offer object.  The selection event is sent to a client
immediately before receiving keyboard focus and when a new
selection is set while the client has keyboard focus.  The
data_offer is valid until a new data_offer or NULL is received
or until the client loses keyboard focus.  Switching surface with
keyboard focus within the same client doesn't mean a new selection
will be sent.  The client must destroy the previous selection
data_offer, if any, upon receiving this event.

<h2 class="decleration interface">
    <a href="?id=WlDataDeviceManager" id="WlDataDeviceManager">
        <span class="codicon codicon-symbol-interface"></span>
        WlDataDeviceManager
    </a>
    <span class="pill">version 3</span>
</h2>

Data transfer interface


The wl_data_device_manager is a singleton global object that
provides access to inter-client data transfer mechanisms such as
copy-and-paste and drag-and-drop.  These mechanisms are tied to
a wl_seat and this interface lets a client get a wl_data_device
corresponding to a wl_seat.

Depending on the version bound, the objects created from the bound
wl_data_device_manager object will have different requirements for
functioning properly. See wl_data_source.set_actions,
wl_data_offer.accept and wl_data_offer.finish for details.


<h3 class="decleration request" title="CreateDataSource request">
    <a href="?id=WlDataDeviceManager_CreateDataSource" id="WlDataDeviceManager_CreateDataSource">
        <span class="codicon codicon-symbol-method method"></span>
        WlDataDeviceManager.<span class="method">CreateDataSource</span>
    </a>
</h3>

```csharp
WlDataSource CreateDataSource()
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id | Data source to create |

**Create a new data source**

Create a new data source.

<h3 class="decleration request" title="GetDataDevice request">
    <a href="?id=WlDataDeviceManager_GetDataDevice" id="WlDataDeviceManager_GetDataDevice">
        <span class="codicon codicon-symbol-method method"></span>
        WlDataDeviceManager.<span class="method">GetDataDevice</span>
    </a>
</h3>

```csharp
WlDataDevice GetDataDevice(WlSeat seat)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id | Data device to create |
| seat | object | Seat associated with the data device |

**Create a new data device**

Create a new data device for a given seat.

<h2 class="decleration interface">
    <a href="?id=WlShell" id="WlShell">
        <span class="codicon codicon-symbol-interface"></span>
        WlShell
    </a>
    <span class="pill">version 1</span>
</h2>

Create desktop-style surfaces


This interface is implemented by servers that provide
desktop-style user interfaces.

It allows clients to associate a wl_shell_surface with
a basic surface.

Note! This protocol is deprecated and not intended for production use.
For desktop-style user interfaces, use xdg_shell. Compositors and clients
should not implement this interface.


<h3 class="decleration request" title="GetShellSurface request">
    <a href="?id=WlShell_GetShellSurface" id="WlShell_GetShellSurface">
        <span class="codicon codicon-symbol-method method"></span>
        WlShell.<span class="method">GetShellSurface</span>
    </a>
</h3>

```csharp
WlShellSurface GetShellSurface(WlSurface surface)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id | Shell surface to create |
| surface | object | Surface to be given the shell surface role |

**Create a shell surface from a surface**

Create a shell surface for an existing surface. This gives
the wl_surface the role of a shell surface. If the wl_surface
already has another role, it raises a protocol error.

Only one shell surface can be associated with a given surface.

<h2 class="decleration interface">
    <a href="?id=WlShellSurface" id="WlShellSurface">
        <span class="codicon codicon-symbol-interface"></span>
        WlShellSurface
    </a>
    <span class="pill">version 1</span>
</h2>

Desktop-style metadata interface


An interface that may be implemented by a wl_surface, for
implementations that provide a desktop-style user interface.

It provides requests to treat surfaces like toplevel, fullscreen
or popup windows, move, resize or maximize them, associate
metadata like title and class, etc.

On the server side the object is automatically destroyed when
the related wl_surface is destroyed. On the client side,
wl_shell_surface_destroy() must be called before destroying
the wl_surface object.


<h3 class="decleration request" title="Pong request">
    <a href="?id=WlShellSurface_Pong" id="WlShellSurface_Pong">
        <span class="codicon codicon-symbol-method method"></span>
        WlShellSurface.<span class="method">Pong</span>
    </a>
</h3>

```csharp
void Pong(uint serial)
```

| Argument | Type | Description |
| --- | --- | --- |
| serial | uint | Serial number of the ping event |

**Respond to a ping event**

A client must respond to a ping event with a pong request or
the client may be deemed unresponsive.

<h3 class="decleration request" title="Move request">
    <a href="?id=WlShellSurface_Move" id="WlShellSurface_Move">
        <span class="codicon codicon-symbol-method method"></span>
        WlShellSurface.<span class="method">Move</span>
    </a>
</h3>

```csharp
void Move(WlSeat seat, uint serial)
```

| Argument | Type | Description |
| --- | --- | --- |
| seat | object | Seat whose pointer is used |
| serial | uint | Serial number of the implicit grab on the pointer |

**Start an interactive move**

Start a pointer-driven move of the surface.

This request must be used in response to a button press event.
The server may ignore move requests depending on the state of
the surface (e.g. fullscreen or maximized).

<h3 class="decleration request" title="Resize request">
    <a href="?id=WlShellSurface_Resize" id="WlShellSurface_Resize">
        <span class="codicon codicon-symbol-method method"></span>
        WlShellSurface.<span class="method">Resize</span>
    </a>
</h3>

```csharp
void Resize(WlSeat seat, uint serial, uint edges)
```

| Argument | Type | Description |
| --- | --- | --- |
| seat | object | Seat whose pointer is used |
| serial | uint | Serial number of the implicit grab on the pointer |
| edges | uint | Which edge or corner is being dragged |

**Start an interactive resize**

Start a pointer-driven resizing of the surface.

This request must be used in response to a button press event.
The server may ignore resize requests depending on the state of
the surface (e.g. fullscreen or maximized).

<h3 class="decleration request" title="SetToplevel request">
    <a href="?id=WlShellSurface_SetToplevel" id="WlShellSurface_SetToplevel">
        <span class="codicon codicon-symbol-method method"></span>
        WlShellSurface.<span class="method">SetToplevel</span>
    </a>
</h3>

```csharp
void SetToplevel()
```


**Make the surface a toplevel surface**

Map the surface as a toplevel surface.

A toplevel surface is not fullscreen, maximized or transient.

<h3 class="decleration request" title="SetTransient request">
    <a href="?id=WlShellSurface_SetTransient" id="WlShellSurface_SetTransient">
        <span class="codicon codicon-symbol-method method"></span>
        WlShellSurface.<span class="method">SetTransient</span>
    </a>
</h3>

```csharp
void SetTransient(WlSurface parent, int x, int y, uint flags)
```

| Argument | Type | Description |
| --- | --- | --- |
| parent | object | Parent surface |
| x | int | Surface-local x coordinate |
| y | int | Surface-local y coordinate |
| flags | uint | Transient surface behavior |

**Make the surface a transient surface**

Map the surface relative to an existing surface.

The x and y arguments specify the location of the upper left
corner of the surface relative to the upper left corner of the
parent surface, in surface-local coordinates.

The flags argument controls details of the transient behaviour.

<h3 class="decleration request" title="SetFullscreen request">
    <a href="?id=WlShellSurface_SetFullscreen" id="WlShellSurface_SetFullscreen">
        <span class="codicon codicon-symbol-method method"></span>
        WlShellSurface.<span class="method">SetFullscreen</span>
    </a>
</h3>

```csharp
void SetFullscreen(uint method, uint framerate, WlOutput? output)
```

| Argument | Type | Description |
| --- | --- | --- |
| method | uint | Method for resolving size conflict |
| framerate | uint | Framerate in mHz |
| output | object | Output on which the surface is to be fullscreen |

**Make the surface a fullscreen surface**

Map the surface as a fullscreen surface.

If an output parameter is given then the surface will be made
fullscreen on that output. If the client does not specify the
output then the compositor will apply its policy - usually
choosing the output on which the surface has the biggest surface
area.

The client may specify a method to resolve a size conflict
between the output size and the surface size - this is provided
through the method parameter.

The framerate parameter is used only when the method is set
to "driver", to indicate the preferred framerate. A value of 0
indicates that the client does not care about framerate.  The
framerate is specified in mHz, that is framerate of 60000 is 60Hz.

A method of "scale" or "driver" implies a scaling operation of
the surface, either via a direct scaling operation or a change of
the output mode. This will override any kind of output scaling, so
that mapping a surface with a buffer size equal to the mode can
fill the screen independent of buffer_scale.

A method of "fill" means we don't scale up the buffer, however
any output scale is applied. This means that you may run into
an edge case where the application maps a buffer with the same
size of the output mode but buffer_scale 1 (thus making a
surface larger than the output). In this case it is allowed to
downscale the results to fit the screen.

The compositor must reply to this request with a configure event
with the dimensions for the output on which the surface will
be made fullscreen.

<h3 class="decleration request" title="SetPopup request">
    <a href="?id=WlShellSurface_SetPopup" id="WlShellSurface_SetPopup">
        <span class="codicon codicon-symbol-method method"></span>
        WlShellSurface.<span class="method">SetPopup</span>
    </a>
</h3>

```csharp
void SetPopup(WlSeat seat, uint serial, WlSurface parent, int x, int y, uint flags)
```

| Argument | Type | Description |
| --- | --- | --- |
| seat | object | Seat whose pointer is used |
| serial | uint | Serial number of the implicit grab on the pointer |
| parent | object | Parent surface |
| x | int | Surface-local x coordinate |
| y | int | Surface-local y coordinate |
| flags | uint | Transient surface behavior |

**Make the surface a popup surface**

Map the surface as a popup.

A popup surface is a transient surface with an added pointer
grab.

An existing implicit grab will be changed to owner-events mode,
and the popup grab will continue after the implicit grab ends
(i.e. releasing the mouse button does not cause the popup to
be unmapped).

The popup grab continues until the window is destroyed or a
mouse button is pressed in any other client's window. A click
in any of the client's surfaces is reported as normal, however,
clicks in other clients' surfaces will be discarded and trigger
the callback.

The x and y arguments specify the location of the upper left
corner of the surface relative to the upper left corner of the
parent surface, in surface-local coordinates.

<h3 class="decleration request" title="SetMaximized request">
    <a href="?id=WlShellSurface_SetMaximized" id="WlShellSurface_SetMaximized">
        <span class="codicon codicon-symbol-method method"></span>
        WlShellSurface.<span class="method">SetMaximized</span>
    </a>
</h3>

```csharp
void SetMaximized(WlOutput? output)
```

| Argument | Type | Description |
| --- | --- | --- |
| output | object | Output on which the surface is to be maximized |

**Make the surface a maximized surface**

Map the surface as a maximized surface.

If an output parameter is given then the surface will be
maximized on that output. If the client does not specify the
output then the compositor will apply its policy - usually
choosing the output on which the surface has the biggest surface
area.

The compositor will reply with a configure event telling
the expected new surface size. The operation is completed
on the next buffer attach to this surface.

A maximized surface typically fills the entire output it is
bound to, except for desktop elements such as panels. This is
the main difference between a maximized shell surface and a
fullscreen shell surface.

The details depend on the compositor implementation.

<h3 class="decleration request" title="SetTitle request">
    <a href="?id=WlShellSurface_SetTitle" id="WlShellSurface_SetTitle">
        <span class="codicon codicon-symbol-method method"></span>
        WlShellSurface.<span class="method">SetTitle</span>
    </a>
</h3>

```csharp
void SetTitle(string title)
```

| Argument | Type | Description |
| --- | --- | --- |
| title | string | Surface title |

**Set surface title**

Set a short title for the surface.

This string may be used to identify the surface in a task bar,
window list, or other user interface elements provided by the
compositor.

The string must be encoded in UTF-8.

<h3 class="decleration request" title="SetClass request">
    <a href="?id=WlShellSurface_SetClass" id="WlShellSurface_SetClass">
        <span class="codicon codicon-symbol-method method"></span>
        WlShellSurface.<span class="method">SetClass</span>
    </a>
</h3>

```csharp
void SetClass(string _class)
```

| Argument | Type | Description |
| --- | --- | --- |
| class_ | string | Surface class |

**Set surface class**

Set a class for the surface.

The surface class identifies the general class of applications
to which the surface belongs. A common convention is to use the
file name (or the full path if it is a non-standard location) of
the application's .desktop file as the class.

<h3 class="decleration event" title="Ping event">
    <a href="?id=OnWlShellSurface_Ping" id="OnWlShellSurface_Ping">
        <span class="codicon codicon-symbol-event event"></span>
        WlShellSurface.<span class="event">OnPing</span>
    </a>
</h3>

```csharp
void PingHandler(uint serial)
```

| Argument | Type | Description |
| --- | --- | --- |
| serial | uint | Serial number of the ping |

**Ping client**

Ping a client to check if it is receiving events and sending
requests. A client is expected to reply with a pong request.

<h3 class="decleration event" title="Configure event">
    <a href="?id=OnWlShellSurface_Configure" id="OnWlShellSurface_Configure">
        <span class="codicon codicon-symbol-event event"></span>
        WlShellSurface.<span class="event">OnConfigure</span>
    </a>
</h3>

```csharp
void ConfigureHandler(uint edges, int width, int height)
```

| Argument | Type | Description |
| --- | --- | --- |
| edges | uint | How the surface was resized |
| width | int | New width of the surface |
| height | int | New height of the surface |

**Suggest resize**

The configure event asks the client to resize its surface.

The size is a hint, in the sense that the client is free to
ignore it if it doesn't resize, pick a smaller size (to
satisfy aspect ratio or resize in steps of NxM pixels).

The edges parameter provides a hint about how the surface
was resized. The client may use this information to decide
how to adjust its content to the new size (e.g. a scrolling
area might adjust its content position to leave the viewable
content unmoved).

The client is free to dismiss all but the last configure
event it received.

The width and height arguments specify the size of the window
in surface-local coordinates.

<h3 class="decleration event" title="PopupDone event">
    <a href="?id=OnWlShellSurface_PopupDone" id="OnWlShellSurface_PopupDone">
        <span class="codicon codicon-symbol-event event"></span>
        WlShellSurface.<span class="event">OnPopupDone</span>
    </a>
</h3>

```csharp
void PopupDoneHandler()
```


**Popup interaction is done**

The popup_done event is sent out when a popup grab is broken,
that is, when the user clicks a surface that doesn't belong
to the client owning the popup surface.

<h2 class="decleration interface">
    <a href="?id=WlSurface" id="WlSurface">
        <span class="codicon codicon-symbol-interface"></span>
        WlSurface
    </a>
    <span class="pill">version 6</span>
</h2>

An onscreen surface


A surface is a rectangular area that may be displayed on zero
or more outputs, and shown any number of times at the compositor's
discretion. They can present wl_buffers, receive user input, and
define a local coordinate system.

The size of a surface (and relative positions on it) is described
in surface-local coordinates, which may differ from the buffer
coordinates of the pixel content, in case a buffer_transform
or a buffer_scale is used.

A surface without a "role" is fairly useless: a compositor does
not know where, when or how to present it. The role is the
purpose of a wl_surface. Examples of roles are a cursor for a
pointer (as set by wl_pointer.set_cursor), a drag icon
(wl_data_device.start_drag), a sub-surface
(wl_subcompositor.get_subsurface), and a window as defined by a
shell protocol (e.g. wl_shell.get_shell_surface).

A surface can have only one role at a time. Initially a
wl_surface does not have a role. Once a wl_surface is given a
role, it is set permanently for the whole lifetime of the
wl_surface object. Giving the current role again is allowed,
unless explicitly forbidden by the relevant interface
specification.

Surface roles are given by requests in other interfaces such as
wl_pointer.set_cursor. The request should explicitly mention
that this request gives a role to a wl_surface. Often, this
request also creates a new protocol object that represents the
role and adds additional functionality to wl_surface. When a
client wants to destroy a wl_surface, they must destroy this role
object before the wl_surface, otherwise a defunct_role_object error is
sent.

Destroying the role object does not remove the role from the
wl_surface, but it may stop the wl_surface from "playing the role".
For instance, if a wl_subsurface object is destroyed, the wl_surface
it was created for will be unmapped and forget its position and
z-order. It is allowed to create a wl_subsurface for the same
wl_surface again, but it is not allowed to use the wl_surface as
a cursor (cursor is a different role than sub-surface, and role
switching is not allowed).


<h3 class="decleration request" title="Destroy request">
    <a href="?id=WlSurface_Destroy" id="WlSurface_Destroy">
        <span class="codicon codicon-symbol-method method"></span>
        WlSurface.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Delete surface**

Deletes the surface and invalidates its object ID.

<h3 class="decleration request" title="Attach request">
    <a href="?id=WlSurface_Attach" id="WlSurface_Attach">
        <span class="codicon codicon-symbol-method method"></span>
        WlSurface.<span class="method">Attach</span>
    </a>
</h3>

```csharp
void Attach(WlBuffer? buffer, int x, int y)
```

| Argument | Type | Description |
| --- | --- | --- |
| buffer | object | Buffer of surface contents |
| x | int | Surface-local x coordinate |
| y | int | Surface-local y coordinate |

**Set the surface contents**

Set a buffer as the content of this surface.

The new size of the surface is calculated based on the buffer
size transformed by the inverse buffer_transform and the
inverse buffer_scale. This means that at commit time the supplied
buffer size must be an integer multiple of the buffer_scale. If
that's not the case, an invalid_size error is sent.

The x and y arguments specify the location of the new pending
buffer's upper left corner, relative to the current buffer's upper
left corner, in surface-local coordinates. In other words, the
x and y, combined with the new surface size define in which
directions the surface's size changes. Setting anything other than 0
as x and y arguments is discouraged, and should instead be replaced
with using the separate wl_surface.offset request.

When the bound wl_surface version is 5 or higher, passing any
non-zero x or y is a protocol violation, and will result in an
'invalid_offset' error being raised. The x and y arguments are ignored
and do not change the pending state. To achieve equivalent semantics,
use wl_surface.offset.

Surface contents are double-buffered state, see wl_surface.commit.

The initial surface contents are void; there is no content.
wl_surface.attach assigns the given wl_buffer as the pending
wl_buffer. wl_surface.commit makes the pending wl_buffer the new
surface contents, and the size of the surface becomes the size
calculated from the wl_buffer, as described above. After commit,
there is no pending buffer until the next attach.

Committing a pending wl_buffer allows the compositor to read the
pixels in the wl_buffer. The compositor may access the pixels at
any time after the wl_surface.commit request. When the compositor
will not access the pixels anymore, it will send the
wl_buffer.release event. Only after receiving wl_buffer.release,
the client may reuse the wl_buffer. A wl_buffer that has been
attached and then replaced by another attach instead of committed
will not receive a release event, and is not used by the
compositor.

If a pending wl_buffer has been committed to more than one wl_surface,
the delivery of wl_buffer.release events becomes undefined. A well
behaved client should not rely on wl_buffer.release events in this
case. Alternatively, a client could create multiple wl_buffer objects
from the same backing storage or use a protocol extension providing
per-commit release notifications.

Destroying the wl_buffer after wl_buffer.release does not change
the surface contents. Destroying the wl_buffer before wl_buffer.release
is allowed as long as the underlying buffer storage isn't re-used (this
can happen e.g. on client process termination). However, if the client
destroys the wl_buffer before receiving the wl_buffer.release event and
mutates the underlying buffer storage, the surface contents become
undefined immediately.

If wl_surface.attach is sent with a NULL wl_buffer, the
following wl_surface.commit will remove the surface content.

If a pending wl_buffer has been destroyed, the result is not specified.
Many compositors are known to remove the surface content on the following
wl_surface.commit, but this behaviour is not universal. Clients seeking to
maximise compatibility should not destroy pending buffers and should
ensure that they explicitly remove content from surfaces, even after
destroying buffers.

<h3 class="decleration request" title="Damage request">
    <a href="?id=WlSurface_Damage" id="WlSurface_Damage">
        <span class="codicon codicon-symbol-method method"></span>
        WlSurface.<span class="method">Damage</span>
    </a>
</h3>

```csharp
void Damage(int x, int y, int width, int height)
```

| Argument | Type | Description |
| --- | --- | --- |
| x | int | Surface-local x coordinate |
| y | int | Surface-local y coordinate |
| width | int | Width of damage rectangle |
| height | int | Height of damage rectangle |

**Mark part of the surface damaged**

This request is used to describe the regions where the pending
buffer is different from the current surface contents, and where
the surface therefore needs to be repainted. The compositor
ignores the parts of the damage that fall outside of the surface.

Damage is double-buffered state, see wl_surface.commit.

The damage rectangle is specified in surface-local coordinates,
where x and y specify the upper left corner of the damage rectangle.

The initial value for pending damage is empty: no damage.
wl_surface.damage adds pending damage: the new pending damage
is the union of old pending damage and the given rectangle.

wl_surface.commit assigns pending damage as the current damage,
and clears pending damage. The server will clear the current
damage as it repaints the surface.

Note! New clients should not use this request. Instead damage can be
posted with wl_surface.damage_buffer which uses buffer coordinates
instead of surface coordinates.

<h3 class="decleration request" title="Frame request">
    <a href="?id=WlSurface_Frame" id="WlSurface_Frame">
        <span class="codicon codicon-symbol-method method"></span>
        WlSurface.<span class="method">Frame</span>
    </a>
</h3>

```csharp
WlCallback Frame()
```

| Argument | Type | Description |
| --- | --- | --- |
| callback | new_id | Callback object for the frame request |

**Request a frame throttling hint**

Request a notification when it is a good time to start drawing a new
frame, by creating a frame callback. This is useful for throttling
redrawing operations, and driving animations.

When a client is animating on a wl_surface, it can use the 'frame'
request to get notified when it is a good time to draw and commit the
next frame of animation. If the client commits an update earlier than
that, it is likely that some updates will not make it to the display,
and the client is wasting resources by drawing too often.

The frame request will take effect on the next wl_surface.commit.
The notification will only be posted for one frame unless
requested again. For a wl_surface, the notifications are posted in
the order the frame requests were committed.

The server must send the notifications so that a client
will not send excessive updates, while still allowing
the highest possible update rate for clients that wait for the reply
before drawing again. The server should give some time for the client
to draw and commit after sending the frame callback events to let it
hit the next output refresh.

A server should avoid signaling the frame callbacks if the
surface is not visible in any way, e.g. the surface is off-screen,
or completely obscured by other opaque surfaces.

The object returned by this request will be destroyed by the
compositor after the callback is fired and as such the client must not
attempt to use it after that point.

The callback_data passed in the callback is the current time, in
milliseconds, with an undefined base.

<h3 class="decleration request" title="SetOpaqueRegion request">
    <a href="?id=WlSurface_SetOpaqueRegion" id="WlSurface_SetOpaqueRegion">
        <span class="codicon codicon-symbol-method method"></span>
        WlSurface.<span class="method">SetOpaqueRegion</span>
    </a>
</h3>

```csharp
void SetOpaqueRegion(WlRegion? region)
```

| Argument | Type | Description |
| --- | --- | --- |
| region | object | Opaque region of the surface |

**Set opaque region**

This request sets the region of the surface that contains
opaque content.

The opaque region is an optimization hint for the compositor
that lets it optimize the redrawing of content behind opaque
regions.  Setting an opaque region is not required for correct
behaviour, but marking transparent content as opaque will result
in repaint artifacts.

The opaque region is specified in surface-local coordinates.

The compositor ignores the parts of the opaque region that fall
outside of the surface.

Opaque region is double-buffered state, see wl_surface.commit.

wl_surface.set_opaque_region changes the pending opaque region.
wl_surface.commit copies the pending region to the current region.
Otherwise, the pending and current regions are never changed.

The initial value for an opaque region is empty. Setting the pending
opaque region has copy semantics, and the wl_region object can be
destroyed immediately. A NULL wl_region causes the pending opaque
region to be set to empty.

<h3 class="decleration request" title="SetInputRegion request">
    <a href="?id=WlSurface_SetInputRegion" id="WlSurface_SetInputRegion">
        <span class="codicon codicon-symbol-method method"></span>
        WlSurface.<span class="method">SetInputRegion</span>
    </a>
</h3>

```csharp
void SetInputRegion(WlRegion? region)
```

| Argument | Type | Description |
| --- | --- | --- |
| region | object | Input region of the surface |

**Set input region**

This request sets the region of the surface that can receive
pointer and touch events.

Input events happening outside of this region will try the next
surface in the server surface stack. The compositor ignores the
parts of the input region that fall outside of the surface.

The input region is specified in surface-local coordinates.

Input region is double-buffered state, see wl_surface.commit.

wl_surface.set_input_region changes the pending input region.
wl_surface.commit copies the pending region to the current region.
Otherwise the pending and current regions are never changed,
except cursor and icon surfaces are special cases, see
wl_pointer.set_cursor and wl_data_device.start_drag.

The initial value for an input region is infinite. That means the
whole surface will accept input. Setting the pending input region
has copy semantics, and the wl_region object can be destroyed
immediately. A NULL wl_region causes the input region to be set
to infinite.

<h3 class="decleration request" title="Commit request">
    <a href="?id=WlSurface_Commit" id="WlSurface_Commit">
        <span class="codicon codicon-symbol-method method"></span>
        WlSurface.<span class="method">Commit</span>
    </a>
</h3>

```csharp
void Commit()
```


**Commit pending surface state**

Surface state (input, opaque, and damage regions, attached buffers,
etc.) is double-buffered. Protocol requests modify the pending state,
as opposed to the active state in use by the compositor.

All requests that need a commit to become effective are documented
to affect double-buffered state.

Other interfaces may add further double-buffered surface state.

A commit request atomically creates a Content Update (CU) from the
pending state, even if the pending state has not been touched. The
content update is placed at the end of a per-surface queue until it
becomes active. After commit, the new pending state is as documented for
each related request.

A CU is either a Desync Content Update (DCU) or a Sync Content Update
(SCU). If the surface is effectively synchronized at the commit request,
it is a SCU, otherwise a DCU.

When a surface transitions from effectively synchronized to effectively
desynchronized, all SCUs in its queue which are not reachable by any
DCU become DCUs and dependency edges from outside the queue to these CUs
are removed.

See wl_subsurface for the definition of 'effectively synchronized' and
'effectively desynchronized'.

When a CU is placed in the queue, the CU has a dependency on the CU in
front of it and to the SCU at end of the queue of every direct child
surface if that SCU exists and does not have another dependent. This can
form a directed acyclic graph of CUs with dependencies as edges.

In addition to surface state, the CU can have constraints that must be
satisfied before it can be applied. Other interfaces may add CU
constraints.

All DCUs which do not have a SCU in front of themselves in their queue,
are candidates. If the graph that's reachable by a candidate does not
have any unsatisfied constraints, the entire graph must be applied
atomically.

When a CU is applied, the wl_buffer is applied before all other state.
This means that all coordinates in double-buffered state are relative to
the newly attached wl_buffers, except for wl_surface.attach itself. If
there is no newly attached wl_buffer, the coordinates are relative to
the previous content update.

<h3 class="decleration request" title="SetBufferTransform request">
    <a href="?id=WlSurface_SetBufferTransform" id="WlSurface_SetBufferTransform">
        <span class="codicon codicon-symbol-method method"></span>
        WlSurface.<span class="method">SetBufferTransform</span>
    </a>
    <span class="pill">since 2</span>
</h3>

```csharp
void SetBufferTransform(int transform)
```

| Argument | Type | Description |
| --- | --- | --- |
| transform | int | Transform for interpreting buffer contents |

**Sets the buffer transformation**

This request sets the transformation that the client has already applied
to the content of the buffer. The accepted values for the transform
parameter are the values for wl_output.transform.

The compositor applies the inverse of this transformation whenever it
uses the buffer contents.

Buffer transform is double-buffered state, see wl_surface.commit.

A newly created surface has its buffer transformation set to normal.

wl_surface.set_buffer_transform changes the pending buffer
transformation. wl_surface.commit copies the pending buffer
transformation to the current one. Otherwise, the pending and current
values are never changed.

The purpose of this request is to allow clients to render content
according to the output transform, thus permitting the compositor to
use certain optimizations even if the display is rotated. Using
hardware overlays and scanning out a client buffer for fullscreen
surfaces are examples of such optimizations. Those optimizations are
highly dependent on the compositor implementation, so the use of this
request should be considered on a case-by-case basis.

Note that if the transform value includes 90 or 270 degree rotation,
the width of the buffer will become the surface height and the height
of the buffer will become the surface width.

If transform is not one of the values from the
wl_output.transform enum the invalid_transform protocol error
is raised.

<h3 class="decleration request" title="SetBufferScale request">
    <a href="?id=WlSurface_SetBufferScale" id="WlSurface_SetBufferScale">
        <span class="codicon codicon-symbol-method method"></span>
        WlSurface.<span class="method">SetBufferScale</span>
    </a>
    <span class="pill">since 3</span>
</h3>

```csharp
void SetBufferScale(int scale)
```

| Argument | Type | Description |
| --- | --- | --- |
| scale | int | Scale for interpreting buffer contents |

**Sets the buffer scaling factor**

This request sets an optional scaling factor on how the compositor
interprets the contents of the buffer attached to the window.

Buffer scale is double-buffered state, see wl_surface.commit.

A newly created surface has its buffer scale set to 1.

wl_surface.set_buffer_scale changes the pending buffer scale.
wl_surface.commit copies the pending buffer scale to the current one.
Otherwise, the pending and current values are never changed.

The purpose of this request is to allow clients to supply higher
resolution buffer data for use on high resolution outputs. It is
intended that you pick the same buffer scale as the scale of the
output that the surface is displayed on. This means the compositor
can avoid scaling when rendering the surface on that output.

Note that if the scale is larger than 1, then you have to attach
a buffer that is larger (by a factor of scale in each dimension)
than the desired surface size.

If scale is not greater than 0 the invalid_scale protocol error is
raised.

<h3 class="decleration request" title="DamageBuffer request">
    <a href="?id=WlSurface_DamageBuffer" id="WlSurface_DamageBuffer">
        <span class="codicon codicon-symbol-method method"></span>
        WlSurface.<span class="method">DamageBuffer</span>
    </a>
    <span class="pill">since 4</span>
</h3>

```csharp
void DamageBuffer(int x, int y, int width, int height)
```

| Argument | Type | Description |
| --- | --- | --- |
| x | int | Buffer-local x coordinate |
| y | int | Buffer-local y coordinate |
| width | int | Width of damage rectangle |
| height | int | Height of damage rectangle |

**Mark part of the surface damaged using buffer coordinates**

This request is used to describe the regions where the pending
buffer is different from the current surface contents, and where
the surface therefore needs to be repainted. The compositor
ignores the parts of the damage that fall outside of the surface.

Damage is double-buffered state, see wl_surface.commit.

The damage rectangle is specified in buffer coordinates,
where x and y specify the upper left corner of the damage rectangle.

The initial value for pending damage is empty: no damage.
wl_surface.damage_buffer adds pending damage: the new pending
damage is the union of old pending damage and the given rectangle.

wl_surface.commit assigns pending damage as the current damage,
and clears pending damage. The server will clear the current
damage as it repaints the surface.

This request differs from wl_surface.damage in only one way - it
takes damage in buffer coordinates instead of surface-local
coordinates. While this generally is more intuitive than surface
coordinates, it is especially desirable when using wp_viewport
or when a drawing library (like EGL) is unaware of buffer scale
and buffer transform.

Note: Because buffer transformation changes and damage requests may
be interleaved in the protocol stream, it is impossible to determine
the actual mapping between surface and buffer damage until
wl_surface.commit time. Therefore, compositors wishing to take both
kinds of damage into account will have to accumulate damage from the
two requests separately and only transform from one to the other
after receiving the wl_surface.commit.

<h3 class="decleration request" title="Offset request">
    <a href="?id=WlSurface_Offset" id="WlSurface_Offset">
        <span class="codicon codicon-symbol-method method"></span>
        WlSurface.<span class="method">Offset</span>
    </a>
    <span class="pill">since 5</span>
</h3>

```csharp
void Offset(int x, int y)
```

| Argument | Type | Description |
| --- | --- | --- |
| x | int | Surface-local x coordinate |
| y | int | Surface-local y coordinate |

**Set the surface contents offset**

The x and y arguments specify the location of the new pending
buffer's upper left corner, relative to the current buffer's upper
left corner, in surface-local coordinates. In other words, the
x and y, combined with the new surface size define in which
directions the surface's size changes.

The exact semantics of wl_surface.offset are role-specific. Refer to
the documentation of specific roles for more information.

Surface location offset is double-buffered state, see
wl_surface.commit.

This request is semantically equivalent to and the replaces the x and y
arguments in the wl_surface.attach request in wl_surface versions prior
to 5. See wl_surface.attach for details.

<h3 class="decleration event" title="Enter event">
    <a href="?id=OnWlSurface_Enter" id="OnWlSurface_Enter">
        <span class="codicon codicon-symbol-event event"></span>
        WlSurface.<span class="event">OnEnter</span>
    </a>
</h3>

```csharp
void EnterHandler(WlOutput output)
```

| Argument | Type | Description |
| --- | --- | --- |
| output | object | Output entered by the surface |

**Surface enters an output**

This is emitted whenever a surface's creation, movement, or resizing
results in some part of it being within the scanout region of an
output.

Note that a surface may be overlapping with zero or more outputs.

<h3 class="decleration event" title="Leave event">
    <a href="?id=OnWlSurface_Leave" id="OnWlSurface_Leave">
        <span class="codicon codicon-symbol-event event"></span>
        WlSurface.<span class="event">OnLeave</span>
    </a>
</h3>

```csharp
void LeaveHandler(WlOutput output)
```

| Argument | Type | Description |
| --- | --- | --- |
| output | object | Output left by the surface |

**Surface leaves an output**

This is emitted whenever a surface's creation, movement, or resizing
results in it no longer having any part of it within the scanout region
of an output.

Clients should not use the number of outputs the surface is on for frame
throttling purposes. The surface might be hidden even if no leave event
has been sent, and the compositor might expect new surface content
updates even if no enter event has been sent. The frame event should be
used instead.

<h3 class="decleration event" title="PreferredBufferScale event">
    <a href="?id=OnWlSurface_PreferredBufferScale" id="OnWlSurface_PreferredBufferScale">
        <span class="codicon codicon-symbol-event event"></span>
        WlSurface.<span class="event">OnPreferredBufferScale</span>
    </a>
    <span class="pill">since 6</span>
</h3>

```csharp
void PreferredBufferScaleHandler(int factor)
```

| Argument | Type | Description |
| --- | --- | --- |
| factor | int | Preferred scaling factor |

**Preferred buffer scale for the surface**

This event indicates the preferred buffer scale for this surface. It is
sent whenever the compositor's preference changes.

Before receiving this event the preferred buffer scale for this surface
is 1.

It is intended that scaling aware clients use this event to scale their
content and use wl_surface.set_buffer_scale to indicate the scale they
have rendered with. This allows clients to supply a higher detail
buffer.

The compositor shall emit a scale value greater than 0.

<h3 class="decleration event" title="PreferredBufferTransform event">
    <a href="?id=OnWlSurface_PreferredBufferTransform" id="OnWlSurface_PreferredBufferTransform">
        <span class="codicon codicon-symbol-event event"></span>
        WlSurface.<span class="event">OnPreferredBufferTransform</span>
    </a>
    <span class="pill">since 6</span>
</h3>

```csharp
void PreferredBufferTransformHandler(uint transform)
```

| Argument | Type | Description |
| --- | --- | --- |
| transform | uint | Preferred transform |

**Preferred buffer transform for the surface**

This event indicates the preferred buffer transform for this surface.
It is sent whenever the compositor's preference changes.

Before receiving this event the preferred buffer transform for this
surface is normal.

Applying this transformation to the surface buffer contents and using
wl_surface.set_buffer_transform might allow the compositor to use the
surface buffer more efficiently.

<h2 class="decleration interface">
    <a href="?id=WlSeat" id="WlSeat">
        <span class="codicon codicon-symbol-interface"></span>
        WlSeat
    </a>
    <span class="pill">version 10</span>
</h2>

Group of input devices


A seat is a group of keyboards, pointer and touch devices. This
object is published as a global during start up, or when such a
device is hot plugged.  A seat typically has a pointer and
maintains a keyboard focus and a pointer focus.


<h3 class="decleration request" title="GetPointer request">
    <a href="?id=WlSeat_GetPointer" id="WlSeat_GetPointer">
        <span class="codicon codicon-symbol-method method"></span>
        WlSeat.<span class="method">GetPointer</span>
    </a>
</h3>

```csharp
WlPointer GetPointer()
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id | Seat pointer |

**Return pointer object**

The ID provided will be initialized to the wl_pointer interface
for this seat.

This request only takes effect if the seat has the pointer
capability, or has had the pointer capability in the past.
It is a protocol violation to issue this request on a seat that has
never had the pointer capability. The missing_capability error will
be sent in this case.

<h3 class="decleration request" title="GetKeyboard request">
    <a href="?id=WlSeat_GetKeyboard" id="WlSeat_GetKeyboard">
        <span class="codicon codicon-symbol-method method"></span>
        WlSeat.<span class="method">GetKeyboard</span>
    </a>
</h3>

```csharp
WlKeyboard GetKeyboard()
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id | Seat keyboard |

**Return keyboard object**

The ID provided will be initialized to the wl_keyboard interface
for this seat.

This request only takes effect if the seat has the keyboard
capability, or has had the keyboard capability in the past.
It is a protocol violation to issue this request on a seat that has
never had the keyboard capability. The missing_capability error will
be sent in this case.

<h3 class="decleration request" title="GetTouch request">
    <a href="?id=WlSeat_GetTouch" id="WlSeat_GetTouch">
        <span class="codicon codicon-symbol-method method"></span>
        WlSeat.<span class="method">GetTouch</span>
    </a>
</h3>

```csharp
WlTouch GetTouch()
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id | Seat touch interface |

**Return touch object**

The ID provided will be initialized to the wl_touch interface
for this seat.

This request only takes effect if the seat has the touch
capability, or has had the touch capability in the past.
It is a protocol violation to issue this request on a seat that has
never had the touch capability. The missing_capability error will
be sent in this case.

<h3 class="decleration request" title="Release request">
    <a href="?id=WlSeat_Release" id="WlSeat_Release">
        <span class="codicon codicon-symbol-method method"></span>
        WlSeat.<span class="method">Release</span>
    </a>
    <span class="pill">since 5</span>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Release()
```


**Release the seat object**

Using this request a client can tell the server that it is not going to
use the seat object anymore.

<h3 class="decleration event" title="Capabilities event">
    <a href="?id=OnWlSeat_Capabilities" id="OnWlSeat_Capabilities">
        <span class="codicon codicon-symbol-event event"></span>
        WlSeat.<span class="event">OnCapabilities</span>
    </a>
</h3>

```csharp
void CapabilitiesHandler(uint capabilities)
```

| Argument | Type | Description |
| --- | --- | --- |
| capabilities | uint | Capabilities of the seat |

**Seat capabilities changed**

This is sent on binding to the seat global or whenever a seat gains
or loses the pointer, keyboard or touch capabilities.
The argument is a capability enum containing the complete set of
capabilities this seat has.

When the pointer capability is added, a client may create a
wl_pointer object using the wl_seat.get_pointer request. This object
will receive pointer events until the capability is removed in the
future.

When the pointer capability is removed, a client should destroy the
wl_pointer objects associated with the seat where the capability was
removed, using the wl_pointer.release request. No further pointer
events will be received on these objects.

In some compositors, if a seat regains the pointer capability and a
client has a previously obtained wl_pointer object of version 4 or
less, that object may start sending pointer events again. This
behavior is considered a misinterpretation of the intended behavior
and must not be relied upon by the client. wl_pointer objects of
version 5 or later must not send events if created before the most
recent event notifying the client of an added pointer capability.

The above behavior also applies to wl_keyboard and wl_touch with the
keyboard and touch capabilities, respectively.

<h3 class="decleration event" title="Name event">
    <a href="?id=OnWlSeat_Name" id="OnWlSeat_Name">
        <span class="codicon codicon-symbol-event event"></span>
        WlSeat.<span class="event">OnName</span>
    </a>
    <span class="pill">since 2</span>
</h3>

```csharp
void NameHandler(string name)
```

| Argument | Type | Description |
| --- | --- | --- |
| name | string | Seat identifier |

**Unique identifier for this seat**

In a multi-seat configuration the seat name can be used by clients to
help identify which physical devices the seat represents.

The seat name is a UTF-8 string with no convention defined for its
contents. Each name is unique among all wl_seat globals. The name is
only guaranteed to be unique for the current compositor instance.

The same seat names are used for all clients. Thus, the name can be
shared across processes to refer to a specific wl_seat global.

The name event is sent after binding to the seat global, and should be sent
before announcing capabilities. This event only sent once per seat object,
and the name does not change over the lifetime of the wl_seat global.

Compositors may re-use the same seat name if the wl_seat global is
destroyed and re-created later.

<h2 class="decleration interface">
    <a href="?id=WlPointer" id="WlPointer">
        <span class="codicon codicon-symbol-interface"></span>
        WlPointer
    </a>
    <span class="pill">version 10</span>
</h2>

Pointer input device


The wl_pointer interface represents one or more input devices,
such as mice, which control the pointer location and pointer_focus
of a seat.

The wl_pointer interface generates motion, enter and leave
events for the surfaces that the pointer is located over,
and button and axis events for button presses, button releases
and scrolling.


<h3 class="decleration request" title="SetCursor request">
    <a href="?id=WlPointer_SetCursor" id="WlPointer_SetCursor">
        <span class="codicon codicon-symbol-method method"></span>
        WlPointer.<span class="method">SetCursor</span>
    </a>
</h3>

```csharp
void SetCursor(uint serial, WlSurface? surface, int hotspotX, int hotspotY)
```

| Argument | Type | Description |
| --- | --- | --- |
| serial | uint | Serial number of the enter event |
| surface | object | Pointer surface |
| hotspot_x | int | Surface-local x coordinate |
| hotspot_y | int | Surface-local y coordinate |

**Set the pointer surface**

Set the pointer surface, i.e., the surface that contains the
pointer image (cursor). This request gives the surface the role
of a cursor. If the surface already has another role, it raises
a protocol error.

The cursor actually changes only if the pointer
focus for this device is one of the requesting client's surfaces
or the surface parameter is the current pointer surface. If
there was a previous surface set with this request it is
replaced. If surface is NULL, the pointer image is hidden.

The parameters hotspot_x and hotspot_y define the position of
the pointer surface relative to the pointer location. Its
top-left corner is always at (x, y) - (hotspot_x, hotspot_y),
where (x, y) are the coordinates of the pointer location, in
surface-local coordinates.

On wl_surface.offset requests to the pointer surface, hotspot_x
and hotspot_y are decremented by the x and y parameters
passed to the request. The offset must be applied by
wl_surface.commit as usual.

The hotspot can also be updated by passing the currently set
pointer surface to this request with new values for hotspot_x
and hotspot_y.

The input region is ignored for wl_surfaces with the role of
a cursor. When the use as a cursor ends, the wl_surface is
unmapped.

The serial parameter must match the latest wl_pointer.enter
serial number sent to the client. Otherwise the request will be
ignored.

<h3 class="decleration request" title="Release request">
    <a href="?id=WlPointer_Release" id="WlPointer_Release">
        <span class="codicon codicon-symbol-method method"></span>
        WlPointer.<span class="method">Release</span>
    </a>
    <span class="pill">since 3</span>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Release()
```


**Release the pointer object**

Using this request a client can tell the server that it is not going to
use the pointer object anymore.

This request destroys the pointer proxy object, so clients must not call
wl_pointer_destroy() after using this request.

<h3 class="decleration event" title="Enter event">
    <a href="?id=OnWlPointer_Enter" id="OnWlPointer_Enter">
        <span class="codicon codicon-symbol-event event"></span>
        WlPointer.<span class="event">OnEnter</span>
    </a>
</h3>

```csharp
void EnterHandler(uint serial, WlSurface surface, WlFixed surfaceX, WlFixed surfaceY)
```

| Argument | Type | Description |
| --- | --- | --- |
| serial | uint | Serial number of the enter event |
| surface | object | Surface entered by the pointer |
| surface_x | fixed | Surface-local x coordinate |
| surface_y | fixed | Surface-local y coordinate |

**Enter event**

Notification that this seat's pointer is focused on a certain
surface.

When a seat's focus enters a surface, the pointer image
is undefined and a client should respond to this event by setting
an appropriate pointer image with the set_cursor request.

<h3 class="decleration event" title="Leave event">
    <a href="?id=OnWlPointer_Leave" id="OnWlPointer_Leave">
        <span class="codicon codicon-symbol-event event"></span>
        WlPointer.<span class="event">OnLeave</span>
    </a>
</h3>

```csharp
void LeaveHandler(uint serial, WlSurface surface)
```

| Argument | Type | Description |
| --- | --- | --- |
| serial | uint | Serial number of the leave event |
| surface | object | Surface left by the pointer |

**Leave event**

Notification that this seat's pointer is no longer focused on
a certain surface.

The leave notification is sent before the enter notification
for the new focus.

<h3 class="decleration event" title="Motion event">
    <a href="?id=OnWlPointer_Motion" id="OnWlPointer_Motion">
        <span class="codicon codicon-symbol-event event"></span>
        WlPointer.<span class="event">OnMotion</span>
    </a>
</h3>

```csharp
void MotionHandler(uint time, WlFixed surfaceX, WlFixed surfaceY)
```

| Argument | Type | Description |
| --- | --- | --- |
| time | uint | Timestamp with millisecond granularity |
| surface_x | fixed | Surface-local x coordinate |
| surface_y | fixed | Surface-local y coordinate |

**Pointer motion event**

Notification of pointer location change. The arguments
surface_x and surface_y are the location relative to the
focused surface.

<h3 class="decleration event" title="Button event">
    <a href="?id=OnWlPointer_Button" id="OnWlPointer_Button">
        <span class="codicon codicon-symbol-event event"></span>
        WlPointer.<span class="event">OnButton</span>
    </a>
</h3>

```csharp
void ButtonHandler(uint serial, uint time, uint button, uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| serial | uint | Serial number of the button event |
| time | uint | Timestamp with millisecond granularity |
| button | uint | Button that produced the event |
| state | uint | Physical state of the button |

**Pointer button event**

Mouse button click and release notifications.

The location of the click is given by the last motion or
enter event.
The time argument is a timestamp with millisecond
granularity, with an undefined base.

The button is a button code as defined in the Linux kernel's
linux/input-event-codes.h header file, e.g. BTN_LEFT.

Any 16-bit button code value is reserved for future additions to the
kernel's event code list. All other button codes above 0xFFFF are
currently undefined but may be used in future versions of this
protocol.

<h3 class="decleration event" title="Axis event">
    <a href="?id=OnWlPointer_Axis" id="OnWlPointer_Axis">
        <span class="codicon codicon-symbol-event event"></span>
        WlPointer.<span class="event">OnAxis</span>
    </a>
</h3>

```csharp
void AxisHandler(uint time, uint axis, WlFixed value)
```

| Argument | Type | Description |
| --- | --- | --- |
| time | uint | Timestamp with millisecond granularity |
| axis | uint | Axis type |
| value | fixed | Length of vector in surface-local coordinate space |

**Axis event**

Scroll and other axis notifications.

For scroll events (vertical and horizontal scroll axes), the
value parameter is the length of a vector along the specified
axis in a coordinate space identical to those of motion events,
representing a relative movement along the specified axis.

For devices that support movements non-parallel to axes multiple
axis events will be emitted.

When applicable, for example for touch pads, the server can
choose to emit scroll events where the motion vector is
equivalent to a motion event vector.

When applicable, a client can transform its content relative to the
scroll distance.

<h3 class="decleration event" title="Frame event">
    <a href="?id=OnWlPointer_Frame" id="OnWlPointer_Frame">
        <span class="codicon codicon-symbol-event event"></span>
        WlPointer.<span class="event">OnFrame</span>
    </a>
    <span class="pill">since 5</span>
</h3>

```csharp
void FrameHandler()
```


**End of a pointer event sequence**

Indicates the end of a set of events that logically belong together.
A client is expected to accumulate the data in all events within the
frame before proceeding.

All wl_pointer events before a wl_pointer.frame event belong
logically together. For example, in a diagonal scroll motion the
compositor will send an optional wl_pointer.axis_source event, two
wl_pointer.axis events (horizontal and vertical) and finally a
wl_pointer.frame event. The client may use this information to
calculate a diagonal vector for scrolling.

When multiple wl_pointer.axis events occur within the same frame,
the motion vector is the combined motion of all events.
When a wl_pointer.axis and a wl_pointer.axis_stop event occur within
the same frame, this indicates that axis movement in one axis has
stopped but continues in the other axis.
When multiple wl_pointer.axis_stop events occur within the same
frame, this indicates that these axes stopped in the same instance.

A wl_pointer.frame event is sent for every logical event group,
even if the group only contains a single wl_pointer event.
Specifically, a client may get a sequence: motion, frame, button,
frame, axis, frame, axis_stop, frame.

The wl_pointer.enter and wl_pointer.leave events are logical events
generated by the compositor and not the hardware. These events are
also grouped by a wl_pointer.frame. When a pointer moves from one
surface to another, a compositor should group the
wl_pointer.leave event within the same wl_pointer.frame.
However, a client must not rely on wl_pointer.leave and
wl_pointer.enter being in the same wl_pointer.frame.
Compositor-specific policies may require the wl_pointer.leave and
wl_pointer.enter event being split across multiple wl_pointer.frame
groups.

<h3 class="decleration event" title="AxisSource event">
    <a href="?id=OnWlPointer_AxisSource" id="OnWlPointer_AxisSource">
        <span class="codicon codicon-symbol-event event"></span>
        WlPointer.<span class="event">OnAxisSource</span>
    </a>
    <span class="pill">since 5</span>
</h3>

```csharp
void AxisSourceHandler(uint axisSource)
```

| Argument | Type | Description |
| --- | --- | --- |
| axis_source | uint | Source of the axis event |

**Axis source event**

Source information for scroll and other axes.

This event does not occur on its own. It is sent before a
wl_pointer.frame event and carries the source information for
all events within that frame.

The source specifies how this event was generated. If the source is
wl_pointer.axis_source.finger, a wl_pointer.axis_stop event will be
sent when the user lifts the finger off the device.

If the source is wl_pointer.axis_source.wheel,
wl_pointer.axis_source.wheel_tilt or
wl_pointer.axis_source.continuous, a wl_pointer.axis_stop event may
or may not be sent. Whether a compositor sends an axis_stop event
for these sources is hardware-specific and implementation-dependent;
clients must not rely on receiving an axis_stop event for these
scroll sources and should treat scroll sequences from these scroll
sources as unterminated by default.

This event is optional. If the source is unknown for a particular
axis event sequence, no event is sent.
Only one wl_pointer.axis_source event is permitted per frame.

The order of wl_pointer.axis_discrete and wl_pointer.axis_source is
not guaranteed.

<h3 class="decleration event" title="AxisStop event">
    <a href="?id=OnWlPointer_AxisStop" id="OnWlPointer_AxisStop">
        <span class="codicon codicon-symbol-event event"></span>
        WlPointer.<span class="event">OnAxisStop</span>
    </a>
    <span class="pill">since 5</span>
</h3>

```csharp
void AxisStopHandler(uint time, uint axis)
```

| Argument | Type | Description |
| --- | --- | --- |
| time | uint | Timestamp with millisecond granularity |
| axis | uint | The axis stopped with this event |

**Axis stop event**

Stop notification for scroll and other axes.

For some wl_pointer.axis_source types, a wl_pointer.axis_stop event
is sent to notify a client that the axis sequence has terminated.
This enables the client to implement kinetic scrolling.
See the wl_pointer.axis_source documentation for information on when
this event may be generated.

Any wl_pointer.axis events with the same axis_source after this
event should be considered as the start of a new axis motion.

The timestamp is to be interpreted identical to the timestamp in the
wl_pointer.axis event. The timestamp value may be the same as a
preceding wl_pointer.axis event.

<h3 class="decleration event" title="AxisDiscrete event">
    <a href="?id=OnWlPointer_AxisDiscrete" id="OnWlPointer_AxisDiscrete">
        <span class="codicon codicon-symbol-event event"></span>
        WlPointer.<span class="event">OnAxisDiscrete</span>
    </a>
    <span class="pill">since 5</span>
</h3>

```csharp
void AxisDiscreteHandler(uint axis, int discrete)
```

| Argument | Type | Description |
| --- | --- | --- |
| axis | uint | Axis type |
| discrete | int | Number of steps |

**Axis click event**

Discrete step information for scroll and other axes.

This event carries the axis value of the wl_pointer.axis event in
discrete steps (e.g. mouse wheel clicks).

This event is deprecated with wl_pointer version 8 - this event is not
sent to clients supporting version 8 or later.

This event does not occur on its own, it is coupled with a
wl_pointer.axis event that represents this axis value on a
continuous scale. The protocol guarantees that each axis_discrete
event is always followed by exactly one axis event with the same
axis number within the same wl_pointer.frame. Note that the protocol
allows for other events to occur between the axis_discrete and
its coupled axis event, including other axis_discrete or axis
events. A wl_pointer.frame must not contain more than one axis_discrete
event per axis type.

This event is optional; continuous scrolling devices
like two-finger scrolling on touchpads do not have discrete
steps and do not generate this event.

The discrete value carries the directional information. e.g. a value
of -2 is two steps towards the negative direction of this axis.

The axis number is identical to the axis number in the associated
axis event.

The order of wl_pointer.axis_discrete and wl_pointer.axis_source is
not guaranteed.

<h3 class="decleration event" title="AxisValue120 event">
    <a href="?id=OnWlPointer_AxisValue120" id="OnWlPointer_AxisValue120">
        <span class="codicon codicon-symbol-event event"></span>
        WlPointer.<span class="event">OnAxisValue120</span>
    </a>
    <span class="pill">since 8</span>
</h3>

```csharp
void AxisValue120Handler(uint axis, int value120)
```

| Argument | Type | Description |
| --- | --- | --- |
| axis | uint | Axis type |
| value120 | int | Scroll distance as fraction of 120 |

**Axis high-resolution scroll event**

Discrete high-resolution scroll information.

This event carries high-resolution wheel scroll information,
with each multiple of 120 representing one logical scroll step
(a wheel detent). For example, an axis_value120 of 30 is one quarter of
a logical scroll step in the positive direction, a value120 of
-240 are two logical scroll steps in the negative direction within the
same hardware event.
Clients that rely on discrete scrolling should accumulate the
value120 to multiples of 120 before processing the event.

The value120 must not be zero.

This event replaces the wl_pointer.axis_discrete event in clients
supporting wl_pointer version 8 or later.

Where a wl_pointer.axis_source event occurs in the same
wl_pointer.frame, the axis source applies to this event.

The order of wl_pointer.axis_value120 and wl_pointer.axis_source is
not guaranteed.

<h3 class="decleration event" title="AxisRelativeDirection event">
    <a href="?id=OnWlPointer_AxisRelativeDirection" id="OnWlPointer_AxisRelativeDirection">
        <span class="codicon codicon-symbol-event event"></span>
        WlPointer.<span class="event">OnAxisRelativeDirection</span>
    </a>
    <span class="pill">since 9</span>
</h3>

```csharp
void AxisRelativeDirectionHandler(uint axis, uint direction)
```

| Argument | Type | Description |
| --- | --- | --- |
| axis | uint | Axis type |
| direction | uint | Physical direction relative to axis motion |

**Axis relative physical direction event**

Relative directional information of the entity causing the axis
motion.

For a wl_pointer.axis event, the wl_pointer.axis_relative_direction
event specifies the movement direction of the entity causing the
wl_pointer.axis event. For example:
- if a user's fingers on a touchpad move down and this
causes a wl_pointer.axis vertical_scroll down event, the physical
direction is 'identical'
- if a user's fingers on a touchpad move down and this causes a
wl_pointer.axis vertical_scroll up scroll up event ('natural
scrolling'), the physical direction is 'inverted'.

A client may use this information to adjust scroll motion of
components. Specifically, enabling natural scrolling causes the
content to change direction compared to traditional scrolling.
Some widgets like volume control sliders should usually match the
physical direction regardless of whether natural scrolling is
active. This event enables clients to match the scroll direction of
a widget to the physical direction.

This event does not occur on its own, it is coupled with a
wl_pointer.axis event that represents this axis value.
The protocol guarantees that each axis_relative_direction event is
always followed by exactly one axis event with the same
axis number within the same wl_pointer.frame. Note that the protocol
allows for other events to occur between the axis_relative_direction
and its coupled axis event.

The axis number is identical to the axis number in the associated
axis event.

The order of wl_pointer.axis_relative_direction,
wl_pointer.axis_discrete and wl_pointer.axis_source is not
guaranteed.

<h2 class="decleration interface">
    <a href="?id=WlKeyboard" id="WlKeyboard">
        <span class="codicon codicon-symbol-interface"></span>
        WlKeyboard
    </a>
    <span class="pill">version 10</span>
</h2>

Keyboard input device


The wl_keyboard interface represents one or more keyboards
associated with a seat.

Each wl_keyboard has the following logical state:

- an active surface (possibly null),
- the keys currently logically down,
- the active modifiers,
- the active group.

By default, the active surface is null, the keys currently logically down
are empty, the active modifiers and the active group are 0.


<h3 class="decleration request" title="Release request">
    <a href="?id=WlKeyboard_Release" id="WlKeyboard_Release">
        <span class="codicon codicon-symbol-method method"></span>
        WlKeyboard.<span class="method">Release</span>
    </a>
    <span class="pill">since 3</span>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Release()
```


**Release the keyboard object**
release
<h3 class="decleration event" title="Keymap event">
    <a href="?id=OnWlKeyboard_Keymap" id="OnWlKeyboard_Keymap">
        <span class="codicon codicon-symbol-event event"></span>
        WlKeyboard.<span class="event">OnKeymap</span>
    </a>
</h3>

```csharp
void KeymapHandler(uint format, int fd, uint size)
```

| Argument | Type | Description |
| --- | --- | --- |
| format | uint | Keymap format |
| fd | fd | Keymap file descriptor |
| size | uint | Keymap size, in bytes |

**Keyboard mapping**

This event provides a file descriptor to the client which can be
memory-mapped in read-only mode to provide a keyboard mapping
description.

From version 7 onwards, the fd must be mapped with MAP_PRIVATE by
the recipient, as MAP_SHARED may fail.

<h3 class="decleration event" title="Enter event">
    <a href="?id=OnWlKeyboard_Enter" id="OnWlKeyboard_Enter">
        <span class="codicon codicon-symbol-event event"></span>
        WlKeyboard.<span class="event">OnEnter</span>
    </a>
</h3>

```csharp
void EnterHandler(uint serial, WlSurface surface, byte[] keys)
```

| Argument | Type | Description |
| --- | --- | --- |
| serial | uint | Serial number of the enter event |
| surface | object | Surface gaining keyboard focus |
| keys | array | The keys currently logically down |

**Enter event**

Notification that this seat's keyboard focus is on a certain
surface.

The compositor must send the wl_keyboard.modifiers event after this
event.

In the wl_keyboard logical state, this event sets the active surface to
the surface argument and the keys currently logically down to the keys
in the keys argument. The compositor must not send this event if the
wl_keyboard already had an active surface immediately before this event.

Clients should not use the list of pressed keys to emulate key-press
events. The order of keys in the list is unspecified.

<h3 class="decleration event" title="Leave event">
    <a href="?id=OnWlKeyboard_Leave" id="OnWlKeyboard_Leave">
        <span class="codicon codicon-symbol-event event"></span>
        WlKeyboard.<span class="event">OnLeave</span>
    </a>
</h3>

```csharp
void LeaveHandler(uint serial, WlSurface surface)
```

| Argument | Type | Description |
| --- | --- | --- |
| serial | uint | Serial number of the leave event |
| surface | object | Surface that lost keyboard focus |

**Leave event**

Notification that this seat's keyboard focus is no longer on
a certain surface.

The leave notification is sent before the enter notification
for the new focus.

In the wl_keyboard logical state, this event resets all values to their
defaults. The compositor must not send this event if the active surface
of the wl_keyboard was not equal to the surface argument immediately
before this event.

<h3 class="decleration event" title="Key event">
    <a href="?id=OnWlKeyboard_Key" id="OnWlKeyboard_Key">
        <span class="codicon codicon-symbol-event event"></span>
        WlKeyboard.<span class="event">OnKey</span>
    </a>
</h3>

```csharp
void KeyHandler(uint serial, uint time, uint key, uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| serial | uint | Serial number of the key event |
| time | uint | Timestamp with millisecond granularity |
| key | uint | Key that produced the event |
| state | uint | Physical state of the key |

**Key event**

A key was pressed or released.
The time argument is a timestamp with millisecond
granularity, with an undefined base.

The key is a platform-specific key code that can be interpreted
by feeding it to the keyboard mapping (see the keymap event).

If this event produces a change in modifiers, then the resulting
wl_keyboard.modifiers event must be sent after this event.

In the wl_keyboard logical state, this event adds the key to the keys
currently logically down (if the state argument is pressed) or removes
the key from the keys currently logically down (if the state argument is
released). The compositor must not send this event if the wl_keyboard
did not have an active surface immediately before this event. The
compositor must not send this event if state is pressed (resp. released)
and the key was already logically down (resp. was not logically down)
immediately before this event.

Since version 10, compositors may send key events with the "repeated"
key state when a wl_keyboard.repeat_info event with a rate argument of
0 has been received. This allows the compositor to take over the
responsibility of key repetition.

<h3 class="decleration event" title="Modifiers event">
    <a href="?id=OnWlKeyboard_Modifiers" id="OnWlKeyboard_Modifiers">
        <span class="codicon codicon-symbol-event event"></span>
        WlKeyboard.<span class="event">OnModifiers</span>
    </a>
</h3>

```csharp
void ModifiersHandler(uint serial, uint modsDepressed, uint modsLatched, uint modsLocked, uint group)
```

| Argument | Type | Description |
| --- | --- | --- |
| serial | uint | Serial number of the modifiers event |
| mods_depressed | uint | Depressed modifiers |
| mods_latched | uint | Latched modifiers |
| mods_locked | uint | Locked modifiers |
| group | uint | Keyboard layout |

**Modifier and group state**

Notifies clients that the modifier and/or group state has
changed, and it should update its local state.

The compositor may send this event without a surface of the client
having keyboard focus, for example to tie modifier information to
pointer focus instead. If a modifier event with pressed modifiers is sent
without a prior enter event, the client can assume the modifier state is
valid until it receives the next wl_keyboard.modifiers event. In order to
reset the modifier state again, the compositor can send a
wl_keyboard.modifiers event with no pressed modifiers.

In the wl_keyboard logical state, this event updates the modifiers and
group.

<h3 class="decleration event" title="RepeatInfo event">
    <a href="?id=OnWlKeyboard_RepeatInfo" id="OnWlKeyboard_RepeatInfo">
        <span class="codicon codicon-symbol-event event"></span>
        WlKeyboard.<span class="event">OnRepeatInfo</span>
    </a>
    <span class="pill">since 4</span>
</h3>

```csharp
void RepeatInfoHandler(int rate, int delay)
```

| Argument | Type | Description |
| --- | --- | --- |
| rate | int | The rate of repeating keys in characters per second |
| delay | int | Delay in milliseconds since key down until repeating starts |

**Repeat rate and delay**

Informs the client about the keyboard's repeat rate and delay.

This event is sent as soon as the wl_keyboard object has been created,
and is guaranteed to be received by the client before any key press
event.

Negative values for either rate or delay are illegal. A rate of zero
will disable any repeating (regardless of the value of delay).

This event can be sent later on as well with a new value if necessary,
so clients should continue listening for the event past the creation
of wl_keyboard.

<h2 class="decleration interface">
    <a href="?id=WlTouch" id="WlTouch">
        <span class="codicon codicon-symbol-interface"></span>
        WlTouch
    </a>
    <span class="pill">version 10</span>
</h2>

Touchscreen input device


The wl_touch interface represents a touchscreen
associated with a seat.

Touch interactions can consist of one or more contacts.
For each contact, a series of events is generated, starting
with a down event, followed by zero or more motion events,
and ending with an up event. Events relating to the same
contact point can be identified by the ID of the sequence.


<h3 class="decleration request" title="Release request">
    <a href="?id=WlTouch_Release" id="WlTouch_Release">
        <span class="codicon codicon-symbol-method method"></span>
        WlTouch.<span class="method">Release</span>
    </a>
    <span class="pill">since 3</span>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Release()
```


**Release the touch object**
release
<h3 class="decleration event" title="Down event">
    <a href="?id=OnWlTouch_Down" id="OnWlTouch_Down">
        <span class="codicon codicon-symbol-event event"></span>
        WlTouch.<span class="event">OnDown</span>
    </a>
</h3>

```csharp
void DownHandler(uint serial, uint time, WlSurface surface, int id, WlFixed x, WlFixed y)
```

| Argument | Type | Description |
| --- | --- | --- |
| serial | uint | Serial number of the touch down event |
| time | uint | Timestamp with millisecond granularity |
| surface | object | Surface touched |
| id | int | The unique ID of this touch point |
| x | fixed | Surface-local x coordinate |
| y | fixed | Surface-local y coordinate |

**Touch down event and beginning of a touch sequence**

A new touch point has appeared on the surface. This touch point is
assigned a unique ID. Future events from this touch point reference
this ID. The ID ceases to be valid after a touch up event and may be
reused in the future.

<h3 class="decleration event" title="Up event">
    <a href="?id=OnWlTouch_Up" id="OnWlTouch_Up">
        <span class="codicon codicon-symbol-event event"></span>
        WlTouch.<span class="event">OnUp</span>
    </a>
</h3>

```csharp
void UpHandler(uint serial, uint time, int id)
```

| Argument | Type | Description |
| --- | --- | --- |
| serial | uint | Serial number of the touch up event |
| time | uint | Timestamp with millisecond granularity |
| id | int | The unique ID of this touch point |

**End of a touch event sequence**

The touch point has disappeared. No further events will be sent for
this touch point and the touch point's ID is released and may be
reused in a future touch down event.

<h3 class="decleration event" title="Motion event">
    <a href="?id=OnWlTouch_Motion" id="OnWlTouch_Motion">
        <span class="codicon codicon-symbol-event event"></span>
        WlTouch.<span class="event">OnMotion</span>
    </a>
</h3>

```csharp
void MotionHandler(uint time, int id, WlFixed x, WlFixed y)
```

| Argument | Type | Description |
| --- | --- | --- |
| time | uint | Timestamp with millisecond granularity |
| id | int | The unique ID of this touch point |
| x | fixed | Surface-local x coordinate |
| y | fixed | Surface-local y coordinate |

**Update of touch point coordinates**

A touch point has changed coordinates.

<h3 class="decleration event" title="Frame event">
    <a href="?id=OnWlTouch_Frame" id="OnWlTouch_Frame">
        <span class="codicon codicon-symbol-event event"></span>
        WlTouch.<span class="event">OnFrame</span>
    </a>
</h3>

```csharp
void FrameHandler()
```


**End of touch frame event**

Indicates the end of a set of events that logically belong together.
A client is expected to accumulate the data in all events within the
frame before proceeding.

A wl_touch.frame terminates at least one event but otherwise no
guarantee is provided about the set of events within a frame. A client
must assume that any state not updated in a frame is unchanged from the
previously known state.

<h3 class="decleration event" title="Cancel event">
    <a href="?id=OnWlTouch_Cancel" id="OnWlTouch_Cancel">
        <span class="codicon codicon-symbol-event event"></span>
        WlTouch.<span class="event">OnCancel</span>
    </a>
</h3>

```csharp
void CancelHandler()
```


**Touch session cancelled**

Sent if the compositor decides the touch stream is a global
gesture. No further events are sent to the clients from that
particular gesture. Touch cancellation applies to all touch points
currently active on this client's surface. The client is
responsible for finalizing the touch points, future touch points on
this surface may reuse the touch point ID.

No frame event is required after the cancel event.

<h3 class="decleration event" title="Shape event">
    <a href="?id=OnWlTouch_Shape" id="OnWlTouch_Shape">
        <span class="codicon codicon-symbol-event event"></span>
        WlTouch.<span class="event">OnShape</span>
    </a>
    <span class="pill">since 6</span>
</h3>

```csharp
void ShapeHandler(int id, WlFixed major, WlFixed minor)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | int | The unique ID of this touch point |
| major | fixed | Length of the major axis in surface-local coordinates |
| minor | fixed | Length of the minor axis in surface-local coordinates |

**Update shape of touch point**

Sent when a touchpoint has changed its shape.

This event does not occur on its own. It is sent before a
wl_touch.frame event and carries the new shape information for
any previously reported, or new touch points of that frame.

Other events describing the touch point such as wl_touch.down,
wl_touch.motion or wl_touch.orientation may be sent within the
same wl_touch.frame. A client should treat these events as a single
logical touch point update. The order of wl_touch.shape,
wl_touch.orientation and wl_touch.motion is not guaranteed.
A wl_touch.down event is guaranteed to occur before the first
wl_touch.shape event for this touch ID but both events may occur within
the same wl_touch.frame.

A touchpoint shape is approximated by an ellipse through the major and
minor axis length. The major axis length describes the longer diameter
of the ellipse, while the minor axis length describes the shorter
diameter. Major and minor are orthogonal and both are specified in
surface-local coordinates. The center of the ellipse is always at the
touchpoint location as reported by wl_touch.down or wl_touch.move.

This event is only sent by the compositor if the touch device supports
shape reports. The client has to make reasonable assumptions about the
shape if it did not receive this event.

<h3 class="decleration event" title="Orientation event">
    <a href="?id=OnWlTouch_Orientation" id="OnWlTouch_Orientation">
        <span class="codicon codicon-symbol-event event"></span>
        WlTouch.<span class="event">OnOrientation</span>
    </a>
    <span class="pill">since 6</span>
</h3>

```csharp
void OrientationHandler(int id, WlFixed orientation)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | int | The unique ID of this touch point |
| orientation | fixed | Angle between major axis and positive surface y-axis in degrees |

**Update orientation of touch point**

Sent when a touchpoint has changed its orientation.

This event does not occur on its own. It is sent before a
wl_touch.frame event and carries the new shape information for
any previously reported, or new touch points of that frame.

Other events describing the touch point such as wl_touch.down,
wl_touch.motion or wl_touch.shape may be sent within the
same wl_touch.frame. A client should treat these events as a single
logical touch point update. The order of wl_touch.shape,
wl_touch.orientation and wl_touch.motion is not guaranteed.
A wl_touch.down event is guaranteed to occur before the first
wl_touch.orientation event for this touch ID but both events may occur
within the same wl_touch.frame.

The orientation describes the clockwise angle of a touchpoint's major
axis to the positive surface y-axis and is normalized to the -180 to
+180 degree range. The granularity of orientation depends on the touch
device, some devices only support binary rotation values between 0 and
90 degrees.

This event is only sent by the compositor if the touch device supports
orientation reports.

<h2 class="decleration interface">
    <a href="?id=WlOutput" id="WlOutput">
        <span class="codicon codicon-symbol-interface"></span>
        WlOutput
    </a>
    <span class="pill">version 4</span>
</h2>

Compositor output region


An output describes part of the compositor geometry.  The
compositor works in the 'compositor coordinate system' and an
output corresponds to a rectangular area in that space that is
actually visible.  This typically corresponds to a monitor that
displays part of the compositor space.  This object is published
as global during start up, or when a monitor is hotplugged.


<h3 class="decleration request" title="Release request">
    <a href="?id=WlOutput_Release" id="WlOutput_Release">
        <span class="codicon codicon-symbol-method method"></span>
        WlOutput.<span class="method">Release</span>
    </a>
    <span class="pill">since 3</span>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Release()
```


**Release the output object**

Using this request a client can tell the server that it is not going to
use the output object anymore.

<h3 class="decleration event" title="Geometry event">
    <a href="?id=OnWlOutput_Geometry" id="OnWlOutput_Geometry">
        <span class="codicon codicon-symbol-event event"></span>
        WlOutput.<span class="event">OnGeometry</span>
    </a>
</h3>

```csharp
void GeometryHandler(int x, int y, int physicalWidth, int physicalHeight, int subpixel, string make, string model, int transform)
```

| Argument | Type | Description |
| --- | --- | --- |
| x | int | X position within the global compositor space |
| y | int | Y position within the global compositor space |
| physical_width | int | Width in millimeters of the output |
| physical_height | int | Height in millimeters of the output |
| subpixel | int | Subpixel orientation of the output |
| make | string | Textual description of the manufacturer |
| model | string | Textual description of the model |
| transform | int | Additional transformation applied to buffer contents during presentation |

**Properties of the output**

The geometry event describes geometric properties of the output.
The event is sent when binding to the output object and whenever
any of the properties change.

The physical size can be set to zero if it doesn't make sense for this
output (e.g. for projectors or virtual outputs).

The geometry event will be followed by a done event (starting from
version 2).

Clients should use wl_surface.preferred_buffer_transform instead of the
transform advertised by this event to find the preferred buffer
transform to use for a surface.

Note: wl_output only advertises partial information about the output
position and identification. Some compositors, for instance those not
implementing a desktop-style output layout or those exposing virtual
outputs, might fake this information. Instead of using x and y, clients
should use xdg_output.logical_position. Instead of using make and model,
clients should use name and description.

<h3 class="decleration event" title="Mode event">
    <a href="?id=OnWlOutput_Mode" id="OnWlOutput_Mode">
        <span class="codicon codicon-symbol-event event"></span>
        WlOutput.<span class="event">OnMode</span>
    </a>
</h3>

```csharp
void ModeHandler(uint flags, int width, int height, int refresh)
```

| Argument | Type | Description |
| --- | --- | --- |
| flags | uint | Bitfield of mode flags |
| width | int | Width of the mode in hardware units |
| height | int | Height of the mode in hardware units |
| refresh | int | Vertical refresh rate in mHz |

**Advertise available modes for the output**

The mode event describes an available mode for the output.

The event is sent when binding to the output object and there
will always be one mode, the current mode.  The event is sent
again if an output changes mode, for the mode that is now
current.  In other words, the current mode is always the last
mode that was received with the current flag set.

Non-current modes are deprecated. A compositor can decide to only
advertise the current mode and never send other modes. Clients
should not rely on non-current modes.

The size of a mode is given in physical hardware units of
the output device. This is not necessarily the same as
the output size in the global compositor space. For instance,
the output may be scaled, as described in wl_output.scale,
or transformed, as described in wl_output.transform. Clients
willing to retrieve the output size in the global compositor
space should use xdg_output.logical_size instead.

The vertical refresh rate can be set to zero if it doesn't make
sense for this output (e.g. for virtual outputs).

The mode event will be followed by a done event (starting from
version 2).

Clients should not use the refresh rate to schedule frames. Instead,
they should use the wl_surface.frame event or the presentation-time
protocol.

Note: this information is not always meaningful for all outputs. Some
compositors, such as those exposing virtual outputs, might fake the
refresh rate or the size.

<h3 class="decleration event" title="Done event">
    <a href="?id=OnWlOutput_Done" id="OnWlOutput_Done">
        <span class="codicon codicon-symbol-event event"></span>
        WlOutput.<span class="event">OnDone</span>
    </a>
    <span class="pill">since 2</span>
</h3>

```csharp
void DoneHandler()
```


**Sent all information about output**

This event is sent after all other properties have been
sent after binding to the output object and after any
other property changes done after that. This allows
changes to the output properties to be seen as
atomic, even if they happen via multiple events.

<h3 class="decleration event" title="Scale event">
    <a href="?id=OnWlOutput_Scale" id="OnWlOutput_Scale">
        <span class="codicon codicon-symbol-event event"></span>
        WlOutput.<span class="event">OnScale</span>
    </a>
    <span class="pill">since 2</span>
</h3>

```csharp
void ScaleHandler(int factor)
```

| Argument | Type | Description |
| --- | --- | --- |
| factor | int | Scaling factor of output |

**Output scaling properties**

This event contains scaling geometry information
that is not in the geometry event. It may be sent after
binding the output object or if the output scale changes
later. The compositor will emit a non-zero, positive
value for scale. If it is not sent, the client should
assume a scale of 1.

A scale larger than 1 means that the compositor will
automatically scale surface buffers by this amount
when rendering. This is used for very high resolution
displays where applications rendering at the native
resolution would be too small to be legible.

Clients should use wl_surface.preferred_buffer_scale
instead of this event to find the preferred buffer
scale to use for a surface.

The scale event will be followed by a done event.

<h3 class="decleration event" title="Name event">
    <a href="?id=OnWlOutput_Name" id="OnWlOutput_Name">
        <span class="codicon codicon-symbol-event event"></span>
        WlOutput.<span class="event">OnName</span>
    </a>
    <span class="pill">since 4</span>
</h3>

```csharp
void NameHandler(string name)
```

| Argument | Type | Description |
| --- | --- | --- |
| name | string | Output name |

**Name of this output**

Many compositors will assign user-friendly names to their outputs, show
them to the user, allow the user to refer to an output, etc. The client
may wish to know this name as well to offer the user similar behaviors.

The name is a UTF-8 string with no convention defined for its contents.
Each name is unique among all wl_output globals. The name is only
guaranteed to be unique for the compositor instance.

The same output name is used for all clients for a given wl_output
global. Thus, the name can be shared across processes to refer to a
specific wl_output global.

The name is not guaranteed to be persistent across sessions, thus cannot
be used to reliably identify an output in e.g. configuration files.

Examples of names include 'HDMI-A-1', 'WL-1', 'X11-1', etc. However, do
not assume that the name is a reflection of an underlying DRM connector,
X11 connection, etc.

The name event is sent after binding the output object. This event is
only sent once per output object, and the name does not change over the
lifetime of the wl_output global.

Compositors may re-use the same output name if the wl_output global is
destroyed and re-created later. Compositors should avoid re-using the
same name if possible.

The name event will be followed by a done event.

<h3 class="decleration event" title="Description event">
    <a href="?id=OnWlOutput_Description" id="OnWlOutput_Description">
        <span class="codicon codicon-symbol-event event"></span>
        WlOutput.<span class="event">OnDescription</span>
    </a>
    <span class="pill">since 4</span>
</h3>

```csharp
void DescriptionHandler(string description)
```

| Argument | Type | Description |
| --- | --- | --- |
| description | string | Output description |

**Human-readable description of this output**

Many compositors can produce human-readable descriptions of their
outputs. The client may wish to know this description as well, e.g. for
output selection purposes.

The description is a UTF-8 string with no convention defined for its
contents. The description is not guaranteed to be unique among all
wl_output globals. Examples might include 'Foocorp 11" Display' or
'Virtual X11 output via :1'.

The description event is sent after binding the output object and
whenever the description changes. The description is optional, and may
not be sent at all.

The description event will be followed by a done event.

<h2 class="decleration interface">
    <a href="?id=WlRegion" id="WlRegion">
        <span class="codicon codicon-symbol-interface"></span>
        WlRegion
    </a>
    <span class="pill">version 1</span>
</h2>

Region interface


A region object describes an area.

Region objects are used to describe the opaque and input
regions of a surface.


<h3 class="decleration request" title="Destroy request">
    <a href="?id=WlRegion_Destroy" id="WlRegion_Destroy">
        <span class="codicon codicon-symbol-method method"></span>
        WlRegion.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy region**

Destroy the region.  This will invalidate the object ID.

<h3 class="decleration request" title="Add request">
    <a href="?id=WlRegion_Add" id="WlRegion_Add">
        <span class="codicon codicon-symbol-method method"></span>
        WlRegion.<span class="method">Add</span>
    </a>
</h3>

```csharp
void Add(int x, int y, int width, int height)
```

| Argument | Type | Description |
| --- | --- | --- |
| x | int | Region-local x coordinate |
| y | int | Region-local y coordinate |
| width | int | Rectangle width |
| height | int | Rectangle height |

**Add rectangle to region**

Add the specified rectangle to the region.

<h3 class="decleration request" title="Subtract request">
    <a href="?id=WlRegion_Subtract" id="WlRegion_Subtract">
        <span class="codicon codicon-symbol-method method"></span>
        WlRegion.<span class="method">Subtract</span>
    </a>
</h3>

```csharp
void Subtract(int x, int y, int width, int height)
```

| Argument | Type | Description |
| --- | --- | --- |
| x | int | Region-local x coordinate |
| y | int | Region-local y coordinate |
| width | int | Rectangle width |
| height | int | Rectangle height |

**Subtract rectangle from region**

Subtract the specified rectangle from the region.

<h2 class="decleration interface">
    <a href="?id=WlSubcompositor" id="WlSubcompositor">
        <span class="codicon codicon-symbol-interface"></span>
        WlSubcompositor
    </a>
    <span class="pill">version 1</span>
</h2>

Sub-surface compositing


The global interface exposing sub-surface compositing capabilities.
A wl_surface, that has sub-surfaces associated, is called the
parent surface. Sub-surfaces can be arbitrarily nested and create
a tree of sub-surfaces.

The root surface in a tree of sub-surfaces is the main
surface. The main surface cannot be a sub-surface, because
sub-surfaces must always have a parent.

A main surface with its sub-surfaces forms a (compound) window.
For window management purposes, this set of wl_surface objects is
to be considered as a single window, and it should also behave as
such.

The aim of sub-surfaces is to offload some of the compositing work
within a window from clients to the compositor. A prime example is
a video player with decorations and video in separate wl_surface
objects. This should allow the compositor to pass YUV video buffer
processing to dedicated overlay hardware when possible.


<h3 class="decleration request" title="Destroy request">
    <a href="?id=WlSubcompositor_Destroy" id="WlSubcompositor_Destroy">
        <span class="codicon codicon-symbol-method method"></span>
        WlSubcompositor.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Unbind from the subcompositor interface**

Informs the server that the client will not be using this
protocol object anymore. This does not affect any other
objects, wl_subsurface objects included.

<h3 class="decleration request" title="GetSubsurface request">
    <a href="?id=WlSubcompositor_GetSubsurface" id="WlSubcompositor_GetSubsurface">
        <span class="codicon codicon-symbol-method method"></span>
        WlSubcompositor.<span class="method">GetSubsurface</span>
    </a>
</h3>

```csharp
WlSubsurface GetSubsurface(WlSurface surface, WlSurface parent)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id | The new sub-surface object ID |
| surface | object | The surface to be turned into a sub-surface |
| parent | object | The parent surface |

**Give a surface the role sub-surface**

Create a sub-surface interface for the given surface, and
associate it with the given parent surface. This turns a
plain wl_surface into a sub-surface.

The to-be sub-surface must not already have another role, and it
must not have an existing wl_subsurface object. Otherwise the
bad_surface protocol error is raised.

Adding sub-surfaces to a parent is a double-buffered operation on the
parent (see wl_surface.commit). The effect of adding a sub-surface
becomes visible on the next time the state of the parent surface is
applied.

The parent surface must not be one of the child surface's descendants,
and the parent must be different from the child surface, otherwise the
bad_parent protocol error is raised.

This request modifies the behaviour of wl_surface.commit request on
the sub-surface, see the documentation on wl_subsurface interface.

<h2 class="decleration interface">
    <a href="?id=WlSubsurface" id="WlSubsurface">
        <span class="codicon codicon-symbol-interface"></span>
        WlSubsurface
    </a>
    <span class="pill">version 1</span>
</h2>

Sub-surface interface to a wl_surface


An additional interface to a wl_surface object, which has been
made a sub-surface. A sub-surface has one parent surface. A
sub-surface's size and position are not limited to that of the parent.
Particularly, a sub-surface is not automatically clipped to its
parent's area.

A sub-surface becomes mapped, when a non-NULL wl_buffer is applied
and the parent surface is mapped. The order of which one happens
first is irrelevant. A sub-surface is hidden if the parent becomes
hidden, or if a NULL wl_buffer is applied. These rules apply
recursively through the tree of surfaces.

A sub-surface can be in one of two modes. The possible modes are
synchronized and desynchronized, see methods wl_subsurface.set_sync and
wl_subsurface.set_desync.

The main surface can be thought to be always in desynchronized mode,
since it does not have a parent in the sub-surfaces sense.

Even if a sub-surface is in desynchronized mode, it will behave as
in synchronized mode, if its parent surface behaves as in
synchronized mode. This rule is applied recursively throughout the
tree of surfaces. This means, that one can set a sub-surface into
synchronized mode, and then assume that all its child and grand-child
sub-surfaces are synchronized, too, without explicitly setting them.

If a surface behaves as in synchronized mode, it is effectively
synchronized, otherwise it is effectively desynchronized.

A sub-surface is initially in the synchronized mode.

The wl_subsurface interface has requests which modify double-buffered
state of the parent surface (wl_subsurface.set_position, .place_above and
.place_below).

Destroying a sub-surface takes effect immediately. If you need to
synchronize the removal of a sub-surface to the parent surface update,
unmap the sub-surface first by attaching a NULL wl_buffer, update parent,
and then destroy the sub-surface.

If the parent wl_surface object is destroyed, the sub-surface is
unmapped.

A sub-surface never has the keyboard focus of any seat.

The wl_surface.offset request is ignored: clients must use set_position
instead to move the sub-surface.


<h3 class="decleration request" title="Destroy request">
    <a href="?id=WlSubsurface_Destroy" id="WlSubsurface_Destroy">
        <span class="codicon codicon-symbol-method method"></span>
        WlSubsurface.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Remove sub-surface interface**

The sub-surface interface is removed from the wl_surface object
that was turned into a sub-surface with a
wl_subcompositor.get_subsurface request. The wl_surface's association
to the parent is deleted. The wl_surface is unmapped immediately.

<h3 class="decleration request" title="SetPosition request">
    <a href="?id=WlSubsurface_SetPosition" id="WlSubsurface_SetPosition">
        <span class="codicon codicon-symbol-method method"></span>
        WlSubsurface.<span class="method">SetPosition</span>
    </a>
</h3>

```csharp
void SetPosition(int x, int y)
```

| Argument | Type | Description |
| --- | --- | --- |
| x | int | X coordinate in the parent surface |
| y | int | Y coordinate in the parent surface |

**Reposition the sub-surface**

This sets the position of the sub-surface, relative to the parent
surface.

The sub-surface will be moved so that its origin (top left
corner pixel) will be at the location x, y of the parent surface
coordinate system. The coordinates are not restricted to the parent
surface area. Negative values are allowed.

The initial position is 0, 0.

Position is double-buffered state on the parent surface, see
wl_subsurface and wl_surface.commit for more information.

<h3 class="decleration request" title="PlaceAbove request">
    <a href="?id=WlSubsurface_PlaceAbove" id="WlSubsurface_PlaceAbove">
        <span class="codicon codicon-symbol-method method"></span>
        WlSubsurface.<span class="method">PlaceAbove</span>
    </a>
</h3>

```csharp
void PlaceAbove(WlSurface sibling)
```

| Argument | Type | Description |
| --- | --- | --- |
| sibling | object | The reference surface |

**Restack the sub-surface**

This sub-surface is taken from the stack, and put back just
above the reference surface, changing the z-order of the sub-surfaces.
The reference surface must be one of the sibling surfaces, or the
parent surface. Using any other surface, including this sub-surface,
will cause a protocol error.

A new sub-surface is initially added as the top-most in the stack
of its siblings and parent.

Z-order is double-buffered state on the parent surface, see
wl_subsurface and wl_surface.commit for more information.

<h3 class="decleration request" title="PlaceBelow request">
    <a href="?id=WlSubsurface_PlaceBelow" id="WlSubsurface_PlaceBelow">
        <span class="codicon codicon-symbol-method method"></span>
        WlSubsurface.<span class="method">PlaceBelow</span>
    </a>
</h3>

```csharp
void PlaceBelow(WlSurface sibling)
```

| Argument | Type | Description |
| --- | --- | --- |
| sibling | object | The reference surface |

**Restack the sub-surface**

The sub-surface is placed just below the reference surface.

See wl_subsurface.place_above.

<h3 class="decleration request" title="SetSync request">
    <a href="?id=WlSubsurface_SetSync" id="WlSubsurface_SetSync">
        <span class="codicon codicon-symbol-method method"></span>
        WlSubsurface.<span class="method">SetSync</span>
    </a>
</h3>

```csharp
void SetSync()
```


**Set sub-surface to synchronized mode**

Change the commit behaviour of the sub-surface to synchronized
mode.

See wl_subsurface and wl_surface.commit for more information.

<h3 class="decleration request" title="SetDesync request">
    <a href="?id=WlSubsurface_SetDesync" id="WlSubsurface_SetDesync">
        <span class="codicon codicon-symbol-method method"></span>
        WlSubsurface.<span class="method">SetDesync</span>
    </a>
</h3>

```csharp
void SetDesync()
```


**Set sub-surface to desynchronized mode**

Change the commit behaviour of the sub-surface to desynchronized
mode.

See wl_subsurface and wl_surface.commit for more information.

<h2 class="decleration interface">
    <a href="?id=WlFixes" id="WlFixes">
        <span class="codicon codicon-symbol-interface"></span>
        WlFixes
    </a>
    <span class="pill">version 1</span>
</h2>

Wayland protocol fixes


This global fixes problems with other core-protocol interfaces that
cannot be fixed in these interfaces themselves.


<h3 class="decleration request" title="Destroy request">
    <a href="?id=WlFixes_Destroy" id="WlFixes_Destroy">
        <span class="codicon codicon-symbol-method method"></span>
        WlFixes.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroys this object**
destroy
<h3 class="decleration request" title="DestroyRegistry request">
    <a href="?id=WlFixes_DestroyRegistry" id="WlFixes_DestroyRegistry">
        <span class="codicon codicon-symbol-method method"></span>
        WlFixes.<span class="method">DestroyRegistry</span>
    </a>
</h3>

```csharp
void DestroyRegistry(WlRegistry registry)
```

| Argument | Type | Description |
| --- | --- | --- |
| registry | object | The registry to destroy |

**Destroy a wl_registry**

This request destroys a wl_registry object.

The client should no longer use the wl_registry after making this
request.

The compositor will emit a wl_display.delete_id event with the object ID
of the registry and will no longer emit any events on the registry. The
client should re-use the object ID once it receives the
wl_display.delete_id event.

