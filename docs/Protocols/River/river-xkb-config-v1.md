# River XKB Config

##### [WaylandDotnet](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet) ![](../../assets/arrow.svg ':class=breadcrumb-arrow') [River](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/River) ![](../../assets/arrow.svg ':class=breadcrumb-arrow') [RiverXkbConfigV1](https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/River/river-xkb-config-v1/)

---

<h2 class="decleration interface">
    <a href="?id=RiverXkbConfigV1" id="RiverXkbConfigV1">
        <span class="codicon codicon-symbol-interface"></span>
        RiverXkbConfigV1
    </a>
    <span class="pill">version 1</span>
</h2>

Xkb config global interface


Global interface for configuring xkb devices.

This global should only be advertised if river_input_manager_v1 is
advertised as well.


<h3 class="decleration request" title="Stop request">
    <a href="?id=RiverXkbConfigV1_Stop" id="RiverXkbConfigV1_Stop">
        <span class="codicon codicon-symbol-method method"></span>
        RiverXkbConfigV1.<span class="method">Stop</span>
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
for a river_xkb_config_v1.finished event before destroying this object.

<h3 class="decleration request" title="Destroy request">
    <a href="?id=RiverXkbConfigV1_Destroy" id="RiverXkbConfigV1_Destroy">
        <span class="codicon codicon-symbol-method method"></span>
        RiverXkbConfigV1.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the river_xkb_config_v1 object**

This request should be called after the finished event has been received
to complete destruction of the object.

It is a protocol error to make this request before the finished event
has been received.

If a client wishes to destroy this object it should send a
river_xkb_config_v1.stop request and wait for a
river_xkb_config_v1.finished event. Once the finished event is received
it is safe to destroy this object and any other objects created through
this interface.

<h3 class="decleration request" title="CreateKeymap request">
    <a href="?id=RiverXkbConfigV1_CreateKeymap" id="RiverXkbConfigV1_CreateKeymap">
        <span class="codicon codicon-symbol-method method"></span>
        RiverXkbConfigV1.<span class="method">CreateKeymap</span>
    </a>
</h3>

```csharp
RiverXkbKeymapV1 CreateKeymap(int fd, uint format)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id |  |
| fd | fd |  |
| format | uint |  |

**Create a keymap object**

The server must be able to mmap the fd with MAP_PRIVATE.
The server will fstat the fd to obtain the size of the keymap.
The client must not modify the contents of the fd after making this request.
The client should seal the fd with fcntl.

<h3 class="decleration event" title="Finished event">
    <a href="?id=OnRiverXkbConfigV1_Finished" id="OnRiverXkbConfigV1_Finished">
        <span class="codicon codicon-symbol-event event"></span>
        RiverXkbConfigV1.<span class="event">OnFinished</span>
    </a>
</h3>

```csharp
void FinishedHandler()
```


**The server has finished with the object**

This event indicates that the server will send no further events on this
object. The client should destroy the object. See
river_xkb_config_v1.destroy for more information.

<h3 class="decleration event" title="XkbKeyboard event">
    <a href="?id=OnRiverXkbConfigV1_XkbKeyboard" id="OnRiverXkbConfigV1_XkbKeyboard">
        <span class="codicon codicon-symbol-event event"></span>
        RiverXkbConfigV1.<span class="event">OnXkbKeyboard</span>
    </a>
</h3>

```csharp
void XkbKeyboardHandler(RiverXkbKeyboardV1 id)
```

| Argument | Type | Description |
| --- | --- | --- |
| id | new_id |  |

**New xkb keyboard**

A new xkbcommon keyboard has been created. Not every
river_input_device_v1 is necessarily an xkbcommon keyboard as well.

<h3 class="decleration enum" title="Error enum">
    <a href="?id=Error" id="Error">
        <span class="codicon codicon-symbol-enum"></span>
        Error
    </a>
</h3>

```csharp
public enum Error : uint
```

| Value | Integer | Description |
| --- | --- | --- |
| InvalidDestroy | 0 |  |
| InvalidFormat | 1 |  |
<h3 class="decleration enum" title="KeymapFormat enum">
    <a href="?id=KeymapFormat" id="KeymapFormat">
        <span class="codicon codicon-symbol-enum"></span>
        KeymapFormat
    </a>
</h3>

```csharp
public enum KeymapFormat : uint
```

| Value | Integer | Description |
| --- | --- | --- |
| TextV1 | 1 | XKB_KEYMAP_FORMAT_TEXT_V1 |
| TextV2 | 2 | XKB_KEYMAP_FORMAT_TEXT_V2 |
<h2 class="decleration interface">
    <a href="?id=RiverXkbKeymapV1" id="RiverXkbKeymapV1">
        <span class="codicon codicon-symbol-interface"></span>
        RiverXkbKeymapV1
    </a>
    <span class="pill">version 1</span>
</h2>

Xkbcommon keymap


This object is the result of attempting to create an xkbcommon keymap.


<h3 class="decleration request" title="Destroy request">
    <a href="?id=RiverXkbKeymapV1_Destroy" id="RiverXkbKeymapV1_Destroy">
        <span class="codicon codicon-symbol-method method"></span>
        RiverXkbKeymapV1.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the keymap object**

This request indicates that the client will no longer use the keymap
object and that it may be safely destroyed.

<h3 class="decleration event" title="Success event">
    <a href="?id=OnRiverXkbKeymapV1_Success" id="OnRiverXkbKeymapV1_Success">
        <span class="codicon codicon-symbol-event event"></span>
        RiverXkbKeymapV1.<span class="event">OnSuccess</span>
    </a>
</h3>

```csharp
void SuccessHandler()
```


**Keymap creation succeeded**

The keymap object was successfully created and may be used with the
river_xkb_keyboard_v1.set_keymap request.

<h3 class="decleration event" title="Failure event">
    <a href="?id=OnRiverXkbKeymapV1_Failure" id="OnRiverXkbKeymapV1_Failure">
        <span class="codicon codicon-symbol-event event"></span>
        RiverXkbKeymapV1.<span class="event">OnFailure</span>
    </a>
</h3>

```csharp
void FailureHandler(string errorMsg)
```

| Argument | Type | Description |
| --- | --- | --- |
| error_msg | string |  |

**Keymap creation failed**

The compositor failed to create a keymap from the given parameters.

It is a protocol error to use this keymap object with
river_xkb_keyboard_v1.set_keymap.

<h2 class="decleration interface">
    <a href="?id=RiverXkbKeyboardV1" id="RiverXkbKeyboardV1">
        <span class="codicon codicon-symbol-interface"></span>
        RiverXkbKeyboardV1
    </a>
    <span class="pill">version 1</span>
</h2>

Xkbcommon keyboard device


This object represent a physical keyboard which has its configuration and
state managed by xkbcommon.


<h3 class="decleration request" title="Destroy request">
    <a href="?id=RiverXkbKeyboardV1_Destroy" id="RiverXkbKeyboardV1_Destroy">
        <span class="codicon codicon-symbol-method method"></span>
        RiverXkbKeyboardV1.<span class="method">Destroy</span>
    </a>
    <span class="pill destructor">Type: destructor</span>
</h3>

```csharp
void Destroy()
```


**Destroy the xkb keyboard object**

This request indicates that the client will no longer use the keyboard
object and that it may be safely destroyed.

<h3 class="decleration request" title="SetKeymap request">
    <a href="?id=RiverXkbKeyboardV1_SetKeymap" id="RiverXkbKeyboardV1_SetKeymap">
        <span class="codicon codicon-symbol-method method"></span>
        RiverXkbKeyboardV1.<span class="method">SetKeymap</span>
    </a>
</h3>

```csharp
void SetKeymap(RiverXkbKeymapV1 keymap)
```

| Argument | Type | Description |
| --- | --- | --- |
| keymap | object |  |

**Set the keymap**

Set the keymap for the keyboard.

It is a protocol error to pass a keymap object for which the
river_xkb_keymap_v1.success event was not received.

<h3 class="decleration request" title="SetLayoutByIndex request">
    <a href="?id=RiverXkbKeyboardV1_SetLayoutByIndex" id="RiverXkbKeyboardV1_SetLayoutByIndex">
        <span class="codicon codicon-symbol-method method"></span>
        RiverXkbKeyboardV1.<span class="method">SetLayoutByIndex</span>
    </a>
</h3>

```csharp
void SetLayoutByIndex(int index)
```

| Argument | Type | Description |
| --- | --- | --- |
| index | int |  |

**Set the active layout by index**

Set the active layout for the keyboard's keymap. Has no effect if the
layout index is out of bounds for the current keymap.

<h3 class="decleration request" title="SetLayoutByName request">
    <a href="?id=RiverXkbKeyboardV1_SetLayoutByName" id="RiverXkbKeyboardV1_SetLayoutByName">
        <span class="codicon codicon-symbol-method method"></span>
        RiverXkbKeyboardV1.<span class="method">SetLayoutByName</span>
    </a>
</h3>

```csharp
void SetLayoutByName(string name)
```

| Argument | Type | Description |
| --- | --- | --- |
| name | string |  |

**Set the active layout by name**

Set the active layout for the keyboard's keymap. Has no effect if there
is no layout with the give name for the keyboard's keymap.

<h3 class="decleration request" title="CapslockEnable request">
    <a href="?id=RiverXkbKeyboardV1_CapslockEnable" id="RiverXkbKeyboardV1_CapslockEnable">
        <span class="codicon codicon-symbol-method method"></span>
        RiverXkbKeyboardV1.<span class="method">CapslockEnable</span>
    </a>
</h3>

```csharp
void CapslockEnable()
```


**Enable capslock**

Enable capslock for the keyboard.

<h3 class="decleration request" title="CapslockDisable request">
    <a href="?id=RiverXkbKeyboardV1_CapslockDisable" id="RiverXkbKeyboardV1_CapslockDisable">
        <span class="codicon codicon-symbol-method method"></span>
        RiverXkbKeyboardV1.<span class="method">CapslockDisable</span>
    </a>
</h3>

```csharp
void CapslockDisable()
```


**Disable capslock**

Disable capslock for the keyboard.

<h3 class="decleration request" title="NumlockEnable request">
    <a href="?id=RiverXkbKeyboardV1_NumlockEnable" id="RiverXkbKeyboardV1_NumlockEnable">
        <span class="codicon codicon-symbol-method method"></span>
        RiverXkbKeyboardV1.<span class="method">NumlockEnable</span>
    </a>
</h3>

```csharp
void NumlockEnable()
```


**Enable numlock**

Enable numlock for the keyboard.

<h3 class="decleration request" title="NumlockDisable request">
    <a href="?id=RiverXkbKeyboardV1_NumlockDisable" id="RiverXkbKeyboardV1_NumlockDisable">
        <span class="codicon codicon-symbol-method method"></span>
        RiverXkbKeyboardV1.<span class="method">NumlockDisable</span>
    </a>
</h3>

```csharp
void NumlockDisable()
```


**Disable numlock**

Disable numlock for the keyboard.

<h3 class="decleration event" title="Removed event">
    <a href="?id=OnRiverXkbKeyboardV1_Removed" id="OnRiverXkbKeyboardV1_Removed">
        <span class="codicon codicon-symbol-event event"></span>
        RiverXkbKeyboardV1.<span class="event">OnRemoved</span>
    </a>
</h3>

```csharp
void RemovedHandler()
```


**The xkb keyboard is removed**

This event indicates that the xkb keyboard has been removed.

The server will send no further events on this object and ignore any
request (other than river_xkb_keyboard_v1.destroy) made after this event
is sent. The client should destroy this object with the
river_xkb_keyboard_v1.destroy request to free up resources.

<h3 class="decleration event" title="InputDevice event">
    <a href="?id=OnRiverXkbKeyboardV1_InputDevice" id="OnRiverXkbKeyboardV1_InputDevice">
        <span class="codicon codicon-symbol-event event"></span>
        RiverXkbKeyboardV1.<span class="event">OnInputDevice</span>
    </a>
</h3>

```csharp
void InputDeviceHandler(RiverInputDeviceV1 device)
```

| Argument | Type | Description |
| --- | --- | --- |
| device | object |  |

**Corresponding river input device**

The river_input_device_v1 corresponding to this xkb keyboard. This event
will always be the first event sent on the river_xkb_keyboard_v1 object,
and it will be sent exactly once.

<h3 class="decleration event" title="Layout event">
    <a href="?id=OnRiverXkbKeyboardV1_Layout" id="OnRiverXkbKeyboardV1_Layout">
        <span class="codicon codicon-symbol-event event"></span>
        RiverXkbKeyboardV1.<span class="event">OnLayout</span>
    </a>
</h3>

```csharp
void LayoutHandler(uint index, string? name)
```

| Argument | Type | Description |
| --- | --- | --- |
| index | uint |  |
| name | string |  |

**Currently active layout**

The currently active layout index and name. The name arg may be null if
the active layout does not have a name.

This event is sent once when the river_xkb_keyboard_v1 is created and
again whenever the layout changes.

<h3 class="decleration event" title="CapslockEnabled event">
    <a href="?id=OnRiverXkbKeyboardV1_CapslockEnabled" id="OnRiverXkbKeyboardV1_CapslockEnabled">
        <span class="codicon codicon-symbol-event event"></span>
        RiverXkbKeyboardV1.<span class="event">OnCapslockEnabled</span>
    </a>
</h3>

```csharp
void CapslockEnabledHandler()
```


**Capslock is currently enabled**

Capslock is currently enabled for the keyboard.

This event is sent once when the river_xkb_keyboard_v1 is created and
again whenever the capslock state changes.

<h3 class="decleration event" title="CapslockDisabled event">
    <a href="?id=OnRiverXkbKeyboardV1_CapslockDisabled" id="OnRiverXkbKeyboardV1_CapslockDisabled">
        <span class="codicon codicon-symbol-event event"></span>
        RiverXkbKeyboardV1.<span class="event">OnCapslockDisabled</span>
    </a>
</h3>

```csharp
void CapslockDisabledHandler()
```


**Capslock is currently disabled**

Capslock is currently disabled for the keyboard.

This event is sent once when the river_xkb_keyboard_v1 is created and
again whenever the capslock state changes.

<h3 class="decleration event" title="NumlockEnabled event">
    <a href="?id=OnRiverXkbKeyboardV1_NumlockEnabled" id="OnRiverXkbKeyboardV1_NumlockEnabled">
        <span class="codicon codicon-symbol-event event"></span>
        RiverXkbKeyboardV1.<span class="event">OnNumlockEnabled</span>
    </a>
</h3>

```csharp
void NumlockEnabledHandler()
```


**Numlock is currently enabled**

Numlock is currently enabled for the keyboard.

This event is sent once when the river_xkb_keyboard_v1 is created and
again whenever the numlock state changes.

<h3 class="decleration event" title="NumlockDisabled event">
    <a href="?id=OnRiverXkbKeyboardV1_NumlockDisabled" id="OnRiverXkbKeyboardV1_NumlockDisabled">
        <span class="codicon codicon-symbol-event event"></span>
        RiverXkbKeyboardV1.<span class="event">OnNumlockDisabled</span>
    </a>
</h3>

```csharp
void NumlockDisabledHandler()
```


**Numlock is currently disabled**

Numlock is currently disabled for the keyboard.

This event is sent once when the river_xkb_keyboard_v1 is created and
again whenever the numlock state changes.

<h3 class="decleration enum" title="Error enum">
    <a href="?id=Error" id="Error">
        <span class="codicon codicon-symbol-enum"></span>
        Error
    </a>
</h3>

```csharp
public enum Error : uint
```

| Value | Integer | Description |
| --- | --- | --- |
| InvalidKeymap | 0 |  |
