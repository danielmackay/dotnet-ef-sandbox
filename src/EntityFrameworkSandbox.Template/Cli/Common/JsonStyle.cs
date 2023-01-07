using Spectre.Console;

namespace EntityFrameworkSandbox.Template.Cli.Common;

public record JsonStyle
{
    public static readonly JsonStyle Default = new();

    public int IndentSize { get; init; } = 2;

    public Style NameStyle { get; init; } = new(Color.LightSkyBlue1);
    public Style StringStyle { get; init; } = new(Color.LightPink3);
    public Style NumberStyle { get; init; } = new(Color.DarkSeaGreen2);
    public Style NullStyle { get; init; } = new(Color.SkyBlue3);
    public Style BooleanStyle { get; init; } = new(Color.SkyBlue3);
    public Style CurlyBracketStyle { get; init; } = new(Color.Grey82);
    public Style SquareBracketStyle { get; init; } = new(Color.Grey82);
    public Style NameSeparatorStyle { get; init; } = new(Color.Grey82);
    public Style ValueSeparatorStyle { get; init; } = new(Color.Grey82);
}