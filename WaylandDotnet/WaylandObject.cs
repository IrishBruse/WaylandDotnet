namespace WaylandDotnet;

using System;

/// <summary>
/// Base class for all Wayland objects
/// </summary>
public abstract class WaylandObject
{
    /// <summary> The native Wayland proxy handle. </summary>
    public IntPtr Handle { get; set; }

    /// <summary> The display connection that owns this object, when applicable. </summary>
    public WlDisplay? Display { get; protected set; }
}
