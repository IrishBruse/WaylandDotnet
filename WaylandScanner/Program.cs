namespace WaylandScanner;

using System;
using System.IO;
using System.Linq;
using WaylandDotnet;
using WaylandScanner.Data;

public class Program
{
    static void Main()
    {
        ProtocolMetadata[] protocols = ProtocolRegistry.GetProtocols();

        foreach (var protocol in protocols)
        {
            var inputFile = protocol.XmlFile;
            var outputFile = protocol.OutputDir;

            if (!File.Exists(inputFile))
            {
                Console.WriteLine($"Error: File '{inputFile}' not found.");
                continue;
            }

            var outputDir = Path.GetDirectoryName(outputFile);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            var generator = new ProtocolGenerator();
            generator.Generate(protocol);
        }

        GenerateSidebar(protocols);

        Console.WriteLine("Code generation complete");
    }

    private static void GenerateSidebar(ProtocolMetadata[] protocols)
    {
        var sb = new SourceFile("../docs/Protocols/_sidebar.md");
        sb.WriteLine("- [Home](/)");
        sb.WriteLine();
        sb.WriteLine("Global");
        sb.WriteLine();
        sb.WriteLine("- [Protocols](/Protocols/)");
        sb.WriteLine();

        var grouped = protocols.GroupBy(p => p.Namespace).OrderBy(g =>
        {
            return g.Key switch
            {
                "Core" => 0,
                "Stable" => 1,
                "Wlr" => 2,
                _ => 3
            };
        });

        foreach (var group in grouped)
        {
            sb.WriteLine($"{group.Key}");
            foreach (var protocol in group)
            {
                var fileName = Path.GetFileNameWithoutExtension(protocol.XmlFile);
                sb.WriteLine($"- [{protocol.Name}](/Protocols/{protocol.Namespace}/{fileName}.md)");
            }
            sb.WriteLine();
        }

        sb.Save();
    }
}