using Microsoft.Extensions.Logging;

namespace DeployService.Application.Services;
using Interfaces;
using Domain.Entities;
using Domain.Enums;


public sealed class AuthService(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IJwtTokenService jwtTokenService, ILogger<AuthService> logger)
{
    public async Task<uint> RegisterAsync(
        string email,
        string password,
        string displayName,
        CancellationToken ct)
    {
        email = email.Trim().ToUpperInvariant();

        var existingUser = await userRepository.GetByEmailAsync(
            email, ct);

        if (existingUser is not null)
        {
            throw new InvalidOperationException(
                "Пользователь с таким email уже существует.");
        }

        var user = new User
        {
            Email = email,
            PasswordHash = passwordHasher.Hash(password),
            DisplayName = displayName.Trim(),
            Role = Roles.Developer,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var createdUser = await userRepository.AddAsync(user, ct);

        return createdUser.Id;
    }

    public async Task<AuthResult> LoginAsync(
        string email,
        string password,
        CancellationToken ct)
    {
        logger.LogInformation($"Invoked LoginAsync with email: {email}, password: {password}");
        var user = await userRepository.GetByEmailAsync(
            email.Trim().ToUpperInvariant(), ct);

        if (user is null ||
            !passwordHasher.Verify(password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException(
                "Неверный email или пароль.");
        }

        if (!user.IsActive)
        {
            throw new UnauthorizedAccessException(
                "Учетная запись отключена.");
        }

        var token = jwtTokenService.CreateToken(user);

        return new AuthResult(
            token.Token,
            token.ExpiresAtUtc);
    }
}

public sealed record AuthResult(
    string AccessToken,
    DateTime ExpiresAtUtc);