using Ignis.Nuke.FluentMigrator.Logging.Nuke;

namespace Ignis.Nuke.FluentMigrator.Logging.IO;

internal sealed class NukeLoggerTextWriters : IDisposable
{
    public NukeLoggerTextWriters(INukeLogger logger)
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
