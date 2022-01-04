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
        FluentMigratorTasks.FluentMigratorMigrateUp(s => s
            .AddConfigureRunner(rb => rb
                .AddSQLite()
                .WithGlobalConnectionString(_database.ConnectionString)
                .ScanIn(GetType().Assembly).For.Migrations()));

        using var connection = _database.Open();
#pragma warning disable CS8605
        var actual = (long) connection.ExecuteScaler("select count(*) from dummy");
#pragma warning restore CS8605

        PAssert.IsTrue(() => actual == 0L);
    }
}
