# Workspace Extension

##### [WaylandDotnet](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet) ![](../../../assets/arrow.svg ':class=breadcrumb-arrow') [Staging](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/Staging) ![](../../../assets/arrow.svg ':class=breadcrumb-arrow') [ExtWorkspaceV1](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/Staging/ext-workspace-v1/)

---

<h2 class="decleration interface">
    <a href="#/Protocols/Staging/ext-workspace-v1/?id=extworkspacemanagerv1" id="extworkspacemanagerv1">
        <span class="codicon codicon-symbol-interface"></span>
        ExtWorkspaceManagerV1
    </a>
    <span class="pill">version 1</span>
</h2>

List and control workspaces


Workspaces, also called virtual desktops, are groups of surfaces. A
compositor with a concept of workspaces may only show some such groups of
surfaces (those of 'active' workspaces) at a time. 'Activating' a
workspace is a request for the compositor to display that workspace's
surfaces as normal, whereas the compositor may hide or otherwise
de-emphasise surfaces that are associated only with 'inactive' workspaces.
Workspaces are grouped by which sets of outputs they correspond to, and
may contain surfaces only from those outputs. In this way, it is possible
for each output to have its own set of workspaces, or for all outputs (or
any other arbitrary grouping) to share workspaces. Compositors may
optionally conceptually arrange each group of workspaces in an
N-dimensional grid.

The purpose of this protocol is to enable the creation of taskbars and
docks by providing them with a list of workspaces and their properties,
and allowing them to activate and deactivate workspaces.

After a client binds the ext_workspace_manager_v1, each workspace will be
sent via the workspace event.


<h3 class="decleration request" title="Commit request">
    <a href="#/Protocols/Staging/ext-workspace-v1/?id=extworkspacemanagerv1_commit" id="extworkspacemanagerv1_commit">
        <span class="codicon codicon-symbol-method method"></span>
        ExtWorkspaceManagerV1.<span class="method">Commit</span>
    </a>
</h3>

```csharp
void Commit()
```


**All requests about the workspaces have been sent**

The client must send this request after it has finished sending other
requests. The compositor must process a series of requests preceding a
commit request atomically.

This allows changes to the workspace properties to be seen as atomic,
even if they happen via multiple events, and even if they involve
multiple ext_workspace_handle_v1 objects, for example, deactivating one
workspace and activating another.

<h3 class="decleration request" title="Stop request">
    <a href="#/Protocols/Staging/ext-workspace-v1/?id=extworkspacemanagerv1_stop" id="extworkspacemanagerv1_stop">
        <span class="codicon codicon-symbol-method method"></span>
        ExtWorkspaceManagerV1.<span class="method">Stop</span>
    </a>
</h3>

```csharp
void Stop()
```


**Stop sending events**

Indicates the client no longer wishes to receive events for new
workspace groups. However the compositor may emit further workspace
events, until the finished event is emitted. The compositor is expected
to send the finished event eventually once the stop request has been processed.

The client must not send any requests after this one, doing so will raise a wl_display
invalid_object error.

<h3 class="decleration event" title="WorkspaceGroup event">
    <a href="#/Protocols/Staging/ext-workspace-v1/?id=onextworkspacemanagerv1_workspacegroup" id="onextworkspacemanagerv1_workspacegroup">
        <span class="codicon codicon-symbol-event event"></span>
        ExtWorkspaceManagerV1.<span class="event">OnWorkspaceGroup</span>
    </a>
</h3>

```csharp
void WorkspaceGroupHandler(ExtWorkspaceGroupHandleV1 workspaceGroup)
```

| Argument | Type | Description |
| --- | --- | --- |
| workspace_group | new_id |  |

**A workspace group has been created**

This event is emitted whenever a new workspace group has been created.

All initial details of the workspace group (outputs) will be
sent immediately after this event via the corresponding events in
ext_workspace_group_handle_v1 and ext_workspace_handle_v1.

<h3 class="decleration event" title="Workspace event">
    <a href="#/Protocols/Staging/ext-workspace-v1/?id=onextworkspacemanagerv1_workspace" id="onextworkspacemanagerv1_workspace">
        <span class="codicon codicon-symbol-event event"></span>
        ExtWorkspaceManagerV1.<span class="event">OnWorkspace</span>
    </a>
</h3>

```csharp
void WorkspaceHandler(ExtWorkspaceHandleV1 workspace)
```

| Argument | Type | Description |
| --- | --- | --- |
| workspace | new_id |  |

**Workspace has been created**

This event is emitted whenever a new workspace has been created.

All initial details of the workspace (name, coordinates, state) will
be sent immediately after this event via the corresponding events in
ext_workspace_handle_v1.

Workspaces start off unassigned to any workspace group.

<h3 class="decleration event" title="Done event">
    <a href="#/Protocols/Staging/ext-workspace-v1/?id=onextworkspacemanagerv1_done" id="onextworkspacemanagerv1_done">
        <span class="codicon codicon-symbol-event event"></span>
        ExtWorkspaceManagerV1.<span class="event">OnDone</span>
    </a>
</h3>

```csharp
void DoneHandler()
```


**All information about the workspaces and workspace groups has been sent**

This event is sent after all changes in all workspaces and workspace groups have been
sent.

This allows changes to one or more ext_workspace_group_handle_v1
properties and ext_workspace_handle_v1 properties
to be seen as atomic, even if they happen via multiple events.
In particular, an output moving from one workspace group to
another sends an output_enter event and an output_leave event to the two
ext_workspace_group_handle_v1 objects in question. The compositor sends
the done event only after updating the output information in both
workspace groups.

<h3 class="decleration event" title="Finished event">
    <a href="#/Protocols/Staging/ext-workspace-v1/?id=onextworkspacemanagerv1_finished" id="onextworkspacemanagerv1_finished">
        <span class="codicon codicon-symbol-event event"></span>
        ExtWorkspaceManagerV1.<span class="event">OnFinished</span>
    </a>
</h3>

```csharp
void FinishedHandler()
```


**The compositor has finished with the workspace_manager**

This event indicates that the compositor is done sending events to the
ext_workspace_manager_v1. The server will destroy the object
immediately after sending this request.

<h2 class="decleration interface">
    <a href="#/Protocols/Staging/ext-workspace-v1/?id=extworkspacegrouphandlev1" id="extworkspacegrouphandlev1">
        <span class="codicon codicon-symbol-interface"></span>
        ExtWorkspaceGroupHandleV1
    </a>
    <span class="pill">version 1</span>
</h2>

A workspace group assigned to a set of outputs


A ext_workspace_group_handle_v1 object represents a workspace group
that is assigned a set of outputs and contains a number of workspaces.

The set of outputs assigned to the workspace group is conveyed to the client via
output_enter and output_leave events, and its workspaces are conveyed with
workspace events.

For example, a compositor which has a set of workspaces for each output may
advertise a workspace group (and its workspaces) per output, whereas a compositor
where a workspace spans all outputs may advertise a single workspace group for all
outputs.


<h3 class="decleration request" title="CreateWorkspace request">
    <a href="#/Protocols/Staging/ext-workspace-v1/?id=extworkspacegrouphandlev1_createworkspace" id="extworkspacegrouphandlev1_createworkspace">
        <span class="codicon codicon-symbol-method method"></span>
        ExtWorkspaceGroupHandleV1.<span class="method">CreateWorkspace</span>
    </a>
</h3>

```csharp
void CreateWorkspace(string workspace)
```

| Argument | Type | Description |
| --- | --- | --- |
| workspace | string |  |

**Create a new workspace**

Request that the compositor create a new workspace with the given name
and assign it to this group.

There is no guarantee that the compositor will create a new workspace,
or that the created workspace will have the provided name.

<h3 class="decleration request" title="Destroy request">
    <a href="#/Protocols/Staging/ext-workspace-v1/?id=extworkspacegrouphandlev1_destroy" id="extworkspacegrouphandlev1_destroy">
        <span class="codicon codicon-symbol-method method"></span>
        ExtWorkspaceGroupHandleV1.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the ext_workspace_group_handle_v1 object**

Destroys the ext_workspace_group_handle_v1 object.

This request should be send either when the client does not want to
use the workspace group object any more or after the removed event to finalize
the destruction of the object.

<h3 class="decleration event" title="Capabilities event">
    <a href="#/Protocols/Staging/ext-workspace-v1/?id=onextworkspacegrouphandlev1_capabilities" id="onextworkspacegrouphandlev1_capabilities">
        <span class="codicon codicon-symbol-event event"></span>
        ExtWorkspaceGroupHandleV1.<span class="event">OnCapabilities</span>
    </a>
</h3>

```csharp
void CapabilitiesHandler(uint capabilities)
```

| Argument | Type | Description |
| --- | --- | --- |
| capabilities | uint | Capabilities |

**Compositor capabilities**

This event advertises the capabilities supported by the compositor. If
a capability isn't supported, clients should hide or disable the UI
elements that expose this functionality. For instance, if the
compositor doesn't advertise support for creating workspaces, a button
triggering the create_workspace request should not be displayed.

The compositor will ignore requests it doesn't support. For instance,
a compositor which doesn't advertise support for creating workspaces will ignore
create_workspace requests.

Compositors must send this event once after creation of an
ext_workspace_group_handle_v1. When the capabilities change, compositors
must send this event again.

<h3 class="decleration event" title="OutputEnter event">
    <a href="#/Protocols/Staging/ext-workspace-v1/?id=onextworkspacegrouphandlev1_outputenter" id="onextworkspacegrouphandlev1_outputenter">
        <span class="codicon codicon-symbol-event event"></span>
        ExtWorkspaceGroupHandleV1.<span class="event">OnOutputEnter</span>
    </a>
</h3>

```csharp
void OutputEnterHandler(WlOutput output)
```

| Argument | Type | Description |
| --- | --- | --- |
| output | object |  |

**Output assigned to workspace group**

This event is emitted whenever an output is assigned to the workspace
group or a new `wl_output` object is bound by the client, which was already
assigned to this workspace_group.

<h3 class="decleration event" title="OutputLeave event">
    <a href="#/Protocols/Staging/ext-workspace-v1/?id=onextworkspacegrouphandlev1_outputleave" id="onextworkspacegrouphandlev1_outputleave">
        <span class="codicon codicon-symbol-event event"></span>
        ExtWorkspaceGroupHandleV1.<span class="event">OnOutputLeave</span>
    </a>
</h3>

```csharp
void OutputLeaveHandler(WlOutput output)
```

| Argument | Type | Description |
| --- | --- | --- |
| output | object |  |

**Output removed from workspace group**

This event is emitted whenever an output is removed from the workspace
group.

<h3 class="decleration event" title="WorkspaceEnter event">
    <a href="#/Protocols/Staging/ext-workspace-v1/?id=onextworkspacegrouphandlev1_workspaceenter" id="onextworkspacegrouphandlev1_workspaceenter">
        <span class="codicon codicon-symbol-event event"></span>
        ExtWorkspaceGroupHandleV1.<span class="event">OnWorkspaceEnter</span>
    </a>
</h3>

```csharp
void WorkspaceEnterHandler(ExtWorkspaceHandleV1 workspace)
```

| Argument | Type | Description |
| --- | --- | --- |
| workspace | object |  |

**Workspace added to workspace group**

This event is emitted whenever a workspace is assigned to this group.
A workspace may only ever be assigned to a single group at a single point
in time, but can be re-assigned during its lifetime.

<h3 class="decleration event" title="WorkspaceLeave event">
    <a href="#/Protocols/Staging/ext-workspace-v1/?id=onextworkspacegrouphandlev1_workspaceleave" id="onextworkspacegrouphandlev1_workspaceleave">
        <span class="codicon codicon-symbol-event event"></span>
        ExtWorkspaceGroupHandleV1.<span class="event">OnWorkspaceLeave</span>
    </a>
</h3>

```csharp
void WorkspaceLeaveHandler(ExtWorkspaceHandleV1 workspace)
```

| Argument | Type | Description |
| --- | --- | --- |
| workspace | object |  |

**Workspace removed from workspace group**

This event is emitted whenever a workspace is removed from this group.

<h3 class="decleration event" title="Removed event">
    <a href="#/Protocols/Staging/ext-workspace-v1/?id=onextworkspacegrouphandlev1_removed" id="onextworkspacegrouphandlev1_removed">
        <span class="codicon codicon-symbol-event event"></span>
        ExtWorkspaceGroupHandleV1.<span class="event">OnRemoved</span>
    </a>
</h3>

```csharp
void RemovedHandler()
```


**This workspace group has been removed**

This event is send when the group associated with the ext_workspace_group_handle_v1
has been removed. After sending this request the compositor will immediately consider
the object inert. Any requests will be ignored except the destroy request.
It is guaranteed there won't be any more events referencing this
ext_workspace_group_handle_v1.

The compositor must remove all workspaces belonging to a workspace group
via a workspace_leave event before removing the workspace group.

<h3 class="decleration enum" title="GroupCapabilities enum">
    <a href="#/Protocols/Staging/ext-workspace-v1/?id=groupcapabilities" id="groupcapabilities">
        <span class="codicon codicon-symbol-enum enum"></span>
        ExtWorkspaceGroupHandleV1.<span class="enum">GroupCapabilities</span>
    </a>
</h3>

```csharp
public enum GroupCapabilitiesFlag
```

| Value | Integer | Description |
| --- | --- | --- |
| CreateWorkspace | 1 | Create_workspace request is available |
<h2 class="decleration interface">
    <a href="#/Protocols/Staging/ext-workspace-v1/?id=extworkspacehandlev1" id="extworkspacehandlev1">
        <span class="codicon codicon-symbol-interface"></span>
        ExtWorkspaceHandleV1
    </a>
    <span class="pill">version 1</span>
</h2>

A workspace handing a group of surfaces


A ext_workspace_handle_v1 object represents a workspace that handles a
group of surfaces.

Each workspace has:
- a name, conveyed to the client with the name event
- potentially an id conveyed with the id event
- a list of states, conveyed to the client with the state event
- and optionally a set of coordinates, conveyed to the client with the
coordinates event

The client may request that the compositor activate or deactivate the workspace.

Each workspace can belong to only a single workspace group.
Depending on the compositor policy, there might be workspaces with
the same name in different workspace groups, but these workspaces are still
separate (e.g. one of them might be active while the other is not).


<h3 class="decleration request" title="Destroy request">
    <a href="#/Protocols/Staging/ext-workspace-v1/?id=extworkspacehandlev1_destroy" id="extworkspacehandlev1_destroy">
        <span class="codicon codicon-symbol-method method"></span>
        ExtWorkspaceHandleV1.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the ext_workspace_handle_v1 object**

Destroys the ext_workspace_handle_v1 object.

This request should be made either when the client does not want to
use the workspace object any more or after the remove event to finalize
the destruction of the object.

<h3 class="decleration request" title="Activate request">
    <a href="#/Protocols/Staging/ext-workspace-v1/?id=extworkspacehandlev1_activate" id="extworkspacehandlev1_activate">
        <span class="codicon codicon-symbol-method method"></span>
        ExtWorkspaceHandleV1.<span class="method">Activate</span>
    </a>
</h3>

```csharp
void Activate()
```


**Activate the workspace**

Request that this workspace be activated.

There is no guarantee the workspace will be actually activated, and
behaviour may be compositor-dependent. For example, activating a
workspace may or may not deactivate all other workspaces in the same
group.

<h3 class="decleration request" title="Deactivate request">
    <a href="#/Protocols/Staging/ext-workspace-v1/?id=extworkspacehandlev1_deactivate" id="extworkspacehandlev1_deactivate">
        <span class="codicon codicon-symbol-method method"></span>
        ExtWorkspaceHandleV1.<span class="method">Deactivate</span>
    </a>
</h3>

```csharp
void Deactivate()
```


**Deactivate the workspace**

Request that this workspace be deactivated.

There is no guarantee the workspace will be actually deactivated.

<h3 class="decleration request" title="Assign request">
    <a href="#/Protocols/Staging/ext-workspace-v1/?id=extworkspacehandlev1_assign" id="extworkspacehandlev1_assign">
        <span class="codicon codicon-symbol-method method"></span>
        ExtWorkspaceHandleV1.<span class="method">Assign</span>
    </a>
</h3>

```csharp
void Assign(ExtWorkspaceGroupHandleV1 workspaceGroup)
```

| Argument | Type | Description |
| --- | --- | --- |
| workspace_group | object |  |

**Assign workspace to group**

Requests that this workspace is assigned to the given workspace group.

There is no guarantee the workspace will be assigned.

<h3 class="decleration request" title="Remove request">
    <a href="#/Protocols/Staging/ext-workspace-v1/?id=extworkspacehandlev1_remove" id="extworkspacehandlev1_remove">
        <span class="codicon codicon-symbol-method method"></span>
        ExtWorkspaceHandleV1.<span class="method">Remove</span>
    </a>
</h3>

```csharp
void Remove()
```


**Remove the workspace**

Request that this workspace be removed.

There is no guarantee the workspace will be actually removed.

<h3 class="decleration event" title="Id event">
    <a href="#/Protocols/Staging/ext-workspace-v1/?id=onextworkspacehandlev1_id" id="onextworkspacehandlev1_id">
        <span class="codicon codicon-symbol-event event"></span>
        ExtWorkspaceHandleV1.<span class="event">OnId</span>
    </a>
</h3>

```csharp
void IdHandler(string id)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | string |  |

**Workspace id**

If this event is emitted, it will be send immediately after the
ext_workspace_handle_v1 is created or when an id is assigned to
a workspace (at most once during its lifetime).

An id will never change during the lifetime of the `ext_workspace_handle_v1`
and is guaranteed to be unique during its lifetime.

Ids are not human-readable and shouldn't be displayed, use `name` for that purpose.

Compositors are expected to only send ids for workspaces likely stable across multiple
sessions and can be used by clients to store preferences for workspaces. Workspaces without
ids should be considered temporary and any data associated with them should be deleted once
the respective object is lost.

<h3 class="decleration event" title="Name event">
    <a href="#/Protocols/Staging/ext-workspace-v1/?id=onextworkspacehandlev1_name" id="onextworkspacehandlev1_name">
        <span class="codicon codicon-symbol-event event"></span>
        ExtWorkspaceHandleV1.<span class="event">OnName</span>
    </a>
</h3>

```csharp
void NameHandler(string name)
```

| Argument | Type | Description |
| --- | --- | --- |
| name | string |  |

**Workspace name changed**

This event is emitted immediately after the ext_workspace_handle_v1 is
created and whenever the name of the workspace changes.

A name is meant to be human-readable and can be displayed to a user.
Unlike the id it is neither stable nor unique.

<h3 class="decleration event" title="Coordinates event">
    <a href="#/Protocols/Staging/ext-workspace-v1/?id=onextworkspacehandlev1_coordinates" id="onextworkspacehandlev1_coordinates">
        <span class="codicon codicon-symbol-event event"></span>
        ExtWorkspaceHandleV1.<span class="event">OnCoordinates</span>
    </a>
</h3>

```csharp
void CoordinatesHandler(byte[] coordinates)
```

| Argument | Type | Description |
| --- | --- | --- |
| coordinates | array |  |

**Workspace coordinates changed**

This event is used to organize workspaces into an N-dimensional grid
within a workspace group, and if supported, is emitted immediately after
the ext_workspace_handle_v1 is created and whenever the coordinates of
the workspace change. Compositors may not send this event if they do not
conceptually arrange workspaces in this way. If compositors simply
number workspaces, without any geometric interpretation, they may send
1D coordinates, which clients should not interpret as implying any
geometry. Sending an empty array means that the compositor no longer
orders the workspace geometrically.

Coordinates have an arbitrary number of dimensions N with an uint32
position along each dimension. By convention if N &gt; 1, the first
dimension is X, the second Y, the third Z, and so on. The compositor may
chose to utilize these events for a more novel workspace layout
convention, however. No guarantee is made about the grid being filled or
bounded; there may be a workspace at coordinate 1 and another at
coordinate 1000 and none in between. Within a workspace group, however,
workspaces must have unique coordinates of equal dimensionality.

<h3 class="decleration event" title="State event">
    <a href="#/Protocols/Staging/ext-workspace-v1/?id=onextworkspacehandlev1_state" id="onextworkspacehandlev1_state">
        <span class="codicon codicon-symbol-event event"></span>
        ExtWorkspaceHandleV1.<span class="event">OnState</span>
    </a>
</h3>

```csharp
void StateHandler(uint state)
```

| Argument | Type | Description |
| --- | --- | --- |
| state | uint |  |

**The state of the workspace changed**

This event is emitted immediately after the ext_workspace_handle_v1 is
created and each time the workspace state changes, either because of a
compositor action or because of a request in this protocol.

Missing states convey the opposite meaning, e.g. an unset active bit
means the workspace is currently inactive.

<h3 class="decleration event" title="Capabilities event">
    <a href="#/Protocols/Staging/ext-workspace-v1/?id=onextworkspacehandlev1_capabilities" id="onextworkspacehandlev1_capabilities">
        <span class="codicon codicon-symbol-event event"></span>
        ExtWorkspaceHandleV1.<span class="event">OnCapabilities</span>
    </a>
</h3>

```csharp
void CapabilitiesHandler(uint capabilities)
```

| Argument | Type | Description |
| --- | --- | --- |
| capabilities | uint | Capabilities |

**Compositor capabilities**

This event advertises the capabilities supported by the compositor. If
a capability isn't supported, clients should hide or disable the UI
elements that expose this functionality. For instance, if the
compositor doesn't advertise support for removing workspaces, a button
triggering the remove request should not be displayed.

The compositor will ignore requests it doesn't support. For instance,
a compositor which doesn't advertise support for remove will ignore
remove requests.

Compositors must send this event once after creation of an
ext_workspace_handle_v1 . When the capabilities change, compositors
must send this event again.

<h3 class="decleration event" title="Removed event">
    <a href="#/Protocols/Staging/ext-workspace-v1/?id=onextworkspacehandlev1_removed" id="onextworkspacehandlev1_removed">
        <span class="codicon codicon-symbol-event event"></span>
        ExtWorkspaceHandleV1.<span class="event">OnRemoved</span>
    </a>
</h3>

```csharp
void RemovedHandler()
```


**This workspace has been removed**

This event is send when the workspace associated with the ext_workspace_handle_v1
has been removed. After sending this request, the compositor will immediately consider
the object inert. Any requests will be ignored except the destroy request.

It is guaranteed there won't be any more events referencing this
ext_workspace_handle_v1.

The compositor must only remove a workspaces not currently belonging to any
workspace_group.

<h3 class="decleration enum" title="State enum">
    <a href="#/Protocols/Staging/ext-workspace-v1/?id=state" id="state">
        <span class="codicon codicon-symbol-enum enum"></span>
        ExtWorkspaceHandleV1.<span class="enum">State</span>
    </a>
</h3>

```csharp
public enum StateFlag
```

Types of states on the workspace


The different states that a workspace can have.


| Value | Integer | Description |
| --- | --- | --- |
| Active | 1 | The workspace is active |
| Urgent | 2 | The workspace requests attention |
| Hidden | 4 |  |
<h3 class="decleration enum" title="WorkspaceCapabilities enum">
    <a href="#/Protocols/Staging/ext-workspace-v1/?id=workspacecapabilities" id="workspacecapabilities">
        <span class="codicon codicon-symbol-enum enum"></span>
        ExtWorkspaceHandleV1.<span class="enum">WorkspaceCapabilities</span>
    </a>
</h3>

```csharp
public enum WorkspaceCapabilitiesFlag
```

| Value | Integer | Description |
| --- | --- | --- |
| Activate | 1 | Activate request is available |
| Deactivate | 2 | Deactivate request is available |
| Remove | 4 | Remove request is available |
| Assign | 8 | Assign request is available |
