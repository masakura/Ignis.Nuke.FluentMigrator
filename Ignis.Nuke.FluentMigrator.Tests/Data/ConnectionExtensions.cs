using System.Data;

namespace Ignis.Nuke.FluentMigrator.Data;

internal static class ConnectionExtensions
{
    public static object? ExecuteScaler(this IDbConnection connection, string sql)
    {
        using var command = connection.CreateCommand();
        command.CommandText = sql;
        return command.ExecuteScalar();
    }
}
