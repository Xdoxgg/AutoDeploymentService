using DeployService.Domain.Entities;

namespace DeployService.Application.Interfaces;

public interface IJwtTokenService
{
    AccessTokenResult CreateToken(User user);
}

public sealed record AccessTokenResult(
    string Token,
    DateTime ExpiresAtUtc);