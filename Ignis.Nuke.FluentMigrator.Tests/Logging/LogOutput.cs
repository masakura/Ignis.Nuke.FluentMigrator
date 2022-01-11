namespace Ignis.Nuke.FluentMigrator.Logging;

internal sealed record LogOutput(string Standard, string Error)
{
    public string Standard { get; } = Normalize(Standard);
    public string Error { get; } = Normalize(Error);

    private static string Normalize(string value)
    {
        return value.Trim();
    }
}
