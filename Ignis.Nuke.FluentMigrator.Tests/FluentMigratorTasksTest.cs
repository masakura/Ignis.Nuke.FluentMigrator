using FluentMigrator.Runner;
using Microsoft.Data.Sqlite;
using PowerAssert;
using Xunit;

namespace Ignis.Nuke.FluentMigrator;

public sealed class FluentMigratorTasksTest
{
    [Fact]
    public void TestFluentMigratorMigrateUp()
    {
        FluentMigratorTasks.FluentMigratorMigrateUp(s => s
            .AddConfigureRunner(rb => rb
                .AddSQLite()
                .WithGlobalConnectionString("Data Source=temp;Mode=Memory;Cache=Shared")
                .ScanIn(GetType().Assembly).For.Migrations()));

        using var connection = new SqliteConnection("Data Source=temp;Mode=Memory;Cache=Shared");
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "select count(*) from dummy";
        var actual = (long) command.ExecuteScalar()!;

        PAssert.IsTrue(() => actual == 0);
    }
}
