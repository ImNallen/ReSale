﻿using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Domain.Customers;
using ReSale.Domain.Employees;
using ReSale.Domain.OrderDetails;
using ReSale.Domain.Orders;
using ReSale.Domain.Products;
using ReSale.Domain.Users;

namespace ReSale.Infrastructure.Persistence;

public class ReSaleDbContext(DbContextOptions<ReSaleDbContext> options) 
    : DbContext(options), IReSaleDbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReSaleDbContext).Assembly);
        
        modelBuilder.HasDefaultSchema(Schemas.Default);
    }
}