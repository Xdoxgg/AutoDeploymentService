using DeployService.Domain.Entities;

namespace DeployService.Application.Interfaces;

public interface IEntityRepository<T>
{
    public Task<List<T>> GetAllAsync(CancellationToken ct);
    public Task<T?> GetByIdAsync(uint id, CancellationToken ct);
    public Task<T> AddAsync(T entity, CancellationToken ct);
    public Task<T> UpdateAsync(T entity, CancellationToken ct);
    public Task<bool> DeleteAsync(uint id, CancellationToken ct);
}