using FluentMigrator.Runner;
using Ignis.Nuke.FluentMigrator.Data;
using PowerAssert;
using Xunit;

namespace Ignis.Nuke.FluentMigrator;

public sealed class FluentMigratorTasksTest
{
    private readonly TestDatabase _database;

    public FluentMigratorTasksTest()
    {
        _database = new TestDatabase();
    }

    [Fact]
    public void TestFluentMigratorMigrateUp()
    {
        var assembly = GetType().Assembly.Location;

        FluentMigratorTasks.FluentMigratorMigrateUp(s => s
            .SetConnectionString(_database.ConnectionString)
            .AddAssembly(assembly)
            .AddConfigureRunner(rb => rb.AddSQLite()));

        using var connection = _database.Open();
#pragma warning disable CS8605
        var actual = (long) connection.ExecuteScaler("select count(*) from dummy");
#pragma warning restore CS8605

        PAssert.IsTrue(() => actual == 0L);
    }
}
