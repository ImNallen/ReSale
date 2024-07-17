using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Persistence.Repositories;
using ReSale.Domain.Shared;
using ReSale.Domain.Users;

namespace ReSale.Infrastructure.Persistence.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ReSaleDbContext context) 
        : base(context)
    {
    }
    
    public async Task<User?> GetByIdAsync(
        Guid id, 
        CancellationToken cancellationToken = default)
    {
        return await Context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(
        Email email, 
        CancellationToken cancellationToken = default)
    {
        return await Context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken: cancellationToken);
    }
    
    public async Task<bool> IsEmailUniqueAsync(Email email)
    {
        return !await Context.Users.AnyAsync(u => u.Email == email);
    }

    public ReSaleDbContext ReSaleDbContext => Context;
}