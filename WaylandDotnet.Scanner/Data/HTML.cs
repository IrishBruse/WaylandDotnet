namespace WaylandDotnet.Scanner.Data;

using System.Text;

public class HTMLNode
{
    public string Tag { get; set; }
    public Dictionary<string, string> Attributes { get; set; } = new();
    public List<HTMLNode> Children { get; set; } = new();

    public string InnerText { get; set; } = string.Empty;
    public bool IsTextNode { get; set; } = false;

    private const int IndentSize = 4; // Adjust spaces here

    public HTMLNode(string tag = "text")
    {
        Tag = tag;
        if (tag == "text") IsTextNode = true;
    }

    public HTMLNode Attr(string key, string value)
    {
        Attributes[key] = value;
        return this;
    }

    public HTMLNode Class(string value) => Attr("class", value);
    public HTMLNode Id(string value) => Attr("id", value);
    public HTMLNode Href(string value) => Attr("href", value);

    public HTMLNode Child(HTMLNode child)
    {
        Children.Add(child);
        return this;
    }

    public override string ToString()
    {
        return BuildString(0);
    }

    private string BuildString(int indentLevel, bool forceInline = false)
    {
        string indent = forceInline ? "" : new string(' ', indentLevel * IndentSize);
        var sb = new StringBuilder();

        if (IsTextNode)
        {
            sb.Append($"{indent}{InnerText}");
        }
        else
        {
            sb.Append($"{indent}<{Tag}");
            foreach (var attr in Attributes)
            {
                sb.Append($" {attr.Key}=\"{attr.Value}\"");
            }
            sb.Append('>');
        }

        if (Children.Count > 0)
        {
            bool childrenShouldBeInline = IsTextNode;

            foreach (var child in Children)
            {
                if (!childrenShouldBeInline) sb.AppendLine();

                sb.Append(child.BuildString(indentLevel + 1, childrenShouldBeInline));
            }

            if (!IsTextNode)
            {
                if (!childrenShouldBeInline)
                {
                    sb.AppendLine();
                    sb.Append($"{indent}</{Tag}>");
                }
                else
                {
                    sb.Append($"</{Tag}>");
                }
            }
        }
        else if (!IsTextNode)
        {
            sb.Append(InnerText);
            sb.Append($"</{Tag}>");
        }

        return sb.ToString();
    }

    public HTMLNode Text(string text)
    {
        InnerText = text;
        return this;
    }
}

public static class Html
{
    public static HTMLNode H1()
    {
        return new HTMLNode("h1");
    }

    public static HTMLNode H2()
    {
        return new HTMLNode("h2");
    }

    public static HTMLNode H3()
    {
        return new HTMLNode("h3");
    }

    public static HTMLNode A()
    {
        return new HTMLNode("a");
    }

    public static HTMLNode Span()
    {
        return new HTMLNode("span");
    }

    public static HTMLNode Text(string text)
    {
        return new HTMLNode("text") { InnerText = text };
    }
}