using Spectre.Console;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace EntityFrameworkSandbox.Template.Cli.Common;

public static class AnsiConsoleExtensions
{
    public static void WriteProperty(this IAnsiConsole console, string propertyValue, [CallerArgumentExpression("propertyValue")] string? propertyName = null)
    {
        console.WriteLine($"{propertyName}: {propertyValue}");
    }

    public static IAnsiConsole WriteJson<T>(this IAnsiConsole console, T obj, JsonStyle? jsonStyle = null)
    {
        var jsonElement = JsonSerializer.SerializeToElement(obj);
        return console.WriteJson(jsonElement, jsonStyle);
    }

    public static IAnsiConsole WriteJson(this IAnsiConsole console, JsonElement node, JsonStyle? jsonStyle = null)
    {
        ArgumentNullException.ThrowIfNull(console);
        ArgumentNullException.ThrowIfNull(node);

        console.WriteJson(node, jsonStyle ?? JsonStyle.Default, 0);
        return console;
    }

    private static IAnsiConsole WriteJson(this IAnsiConsole console, JsonElement node, JsonStyle jsonStyle, int indentionLevel)
    {
        switch (node.ValueKind)
        {
            case JsonValueKind.Undefined:
                break;

            case JsonValueKind.Object:
                var indent = new string(' ', indentionLevel * jsonStyle.IndentSize);
                var firstPropertyWritten = false;
                foreach (var property in node.EnumerateObject())
                {
                    if (!firstPropertyWritten)
                    {
                        console.Write(new Text("{", jsonStyle.CurlyBracketStyle));
                        console.WriteLine();
                    }
                    else
                    {
                        console.Write(new Text(",", jsonStyle.ValueSeparatorStyle));
                        console.WriteLine();
                    }

                    console.Write(indent + "  \"");
                    console.Write(new Text(property.Name, jsonStyle.NameStyle));
                    console.Write("\"");
                    console.Write(new Text(": ", jsonStyle.NameSeparatorStyle));
                    console.WriteJson(property.Value, jsonStyle, indentionLevel + 1);
                    firstPropertyWritten = true;
                }

                if (firstPropertyWritten)
                {
                    console.WriteLine();
                    console.Write(indent);
                    console.Write(new Text("}", jsonStyle.CurlyBracketStyle));
                }
                else
                {
                    console.Write(new Text("{}", jsonStyle.CurlyBracketStyle));
                }

                break;

            case JsonValueKind.Array:
                var indentArray = new string(' ', (indentionLevel + 1) * jsonStyle.IndentSize);
                var firstPropertyWritten2 = false;
                foreach (var child in node.EnumerateArray())
                {
                    if (!firstPropertyWritten2)
                    {
                        console.Write(new Text("[", jsonStyle.SquareBracketStyle));
                        console.WriteLine();
                    }
                    else
                    {
                        console.Write(new Text(",", jsonStyle.ValueSeparatorStyle));
                        console.WriteLine();
                    }

                    console.Write(indentArray);
                    console.WriteJson(child, jsonStyle, indentionLevel + 1);
                    firstPropertyWritten2 = true;
                }

                if (firstPropertyWritten2)
                {
                    console.WriteLine();
                    console.Write(indentArray[..^2]);
                    console.Write(new Text("]", jsonStyle.SquareBracketStyle));
                }
                else
                {
                    console.Write(new Text("[]", jsonStyle.SquareBracketStyle));
                }
                break;

            case JsonValueKind.String:
                console.Write(new Text("\"" + node.GetString() + "\"", jsonStyle.StringStyle));
                break;

            case JsonValueKind.Number:
                console.Write(new Text(node.GetRawText(), jsonStyle.NumberStyle));
                break;

            case JsonValueKind.True:
                console.Write(new Text("true", jsonStyle.BooleanStyle));
                break;

            case JsonValueKind.False:
                console.Write(new Text("false", jsonStyle.BooleanStyle));
                break;

            case JsonValueKind.Null:
                console.Write(new Text("null", jsonStyle.NullStyle));
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }

        if (indentionLevel == 0)
            console.WriteLine();

        return console;
    }
}
