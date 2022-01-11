using System;
using System.Collections.Generic;
using Dapper;
using FluentMigrator.Runner;
using Ignis.Nuke.FluentMigrator.Data;
using Ignis.Nuke.FluentMigrator.Logging.Nuke;
using Ignis.Nuke.FluentMigrator.Logging.XUnit;
using Nuke.Common.Tooling;
using PowerAssert;
using Xunit;
using Xunit.Abstractions;

namespace Ignis.Nuke.FluentMigrator;

public sealed class FluentMigratorTasksTest : IDisposable
{
    private readonly string _assembly;
    private readonly TestDatabase _database;
    private readonly Action<OutputType, string> _originalLogger;
    private readonly LogRecorder _recorder;

    public FluentMigratorTasksTest(ITestOutputHelper output)
    {
        _database = new TestDatabase();
        _assembly = GetType().Assembly.Location;
        _recorder = new LogRecorder();
        var logger = new AggregateNukeLogger(_recorder, new XUnitNukeLogger(output));

        _originalLogger = FluentMigratorTasks.Logger;
        FluentMigratorTasks.Logger = logger.Logger;
    }

    public void Dispose()
    {
        FluentMigratorTasks.Logger = _originalLogger;
    }

    [Fact]
    public void TestFluentMigratorMigrateUp()
    {
        MigrateUp();

        using var connection = _database.Open();
        var actual = connection.QueryFirst<int>("select count(*) from dummy");

        PAssert.IsTrue(() => actual == 0);
    }

    [Fact]
    public void TestCustomMetadata()
    {
        MigrateUp();

        using var connection = _database.Open();
        var actual = connection.QueryFirst<int>("select count(*) from CustomVersionInfo");

        PAssert.IsTrue(() => actual == 1);
    }

    [Fact]
    public void TestLoggingConsole()
    {
        MigrateUp();

        var actual = _recorder.Output().Standard;

        PAssert.IsTrue(() => actual.Contains("DummyTable migrated"));
    }

    [Fact]
    public void TestLoggingOutputs()
    {
        var actual = MigrateUp().AllText();

        PAssert.IsTrue(() => actual.Contains("DummyTable migrated"));
    }

    private IReadOnlyCollection<Output> MigrateUp()
    {
        return FluentMigratorTasks.FluentMigratorMigrateUp(s => s
            .SetConnectionString(_database.ConnectionString)
            .AddAssembly(_assembly)
            .AddConfigureRunner(rb => rb.AddSQLite()));
    }
}
