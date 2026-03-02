namespace WaylandDotnet;

using System;

/// <summary>
/// Base class for all Wayland objects
/// </summary>
public abstract class WaylandObject
{
    public IntPtr Handle { get; set; }
    public WlDisplay? Display { get; }
}
