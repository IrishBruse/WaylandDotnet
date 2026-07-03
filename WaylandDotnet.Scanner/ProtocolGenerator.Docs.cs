namespace WaylandDotnet.Scanner;

using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using WaylandDotnet.Scanner.Data;

public partial class ProtocolGenerator
{
    public SourceFile md = new("");
    public SourceFile navbar = new("");
    private string mdHref = "";

    private void InitializeDocumentation(ProtocolMetadata metadata, WaylandProtocol protocol)
    {
        string mdFile = Path.GetFileNameWithoutExtension(metadata.XmlFile);
        mdHref = $"#/Protocols/{metadata.Namespace}/{mdFile}";

        if (metadata.DocsDir != null)
        {
            string docsPath = Path.Combine(metadata.DocsDir, "Protocols", metadata.Namespace, mdFile);
            Directory.CreateDirectory(docsPath);
            md = new(Path.Combine(docsPath, "README.md"));
            navbar = new(Path.Combine(docsPath, "navbar.md"));
        }
        else
        {
            md = new("");
            navbar = new("");
        }

        GenerateBreadcrumbDocumentation(metadata, protocol);
    }

    private void SaveDocumentation()
    {
        if (md.Path != "")
        {
            md.Save();
        }

        if (navbar.Path != "")
        {
            navbar.Save();
        }
    }

    private void GenerateBreadcrumbDocumentation(ProtocolMetadata metadata, WaylandProtocol protocol)
    {
        const string arrowImg = "<img src=\"assets/arrow.svg\" class=\"breadcrumb-arrow\" alt=\"\" />";
        string protocolDir = Path.GetFileNameWithoutExtension(metadata.XmlFile);

        string breadcrumb =
            $"<a href=\"https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet\">WaylandDotnet</a>" +
            $" {arrowImg} " +
            $"<a href=\"https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/{metadata.Namespace}\">{metadata.Namespace}</a>" +
            $" {arrowImg} " +
            $"<a href=\"https://github.com/IrishBruse/WaylandDotnet/blob/main/WaylandDotnet/Protocols/{metadata.Namespace}/{protocolDir}/\">{protocol.Name.ToPascal()}</a>";

        md.WriteLine($"# {metadata.Name}");
        md.WriteLine();
        md.WriteLine($"<p class=\"breadcrumb\">{breadcrumb}</p>");
        md.WriteLine();
        md.WriteLine("---");
        md.WriteLine();
    }

    private void DocumentInterface(WaylandInterface iface)
    {
        string className = iface.Name.ToPascal();
        string interfaceDocId = className.ToDocId();

        navbar.WriteLine($"- [{className}](#{interfaceDocId} ':class=interface')");

        var node = Html.H2().Class("decleration interface")
            .Child(Html.A().Href(mdHref + "/?id=" + interfaceDocId).Id(interfaceDocId)
                .Child(Html.Span().Class("codicon codicon-symbol-interface"))
                .Child(Html.Text(className)))
            .Child(Html.Span().Class("pill").Text($"version {iface.Version}"));

        md.WriteLine(node.ToString());
        md.WriteLine();
        md.WriteLine(iface.Description.Summary.CapitalizeFirst());
        md.WriteLine();
        md.WriteLine(iface.Description.Text.TrimLinesStart());
        md.WriteLine();
    }

    private void GenerateEventDocumentation(WaylandInterface iface)
    {
        if (iface.Events.Count == 0) return;

        foreach (var evt in iface.Events)
        {
            var eventName = evt.Name.ToPascal();
            var delegateName = eventName + "Handler";
            var parameters = string.Join(", ", evt.Args.Select(arg => $"{MapTypeToCSharp(arg.Type, arg.Interface, arg.AllowNull)} {arg.Name.ToCamel()}"));
            var delegateDeclaration = $"void {delegateName}({parameters})";

            EventDocumentation(iface, evt, delegateDeclaration);
        }
    }

    private void GenerateEnumDocumentation(WaylandInterface iface)
    {
        if (iface.Enums.Count == 0) return;

        foreach (var enumDef in iface.Enums)
        {
            GenerateEnumDocumentation(iface, enumDef);
        }
    }

    private void GenerateEnumDocumentation(WaylandInterface iface, WaylandEnum? enumDef)
    {
        if (enumDef == null) return;

        var enumName = (enumDef.Name ?? "").ToPascal();
        string enumDocId = $"{iface.Name.ToPascal()}_{enumName}_enum".ToDocId();

        var isBitfield = enumDef.Bitfield?.ToLower() == "true";
        var suffix = isBitfield ? "Flag" : "";

        var enumNode = Html.H3().Class("decleration enum").Title($"{enumName} enum")
            .Child(
                Html.A().Href(mdHref + "/?id=" + enumDocId).Id(enumDocId)
                .Child(Html.Span().Class("codicon codicon-symbol-enum enum"))
                .Child(Html.Text(iface.Name.ToPascal() + ".").Child(Html.Span().Class("enum").Text(enumName)))

            );

        md.WriteLine(enumNode.ToString());

        md.WriteLine();
        md.WriteLine("```csharp");
        md.WriteLine($"public enum {enumName}{suffix}");
        md.WriteLine("```");
        md.WriteLine();

        if (enumDef.Description != null)
        {
            md.WriteLine(enumDef.Description.Summary.CapitalizeFirst());
            md.WriteLine();
            md.WriteLine(enumDef.Description.Text.TrimLinesStart());
            md.WriteLine();
        }

        md.WriteLine("| Value | Integer | Description |");
        md.WriteLine("| --- | --- | --- |");
        foreach (var entry in enumDef.Entries ?? [])
        {
            md.WriteLine($"| {entry.Name.ToPascal()} | {entry.Value} | {entry.Summary.CapitalizeFirst()} |");
        }
    }

    private void EventDocumentation(WaylandInterface iface, WaylandEvent evt, string delegateDeclaration)
    {
        var summary = EscapeXmlDoc(evt.Description?.Summary ?? evt.Name).CapitalizeFirst();
        var docs = EscapeXmlDoc(evt.Description?.Text ?? evt.Name);
        var eventName = evt.Name.ToPascal();
        string eventDocId = ("On" + iface.Name.ToPascal() + "_" + eventName).ToDocId();

        var eventNode = Html.H3().Class("decleration event").Title($"{eventName} event")
            .Child(
                Html.A().Href(mdHref + "/?id=" + eventDocId).Id(eventDocId)
                .Child(Html.Span().Class("codicon codicon-symbol-event event"))
                .Child(Html.Text(iface.Name.ToPascal() + ".").Child(Html.Span().Class("event").Text("On" + eventName)))
            );

        if (evt.Since > 0)
        {
            eventNode.Child(Html.Span().Class("pill").Text($"since {evt.Since}"));
        }

        md.WriteLine(eventNode.ToString());

        md.WriteLine();
        md.WriteLine("```csharp");
        md.WriteLine(delegateDeclaration);
        md.WriteLine("```");
        md.WriteLine();
        if (evt.Args.Count > 0)
        {
            md.WriteLine("| Argument | Type | Description |");
            md.WriteLine("| --- | --- | --- |");
        }
        foreach (var arg in evt.Args)
        {
            md.WriteLine($"| {arg.Name} | {arg.Type} | {arg.Summary.CapitalizeFirst()} |");
        }
        md.WriteLine();
        md.WriteLine($"**{summary}**");
        md.WriteLine(docs);
    }

    private void RequestDocumentation(WaylandInterface iface, WaylandRequest request, string summary, string docs, string methodName, string requestDeclaration)
    {
        string requestDocId = (iface.Name.ToPascal() + "_" + methodName).ToDocId();

        var h3Node = Html.H3().Class("decleration request").Title($"{methodName} request")
            .Child(Html.A().Href(mdHref + "/?id=" + requestDocId).Id(requestDocId)
                .Child(Html.Span().Class("codicon codicon-symbol-method method"))
                .Child(Html.Text(iface.Name.ToPascal() + ".").Child(Html.Span().Class("method").Text(methodName)))
            );

        if (request.Since > 0)
        {
            h3Node.Child(Html.Span().Class("pill").Text($"since {request.Since}"));
        }

        if (!string.IsNullOrEmpty(request.Type))
        {
            Assert(request.Type == "destructor");
            h3Node.Child(Html.Span().Class("pill destructor").Text($"Type: {request.Type}"));
        }

        md.WriteLine(h3Node.ToString());

        md.WriteLine();
        md.WriteLine("```csharp");
        md.WriteLine(requestDeclaration);
        md.WriteLine("```");
        md.WriteLine();
        if (request.Args.Count > 0)
        {
            md.WriteLine("| Argument | Type | Description |");
            md.WriteLine("| --- | --- | --- |");
        }
        foreach (var arg in request.Args)
        {
            md.WriteLine($"| {arg.Name} | {arg.Type} | {arg.Summary.CapitalizeFirst()} |");
        }
        md.WriteLine();
        md.WriteLine($"**{summary}**");
        md.WriteLine(docs);
    }

    private void Assert(bool condition, [CallerArgumentExpression("condition")] string? message = null)
    {
        if (!condition)
        {
            throw new InvalidOperationException($"Assertion failed: {message}");
        }
    }
}
