using System;
using Dapper;
using FluentMigrator.Runner;
using Ignis.Nuke.FluentMigrator.Data;
using Ignis.Nuke.FluentMigrator.Logging;
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
        FluentMigratorTasks.FluentMigratorMigrateUp(s => s
            .SetConnectionString(_database.ConnectionString)
            .AddAssembly(_assembly)
            .AddConfigureRunner(rb => rb.AddSQLite()));

        using var connection = _database.Open();
        var actual = connection.QueryFirst<int>("select count(*) from dummy");

        PAssert.IsTrue(() => actual == 0);
    }

    [Fact]
    public void TestCustomMetadata()
    {
        FluentMigratorTasks.FluentMigratorMigrateUp(s => s
            .SetConnectionString(_database.ConnectionString)
            .AddAssemblies(_assembly)
            .AddConfigureRunner(rb => rb.AddSQLite()));

        using var connection = _database.Open();
        var actual = connection.QueryFirst<int>("select count(*) from CustomVersionInfo");

        PAssert.IsTrue(() => actual == 1);
    }

    [Fact]
    public void TestLoggingConsole()
    {
        FluentMigratorTasks.FluentMigratorMigrateUp(s => s
            .SetConnectionString(_database.ConnectionString)
            .AddAssemblies(_assembly)
            .AddConfigureRunner(rb => rb.AddSQLite()));

        var actual = _recorder.Output().Standard;

        PAssert.IsTrue(() => actual.Contains("DummyTable migrated"));
    }
}
