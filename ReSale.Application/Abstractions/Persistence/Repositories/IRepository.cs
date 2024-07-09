using System.Linq.Expressions;

namespace ReSale.Application.Abstractions.Persistence.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task AddManyAsync(List<TEntity> entities, CancellationToken cancellationToken = default);
    
    void Remove(TEntity entity);
    void RemoveMany(IEnumerable<TEntity> entities);
}