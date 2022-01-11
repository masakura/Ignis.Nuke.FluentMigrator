using Ignis.Nuke.FluentMigrator.Logging.Nuke;
using Nuke.Common.Tooling;
using Xunit.Abstractions;

namespace Ignis.Nuke.FluentMigrator.Logging.XUnit;

internal sealed class XUnitNukeLogger : INukeLogger
{
    private readonly ITestOutputHelper _output;

    public XUnitNukeLogger(ITestOutputHelper output)
    {
        _output = output;
    }

    public void Logger(OutputType type, string message)
    {
        _output.WriteLine($"{type}: {message}");
    }
}
