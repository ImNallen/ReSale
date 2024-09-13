using System.Collections.ObjectModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Domain.Common;
using ReSale.Domain.Customers;
using ReSale.Domain.Employees;
using ReSale.Domain.Messages;
using ReSale.Domain.OrderDetails;
using ReSale.Domain.Orders;
using ReSale.Domain.Products;
using ReSale.Domain.Users;

namespace ReSale.Infrastructure.Persistence;

public class ReSaleDbContext(
    DbContextOptions<ReSaleDbContext> options,
    IPublisher publisher)
    : DbContext(options), IReSaleDbContext
{
    public DbSet<User> Users => Set<User>();

    public DbSet<Customer> Customers => Set<Customer>();

    public DbSet<Employee> Employees => Set<Employee>();

    public DbSet<Product> Products => Set<Product>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();

    public DbSet<Message> Messages => Set<Message>();

    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        int result = await base.SaveChangesAsync(cancellationToken);

        await PublishDomainEvents();

        return result;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReSaleDbContext).Assembly);

        modelBuilder.HasDefaultSchema(Schemas.Default);
    }

    private async Task PublishDomainEvents()
    {
        var domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(x => x.Entity)
            .SelectMany(x =>
            {
                IReadOnlyList<IDomainEvent> domainEvents = x.GetDomainEvents();

                x.ClearDomainEvents();

                return domainEvents;
            })
            .ToList();

        foreach (IDomainEvent domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent);
        }
    }
}
