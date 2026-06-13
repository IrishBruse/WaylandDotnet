namespace WaylandDotnet;

using System;

/// <summary>
/// Untyped Wayland object wrapper for protocol arguments without a declared interface.
/// </summary>
public sealed class WaylandProxy : WaylandObject
{
    /// <summary> Wraps an existing Wayland proxy handle. </summary>
    public WaylandProxy(IntPtr handle, WlDisplay? display = null)
    {
        Handle = handle;
        Display = display;
    }
}