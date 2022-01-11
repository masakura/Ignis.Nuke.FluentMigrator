using FluentMigrator.Runner;
using FluentMigrator.Runner.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ignis.Nuke.FluentMigrator.Logging;

internal sealed class NukeLoggerAdapterProvider : ILoggerProvider
{
    private readonly IOptions<FluentMigratorLoggerOptions> _options;
    private readonly NukeLoggerWriters _writers;

    public NukeLoggerAdapterProvider(NukeLoggerWriters writers,
        IOptions<FluentMigratorLoggerOptions> options)
    {
        _writers = writers;
        _options = options;
    }

    public void Dispose()
    {
        _writers.Dispose();
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new FluentMigratorRunnerLogger(_writers.Std, _writers.Err, _options.Value);
    }
}
