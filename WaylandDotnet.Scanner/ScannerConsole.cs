namespace WaylandDotnet.Scanner;

using WaylandDotnet.Scanner.Data;

internal static class ScannerConsole
{
    const string Reset = "\e[0m";
    const string Bold = "\e[1m";
    const string Dim = "\e[2m";
    const string Red = "\e[31m";
    const string Green = "\e[32m";
    const string Yellow = "\e[33m";
    const string Cyan = "\e[36m";

    static readonly bool useColor = ResolveUseColor();

    static bool ResolveUseColor()
    {
        if (Environment.GetEnvironmentVariable("NO_COLOR") is not null)
        {
            return false;
        }

        if (Environment.GetEnvironmentVariable("FORCE_COLOR") is not null)
        {
            return true;
        }

        return !Console.IsOutputRedirected;
    }

    static string Paint(string code, string text) => useColor ? $"{code}{text}{Reset}" : text;

    public static void WritePhase(string phase)
    {
        Console.WriteLine();
        Console.WriteLine(Paint(Bold + Cyan, $"wayland-dotnet-scanner") + Paint(Dim, $" - {phase}"));
        Console.WriteLine();
    }

    public static void WriteSection(string label)
    {
        Console.WriteLine(Paint(Bold, $"  {label}"));
    }

    public static void WriteGenerated(string fileName)
    {
        Console.WriteLine($"    {Paint(Green, "+")} {fileName}");
    }

    public static void WriteSkipped(string detail)
    {
        Console.WriteLine($"    {Paint(Yellow, "~")} {Paint(Dim, detail)}");
    }

    public static void WriteDownloaded(string name)
    {
        Console.WriteLine($"    {Paint(Green, "+")} {name}");
    }

    public static void WriteDone(string message)
    {
        Console.WriteLine();
        Console.WriteLine(Paint(Bold + Green, message));
        Console.WriteLine();
    }

    public static void WriteInfo(string message)
    {
        Console.WriteLine(Paint(Dim, message));
    }

    public static void WriteError(string message)
    {
        Console.Error.WriteLine(Paint(Red, message));
    }

    public static string ProtocolLabel(ProtocolMetadata metadata)
    {
        var id = Path.GetFileNameWithoutExtension(metadata.XmlFile);
        return $"{metadata.Namespace} / {id}";
    }

}

internal sealed class GenerationReport
{
    readonly List<string> generated = [];
    readonly List<string> skipped = [];

    public void AddGenerated(string fileName) => generated.Add(fileName);

    public void AddSkipped(string detail) => skipped.Add(detail);

    public int GeneratedCount => generated.Count;

    public void Print(ProtocolMetadata metadata)
    {
        ScannerConsole.WriteSection(ScannerConsole.ProtocolLabel(metadata));

        var remainingSkipped = new List<string>(skipped);

        foreach (var file in generated)
        {
            ScannerConsole.WriteGenerated(file);

            var typeName = Path.GetFileNameWithoutExtension(file);
            for (var i = remainingSkipped.Count - 1; i >= 0; i--)
            {
                if (!remainingSkipped[i].StartsWith(typeName + ".", StringComparison.Ordinal))
                {
                    continue;
                }

                ScannerConsole.WriteSkipped(remainingSkipped[i]);
                remainingSkipped.RemoveAt(i);
            }
        }

        foreach (var detail in remainingSkipped)
        {
            ScannerConsole.WriteSkipped(detail);
        }
    }

    public void Clear()
    {
        generated.Clear();
        skipped.Clear();
    }
}
