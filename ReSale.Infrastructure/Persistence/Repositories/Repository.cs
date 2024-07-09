using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Persistence.Repositories;

namespace ReSale.Infrastructure.Persistence.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    protected readonly ReSaleDbContext Context;
    
    public Repository(ReSaleDbContext context)
    {
        Context = context;
    }
    
    public async Task<TEntity?> GetAsync(
        Guid id, 
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<TEntity>().FindAsync(id, cancellationToken);
    }

    public async Task<List<TEntity>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<TEntity>().ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        TEntity entity, 
        CancellationToken cancellationToken = default)
    {
        await Context.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public async Task AddManyAsync(
        List<TEntity> entities, 
        CancellationToken cancellationToken = default)
    {
        await Context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
    }

    public void Remove(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
    }

    public void RemoveMany(IEnumerable<TEntity> entities)
    {
        Context.Set<TEntity>().RemoveRange(entities);
    }
}