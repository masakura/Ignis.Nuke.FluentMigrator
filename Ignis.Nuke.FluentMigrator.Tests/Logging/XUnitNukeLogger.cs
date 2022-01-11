using Nuke.Common.Tooling;
using Xunit.Abstractions;

namespace Ignis.Nuke.FluentMigrator.Logging;

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
