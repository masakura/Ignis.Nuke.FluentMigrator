using Nuke.Common.Tooling;

namespace Ignis.Nuke.FluentMigrator.Logging.Nuke;

internal interface INukeLogger
{
    void Logger(OutputType type, string text);
}
