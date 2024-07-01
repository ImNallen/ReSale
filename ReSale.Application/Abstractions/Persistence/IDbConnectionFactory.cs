using System.Data;

namespace ReSale.Application.Abstractions.Persistence;

public interface IDbConnectionFactory
{
    IDbConnection GetOpenConnection();
}
