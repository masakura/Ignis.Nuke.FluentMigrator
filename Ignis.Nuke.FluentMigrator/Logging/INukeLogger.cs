using Nuke.Common.Tooling;

namespace Ignis.Nuke.FluentMigrator.Logging;

internal interface INukeLogger
{
    void Logger(OutputType type, string text);
}
