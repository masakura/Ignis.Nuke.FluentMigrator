using System;
using System.Data;
using Microsoft.Data.Sqlite;

namespace Ignis.Nuke.FluentMigrator.Data;

internal sealed class TestDatabase
{
    public TestDatabase() : this(Guid.NewGuid().ToString())
    {
    }

    private TestDatabase(string name)
    {
        ConnectionString = new SqliteConnectionStringBuilder
        {
            DataSource = name,
            Mode = SqliteOpenMode.Memory,
            Cache = SqliteCacheMode.Shared
        }.ConnectionString;
    }

    public string ConnectionString { get; }

    public IDbConnection Open()
    {
        var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        return connection;
    }
}
