namespace WaylandDotnet.Scanner.Data;

public static class Extensions
{
    public static readonly HashSet<string> CSharpKeywords =
    [
        "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked",
        "class", "const", "continue", "decimal", "default", "delegate", "do", "double", "else",
        "enum", "event", "explicit", "extern", "false", "finally", "fixed", "float", "for",
        "foreach", "goto", "if", "implicit", "in", "int", "interface", "internal", "is", "lock",
        "long", "namespace", "new", "null", "object", "operator", "out", "override", "params",
        "internal", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed",
        "short", "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw",
        "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using",
        "virtual", "void", "volatile", "while"
    ];

    public static string ToPascal(this string str)
    {
        if (string.IsNullOrEmpty(str)) return str;

        var parts = str.Split('_');
        var result = string.Join("", parts.Select(p => p.Length > 0 ? char.ToUpperInvariant(p[0]) + p.Substring(1).ToLowerInvariant() : ""));

        if (string.IsNullOrEmpty(result)) return "_";
        if (char.IsDigit(result[0])) result = "_" + result;
        if (CSharpKeywords.Contains(result)) result = "_" + result;

        return result;
    }

    public static string CapitalizeFirst(this string str)
    {
        if (string.IsNullOrEmpty(str)) return "";
        return char.ToUpper(str[0]) + str[1..];
    }

    public static string TrimLinesStart(this string str)
    {
        if (string.IsNullOrEmpty(str)) return str;

        var lines = str.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)
                       .Select(line => line.TrimStart());

        return string.Join(Environment.NewLine, lines);
    }


    public static string ToCamel(this string name)
    {
        if (string.IsNullOrEmpty(name)) return name;

        var pascal = name.ToPascal();
        var cleanPascal = pascal.TrimStart('@', '_');

        if (string.IsNullOrEmpty(cleanPascal)) return name;

        var result = char.ToLowerInvariant(cleanPascal[0]) + cleanPascal.Substring(1);

        if (char.IsDigit(cleanPascal[0])) result = "_" + result;
        if (CSharpKeywords.Contains(result)) result = "_" + result;

        return result;
    }
}
