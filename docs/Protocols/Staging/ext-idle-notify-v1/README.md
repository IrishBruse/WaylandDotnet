# Idle Notify

<p class="breadcrumb"><a href="https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet">WaylandDotnet</a> <img src="../../../assets/arrow.svg" class="breadcrumb-arrow" alt="" /> <a href="https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/Staging">Staging</a> <img src="../../../assets/arrow.svg" class="breadcrumb-arrow" alt="" /> <a href="https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/Staging/ext-idle-notify-v1/">ExtIdleNotifyV1</a></p>

---

<h2 class="decleration interface">
    <a href="#/Protocols/Staging/ext-idle-notify-v1/?id=extidlenotifierv1" id="extidlenotifierv1">
        <span class="codicon codicon-symbol-interface"></span>
        ExtIdleNotifierV1
    </a>
    <span class="pill">version 2</span>
</h2>

Idle notification manager


This interface allows clients to monitor user idle status.

After binding to this global, clients can create ext_idle_notification_v1
objects to get notified when the user is idle for a given amount of time.


<h3 class="decleration request" title="Destroy request">
    <a href="#/Protocols/Staging/ext-idle-notify-v1/?id=extidlenotifierv1_destroy" id="extidlenotifierv1_destroy">
        <span class="codicon codicon-symbol-method method"></span>
        ExtIdleNotifierV1.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the manager**

Destroy the manager object. All objects created via this interface
remain valid.

<h3 class="decleration request" title="GetIdleNotification request">
    <a href="#/Protocols/Staging/ext-idle-notify-v1/?id=extidlenotifierv1_getidlenotification" id="extidlenotifierv1_getidlenotification">
        <span class="codicon codicon-symbol-method method"></span>
        ExtIdleNotifierV1.<span class="method">GetIdleNotification</span>
    </a>
</h3>

```csharp
ExtIdleNotificationV1 GetIdleNotification(uint timeout, WlSeat seat)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id |  |
| timeout | uint | Minimum idle timeout in msec |
| seat | object |  |

**Create a notification object**

Create a new idle notification object.

The notification object has a minimum timeout duration and is tied to a
seat. The client will be notified if the seat is inactive for at least
the provided timeout. See ext_idle_notification_v1 for more details.

A zero timeout is valid and means the client wants to be notified as
soon as possible when the seat is inactive.

<h3 class="decleration request" title="GetInputIdleNotification request">
    <a href="#/Protocols/Staging/ext-idle-notify-v1/?id=extidlenotifierv1_getinputidlenotification" id="extidlenotifierv1_getinputidlenotification">
        <span class="codicon codicon-symbol-method method"></span>
        ExtIdleNotifierV1.<span class="method">GetInputIdleNotification</span>
    </a>
    <span class="pill">since 2</span>
</h3>

```csharp
ExtIdleNotificationV1 GetInputIdleNotification(uint timeout, WlSeat seat)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id |  |
| timeout | uint | Minimum idle timeout in msec |
| seat | object |  |

**Create a notification object**

Create a new idle notification object to track input from the
user, such as keyboard and mouse movement. Because this object is
meant to track user input alone, it ignores idle inhibitors.

The notification object has a minimum timeout duration and is tied to a
seat. The client will be notified if the seat is inactive for at least
the provided timeout. See ext_idle_notification_v1 for more details.

A zero timeout is valid and means the client wants to be notified as
soon as possible when the seat is inactive.

<h2 class="decleration interface">
    <a href="#/Protocols/Staging/ext-idle-notify-v1/?id=extidlenotificationv1" id="extidlenotificationv1">
        <span class="codicon codicon-symbol-interface"></span>
        ExtIdleNotificationV1
    </a>
    <span class="pill">version 2</span>
</h2>

Idle notification


This interface is used by the compositor to send idle notification events
to clients.

Initially the notification object is not idle. The notification object
becomes idle when no user activity has happened for at least the timeout
duration, starting from the creation of the notification object. User
activity may include input events or a presence sensor, but is
compositor-specific.

How this notification responds to idle inhibitors depends on how
it was constructed. If constructed from the
get_idle_notification request, then if an idle inhibitor is
active (e.g. another client has created a zwp_idle_inhibitor_v1
on a visible surface), the compositor must not make the
notification object idle. However, if constructed from the
get_input_idle_notification request, then idle inhibitors are
ignored, and only input from the user, e.g. from a keyboard or
mouse, counts as activity.

When the notification object becomes idle, an idled event is sent. When
user activity starts again, the notification object stops being idle,
a resumed event is sent and the timeout is restarted.


<h3 class="decleration request" title="Destroy request">
    <a href="#/Protocols/Staging/ext-idle-notify-v1/?id=extidlenotificationv1_destroy" id="extidlenotificationv1_destroy">
        <span class="codicon codicon-symbol-method method"></span>
        ExtIdleNotificationV1.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the notification object**

Destroy the notification object.

<h3 class="decleration event" title="Idled event">
    <a href="#/Protocols/Staging/ext-idle-notify-v1/?id=onextidlenotificationv1_idled" id="onextidlenotificationv1_idled">
        <span class="codicon codicon-symbol-event event"></span>
        ExtIdleNotificationV1.<span class="event">OnIdled</span>
    </a>
</h3>

```csharp
void IdledHandler()
```


**Notification object is idle**

This event is sent when the notification object becomes idle.

It's a compositor protocol error to send this event twice without a
resumed event in-between.

<h3 class="decleration event" title="Resumed event">
    <a href="#/Protocols/Staging/ext-idle-notify-v1/?id=onextidlenotificationv1_resumed" id="onextidlenotificationv1_resumed">
        <span class="codicon codicon-symbol-event event"></span>
        ExtIdleNotificationV1.<span class="event">OnResumed</span>
    </a>
</h3>

```csharp
void ResumedHandler()
```


**Notification object is no longer idle**

This event is sent when the notification object stops being idle.

It's a compositor protocol error to send this event twice without an
idled event in-between. It's a compositor protocol error to send this
event prior to any idled event.

