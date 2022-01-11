using System.Collections.Generic;
using System.Collections.Immutable;
using Nuke.Common.Tooling;

namespace Ignis.Nuke.FluentMigrator.Logging;

internal sealed class AggregateNukeLogger : INukeLogger
{
    private readonly IEnumerable<INukeLogger> _loggers;

    public AggregateNukeLogger(params INukeLogger[] loggers) : this((IEnumerable<INukeLogger>) loggers)
    {
    }

    private AggregateNukeLogger(IEnumerable<INukeLogger> loggers)
    {
        _loggers = loggers.ToImmutableArray();
    }

    public void Logger(OutputType type, string message)
    {
        foreach (var logger in _loggers) logger.Logger(type, message);
    }
}
