namespace WaylandDotnet.Internal;

using System.Runtime.InteropServices;

/// <summary> Marshaling helpers for Wayland wire types. </summary>
public static class WaylandMarshal
{
    /// <summary> Copies a native Wayland array into a managed byte array. </summary>
    /// <param name="array">The native array pointer.</param>
    /// <returns>The array contents, or an empty array when absent.</returns>
    public unsafe static byte[] ToSpan(WlArray* array)
    {
        if (array == null || array->data == null || array->size == 0)
        {
            return [];
        }

        return new ReadOnlySpan<byte>(array->data, array->size).ToArray();
    }

    /// <summary> Allocates a native Wayland array from managed bytes. </summary>
    /// <param name="data">The bytes to copy, or null for an empty array.</param>
    /// <returns>A pointer to the allocated array, or null when empty.</returns>
    public unsafe static WlArray* CreateWlArray(byte[]? data)
    {
        if (data == null || data.Length == 0)
        {
            return null;
        }

        var arrayPtr = (WlArray*)Marshal.AllocHGlobal(sizeof(WlArray));
        var dataPtr = Marshal.AllocHGlobal(data.Length);

        CopyMemory(dataPtr, data, data.Length);

        arrayPtr->size = data.Length;
        arrayPtr->alloc = data.Length;
        arrayPtr->data = (void*)dataPtr;

        return arrayPtr;
    }

    private static unsafe void CopyMemory(nint dest, byte[] src, int length)
    {
        for (int i = 0; i < length; i++)
        {
            ((byte*)dest)[i] = src[i];
        }
    }
}

/// <summary> Factory for wrapping native Wayland proxy handles. </summary>
/// <typeparam name="T">The Wayland object type.</typeparam>
public interface IWaylandObjectFactory<T> where T : WaylandObject
{
    /// <summary> Used internally for generic binding. </summary>
    public static abstract string _StaticInterfaceName { get; }

    /// <summary> Creates a typed wrapper from a native proxy handle. </summary>
    /// <param name="handle">The native Wayland proxy handle.</param>
    /// <param name="display">The display connection, when required by the interface.</param>
    /// <returns>A new wrapper instance.</returns>
    public static abstract T Create(nint handle, WlDisplay? display);
}