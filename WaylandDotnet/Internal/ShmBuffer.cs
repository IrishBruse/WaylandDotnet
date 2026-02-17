namespace WaylandDotnet.Internal;

using System.Runtime.InteropServices;

/// <summary>
/// Helper for creating shared memory buffers for Wayland surfaces.
/// This provides a pure C# interface for the Linux-specific operations needed for wl_shm.
/// </summary>
public static unsafe class ShmBuffer
{
    [DllImport("libc", SetLastError = true)]
    private static extern int memfd_create(string name, uint flags);

    [DllImport("libc", SetLastError = true)]
    private static extern int ftruncate(int fd, long length);

    [DllImport("libc", SetLastError = true)]
    private static extern IntPtr mmap(IntPtr addr, nint length, int prot, int flags, int fd, long offset);

    [DllImport("libc", SetLastError = true)]
    private static extern int munmap(IntPtr addr, nint length);

    [DllImport("libc", SetLastError = true)]
    private static extern int close(int fd);

    private const int PROT_READ = 0x1;
    private const int PROT_WRITE = 0x2;
    private const int MAP_SHARED = 0x01;
    private const uint MFD_CLOEXEC = 0x0001;

    /// <summary>
    /// Creates a shared memory buffer filled with a solid color.
    /// </summary>
    /// <param name="shm">The wl_shm global</param>
    /// <param name="width">Buffer width in pixels</param>
    /// <param name="height">Buffer height in pixels</param>
    /// <param name="color">ARGB color value (e.g., 0xFF0000FF for blue)</param>
    /// <returns>A WlBuffer that can be attached to a surface, or null on failure</returns>
    public static WlBuffer? CreateSolidColorBuffer(WlShm shm, int width, int height, uint color)
    {
        int stride = width * 4;
        int size = stride * height;

        int fd = memfd_create("wayland-buffer", MFD_CLOEXEC);
        if (fd < 0) return null;

        if (ftruncate(fd, size) != 0)
        {
            close(fd);
            return null;
        }

        IntPtr data = mmap(IntPtr.Zero, size, PROT_READ | PROT_WRITE, MAP_SHARED, fd, 0);
        if (data == new IntPtr(-1))
        {
            close(fd);
            return null;
        }

        // Fill with color (convert ARGB to BGRA for XRGB format)
        byte b = (byte)(color & 0xFF);
        byte g = (byte)((color >> 8) & 0xFF);
        byte r = (byte)((color >> 16) & 0xFF);
        byte a = (byte)((color >> 24) & 0xFF);

        byte* pixels = (byte*)data;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                pixels[y * stride + x * 4 + 0] = b;
                pixels[y * stride + x * 4 + 1] = g;
                pixels[y * stride + x * 4 + 2] = r;
                pixels[y * stride + x * 4 + 3] = a;
            }
        }

        munmap(data, size);

        WlShmPool pool = shm.CreatePool(fd, size);
        WlBuffer buffer = pool.CreateBuffer(0, width, height, stride, (uint)WlShm.Format.Xrgb8888);
        pool.Destroy();
        close(fd);

        return buffer;
    }
}
