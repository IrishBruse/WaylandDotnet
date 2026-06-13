namespace WaylandDotnet.Tests;

using WaylandDotnet.Scanner;
using WaylandDotnet.Scanner.Data;

internal static class ProtocolGeneratorTestHelpers
{
    public static WaylandDescription Desc(string summary = "", string text = "") =>
        new() { Summary = summary, Text = text };

    public static WaylandArg Arg(
        string name,
        string type,
        string summary = "",
        string iface = "",
        string since = "",
        string allowNull = "",
        string enumType = "") =>
        new()
        {
            Name = name,
            Type = type,
            Summary = summary,
            Interface = iface,
            Since = since,
            AllowNull = allowNull,
            EnumType = enumType,
            Description = Desc(summary)
        };

    public static WaylandRequest Req(
        string name,
        string type = "",
        int since = 0,
        string summary = "",
        string text = "",
        params WaylandArg[] args) =>
        new()
        {
            Name = name,
            Type = type,
            Since = since,
            SinceSpecified = since > 0,
            Description = Desc(summary, text),
            Args = [.. args]
        };

    public static WaylandEvent Evt(
        string name,
        string type = "",
        int since = 0,
        string summary = "",
        string text = "",
        params WaylandArg[] args) =>
        new()
        {
            Name = name,
            Type = type,
            Since = since,
            SinceSpecified = since > 0,
            Description = Desc(summary, text),
            Args = [.. args]
        };

    public static WaylandEnum EnumDef(
        string name,
        bool bitfield = false,
        params (string name, string value, string summary)[] entries)
    {
        return new WaylandEnum
        {
            Name = name,
            Since = 0,
            SinceSpecified = false,
            Bitfield = bitfield ? "true" : "",
            Description = Desc($"{name} enum"),
            Entries = entries.Select(e => new WaylandEntry
            {
                Name = e.name,
                Value = e.value,
                Summary = e.summary,
                Since = 0,
                SinceSpecified = false,
                Description = Desc(e.summary)
            }).ToList()
        };
    }

    public static WaylandInterface Iface(
        string name,
        uint version = 1,
        string summary = "",
        string text = "",
        IEnumerable<WaylandRequest>? requests = null,
        IEnumerable<WaylandEvent>? events = null,
        IEnumerable<WaylandEnum>? enums = null) =>
        new()
        {
            Name = name,
            Version = version,
            Description = Desc(summary, text),
            Requests = requests?.ToList() ?? [],
            Events = events?.ToList() ?? [],
            Enums = enums?.ToList() ?? []
        };

    public static ProtocolMetadata Metadata(
        string xmlFile,
        string outputDir,
        string ns = "Test",
        string? link = null,
        string? docsDir = null,
        bool debug = false) =>
        new()
        {
            Name = "Test Protocol",
            XmlFile = xmlFile,
            OutputDir = outputDir,
            Namespace = ns,
            Link = link,
            DocsDir = docsDir,
            Debug = debug
        };

    public static string GenerateInterfaceSource(
        ProtocolGenerator generator,
        ProtocolMetadata metadata,
        WaylandInterface iface)
    {
        generator.sb.Clear();
        generator.indentLevel = 0;
        generator.GenerateFileHeader(metadata);
        generator.GenerateInterface(iface);
        return generator.sb.ToString();
    }
}
