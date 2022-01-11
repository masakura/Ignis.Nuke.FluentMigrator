using System;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PowerAssert;
using Xunit;
using Xunit.Abstractions;

namespace Ignis.Nuke.FluentMigrator.Logging;

public sealed class LoggerAdapterTest : IDisposable
{
    private readonly LogRecorder _recorder;
    private readonly ServiceProvider _services;
    private readonly ILogger _target;

    public LoggerAdapterTest(ITestOutputHelper output)
    {
        _recorder = new LogRecorder();
        var logger = new AggregateNukeLogger(_recorder, new XUnitNukeLogger(output));

        _services = new ServiceCollection()
            .Configure<FluentMigratorLoggerOptions>(_ => { })
            .AddLogging(logging => logging.AddNukeLogger(logger.Logger))
            .BuildServiceProvider();

        _target = _services.GetRequiredService<ILoggerProvider>().CreateLogger("unused category");
    }

    public void Dispose()
    {
        _services.Dispose();
    }

    [Theory]
    [InlineData(LogLevel.Information)]
    [InlineData(LogLevel.Warning)]
    public void TestLogStandard(LogLevel logLevel)
    {
        _target.Log(logLevel, "log");
        var (standard, error) = _recorder.Output();

        PAssert.IsTrue(() => standard.Contains("log") && error == string.Empty);
    }

    [Theory]
    [InlineData(LogLevel.Error)]
    [InlineData(LogLevel.Critical)]
    public void TestLogError(LogLevel logLevel)
    {
        _target.Log(logLevel, "log");
        var (standard, error) = _recorder.Output();

        PAssert.IsTrue(() => standard.Contains("log") && error == string.Empty);
    }

    [Fact]
    public void TestLogException()
    {
        _target.LogError(new Exception("EXCEPTION"), "error");
        var actual = _recorder.Output().Error;

        PAssert.IsTrue(() => actual.Contains("EXCEPTION"));
    }
}
