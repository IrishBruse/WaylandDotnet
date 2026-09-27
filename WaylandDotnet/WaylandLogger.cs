namespace WaylandDotnet;

using System.Diagnostics;
using System.Runtime.InteropServices;
using WaylandDotnet.Internal;

/// <summary> Routes Wayland client library log output to the console. </summary>
public static partial class WaylandLogger
{
    private static wl_log_func_t? clientLogHandler;

    [LibraryImport("libc")]
    private static partial int vsnprintf(IntPtr buffer, UIntPtr size, IntPtr format, IntPtr args);

    /// <summary> Installs the Wayland client log handler. </summary>
    public static void Initialize()
    {
        clientLogHandler = ClientLogHandler;
        WaylandNative.LogSetHandlerClient(clientLogHandler);
    }

    private static void ClientLogHandler(IntPtr fmt, IntPtr args)
    {
        try
        {
            int bufferSize = 4096;
            IntPtr buffer = Marshal.AllocHGlobal(bufferSize);

            try
            {
                Debug.Assert(fmt != IntPtr.Zero);
                int length = vsnprintf(buffer, (UIntPtr)bufferSize, fmt, args);

                if (length < 0)
                {
                    Console.Error.WriteLine("[Wayland] Error formatting log message.");
                    return;
                }

                Debug.Assert(length >= 0 && length < bufferSize);
                string message = Marshal.PtrToStringAnsi(buffer, length) ?? "[Wayland] <empty>";
                Console.Error.WriteLine($"[Wayland] {message}");
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[Wayland] Exception in log handler: {ex.Message}");
        }
    }
}