using System.Linq;
using Nuke.Common.Tooling;
using PowerAssert;
using Xunit;

namespace Ignis.Nuke.FluentMigrator.Logging.Nuke;

public sealed class OutputNukeLoggerTest
{
    private readonly OutputNukeLogger _target;

    public OutputNukeLoggerTest()
    {
        _target = new OutputNukeLogger();
    }

    [Fact]
    public void TestOutputs()
    {
        _target.Logger(OutputType.Std, "ok");
        _target.Logger(OutputType.Err, "ng");

        var actual = _target.Outputs().ToArray();

        PAssert.IsTrue(() => actual.SequenceEqual(new[]
        {
            new Output {Type = OutputType.Std, Text = "ok"},
            new Output {Type = OutputType.Err, Text = "ng"}
        }));
    }
}
