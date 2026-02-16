namespace WaylandDotnet;

using System.Runtime.CompilerServices;
using System.Text;

public class SourceFile
{
    public string Path { get; }

    private readonly StreamWriter? writer;
    private int indentLevel;

    public SourceFile(string path)
    {
        Path = path;

        if (path == "") return;

        var directory = System.IO.Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        writer = new StreamWriter(path, false, Encoding.UTF8);
    }

    public void WriteLine(string text)
    {
        writer?.WriteLine(text);
    }

    public void WriteLine()
    {
        writer?.WriteLine();
    }

    public void Write(string text)
    {
        writer?.Write(text);
    }


    public void BeginBlock()
    {
        WriteLine("{");
        indentLevel++;
    }

    public void EndBlock(string suffix = "")
    {
        indentLevel--;
        WriteLine("}" + suffix);
    }

    public void BeginRegion([CallerMemberName] string methodName = "")
    {
        WriteLine();
        WriteLine($"#region {methodName}");
        WriteLine();
    }

    public void EndRegion([CallerMemberName] string methodName = "")
    {
        WriteLine();
        WriteLine($"#endregion // {methodName}");
        WriteLine();
    }

    public void Save()
    {
        writer?.Flush();
        writer?.Dispose();
        Console.WriteLine("Saved " + Path);
    }
}
