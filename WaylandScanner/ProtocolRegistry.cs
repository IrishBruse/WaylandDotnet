namespace WaylandScanner;

using WaylandScanner.Data;

public class ProtocolRegistry
{
    public static ProtocolMetadata[] GetProtocols()
    {
        var protocols = new List<ProtocolMetadata>();

        protocols.Add(CreateMetadata("Core", "wayland", "Wayland"));
        protocols.Add(CreateMetadata("Stable", "xdg-shell", "XDG shell"));
        protocols.Add(CreateMetadata("Wlr", "wlr-layer-shell-unstable-v1", "Layer Shell"));

        return protocols.ToArray();
    }

    private static ProtocolMetadata CreateMetadata(string category, string fileName, string name)
    {
        return new()
        {
            Name = name,
            XmlFile = $"Protocols/{category}/{fileName}.xml",
            Namespace = category,
            OutputDir = $"../WaylandDotnet/Protocols/{category}/{fileName}/",
            Link = $"https://wayland.app/protocols/{fileName}"
        };
    }
}
