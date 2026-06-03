namespace WaylandDotnet.Scanner;

using System.CommandLine;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using WaylandDotnet.Scanner.Data;

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

        var debugOption = new Option<bool?>("--debug")
        {
            Description = "Enable debug output during generation"
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
            var debug = parseResult.GetValue(debugOption) ?? false;

            if (File.Exists("protocols.json"))
            {
                input = new FileInfo("protocols.json");
            }

            if (input == null)
            {
                Console.Error.WriteLine("Usage: WaylandScanner <input.xml|protocols.json> [output-dir] [options]");
                Console.Error.WriteLine("       WaylandScanner init");
                Console.Error.WriteLine();
                Console.Error.WriteLine("Run 'WaylandScanner init' to create a protocols.json configuration file.");
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
                Namespace = ns ?? "Generated",
                Debug = debug
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

        var initCommand = new Command("init", "Create a default protocols.json configuration file");
        initCommand.SetAction((parseResult) =>
        {
            const string defaultConfig = """"
            {
              "OutputRoot": "./Generated",
              "DocsDir": null,
              "Protocols": [
                {
                  "Name": "Wayland",
                  "XmlFile": "Protocols/Core/wayland.xml",
                  "Namespace": "Core",
                  "SourceUrl": "https://gitlab.freedesktop.org/wayland/wayland/-/raw/main/protocol/wayland.xml"
                }
              ]
            }
            """";

            if (File.Exists("protocols.json"))
            {
                Console.Error.WriteLine("Error: protocols.json already exists");
                return;
            }

            File.WriteAllText("protocols.json", defaultConfig);
            Console.WriteLine("Created protocols.json");
            Console.WriteLine("Edit the file to configure your protocols, then run 'wayland-dotnet-scanner' to generate code.");
        });

        rootCommand.Subcommands.Add(initCommand);

        var downloadCommand = new Command("download", "Download protocol XML files listed with SourceUrl in protocols.json");
        downloadCommand.Add(inputArg);
        downloadCommand.SetAction((parseResult) =>
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
            DownloadProtocols(input);
        });

        rootCommand.Subcommands.Add(downloadCommand);

        return rootCommand.Parse(args).Invoke();
    }

    static void GenerateFromConfig(FileInfo input)
    {
        if (!input.Exists)
        {
            ScannerConsole.WriteError($"Error: Config file '{input.FullName}' not found.");
            return;
        }

        var protocols = LoadConfig(input);
        ScannerConsole.WritePhase("generate");

        var generator = new ProtocolGenerator();
        var protocolCount = 0;
        var fileCount = 0;

        foreach (var protocol in protocols)
        {
            if (!File.Exists(protocol.XmlFile))
            {
                ScannerConsole.WriteError($"Error: File '{protocol.XmlFile}' not found.");
                continue;
            }

            if (!Directory.Exists(protocol.OutputDir))
            {
                Directory.CreateDirectory(protocol.OutputDir);
            }

            generator.Generate(protocol);
            protocolCount++;
            fileCount += generator.LastGeneratedCount;
        }

        if (protocols.Any(p => p.DocsDir != null))
        {
            GenerateSidebar(protocols);
            GenerateSidebarActiveCss(protocols);
        }

        ScannerConsole.WriteDone($"Done - {protocolCount} protocols, {fileCount} files");
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

        ScannerConsole.WriteDone("Done");
    }

    static void DownloadProtocols(FileInfo input)
    {
        var rootConfig = ParseRootConfig(input);
        var baseDir = input.Directory?.FullName ?? Directory.GetCurrentDirectory();
        var downloads = rootConfig.Protocols
            .Where(p => !string.IsNullOrWhiteSpace(p.SourceUrl))
            .Select(p => (
                Namespace: p.Namespace,
                Dest: Path.GetFullPath(p.XmlFile, baseDir),
                Url: p.SourceUrl!,
                Name: p.Name))
            .ToArray();

        if (downloads.Length == 0)
        {
            ScannerConsole.WriteInfo("No protocols with SourceUrl in config.");
            return;
        }

        ScannerConsole.WritePhase("download");

        Parallel.ForEach(downloads, item =>
        {
            var dir = Path.GetDirectoryName(item.Dest);
            if (dir != null)
            {
                Directory.CreateDirectory(dir);
            }

            using var http = new HttpClient();
            var bytes = http.GetByteArrayAsync(item.Url).GetAwaiter().GetResult();
            File.WriteAllBytes(item.Dest, bytes);
        });

        foreach (var group in downloads.GroupBy(d => d.Namespace).OrderBy(g => g.Key))
        {
            ScannerConsole.WriteSection(group.Key);
            foreach (var item in group.OrderBy(d => d.Name))
            {
                ScannerConsole.WriteDownloaded(item.Name);
            }
        }

        ScannerConsole.WriteDone($"Done - {downloads.Length} protocols");
    }

    static RootConfig ParseRootConfig(FileInfo input)
    {
        var json = File.ReadAllText(input.FullName);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true
        };

        return JsonSerializer.Deserialize<RootConfig>(json, options)
            ?? throw new InvalidOperationException("Failed to parse config file");
    }

    static ProtocolMetadata[] LoadConfig(FileInfo input)
    {
        var rootConfig = ParseRootConfig(input);
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

    const string SidebarBeginMarker = "<!-- wayland-dotnet-scanner:protocols -->";
    const string SidebarEndMarker = "<!-- /wayland-dotnet-scanner:protocols -->";
    const string SidebarActiveBeginMarker = "/* wayland-dotnet-scanner:sidebar-active */";
    const string SidebarActiveEndMarker = "/* /wayland-dotnet-scanner:sidebar-active */";

    static readonly string[] SidebarNamespaceOrder =
    [
        "Core",
        "Stable",
        "Wlr",
        "Staging",
        "River",
    ];

    static void GenerateSidebar(ProtocolMetadata[] protocols)
    {
        var protocolsWithDocs = protocols.Where(p => p.DocsDir != null).ToArray();
        if (protocolsWithDocs.Length == 0) return;

        var sidebarPath = Path.Combine(protocolsWithDocs[0].DocsDir!, "sidebar.md");
        if (!File.Exists(sidebarPath))
        {
            ScannerConsole.WriteError($"Error: Sidebar file not found: {sidebarPath}");
            return;
        }

        var content = File.ReadAllText(sidebarPath);
        var beginIndex = content.IndexOf(SidebarBeginMarker, StringComparison.Ordinal);
        var endIndex = content.IndexOf(SidebarEndMarker, StringComparison.Ordinal);

        if (beginIndex < 0 || endIndex < 0 || endIndex <= beginIndex)
        {
            ScannerConsole.WriteError(
                $"Error: {sidebarPath} must contain '{SidebarBeginMarker}' and '{SidebarEndMarker}' markers.");
            return;
        }

        var generated = BuildProtocolsSidebar(protocolsWithDocs);
        var before = content[..(beginIndex + SidebarBeginMarker.Length)];
        var after = content[endIndex..];
        File.WriteAllText(sidebarPath, $"{before}\n\n{generated}\n\n{after}");
    }

    static string BuildProtocolsSidebar(ProtocolMetadata[] protocols)
    {
        var sb = new StringBuilder();
        var grouped = protocols
            .GroupBy(p => p.Namespace)
            .OrderBy(g => Array.IndexOf(SidebarNamespaceOrder, g.Key) is var index and >= 0 ? index : 100)
            .ThenBy(g => g.Key);

        foreach (var group in grouped)
        {
            sb.AppendLine(group.Key);
            sb.AppendLine();

            foreach (var protocol in group.OrderBy(p => p.Name))
            {
                var fileName = Path.GetFileNameWithoutExtension(protocol.XmlFile);
                sb.AppendLine($"- [{protocol.Name}](/Protocols/{protocol.Namespace}/{fileName}/)");
            }

            sb.AppendLine();
        }

        return sb.ToString().TrimEnd();
    }

    static void GenerateSidebarActiveCss(ProtocolMetadata[] protocols)
    {
        var protocolsWithDocs = protocols.Where(p => p.DocsDir != null).ToArray();
        if (protocolsWithDocs.Length == 0)
        {
            return;
        }

        var sidebarCssPath = Path.Combine(protocolsWithDocs[0].DocsDir!, "styles", "sidebar.css");
        if (!File.Exists(sidebarCssPath))
        {
            ScannerConsole.WriteError($"Error: Sidebar CSS file not found: {sidebarCssPath}");
            return;
        }

        var content = File.ReadAllText(sidebarCssPath);
        var beginIndex = content.IndexOf(SidebarActiveBeginMarker, StringComparison.Ordinal);
        var endIndex = content.IndexOf(SidebarActiveEndMarker, StringComparison.Ordinal);

        if (beginIndex < 0 || endIndex < 0 || endIndex <= beginIndex)
        {
            ScannerConsole.WriteError(
                $"Error: {sidebarCssPath} must contain '{SidebarActiveBeginMarker}' and '{SidebarActiveEndMarker}' markers.");
            return;
        }

        var generated = BuildSidebarActiveSelectors(protocolsWithDocs);
        var before = content[..(beginIndex + SidebarActiveBeginMarker.Length)];
        var after = content[endIndex..];
        File.WriteAllText(sidebarCssPath, $"{before}\n{generated}\n{after}");
    }

    static string BuildSidebarActiveSelectors(ProtocolMetadata[] protocols)
    {
        var sb = new StringBuilder();

        foreach (var protocol in protocols.OrderBy(p => p.Namespace).ThenBy(p => p.Name))
        {
            var fileName = Path.GetFileNameWithoutExtension(protocol.XmlFile);
            var path = $"/Protocols/{protocol.Namespace}/{fileName}/";
            var dataPage = $"Protocols/{protocol.Namespace}/{fileName}/README.md";
            sb.AppendLine(
                $"body:has(.markdown-section[data-page=\"{dataPage}\"]) .sidebar li:has(> a[href*=\"{path}\"]) > a,");
        }

        return sb.ToString().TrimEnd().TrimEnd(',');
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
    public string? SourceUrl { get; set; }
    public string? Link { get; set; }
}
