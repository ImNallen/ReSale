using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Persistence.Repositories;
using ReSale.Domain.Shared;
using ReSale.Domain.Users;

namespace ReSale.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ReSaleDbContext _context;

    public UserRepository(ReSaleDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(
        Guid id, 
        CancellationToken cancellationToken = default)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(
        Email email, 
        CancellationToken cancellationToken = default)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken: cancellationToken);
    }
    
    public async Task<bool> IsEmailUniqueAsync(Email email)
    {
        return !await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task CreateAsync(
        User user, 
        CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(user, cancellationToken);
    }
}