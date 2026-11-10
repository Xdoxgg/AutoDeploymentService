using DeployService.Domain.Entities;

namespace DeployService.Application.Interfaces;

public interface IUserRepository : IEntityRepository<User>
{
    public Task<User?> GetByEmailAsync(string email, CancellationToken ct);
}