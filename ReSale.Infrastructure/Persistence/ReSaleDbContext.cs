using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Domain.Customers;
using ReSale.Domain.Users;

namespace ReSale.Infrastructure.Persistence;

public class ReSaleDbContext(DbContextOptions<ReSaleDbContext> options) 
    : DbContext(options), IUnitOfWork
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Customer> Customers => Set<Customer>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReSaleDbContext).Assembly);
        
        modelBuilder.HasDefaultSchema(Schemas.Default);
    }
    
    public async Task<IDbTransaction> BeginTransactionAsync()
    {
        return (await Database.BeginTransactionAsync()).GetDbTransaction();
    }
}