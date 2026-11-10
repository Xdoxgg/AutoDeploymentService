using DeployService.Application.Interfaces;
using DeployService.Domain.Entities;
using DeployService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DeployService.Infrastructure.Repositories;

public class UserRepository(AppDbContext appDbContext) : IUserRepository
{
    public Task<List<User>> GetAllAsync(CancellationToken ct)
    {
        return appDbContext.Users
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<User?> GetByIdAsync(uint id, CancellationToken ct)
    {
        return await appDbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<User> AddAsync(User entity, CancellationToken ct)
    {
        await appDbContext.Users.AddAsync(entity, ct);
        await appDbContext.SaveChangesAsync(ct);

        return entity;
    }

    public async Task<User> UpdateAsync(User entity, CancellationToken ct)
    {
        var existingUser = await appDbContext.Users
            .FirstOrDefaultAsync(x => x.Id == entity.Id, ct);

        if (existingUser is null)
        {
            throw new KeyNotFoundException(
                $"User with ID {entity.Id} was not found.");
        }

        appDbContext.Entry(existingUser).CurrentValues.SetValues(entity);

        await appDbContext.SaveChangesAsync(ct);

        return existingUser;
    }

    public async Task<bool> DeleteAsync(uint id, CancellationToken ct)
    {
        var user = await appDbContext.Users
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        if (user is null)
        {
            return false;
        }

        appDbContext.Users.Remove(user);
        await appDbContext.SaveChangesAsync(ct);

        return true;
    }
    
    
    public Task<User?> GetByEmailAsync(
        string email,
        CancellationToken ct)
    {
        var normalizedEmail = email.Trim().ToUpperInvariant();

        return appDbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Email.ToUpper() == normalizedEmail,
                ct);
    }
}