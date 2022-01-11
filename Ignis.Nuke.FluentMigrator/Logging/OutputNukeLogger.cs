using Nuke.Common.Tooling;

namespace Ignis.Nuke.FluentMigrator.Logging;

internal sealed class OutputNukeLogger : INukeLogger
{
    private readonly List<Output> _outputs = new();

    public void Logger(OutputType type, string text)
    {
        _outputs.Add(new Output {Type = type, Text = text});
    }

    public IReadOnlyCollection<Output> Outputs()
    {
        return _outputs.AsReadOnly();
    }
}
