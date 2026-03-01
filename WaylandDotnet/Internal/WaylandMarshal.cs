namespace WaylandDotnet.Internal;

using System.Runtime.InteropServices;

public static class WaylandMarshal
{
    public unsafe static byte[] ToSpan(WlArray* array)
    {
        if (array == null || array->data == null || array->size == 0)
        {
            return [];
        }

        return new ReadOnlySpan<byte>(array->data, array->size).ToArray();
    }

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

public interface IWaylandObjectFactory<T> where T : WaylandObject
{
    /// <summary> Used interally for generics </summary>
    public static abstract string _StaticInterfaceName { get; }
    public static abstract T Create(nint handle, WlDisplay? display);
}