using Nuke.Common.Tooling;

namespace Ignis.Nuke.FluentMigrator.Logging;

internal sealed class NukeLoggerWriters : IDisposable
{
    public NukeLoggerWriters(INukeLogger logger)
    {
        Std = NukeLoggerTextWriter.Std(logger.Logger);
        Err = NukeLoggerTextWriter.Err(logger.Logger);
    }

    public TextWriter Std { get; }
    public TextWriter Err { get; }

    public void Dispose()
    {
        Std.Dispose();
        Err.Dispose();
    }
}
