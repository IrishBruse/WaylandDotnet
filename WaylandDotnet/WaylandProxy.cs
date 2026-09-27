namespace WaylandDotnet;

using System;
using System.Diagnostics;

/// <summary>
/// Untyped Wayland object wrapper for protocol arguments without a declared interface.
/// </summary>
public sealed class WaylandProxy : WaylandObject
{
    /// <summary> Wraps an existing Wayland proxy handle. </summary>
    public WaylandProxy(IntPtr handle, WlDisplay? display = null)
    {
        Debug.Assert(handle != IntPtr.Zero, "Wayland proxy handle is null");
        Handle = handle;
        Display = display;
    }
}