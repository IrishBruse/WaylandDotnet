namespace WaylandDotnet.Scanner.Data;

using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("protocol")]
[XmlInclude(typeof(WaylandInterface))]
public record WaylandProtocol
{
    [XmlAttribute("name")]
    public required string Name { get; init; }

    [XmlElement("copyright")]
    public required string Copyright { get; init; }

    [XmlElement("interface")]
    public required List<WaylandInterface> Interfaces { get; init; } = [];
}

public record WaylandInterface
{
    [XmlAttribute("name")]
    public required string Name { get; init; }

    [XmlAttribute("version")]
    public required uint Version { get; init; }

    [XmlElement("description")]
    public required WaylandDescription Description { get; init; }

    [XmlElement("request")]
    public required List<WaylandRequest> Requests { get; init; } = [];

    [XmlElement("event")]
    public required List<WaylandEvent> Events { get; init; } = [];

    [XmlElement("enum")]
    public required List<WaylandEnum> Enums { get; init; } = [];
}

public record WaylandDescription
{
    [XmlAttribute("summary")]
    public required string Summary { get; init; }

    [XmlText]
    public required string Text { get; init; }
}

public record WaylandRequest
{
    [XmlAttribute("name")]
    public required string Name { get; init; }

    [XmlAttribute("type")]
    public required string Type { get; init; }

    [XmlAttribute("since")]
    public required int Since { get; init; }

    [XmlIgnore]
    public required bool SinceSpecified { get; init; }

    [XmlElement("description")]
    public required WaylandDescription Description { get; init; }

    [XmlElement("arg")]
    public required List<WaylandArg> Args { get; init; } = [];
}

public record WaylandEvent
{
    [XmlAttribute("name")]
    public required string Name { get; init; }

    [XmlAttribute("type")]
    public required string Type { get; init; }

    [XmlAttribute("since")]
    public required int Since { get; init; }

    [XmlIgnore]
    public required bool SinceSpecified { get; init; }

    [XmlElement("description")]
    public required WaylandDescription Description { get; init; }

    [XmlElement("arg")]
    public required List<WaylandArg> Args { get; init; } = [];
}

public record WaylandArg
{
    [XmlAttribute("name")]
    public required string Name { get; init; }

    [XmlAttribute("type")]
    public required string Type { get; init; }

    [XmlAttribute("summary")]
    public required string Summary { get; init; }

    [XmlAttribute("interface")]
    public required string Interface { get; init; }

    [XmlAttribute("since")]
    public required string Since { get; init; }

    [XmlAttribute("allow-null")]
    public required string AllowNull { get; init; }

    [XmlAttribute("enum")]
    public required string EnumType { get; init; }

    [XmlElement("description")]
    public required WaylandDescription Description { get; init; }
}

public record WaylandEnum
{
    [XmlAttribute("name")]
    public required string Name { get; init; }

    [XmlAttribute("since")]
    public required int Since { get; init; }

    [XmlIgnore]
    public required bool SinceSpecified { get; init; }

    [XmlAttribute("bitfield")]
    public required string Bitfield { get; init; }

    [XmlElement("description")]
    public required WaylandDescription Description { get; init; }

    [XmlElement("entry")]
    public required List<WaylandEntry> Entries { get; init; } = [];
}

public record WaylandEntry
{
    [XmlAttribute("name")]
    public required string Name { get; init; }

    [XmlAttribute("value")]
    public required string Value { get; init; }

    [XmlAttribute("summary")]
    public required string Summary { get; init; }

    [XmlAttribute("since")]
    public required int Since { get; init; }

    [XmlIgnore]
    public required bool SinceSpecified { get; init; }

    [XmlElement("description")]
    public required WaylandDescription Description { get; init; }
}