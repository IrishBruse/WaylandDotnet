# Tutorials

Learn how to build Wayland applications using WaylandDotnet's C# bindings.

## Your First Window

This tutorial walks through creating a minimal window that displays a solid color. We'll cover the key differences between WaylandDotnet's C# API and traditional C Wayland development.

### The Minimal Example

The complete example is in `Examples/Minimal/Program.cs`. Here's what makes WaylandDotnet different from C:

#### Namespaced Protocols

Instead of C's flat naming (`wl_display`, `xdg_wm_base`), WaylandDotnet organizes protocols into namespaces:

```csharp
using WaylandDotnet;        // Core protocol: WlDisplay, WlRegistry, etc.
using WaylandDotnet.Stable; // Stable protocols: XdgWmBase, XdgSurface, etc.
using WaylandDotnet.Internal; // Utilities like ShmBuffer
```

#### Type-Safe Protocol Binding

In C, you use `wl_registry_bind` with interface pointers. In WaylandDotnet, protocols expose static `InterfaceName` properties and you use the generic `Bind<T>` method:

```csharp
// C way:
// wl_compositor *compositor = wl_registry_bind(registry, name, &wl_compositor_interface, version);

// WaylandDotnet way:
registry.OnGlobal += (name, interfaceName, version) =>
{
    if (interfaceName == WlCompositor.InterfaceName)
        compositor = registry.Bind<WlCompositor>(interfaceName, version, name);
};
```

Key differences:
- **InterfaceName**: Each protocol class exposes its interface name as a static property
- **Generic Bind<T>()**: Type-safe binding with compile-time checking
- **No manual casting**: The generic method returns the correct type directly

#### C# Events Instead of Callbacks

Wayland's C API uses callback function pointers. WaylandDotnet maps these to C# events:

```csharp
// C way:
// static void registry_global(void *data, struct wl_registry *registry, uint32_t name, ...)
// wl_registry_add_listener(registry, &registry_listener, NULL);

// WaylandDotnet way:
registry.OnGlobal += (name, interfaceName, version) =>
{
    // Handle new global
};

xdgSurface.OnConfigure += (serial) =>
{
    xdgSurface.AckConfigure(serial);
};

topLevel.OnClose += () => running = false;
```

Benefits:
- **Multiple handlers**: You can attach multiple delegates to events
- **Anonymous methods**: Use lambdas for inline handling
- **Automatic unmarshaling**: Event parameters are already converted to C# types

#### Nullable Reference Types

WaylandDotnet uses C# nullable reference types to indicate which objects might not be available:

```csharp
WlCompositor? compositor = null;
XdgWmBase? xdg = null;
WlShm? shm = null;
```

The null-forgiving operator (`!`) tells the compiler you know the value is set:

```csharp
WlSurface surface = compositor!.CreateSurface();
XdgSurface xdgSurface = xdg!.GetXdgSurface(surface);
```

#### Resource Management

WaylandDotnet follows C#'s IDisposable pattern, but Wayland objects also have explicit `Destroy()` methods:

```csharp
// Clean up in reverse order of creation
buffer.Destroy();
surface.Destroy();
xdgSurface.Destroy();
topLevel.Destroy();
display.Disconnect();
```

Unlike C, you don't need to manage proxy wrappers or listener structs.

#### Helper Utilities

WaylandDotnet provides convenience methods for common operations. The `ShmBuffer` class handles shared memory buffer creation:

```csharp
// Create a solid color buffer without manual memory mapping
WlBuffer? buffer = ShmBuffer.CreateSolidColorBuffer(shm, width, height, 0xFF6495ED);
```

In C, this would require manually creating a tempfile, mmap, and wl_shm_pool.

#### Method Naming Conventions

WaylandDotnet uses C# PascalCase naming conventions:

| C Name                         | C# Name                      |
| ------------------------------ | ---------------------------- |
| `wl_display_connect`           | `WlDisplay.Connect()`        |
| `wl_compositor_create_surface` | `compositor.CreateSurface()` |
| `xdg_toplevel_set_title`       | `topLevel.SetTitle()`        |
| `wl_surface_commit`            | `surface.Commit()`           |

### Event Loop

The event loop in WaylandDotnet looks similar to C but uses C# threading:

```csharp
while (running)
{
    display.Dispatch();      // Process Wayland events
    Thread.Sleep(16);        // ~60 FPS (Just for demo)
}
```

### Next Steps

Once you understand the basics:

- **Layer Shell Tutorial** - Create overlays and panels with wlr-layer-shell
- **Protocol Discovery** - Handle missing protocols gracefully
- **Rendering** - Integrate with GPU libraries like SDL3

## See Also

- [Protocol Reference](../Protocols/README.md) - Available protocols and namespaces
- [Examples](../../Examples/) - Complete working applications
