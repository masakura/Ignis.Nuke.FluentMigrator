using System;
using System.IO;
using Ignis.Nuke.FluentMigrator.Logging.Nuke;
using PowerAssert;
using Xunit;

namespace Ignis.Nuke.FluentMigrator.Logging.IO;

public sealed class NukeLoggerTextWriterTest
{
    private readonly LogRecorder _recorder;
    private readonly TextWriter _target;

    public NukeLoggerTextWriterTest()
    {
        _recorder = new LogRecorder("std: ");
        _target = NukeLoggerTextWriter.Std(_recorder.Logger);
    }

    [Fact]
    public void TestSingleLine()
    {
        _target.WriteLine("hello");
        var actual = _recorder.Output().Standard;

        PAssert.IsTrue(() => actual == $"std: hello{Environment.NewLine}");
    }

    [Fact]
    public void TestMultiLine()
    {
        _target.WriteLine($"hello1{Environment.NewLine}hello2");
        var actual = _recorder.Output().Standard;

        PAssert.IsTrue(() => actual == $"std: hello1{Environment.NewLine}std: hello2{Environment.NewLine}");
    }
}
