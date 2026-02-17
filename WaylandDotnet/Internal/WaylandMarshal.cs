namespace WaylandDotnet.Internal;

public static class WaylandMarshal
{
    public static T CreateTypedObject<T>(nint newProxy, WlDisplay display) where T : WaylandObject, IWaylandObjectFactory<T>
    {
        return T.Create(newProxy, display);
    }

    public unsafe static byte[] ToSpan(WlArray* array)
    {
        if (array == null || array->data == null || array->size == 0)
        {
            return [];
        }

        return new ReadOnlySpan<byte>(array->data, array->size).ToArray();
    }
}

public interface IWaylandObjectFactory<T> where T : WaylandObject
{
    static abstract T Create(nint handle, WlDisplay display);
}