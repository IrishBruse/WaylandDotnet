namespace WaylandDotnetScanner;

using System.CommandLine;
using System.IO;
using System.Linq;
using System.Text.Json;
using WaylandDotnet;
using WaylandDotnetScanner.Data;

public class Program
{
    public static int Main(string[] args)
    {
        var inputArg = new Argument<FileInfo?>("input")
        {
            Description = "Input XML protocol file or JSON config file",
            Arity = ArgumentArity.ZeroOrOne,
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

        var rootCommand = new RootCommand("Wayland protocol scanner - Generate C# bindings from Wayland XML protocols")
        {
            inputArg,
            outputArg,
            namespaceOption,
            nameOption
        };

        rootCommand.SetAction((parseResult) =>
        {
            var input = parseResult.GetValue(inputArg);
            var output = parseResult.GetValue(outputArg);
            var ns = parseResult.GetValue(namespaceOption);
            var name = parseResult.GetValue(nameOption);

            if (File.Exists("protocols.json"))
            {
                input = new FileInfo("protocols.json");
            }

            if (input == null)
            {
                Console.Error.WriteLine("Usage: WaylandScanner <input.xml|protocols.json> [output-dir] [options]");
                return;
            }

            Environment.CurrentDirectory = input.DirectoryName ?? Environment.CurrentDirectory;

            var isJson = string.Equals(input.Extension, ".json", StringComparison.OrdinalIgnoreCase);

            if (isJson && output == null)
            {
                GenerateFromConfig(input);
                return;
            }

            if (output == null)
            {
                Console.Error.WriteLine("Error: output directory is required for XML input");
                Console.Error.WriteLine("Usage: WaylandScanner <input.xml> <output-dir> [options]");
                return;
            }

            var metadata = new ProtocolMetadata
            {
                Name = name ?? Path.GetFileNameWithoutExtension(input.Name),
                XmlFile = input.FullName,
                OutputDir = output.FullName,
                Namespace = ns ?? "Generated"
            };

            GenerateProtocol(metadata);
        });

        var listCommand = new Command("list", "List available protocols in config file");
        listCommand.Add(inputArg);

        listCommand.SetAction((parseResult) =>
        {
            var input = parseResult.GetValue(inputArg);

            if (File.Exists("protocols.json"))
            {
                input = new FileInfo("protocols.json");
            }

            if (input == null || !input.Exists)
            {
                Console.Error.WriteLine("Error: Config file not found");
                return;
            }

            Environment.CurrentDirectory = input.DirectoryName ?? Environment.CurrentDirectory;

            var protocols = LoadConfig(input);
            foreach (var p in protocols)
            {
                Console.WriteLine($"{p.Namespace}/{p.Name}: {p.XmlFile}");
            }
        });

        rootCommand.Subcommands.Add(listCommand);

        return rootCommand.Parse(args).Invoke();
    }

    static void GenerateFromConfig(FileInfo input)
    {
        if (!input.Exists)
        {
            Console.Error.WriteLine($"Error: Config file '{input.FullName}' not found.");
            return;
        }

        var protocols = LoadConfig(input);

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

    static ProtocolMetadata[] LoadConfig(FileInfo input)
    {
        var json = File.ReadAllText(input.FullName);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var rootConfig = JsonSerializer.Deserialize<RootConfig>(json, options)
            ?? throw new InvalidOperationException("Failed to parse config file");

        var baseDir = input.Directory?.FullName ?? Directory.GetCurrentDirectory();
        var outputRoot = Path.GetFullPath(rootConfig.OutputRoot, baseDir);
        var docsDir = rootConfig.DocsDir != null ? Path.GetFullPath(rootConfig.DocsDir, baseDir) : null;

        return rootConfig.Protocols.Select(c =>
        {
            var xmlFileName = Path.GetFileNameWithoutExtension(c.XmlFile);
            return new ProtocolMetadata
            {
                Name = c.Name,
                XmlFile = Path.GetFullPath(c.XmlFile, baseDir),
                OutputDir = Path.Combine(outputRoot, c.Namespace, xmlFileName),
                Namespace = c.Namespace,
                Link = c.Link ?? $"https://wayland.app/protocols/{Path.GetFileNameWithoutExtension(c.XmlFile)}",
                DocsDir = docsDir
            };
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

internal class RootConfig
{
    public string OutputRoot { get; set; } = "";
    public string? DocsDir { get; set; }
    public ProtocolConfig[] Protocols { get; set; } = [];
}

internal class ProtocolConfig
{
    public string Name { get; set; } = "";
    public string XmlFile { get; set; } = "";
    public string Namespace { get; set; } = "";
    public string? Link { get; set; }
}
