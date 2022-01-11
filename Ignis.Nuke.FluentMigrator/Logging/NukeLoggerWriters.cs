using Nuke.Common.Tooling;

namespace Ignis.Nuke.FluentMigrator.Logging;

internal sealed class NukeLoggerWriters : IDisposable
{
    public NukeLoggerWriters(Action<OutputType, string> logger)
    {
        Std = NukeLoggerTextWriter.Std(logger);
        Err = NukeLoggerTextWriter.Err(logger);
    }

    public TextWriter Std { get; }
    public TextWriter Err { get; }

    public void Dispose()
    {
        Std.Dispose();
        Err.Dispose();
    }
}
