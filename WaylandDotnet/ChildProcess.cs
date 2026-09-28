namespace WaylandDotnet;

using System.Runtime.InteropServices;

/// <summary>
/// POSIX child-process helpers for long-running Wayland clients.
/// </summary>
public static partial class ChildProcess
{
    private const int SigChld = 17;
    private static readonly IntPtr SigIgn = 1;

    /// <summary>
    /// Ignores child exit notifications so the kernel auto-reaps terminated children.
    /// Call this before spawning subprocesses (for example a terminal emulator) from a client that does not wait on <c>waitpid</c>.
    /// </summary>
    public static void IgnoreExit()
    {
        signal(SigChld, SigIgn);
    }

    [LibraryImport("libc", EntryPoint = "signal")]
    private static partial IntPtr signal(int signum, IntPtr handler);
}
