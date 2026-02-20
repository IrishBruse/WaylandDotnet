namespace WaylandScanner;

using System.CommandLine;
using System.IO;
using System.Linq;
using System.Text.Json;
using WaylandDotnet;
using WaylandScanner.Data;

public class Program
{
    static async Task<int> Main(string[] args)
    {
        var inputArg = new Argument<FileInfo?>("input")
        {
            Description = "Input XML protocol file",
            Arity = ArgumentArity.ZeroOrOne
        };

        var outputArg = new Argument<DirectoryInfo?>("output")
        {
            Description = "Output directory for generated C# files",
            Arity = ArgumentArity.ZeroOrOne
        };

        var namespaceOption = new Option<string?>("--namespace", "-n")
        {
            Description = "Namespace/category for the protocol (e.g., Core, Stable, Wlr)"
        };

        var nameOption = new Option<string?>("--name")
        {
            Description = "Display name for the protocol"
        };

        var linkOption = new Option<string?>("--link")
        {
            Description = "Link to protocol documentation"
        };

        var docsOption = new Option<DirectoryInfo?>("--docs")
        {
            Description = "Output directory for generated documentation"
        };

        var configOption = new Option<FileInfo?>("--config", "-c")
        {
            DefaultValueFactory = (_) => new FileInfo("./protocols.json"),
            Description = "JSON config file with multiple protocols"
        };

        var rootCommand = new RootCommand("Wayland protocol scanner - Generate C# bindings from Wayland XML protocols")
        {
            inputArg,
            outputArg,
            namespaceOption,
            nameOption,
            linkOption,
            docsOption,
            configOption
        };

        rootCommand.SetAction((parseResult) =>
        {
            var config = parseResult.GetValue(configOption);

            if (config != null)
            {
                GenerateFromConfig(config);
                return;
            }

            var input = parseResult.GetValue(inputArg);
            var output = parseResult.GetValue(outputArg);
            var ns = parseResult.GetValue(namespaceOption);
            var name = parseResult.GetValue(nameOption);
            var link = parseResult.GetValue(linkOption);
            var docs = parseResult.GetValue(docsOption);

            if (input == null || output == null)
            {
                Console.Error.WriteLine("Error: input and output are required");
                Console.Error.WriteLine("Usage: WaylandScanner <input.xml> <output-dir> [options]");
                return;
            }

            var metadata = new ProtocolMetadata
            {
                Name = name ?? Path.GetFileNameWithoutExtension(input.Name),
                XmlFile = input.FullName,
                OutputDir = output.FullName,
                Namespace = ns ?? "Generated",
                Link = link ?? $"https://wayland.app/protocols/{Path.GetFileNameWithoutExtension(input.Name)}",
                DocsDir = docs?.FullName
            };

            GenerateProtocol(metadata);
        });

        var listCommand = new Command("list", "List available protocols in config file");
        listCommand.Add(configOption);

        listCommand.SetAction((parseResult) =>
        {
            var config = parseResult.GetValue(configOption);
            if (config == null || !config.Exists)
            {
                Console.Error.WriteLine("Error: Config file not found");
                return;
            }

            var protocols = LoadConfig(config);
            foreach (var p in protocols)
            {
                Console.WriteLine($"{p.Namespace}/{p.Name}: {p.XmlFile}");
            }
        });

        rootCommand.Subcommands.Add(listCommand);

        return rootCommand.Parse(args).InvokeAsync().Result;
    }

    static void GenerateFromConfig(FileInfo config)
    {
        if (!config.Exists)
        {
            Console.Error.WriteLine($"Error: Config file '{config.FullName}' not found.");
            return;
        }

        var protocols = LoadConfig(config);

        foreach (var protocol in protocols)
        {
            if (!File.Exists(protocol.XmlFile))
            {
                Console.Error.WriteLine($"Error: File '{protocol.XmlFile}' not found.");
                continue;
            }

            if (!Directory.Exists(protocol.OutputDir))
            {
                Directory.CreateDirectory(protocol.OutputDir);
            }

            var generator = new ProtocolGenerator();
            generator.Generate(protocol);
        }

        if (protocols.Any(p => p.DocsDir != null))
        {
            GenerateSidebar(protocols);
        }

        Console.WriteLine("Code generation complete");
    }

    static void GenerateProtocol(ProtocolMetadata metadata)
    {
        if (!File.Exists(metadata.XmlFile))
        {
            Console.Error.WriteLine($"Error: File '{metadata.XmlFile}' not found.");
            return;
        }

        if (!Directory.Exists(metadata.OutputDir))
        {
            Directory.CreateDirectory(metadata.OutputDir);
        }

        var generator = new ProtocolGenerator();
        generator.Generate(metadata);

        Console.WriteLine("Code generation complete");
    }

    static ProtocolMetadata[] LoadConfig(FileInfo config)
    {
        var json = File.ReadAllText(config.FullName);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var configs = JsonSerializer.Deserialize<ProtocolConfig[]>(json, options)
            ?? Array.Empty<ProtocolConfig>();

        var baseDir = config.Directory?.FullName ?? Directory.GetCurrentDirectory();

        return configs.Select(c => new ProtocolMetadata
        {
            Name = c.Name,
            XmlFile = Path.GetFullPath(c.XmlFile, baseDir),
            OutputDir = Path.GetFullPath(c.OutputDir, baseDir),
            Namespace = c.Namespace,
            Link = c.Link ?? $"https://wayland.app/protocols/{Path.GetFileNameWithoutExtension(c.XmlFile)}",
            DocsDir = c.DocsDir != null ? Path.GetFullPath(c.DocsDir, baseDir) : null
        }).ToArray();
    }

    static void GenerateSidebar(ProtocolMetadata[] protocols)
    {
        var protocolsWithDocs = protocols.Where(p => p.DocsDir != null).ToArray();
        if (protocolsWithDocs.Length == 0) return;

        var first = protocolsWithDocs.First();
        var sidebarDir = Path.Combine(first.DocsDir!, "Protocols");
        Directory.CreateDirectory(sidebarDir);

        var sb = new SourceFile(Path.Combine(sidebarDir, "_sidebar.md"));
        sb.WriteLine("- [Home](/)");
        sb.WriteLine();
        sb.WriteLine("Global");
        sb.WriteLine();
        sb.WriteLine("- [Protocols](/Protocols/)");
        sb.WriteLine();

        var grouped = protocolsWithDocs.GroupBy(p => p.Namespace).OrderBy(g =>
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

internal class ProtocolConfig
{
    public string Name { get; set; } = "";
    public string XmlFile { get; set; } = "";
    public string OutputDir { get; set; } = "";
    public string Namespace { get; set; } = "";
    public string? Link { get; set; }
    public string? DocsDir { get; set; }
}
