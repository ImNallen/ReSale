using Microsoft.EntityFrameworkCore;
using ReSale.Domain.Customers;
using ReSale.Domain.Employees;
using ReSale.Domain.Messages;
using ReSale.Domain.OrderDetails;
using ReSale.Domain.Orders;
using ReSale.Domain.Products;
using ReSale.Domain.Users;

namespace ReSale.Application.Abstractions.Persistence;

public interface IReSaleDbContext
{
    DbSet<User> Users { get; }

    DbSet<Customer> Customers { get; }

    DbSet<Employee> Employees { get; }

    DbSet<Product> Products { get; }

    DbSet<Order> Orders { get; }

    DbSet<OrderDetail> OrderDetails { get; }

    DbSet<Message> Messages { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
