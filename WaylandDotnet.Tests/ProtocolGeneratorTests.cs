namespace WaylandDotnet.Tests;

using Xunit;
using WaylandDotnet.Scanner;
using WaylandDotnet.Scanner.Data;

public class ProtocolGeneratorTests : IDisposable
{
    private readonly string tempRoot;
    private readonly string fixturePath;

    public ProtocolGeneratorTests()
    {
        tempRoot = Path.Combine(Path.GetTempPath(), "wayland-dotnet-tests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempRoot);

        fixturePath = Path.Combine(
            AppContext.BaseDirectory,
            "Fixtures",
            "test-protocol.xml");
    }

    public void Dispose()
    {
        if (Directory.Exists(tempRoot))
        {
            Directory.Delete(tempRoot, recursive: true);
        }
    }

    [Fact]
    public void WriteLine_IndentsAndSupportsBlankLines()
    {
        var generator = new ProtocolGenerator();

        generator.WriteLine("outer");
        generator.indentLevel = 1;
        generator.WriteLine("inner");
        generator.WriteLine();

        var output = generator.sb.ToString();

        Assert.Contains("outer" + Environment.NewLine, output);
        Assert.Contains("    inner" + Environment.NewLine, output);
        Assert.EndsWith(Environment.NewLine + Environment.NewLine, output);
    }

    [Fact]
    public void GenerateFileHeader_CoreNamespace_UsesRootNamespace()
    {
        var generator = new ProtocolGenerator();
        var metadata = ProtocolGeneratorTestHelpers.Metadata("wayland.xml", tempRoot, ns: "Core", link: "https://example.test/wayland");

        generator.GenerateFileHeader(metadata);

        var output = generator.sb.ToString();
        Assert.Contains("// Stability: Core", output);
        Assert.Contains("// Link: https://example.test/wayland", output);
        Assert.Contains("namespace WaylandDotnet;", output);
        Assert.DoesNotContain("namespace WaylandDotnet.Core;", output);
        Assert.Contains("#nullable enable", output);
        Assert.Contains("using WaylandDotnet.Internal;", output);
    }

    [Fact]
    public void GenerateFileHeader_NonCoreNamespace_UsesNestedNamespace()
    {
        var generator = new ProtocolGenerator();
        var metadata = ProtocolGeneratorTestHelpers.Metadata("xdg-shell.xml", tempRoot, ns: "Stable");

        generator.GenerateFileHeader(metadata);

        var output = generator.sb.ToString();
        Assert.Contains("namespace WaylandDotnet.Stable;", output);
        Assert.DoesNotContain("// Link:", output);
    }

    [Fact]
    public void GenerateEnum_NormalEnum_EmitsValues()
    {
        var generator = new ProtocolGenerator();
        var metadata = ProtocolGeneratorTestHelpers.Metadata("test.xml", tempRoot);
        generator.GenerateFileHeader(metadata);

        generator.GenerateEnum(ProtocolGeneratorTestHelpers.EnumDef(
            "state",
            entries: [("idle", "0", "idle state"), ("active", "1", "active state")]));

        var output = generator.sb.ToString();
        Assert.Contains("public enum State : uint", output);
        Assert.Contains("Idle = 0,", output);
        Assert.Contains("Active = 1,", output);
        Assert.DoesNotContain("[Flags]", output);
    }

    [Fact]
    public void GenerateEnum_BitfieldEnum_EmitsFlagsAttribute()
    {
        var generator = new ProtocolGenerator();
        var metadata = ProtocolGeneratorTestHelpers.Metadata("test.xml", tempRoot);
        generator.GenerateFileHeader(metadata);

        generator.GenerateEnum(ProtocolGeneratorTestHelpers.EnumDef(
            "flags",
            bitfield: true,
            entries: [("visible", "1", "visible flag")]));

        var output = generator.sb.ToString();
        Assert.Contains("[Flags]", output);
        Assert.Contains("public enum FlagsFlag : uint", output);
        Assert.Contains("Visible = 1,", output);
    }

    [Fact]
    public void GenerateInterface_MinimalInterface_EmitsClassShell()
    {
        var generator = new ProtocolGenerator();
        var metadata = ProtocolGeneratorTestHelpers.Metadata("test.xml", tempRoot, link: "https://example.test/proto");
        var iface = ProtocolGeneratorTestHelpers.Iface(
            "test_ping",
            summary: "minimal ping interface",
            text: "Ping only interface.",
            requests: [ProtocolGeneratorTestHelpers.Req("ping", summary: "send ping", args: [ProtocolGeneratorTestHelpers.Arg("serial", "uint", "serial number")])]);

        var output = ProtocolGeneratorTestHelpers.GenerateInterfaceSource(generator, metadata, iface);

        Assert.Contains("public sealed partial class TestPing : WaylandObject, IWaylandObjectFactory<TestPing>", output);
        Assert.Contains("public const string InterfaceName = \"test_ping\";", output);
        Assert.Contains("public const int InterfaceVersion = 1;", output);
        Assert.Contains("public static TestPing Create(nint handle, WlDisplay? display = null)", output);
        Assert.Contains("return new TestPing(handle);", output);
        Assert.DoesNotContain("ArgumentNullException.ThrowIfNull(display);", output);
        Assert.Contains("public unsafe void Ping(uint serial)", output);
        Assert.Contains("https://example.test/proto/#test_ping", output);
        Assert.DoesNotContain("private GCHandle gcHandle;", output);
        Assert.DoesNotContain("private bool disposed;", output);
    }

    [Fact]
    public void GenerateInterface_WlDisplay_DoesNotRequireDisplayParameter()
    {
        var generator = new ProtocolGenerator();
        var metadata = ProtocolGeneratorTestHelpers.Metadata("wayland.xml", tempRoot, ns: "Core");
        var iface = ProtocolGeneratorTestHelpers.Iface(
            "wl_display",
            events: [ProtocolGeneratorTestHelpers.Evt("error", summary: "fatal error", args: [
                ProtocolGeneratorTestHelpers.Arg("object_id", "object", summary: "object id"),
                ProtocolGeneratorTestHelpers.Arg("code", "uint", summary: "error code"),
                ProtocolGeneratorTestHelpers.Arg("message", "string", summary: "error message")
            ])]);

        var output = ProtocolGeneratorTestHelpers.GenerateInterfaceSource(generator, metadata, iface);

        Assert.Contains("public WlDisplay(IntPtr handle)", output);
        Assert.Contains("return new WlDisplay(handle);", output);
        Assert.DoesNotContain("ArgumentNullException.ThrowIfNull(display);", output);
    }

    [Fact]
    public void GenerateRequestMethod_AllArgumentTypes_EmitMarshalCode()
    {
        var generator = new ProtocolGenerator();
        var metadata = ProtocolGeneratorTestHelpers.Metadata("test.xml", tempRoot, debug: true);
        var iface = ProtocolGeneratorTestHelpers.Iface(
            "test_widget",
            requests: [ProtocolGeneratorTestHelpers.Req(
                "configure",
                summary: "configure widget",
                text: "Configure all supported types.",
                args: [
                    ProtocolGeneratorTestHelpers.Arg("width", "int", "width"),
                    ProtocolGeneratorTestHelpers.Arg("height", "uint", "height"),
                    ProtocolGeneratorTestHelpers.Arg("scale", "fixed", "scale"),
                    ProtocolGeneratorTestHelpers.Arg("label", "string", "label", allowNull: "true"),
                    ProtocolGeneratorTestHelpers.Arg("parent", "object", "parent", iface: "test_widget", allowNull: "true"),
                    ProtocolGeneratorTestHelpers.Arg("data", "array", "data"),
                    ProtocolGeneratorTestHelpers.Arg("fd", "fd", "fd")
                ])]);

        generator.GenerateFileHeader(metadata);
        generator.GenerateInterface(iface);

        var output = generator.sb.ToString();
        Assert.Contains("public unsafe void Configure(int width, uint height, WlFixed scale, string? label, TestWidget? parent, byte[] data, int fd)", output);
        Assert.Contains("args[0].i = width;", output);
        Assert.Contains("args[1].u = height;", output);
        Assert.Contains("args[2].f = scale;", output);
        Assert.Contains("Utf8StringMarshaller.ConvertToUnmanaged(label)", output);
        Assert.Contains("args[4].o = (WlObject*)(parent?.Handle ?? IntPtr.Zero);", output);
        Assert.Contains("WaylandMarshal.CreateWlArray(data)", output);
        Assert.Contains("args[6].h = fd;", output);
        Assert.Contains("#region GenerateRequests", output);
    }

    [Fact]
    public void GenerateRequestMethod_TypedNewId_ReturnsCreatedInterface()
    {
        var generator = new ProtocolGenerator();
        var metadata = ProtocolGeneratorTestHelpers.Metadata("test.xml", tempRoot);
        var iface = ProtocolGeneratorTestHelpers.Iface(
            "test_widget",
            requests: [ProtocolGeneratorTestHelpers.Req(
                "create_child",
                summary: "create child",
                args: [ProtocolGeneratorTestHelpers.Arg("child", "new_id", "child", iface: "test_widget")])]);

        generator.GenerateFileHeader(metadata);
        generator.GenerateInterface(iface);

        var output = generator.sb.ToString();
        Assert.Contains("public unsafe TestWidget CreateChild()", output);
        Assert.Contains("stackalloc WlArgument[1]", output);
        Assert.Contains("WaylandInterfaces.TestWidget", output);
        Assert.Contains("return new TestWidget(newProxy, Display);", output);
    }

    [Fact]
    public void GenerateRequestMethod_Destructor_SetsDisposedFlag()
    {
        var generator = new ProtocolGenerator();
        var metadata = ProtocolGeneratorTestHelpers.Metadata("test.xml", tempRoot);
        var iface = ProtocolGeneratorTestHelpers.Iface(
            "test_widget",
            requests: [ProtocolGeneratorTestHelpers.Req("destroy", type: "destructor", summary: "destroy widget")]);

        generator.GenerateFileHeader(metadata);
        generator.GenerateInterface(iface);

        var output = generator.sb.ToString();
        Assert.Contains("private bool disposed;", output);
        Assert.Contains("public unsafe void Destroy()", output);
        Assert.Contains("ObjectDisposedException.ThrowIf(disposed, this);", output);
        Assert.Contains("disposed = true;", output);
    }

    [Fact]
    public void GenerateRequestMethod_WlRegistryBind_IsSkipped()
    {
        var generator = new ProtocolGenerator();
        var metadata = ProtocolGeneratorTestHelpers.Metadata("test.xml", tempRoot);
        var iface = ProtocolGeneratorTestHelpers.Iface(
            "wl_registry",
            requests: [ProtocolGeneratorTestHelpers.Req(
                "bind",
                summary: "bind global",
                args: [
                    ProtocolGeneratorTestHelpers.Arg("name", "uint", "name"),
                    ProtocolGeneratorTestHelpers.Arg("id", "new_id", "id")
                ])]);

        generator.GenerateFileHeader(metadata);
        generator.GenerateInterface(iface);

        var output = generator.sb.ToString();
        Assert.DoesNotContain("public unsafe WaylandObject Bind(", output);
    }

    [Fact]
    public void GenerateRequestMethod_InvalidRequestType_Throws()
    {
        var generator = new ProtocolGenerator();
        var metadata = ProtocolGeneratorTestHelpers.Metadata("test.xml", tempRoot);
        var iface = ProtocolGeneratorTestHelpers.Iface(
            "test_widget",
            requests: [ProtocolGeneratorTestHelpers.Req("bad", type: "request", summary: "bad request")]);

        generator.GenerateFileHeader(metadata);

        var ex = Assert.Throws<InvalidOperationException>(() => generator.GenerateInterface(iface));
        Assert.Contains("Assertion failed", ex.Message);
    }

    [Fact]
    public void GenerateEventDelegates_EmitsLazyRegistration()
    {
        var generator = new ProtocolGenerator();
        var metadata = ProtocolGeneratorTestHelpers.Metadata("test.xml", tempRoot);
        var iface = ProtocolGeneratorTestHelpers.Iface(
            "test_widget",
            events: [ProtocolGeneratorTestHelpers.Evt(
                "update",
                since: 2,
                summary: "widget updated",
                text: "Update event body.",
                args: [
                    ProtocolGeneratorTestHelpers.Arg("serial", "uint", "serial"),
                    ProtocolGeneratorTestHelpers.Arg("payload", "array", "payload")
                ])]);

        generator.GenerateFileHeader(metadata);
        generator.GenerateInterface(iface);

        var output = generator.sb.ToString();
        Assert.Contains("public delegate void UpdateHandler(uint serial, byte[] payload);", output);
        Assert.Contains("public event UpdateHandler? OnUpdate", output);
        Assert.Contains("EnsureDispatcherRegistered();", output);
        Assert.Contains("private GCHandle gcHandle;", output);
    }

    [Fact]
    public void GenerateEvents_DispatcherHandlesArrayAndDestructorEvent()
    {
        var generator = new ProtocolGenerator();
        var metadata = ProtocolGeneratorTestHelpers.Metadata("test.xml", tempRoot);
        var iface = ProtocolGeneratorTestHelpers.Iface(
            "test_widget",
            requests: [ProtocolGeneratorTestHelpers.Req("destroy", type: "destructor", summary: "destroy widget")],
            events: [
                ProtocolGeneratorTestHelpers.Evt(
                    "update",
                    summary: "widget updated",
                    args: [
                        ProtocolGeneratorTestHelpers.Arg("serial", "uint", "serial"),
                        ProtocolGeneratorTestHelpers.Arg("payload", "array", "payload")
                    ]),
                ProtocolGeneratorTestHelpers.Evt("gone", type: "destructor", summary: "widget gone")
            ]);

        generator.GenerateFileHeader(metadata);
        generator.GenerateInterface(iface);

        var output = generator.sb.ToString();
        Assert.Contains("[UnmanagedCallersOnly]", output);
        Assert.Contains("private static unsafe int DispatchEvent(", output);
        Assert.Contains("case 0: // update", output);
        Assert.Contains("var _payload = args[1].a;", output);
        Assert.Contains("WaylandMarshal.ToSpan(_payload)", output);
        Assert.Contains("case 1: // gone", output);
        Assert.Contains("obj.disposed = true;", output);
        Assert.Contains("ObjectDisposedException.ThrowIf(disposed, this);", output);
    }

    [Fact]
    public void GenerateEvents_NullableTypedObject_UnmarshalsWithDisplayWhenNeeded()
    {
        var generator = new ProtocolGenerator();
        var metadata = ProtocolGeneratorTestHelpers.Metadata("test.xml", tempRoot);
        var iface = ProtocolGeneratorTestHelpers.Iface(
            "test_widget",
            events: [ProtocolGeneratorTestHelpers.Evt(
                "parent_changed",
                summary: "parent changed",
                args: [ProtocolGeneratorTestHelpers.Arg("parent", "object", "parent", iface: "test_widget", allowNull: "true")])]);

        generator.GenerateFileHeader(metadata);
        generator.GenerateInterface(iface);

        var output = generator.sb.ToString();
        Assert.Contains("TestWidget? _parent = null;", output);
        Assert.Contains("if (args[0].o != (WlObject*)IntPtr.Zero)", output);
        Assert.Contains("_parent = new TestWidget((IntPtr)args[0].o, obj.Display!);", output);
    }

    [Fact]
    public void Generate_EmitsInterfaceFilesWaylandInterfacesAndCopyright()
    {
        Assert.True(File.Exists(fixturePath), $"Fixture not found: {fixturePath}");

        var outputDir = Path.Combine(tempRoot, "generated");
        var docsDir = Path.Combine(tempRoot, "docs");
        Directory.CreateDirectory(outputDir);

        var metadata = ProtocolGeneratorTestHelpers.Metadata(
            fixturePath,
            outputDir,
            ns: "Test",
            link: "https://example.test/test-protocol",
            docsDir: docsDir);

        var generator = new ProtocolGenerator();
        generator.Generate(metadata);

        Assert.Equal(3, generator.LastGeneratedCount);
        Assert.True(File.Exists(Path.Combine(outputDir, "TestPing.cs")));
        Assert.True(File.Exists(Path.Combine(outputDir, "TestWidget.cs")));
        Assert.True(File.Exists(Path.Combine(outputDir, "WlRegistry.cs")));
        Assert.True(File.Exists(Path.Combine(outputDir, "WaylandInterfaces.cs")));
        Assert.True(File.Exists(Path.Combine(outputDir, "Copyright.txt")));

        var pingSource = File.ReadAllText(Path.Combine(outputDir, "TestPing.cs"));
        Assert.Contains("namespace WaylandDotnet.Test;", pingSource);
        Assert.Contains("public unsafe void Ping(uint serial)", pingSource);

        var widgetSource = File.ReadAllText(Path.Combine(outputDir, "TestWidget.cs"));
        Assert.Contains("public enum State : uint", widgetSource);
        Assert.Contains("[Flags]", widgetSource);
        Assert.Contains("public enum FlagsFlag : uint", widgetSource);
        Assert.Contains("widget &amp; gadget &lt;test&gt;", widgetSource);
        Assert.Contains("public unsafe TestWidget CreateChild()", widgetSource);
        Assert.Contains("public unsafe void Destroy()", widgetSource);

        var registrySource = File.ReadAllText(Path.Combine(outputDir, "WlRegistry.cs"));
        Assert.DoesNotContain("public unsafe WaylandObject Bind(", registrySource);
        Assert.Contains("public event GlobalHandler? OnGlobal", registrySource);

        var interfacesSource = File.ReadAllText(Path.Combine(outputDir, "WaylandInterfaces.cs"));
        Assert.Contains("public static void CreateWlRegistryInterface()", interfacesSource);
        Assert.Contains("Signature = Utf8StringMarshaller.ConvertToUnmanaged(\"usun\")", interfacesSource);
        Assert.Contains("CreateTypesArray([(WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])", interfacesSource);
        Assert.Contains("Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])", interfacesSource);

        var copyright = File.ReadAllLines(Path.Combine(outputDir, "Copyright.txt"));
        Assert.Equal("Copyright (c) Test Project", copyright[0]);
        Assert.Equal("All rights reserved.", copyright[1]);

        var readmePath = Path.Combine(docsDir, "Protocols", "Test", "test-protocol", "README.md");
        var navbarPath = Path.Combine(docsDir, "Protocols", "Test", "test-protocol", "navbar.md");
        Assert.True(File.Exists(readmePath));
        Assert.True(File.Exists(navbarPath));

        var readme = File.ReadAllText(readmePath);
        Assert.Contains("# Test Protocol", readme);
        Assert.Contains("class=\"breadcrumb\"", readme);
        Assert.Contains("Widget & gadget <test>", readme);
        Assert.Contains("| serial | uint | Serial number |", readme);
        Assert.Contains("since 2", readme);
        Assert.Contains("Type: destructor", readme);

        var navbar = File.ReadAllText(navbarPath);
        Assert.Contains("- [TestPing](#testping", navbar);
        Assert.Contains("- [TestWidget](#testwidget", navbar);
        Assert.Contains("- [WlRegistry](#wlregistry", navbar);
    }

    [Fact]
    public void Generate_WithoutDocsDir_DoesNotWriteDocumentationFiles()
    {
        Assert.True(File.Exists(fixturePath), $"Fixture not found: {fixturePath}");

        var outputDir = Path.Combine(tempRoot, "generated-no-docs");
        Directory.CreateDirectory(outputDir);

        var metadata = ProtocolGeneratorTestHelpers.Metadata(fixturePath, outputDir, ns: "Test");
        var generator = new ProtocolGenerator();
        generator.Generate(metadata);

        Assert.False(Directory.Exists(Path.Combine(tempRoot, "Protocols")));
        Assert.Equal("", generator.md.Path);
        Assert.Equal("", generator.navbar.Path);
    }

    [Fact]
    public void Generate_SelfReferencingObject_UsesZeroPointerInTypesArray()
    {
        Assert.True(File.Exists(fixturePath), $"Fixture not found: {fixturePath}");

        var outputDir = Path.Combine(tempRoot, "generated-self-ref");
        Directory.CreateDirectory(outputDir);

        var metadata = ProtocolGeneratorTestHelpers.Metadata(fixturePath, outputDir, ns: "Test");
        var generator = new ProtocolGenerator();
        generator.Generate(metadata);

        var interfacesSource = File.ReadAllText(Path.Combine(outputDir, "WaylandInterfaces.cs"));
        Assert.Contains(
            "Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero, (WlInterface*)IntPtr.Zero])",
            interfacesSource);
    }

    [Fact]
    public void GenerateEvents_AllWireTypes_UnmarshalsArguments()
    {
        var generator = new ProtocolGenerator();
        var metadata = ProtocolGeneratorTestHelpers.Metadata("test.xml", tempRoot);
        var iface = ProtocolGeneratorTestHelpers.Iface(
            "test_sensor",
            events: [ProtocolGeneratorTestHelpers.Evt(
                "reading",
                summary: "sensor reading",
                args: [
                    ProtocolGeneratorTestHelpers.Arg("value", "int", "value"),
                    ProtocolGeneratorTestHelpers.Arg("flags", "uint", "flags"),
                    ProtocolGeneratorTestHelpers.Arg("ratio", "fixed", "ratio"),
                    ProtocolGeneratorTestHelpers.Arg("label", "string", "label"),
                    ProtocolGeneratorTestHelpers.Arg("peer", "object", "peer", iface: "test_sensor"),
                    ProtocolGeneratorTestHelpers.Arg("handle", "new_id", "handle"),
                    ProtocolGeneratorTestHelpers.Arg("fd", "fd", "fd")
                ])]);

        generator.GenerateFileHeader(metadata);
        generator.GenerateInterface(iface);

        var output = generator.sb.ToString();
        Assert.Contains("var _value = args[0].i;", output);
        Assert.Contains("var _flags = args[1].u;", output);
        Assert.Contains("var _ratio = args[2].f;", output);
        Assert.Contains("Utf8StringMarshaller.ConvertToManaged(args[3].s)", output);
        Assert.Contains("Received null object for non-nullable argument 'peer'", output);
        Assert.Contains("var _handle = args[5].o;", output);
        Assert.Contains("var _fd = args[6].h;", output);
    }

    [Fact]
    public void GenerateRequestMethod_UntypedNewId_EmitsExpandedWireArguments()
    {
        var generator = new ProtocolGenerator();
        var metadata = ProtocolGeneratorTestHelpers.Metadata("test.xml", tempRoot);
        var iface = ProtocolGeneratorTestHelpers.Iface(
            "test_binder",
            requests: [ProtocolGeneratorTestHelpers.Req(
                "create",
                summary: "create object",
                args: [
                    ProtocolGeneratorTestHelpers.Arg("name", "uint", "name"),
                    ProtocolGeneratorTestHelpers.Arg("id", "new_id", "id")
                ])]);

        generator.GenerateFileHeader(metadata);
        generator.GenerateInterface(iface);

        var output = generator.sb.ToString();
        Assert.Contains("public unsafe WaylandObject Create(string interfaceName, uint version, uint name)", output);
        Assert.Contains("stackalloc WlArgument[4]", output);
        Assert.Contains("args[1].s = Utf8StringMarshaller.ConvertToUnmanaged(interfaceName);", output);
        Assert.Contains("args[2].u = version;", output);
        Assert.Contains("args[3].o = (WlObject*)IntPtr.Zero;", output);
        Assert.Contains("WaylandInterfaces.GetInterfacePtr(interfaceName)", output);
        Assert.Contains("return new WaylandProxy(newProxy, Display);", output);
    }

    [Fact]
    public void Generate_ChildInterfaceWithoutDisplay_ReturnsWithoutDisplayParameter()
    {
        var xmlPath = Path.Combine(tempRoot, "parent-child-protocol.xml");
        File.WriteAllText(xmlPath, """
            <?xml version="1.0" encoding="UTF-8"?>
            <protocol name="parent_child">
              <copyright>Test</copyright>
              <interface name="test_child" version="1">
                <description summary="child interface">
                  Child with no events.
                </description>
                <request name="poke">
                  <description summary="poke child">
                    Poke the child.
                  </description>
                </request>
              </interface>
              <interface name="test_parent" version="1">
                <description summary="parent interface">
                  Parent that creates children.
                </description>
                <request name="create_child">
                  <description summary="create child">
                    Create a child object.
                  </description>
                  <arg name="child" type="new_id" interface="test_child" summary="child object"/>
                </request>
              </interface>
            </protocol>
            """);

        var outputDir = Path.Combine(tempRoot, "parent-child-generated");
        Directory.CreateDirectory(outputDir);

        var metadata = ProtocolGeneratorTestHelpers.Metadata(xmlPath, outputDir, ns: "Test");
        var generator = new ProtocolGenerator();
        generator.Generate(metadata);

        var parentSource = File.ReadAllText(Path.Combine(outputDir, "TestParent.cs"));
        Assert.Contains("return new TestChild(newProxy);", parentSource);
        Assert.DoesNotContain("return new TestChild(newProxy, Display);", parentSource);
    }

    [Fact]
    public void GenerateEvents_NullableUntypedObject_UsesWaylandProxy()
    {
        var generator = new ProtocolGenerator();
        var metadata = ProtocolGeneratorTestHelpers.Metadata("test.xml", tempRoot);
        var iface = ProtocolGeneratorTestHelpers.Iface(
            "test_sensor",
            events: [ProtocolGeneratorTestHelpers.Evt(
                "peer_changed",
                summary: "peer changed",
                args: [ProtocolGeneratorTestHelpers.Arg("peer", "object", "peer", allowNull: "true")])]);

        generator.GenerateFileHeader(metadata);
        generator.GenerateInterface(iface);

        var output = generator.sb.ToString();
        Assert.Contains("WaylandProxy? _peer = null;", output);
        Assert.Contains("_peer = new WaylandProxy((IntPtr)args[0].o);", output);
    }

    [Fact]
    public void Generate_WaylandInterfaces_IncludesSinceAndNullableSignaturePrefixes()
    {
        Assert.True(File.Exists(fixturePath), $"Fixture not found: {fixturePath}");

        var xmlPath = Path.Combine(tempRoot, "signature-protocol.xml");
        File.WriteAllText(xmlPath, """
            <?xml version="1.0" encoding="UTF-8"?>
            <protocol name="signature_test">
              <copyright>Test</copyright>
              <interface name="test_signature" version="2">
                <description summary="signature test">
                  Signature coverage.
                </description>
                <request name="apply">
                  <description summary="apply values">
                    Apply values.
                  </description>
                  <arg name="mode" type="uint" since="2" summary="mode since 2"/>
                  <arg name="name" type="string" allow-null="true" summary="optional name"/>
                  <arg name="unknown" type="weird" summary="unknown type"/>
                </request>
              </interface>
            </protocol>
            """);

        var outputDir = Path.Combine(tempRoot, "signature-generated");
        Directory.CreateDirectory(outputDir);

        var metadata = ProtocolGeneratorTestHelpers.Metadata(xmlPath, outputDir, ns: "Test");
        var generator = new ProtocolGenerator();
        generator.Generate(metadata);

        var interfacesSource = File.ReadAllText(Path.Combine(outputDir, "WaylandInterfaces.cs"));
        Assert.Contains("Signature = Utf8StringMarshaller.ConvertToUnmanaged(\"2u?s?\")", interfacesSource);

        var ifaceSource = File.ReadAllText(Path.Combine(outputDir, "TestSignature.cs"));
        Assert.Contains("public unsafe void Apply(uint mode, string? name, object unknown)", ifaceSource);
    }

    [Fact]
    public void GenerateRequestMethod_SincePill_AppearsInDocumentation()
    {
        Assert.True(File.Exists(fixturePath), $"Fixture not found: {fixturePath}");

        var outputDir = Path.Combine(tempRoot, "docs-since");
        var docsDir = Path.Combine(tempRoot, "docs-since-out");
        Directory.CreateDirectory(outputDir);

        var metadata = ProtocolGeneratorTestHelpers.Metadata(fixturePath, outputDir, ns: "Test", docsDir: docsDir);
        var generator = new ProtocolGenerator();
        generator.Generate(metadata);

        var readme = File.ReadAllText(Path.Combine(docsDir, "Protocols", "Test", "test-protocol", "README.md"));
        Assert.Contains("since 2", readme);
        Assert.Contains("Type: destructor", readme);
    }

    [Fact]
    public void GenerateInterface_EscapesCStringInInterfaceName()
    {
        var generator = new ProtocolGenerator();
        var metadata = ProtocolGeneratorTestHelpers.Metadata("escape.xml", tempRoot);
        var iface = ProtocolGeneratorTestHelpers.Iface(
            "test\"quote",
            summary: "quoted interface",
            requests: [ProtocolGeneratorTestHelpers.Req("go", summary: "go")]);

        var output = ProtocolGeneratorTestHelpers.GenerateInterfaceSource(generator, metadata, iface);

        Assert.Contains("public const string InterfaceName = \"test\\\"quote\";", output);
    }

    [Fact]
    public void GenerateEvents_TypedNewId_UnmarshalsWithDisplay()
    {
        var generator = new ProtocolGenerator();
        var metadata = ProtocolGeneratorTestHelpers.Metadata("test.xml", tempRoot);
        var iface = ProtocolGeneratorTestHelpers.Iface(
            "test_factory",
            events: [ProtocolGeneratorTestHelpers.Evt(
                "created",
                summary: "object created",
                args: [ProtocolGeneratorTestHelpers.Arg("child", "new_id", "child", iface: "test_widget")])]);

        generator.GenerateFileHeader(metadata);
        generator.GenerateInterface(iface);

        var output = generator.sb.ToString();
        Assert.Contains("var _child = new TestWidget((IntPtr)args[0].o, obj.Display!);", output);
    }

    [Fact]
    public void Generate_RequestSincePill_AppearsInDocumentation()
    {
        var xmlPath = Path.Combine(tempRoot, "since-request-protocol.xml");
        File.WriteAllText(xmlPath, """
            <?xml version="1.0" encoding="UTF-8"?>
            <protocol name="since_request">
              <copyright>Test</copyright>
              <interface name="test_since" version="2">
                <description summary="since request test">
                  Since request coverage.
                </description>
                <request name="refresh" since="2">
                  <description summary="refresh values">
                    Refresh values.
                  </description>
                </request>
              </interface>
            </protocol>
            """);

        var outputDir = Path.Combine(tempRoot, "since-request-generated");
        var docsDir = Path.Combine(tempRoot, "since-request-docs");
        Directory.CreateDirectory(outputDir);

        var metadata = ProtocolGeneratorTestHelpers.Metadata(xmlPath, outputDir, ns: "Test", docsDir: docsDir);
        var generator = new ProtocolGenerator();
        generator.Generate(metadata);

        var readme = File.ReadAllText(Path.Combine(docsDir, "Protocols", "Test", "since-request-protocol", "README.md"));
        Assert.Contains("since 2", readme);
    }

    [Fact]
    public void Generate_ChildObjectEvent_UsesConstructorWithoutDisplay()
    {
        var xmlPath = Path.Combine(tempRoot, "child-event-protocol.xml");
        File.WriteAllText(xmlPath, """
            <?xml version="1.0" encoding="UTF-8"?>
            <protocol name="child_event">
              <copyright>Test</copyright>
              <interface name="test_child" version="1">
                <description summary="child interface">
                  Child with no events.
                </description>
              </interface>
              <interface name="test_parent" version="1">
                <description summary="parent interface">
                  Parent with child events.
                </description>
                <event name="child_added">
                  <description summary="child added">
                    A child was added.
                  </description>
                  <arg name="child" type="object" interface="test_child" summary="child object"/>
                </event>
              </interface>
            </protocol>
            """);

        var outputDir = Path.Combine(tempRoot, "child-event-generated");
        Directory.CreateDirectory(outputDir);

        var metadata = ProtocolGeneratorTestHelpers.Metadata(xmlPath, outputDir, ns: "Test");
        var generator = new ProtocolGenerator();
        generator.Generate(metadata);

        var parentSource = File.ReadAllText(Path.Combine(outputDir, "TestParent.cs"));
        Assert.Contains("var _child = new TestChild((IntPtr)args[0].o);", parentSource);
        Assert.DoesNotContain("var _child = new TestChild((IntPtr)args[0].o, obj.Display!);", parentSource);
    }

    [Fact]
    public void Generate_UntypedNewIdNonBind_UsesBaseSignatureInWaylandInterfaces()
    {
        var xmlPath = Path.Combine(tempRoot, "untyped-nonbind-protocol.xml");
        File.WriteAllText(xmlPath, """
            <?xml version="1.0" encoding="UTF-8"?>
            <protocol name="untyped_nonbind">
              <copyright>Test</copyright>
              <interface name="test_factory" version="1">
                <description summary="factory interface">
                  Factory with untyped create.
                </description>
                <request name="create">
                  <description summary="create object">
                    Create an object.
                  </description>
                  <arg name="id" type="new_id" summary="created object"/>
                </request>
              </interface>
            </protocol>
            """);

        var outputDir = Path.Combine(tempRoot, "untyped-nonbind-generated");
        Directory.CreateDirectory(outputDir);

        var metadata = ProtocolGeneratorTestHelpers.Metadata(xmlPath, outputDir, ns: "Test");
        var generator = new ProtocolGenerator();
        generator.Generate(metadata);

        var interfacesSource = File.ReadAllText(Path.Combine(outputDir, "WaylandInterfaces.cs"));
        Assert.Contains("Signature = Utf8StringMarshaller.ConvertToUnmanaged(\"n\")", interfacesSource);
        Assert.Contains("Types = (WlInterface**)CreateTypesArray([(WlInterface*)IntPtr.Zero])", interfacesSource);
    }

    [Fact]
    public void GenerateInterface_EscapesXmlDocAndCStringContent()
    {
        var generator = new ProtocolGenerator();
        var metadata = ProtocolGeneratorTestHelpers.Metadata("escape.xml", tempRoot);
        var iface = ProtocolGeneratorTestHelpers.Iface(
            "test_escape",
            summary: "summary with <tags> & ampersand",
            text: "  line one\n  line two",
            requests: [ProtocolGeneratorTestHelpers.Req(
                "say",
                summary: "say \"hello\"",
                text: "backslash \\ test",
                args: [ProtocolGeneratorTestHelpers.Arg("message", "string", summary: "msg")])]);

        var output = ProtocolGeneratorTestHelpers.GenerateInterfaceSource(generator, metadata, iface);

        Assert.Contains("summary with &lt;tags&gt; &amp; ampersand", output);
        Assert.Contains("public const string InterfaceName = \"test_escape\";", output);
        Assert.Contains("Say \"hello\"", output);
        Assert.Contains("backslash \\ test", output);
    }
}
