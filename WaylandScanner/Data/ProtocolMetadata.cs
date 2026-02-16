namespace WaylandScanner.Data;

public record ProtocolMetadata
{
    public required string Name { get; init; }
    public required string XmlFile { get; init; }
    public required string OutputDir { get; init; }
    public required string Namespace { get; init; }
    public required string Link { get; init; }
}