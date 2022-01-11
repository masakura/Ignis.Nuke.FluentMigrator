using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nuke.Common.Tooling;

namespace Ignis.Nuke.FluentMigrator.Logging;

internal static class LoggingExtensions
{
    // ReSharper disable once UnusedMethodReturnValue.Global
    public static ILoggingBuilder AddNukeLogger(this ILoggingBuilder logging, INukeLogger nukeLogger)
    {
        logging.Services
            .AddSingleton(new NukeLoggerWriters(nukeLogger))
            .AddSingleton<ILoggerProvider, NukeLoggerAdapterProvider>();

        return logging;
    }
}
