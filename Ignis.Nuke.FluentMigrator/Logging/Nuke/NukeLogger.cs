using Nuke.Common.Tooling;

namespace Ignis.Nuke.FluentMigrator.Logging.Nuke;

internal sealed class NukeLogger : INukeLogger
{
    private readonly Action<OutputType, string> _logger;

    public NukeLogger(Action<OutputType, string> logger)
    {
        _logger = logger;
    }

    public void Logger(OutputType type, string text)
    {
        _logger(type, text);
    }
}
