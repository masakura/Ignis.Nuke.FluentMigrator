using Ignis.Nuke.FluentMigrator.Logging.IO;
using Ignis.Nuke.FluentMigrator.Logging.Nuke;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Ignis.Nuke.FluentMigrator.Logging;

internal static class LoggingExtensions
{
    // ReSharper disable once UnusedMethodReturnValue.Global
    public static ILoggingBuilder AddNukeLogger(this ILoggingBuilder logging, INukeLogger nukeLogger)
    {
        logging.Services
            .AddSingleton(new NukeLoggerTextWriters(nukeLogger))
            .AddSingleton<ILoggerProvider, NukeLoggerAdapterProvider>();

        return logging;
    }
}
