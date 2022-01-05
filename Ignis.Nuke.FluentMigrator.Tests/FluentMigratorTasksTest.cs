using Dapper;
using FluentMigrator.Runner;
using Ignis.Nuke.FluentMigrator.Data;
using PowerAssert;
using Xunit;

namespace Ignis.Nuke.FluentMigrator;

public sealed class FluentMigratorTasksTest
{
    private readonly string _assembly;
    private readonly TestDatabase _database;

    public FluentMigratorTasksTest()
    {
        _database = new TestDatabase();
        _assembly = GetType().Assembly.Location;
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
}
