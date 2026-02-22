namespace WaylandDotnet.Scanner.Data;

public record ProtocolMetadata
{
    public required string Name { get; init; }
    public required string XmlFile { get; init; }
    public required string OutputDir { get; init; }
    public required string Namespace { get; init; }
    public string? Link { get; init; }
    public string? DocsDir { get; init; }
    public bool Debug { get; init; }
}
