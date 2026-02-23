# Presentation time

##### [WaylandDotnet](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet) ![](../../assets/arrow.svg ':class=breadcrumb-arrow') [Stable](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/Stable) ![](../../assets/arrow.svg ':class=breadcrumb-arrow') [PresentationTime](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/Stable/presentation-time/)

---

<h2 class="decleration interface">
    <a href="?id=WpPresentation" id="WpPresentation">
        <span class="codicon codicon-symbol-interface"></span>
        WpPresentation
    </a>
    <span class="pill">version 2</span>
</h2>

Timed presentation related wl_surface requests



When the final realized presentation time is available, e.g.
after a framebuffer flip completes, the requested
presentation_feedback.presented events are sent. The final
presentation time can differ from the compositor's predicted
display update time and the update's target time, especially
when the compositor misses its target vertical blanking period.


<h3 class="decleration request" title="Destroy request">
    <a href="?id=WpPresentation_Destroy" id="WpPresentation_Destroy">
        <span class="codicon codicon-symbol-method method"></span>
        WpPresentation.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Unbind from the presentation interface**

Informs the server that the client will no longer be using
this protocol object. Existing objects created by this object
are not affected.

<h3 class="decleration request" title="Feedback request">
    <a href="?id=WpPresentation_Feedback" id="WpPresentation_Feedback">
        <span class="codicon codicon-symbol-method method"></span>
        WpPresentation.<span class="method">Feedback</span>
    </a>
</h3>

```csharp
WpPresentationFeedback Feedback(WlSurface surface)
```

| Argument | Type | Description |
| --- | --- | --- |
| surface | object | Target surface |
| callback | new_id | New feedback object |

**Request presentation feedback information**

Request presentation feedback for the current content submission
on the given surface. This creates a new presentation_feedback
object, which will deliver the feedback information once. If
multiple presentation_feedback objects are created for the same
submission, they will all deliver the same information.

For details on what information is returned, see the
presentation_feedback interface.

<h3 class="decleration event" title="ClockId event">
    <a href="?id=OnWpPresentation_ClockId" id="OnWpPresentation_ClockId">
        <span class="codicon codicon-symbol-event event"></span>
        WpPresentation.<span class="event">OnClockId</span>
    </a>
</h3>

```csharp
void ClockIdHandler(uint clkId)
```

| Argument | Type | Description |
| --- | --- | --- |
| clk_id | uint | Platform clock identifier |

**Clock ID for timestamps**

This event tells the client in which clock domain the
compositor interprets the timestamps used by the presentation
extension. This clock is called the presentation clock.

The compositor sends this event when the client binds to the
presentation interface. The presentation clock does not change
during the lifetime of the client connection.

The clock identifier is platform dependent. On POSIX platforms, the
identifier value is one of the clockid_t values accepted by
clock_gettime(). clock_gettime() is defined by POSIX.1-2001.

Timestamps in this clock domain are expressed as tv_sec_hi,
tv_sec_lo, tv_nsec triples, each component being an unsigned
32-bit value. Whole seconds are in tv_sec which is a 64-bit
value combined from tv_sec_hi and tv_sec_lo, and the
additional fractional part in tv_nsec as nanoseconds. Hence,
for valid timestamps tv_nsec must be in [0, 999999999].

Note that clock_id applies only to the presentation clock,
and implies nothing about e.g. the timestamps used in the
Wayland core protocol input events.

Compositors should prefer a clock which does not jump and is
not slewed e.g. by NTP. The absolute value of the clock is
irrelevant. Precision of one millisecond or better is
recommended. Clients must be able to query the current clock
value directly, not by asking the compositor.

<h2 class="decleration interface">
    <a href="?id=WpPresentationFeedback" id="WpPresentationFeedback">
        <span class="codicon codicon-symbol-interface"></span>
        WpPresentationFeedback
    </a>
    <span class="pill">version 2</span>
</h2>

Presentation time feedback event


A presentation_feedback object returns an indication that a
wl_surface content update has become visible to the user.
One object corresponds to one content update submission
(wl_surface.commit). There are two possible outcomes: the
content update is presented to the user, and a presentation
timestamp delivered; or, the user did not see the content
update because it was superseded or its surface destroyed,
and the content update is discarded.

Once a presentation_feedback object has delivered a 'presented'
or 'discarded' event it is automatically destroyed.


<h3 class="decleration event" title="SyncOutput event">
    <a href="?id=OnWpPresentationFeedback_SyncOutput" id="OnWpPresentationFeedback_SyncOutput">
        <span class="codicon codicon-symbol-event event"></span>
        WpPresentationFeedback.<span class="event">OnSyncOutput</span>
    </a>
</h3>

```csharp
void SyncOutputHandler(WlOutput output)
```

| Argument | Type | Description |
| --- | --- | --- |
| output | object | Presentation output |

**Presentation synchronized to this output**

As presentation can be synchronized to only one output at a
time, this event tells which output it was. This event is only
sent prior to the presented event.

As clients may bind to the same global wl_output multiple
times, this event is sent for each bound instance that matches
the synchronized output. If a client has not bound to the
right wl_output global at all, this event is not sent.

<h3 class="decleration event" title="Presented event">
    <a href="?id=OnWpPresentationFeedback_Presented" id="OnWpPresentationFeedback_Presented">
        <span class="codicon codicon-symbol-event event"></span>
        WpPresentationFeedback.<span class="event">OnPresented</span>
    </a>
</h3>

```csharp
void PresentedHandler(uint tvSecHi, uint tvSecLo, uint tvNsec, uint refresh, uint seqHi, uint seqLo, uint flags)
```

| Argument | Type | Description |
| --- | --- | --- |
| tv_sec_hi | uint | High 32 bits of the seconds part of the presentation timestamp |
| tv_sec_lo | uint | Low 32 bits of the seconds part of the presentation timestamp |
| tv_nsec | uint | Nanoseconds part of the presentation timestamp |
| refresh | uint | Nanoseconds till next refresh |
| seq_hi | uint | High 32 bits of refresh counter |
| seq_lo | uint | Low 32 bits of refresh counter |
| flags | uint | Combination of 'kind' values |

**The content update was displayed**

The associated content update was displayed to the user at the
indicated time (tv_sec_hi/lo, tv_nsec). For the interpretation of
the timestamp, see presentation.clock_id event.

The timestamp corresponds to the time when the content update
turned into light the first time on the surface's main output.
Compositors may approximate this from the framebuffer flip
completion events from the system, and the latency of the
physical display path if known.

This event is preceded by all related sync_output events
telling which output's refresh cycle the feedback corresponds
to, i.e. the main output for the surface. Compositors are
recommended to choose the output containing the largest part
of the wl_surface, or keeping the output they previously
chose. Having a stable presentation output association helps
clients predict future output refreshes (vblank).

The 'refresh' argument gives the compositor's prediction of how
many nanoseconds after tv_sec, tv_nsec the very next output
refresh may occur. This is to further aid clients in
predicting future refreshes, i.e., estimating the timestamps
targeting the next few vblanks. If such prediction cannot
usefully be done, the argument is zero.

For version 2 and later, if the output does not have a constant
refresh rate, explicit video mode switches excluded, then the
refresh argument must be either an appropriate rate picked by the
compositor (e.g. fastest rate), or 0 if no such rate exists.
For version 1, if the output does not have a constant refresh rate,
the refresh argument must be zero.

The 64-bit value combined from seq_hi and seq_lo is the value
of the output's vertical retrace counter when the content
update was first scanned out to the display. This value must
be compatible with the definition of MSC in
GLX_OML_sync_control specification. Note, that if the display
path has a non-zero latency, the time instant specified by
this counter may differ from the timestamp's.

If the output does not have a concept of vertical retrace or a
refresh cycle, or the output device is self-refreshing without
a way to query the refresh count, then the arguments seq_hi
and seq_lo must be zero.

<h3 class="decleration event" title="Discarded event">
    <a href="?id=OnWpPresentationFeedback_Discarded" id="OnWpPresentationFeedback_Discarded">
        <span class="codicon codicon-symbol-event event"></span>
        WpPresentationFeedback.<span class="event">OnDiscarded</span>
    </a>
</h3>

```csharp
void DiscardedHandler()
```


**The content update was not displayed**

The content update was never displayed to the user.

