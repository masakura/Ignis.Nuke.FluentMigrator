using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;

namespace Ignis.Nuke.FluentMigrator.Logging;

internal static class LoggerExtensions
{
    public static void Log(this ILogger logger, LogLevel logLevel, string message)
    {
        logger.Log(logLevel,
            new EventId(0, "FluentMigrator.Runner"),
            new FormattedLogValues(message), null,
            (values, _) => values.ToString());
    }
}
