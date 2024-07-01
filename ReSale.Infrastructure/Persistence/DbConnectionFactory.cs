using System.Data;
using Npgsql;
using ReSale.Application.Abstractions.Persistence;

namespace ReSale.Infrastructure.Persistence;

internal sealed class DbConnectionFactory(NpgsqlDataSource dataSource) : IDbConnectionFactory
{
    public IDbConnection GetOpenConnection()
    {
        NpgsqlConnection connection = dataSource.OpenConnection();

        return connection;
    }
}