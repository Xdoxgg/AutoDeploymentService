using DeployService.Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DeployService.Infrastructure.Authentication;

public class PasswordHasher:IPasswordHasher
{
    private readonly PasswordHasher<object> _hasher = new();

    public string Hash(string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(password);

        return _hasher.HashPassword(new object(), password);
    }

    public bool Verify(string password, string passwordHash)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(password);
        ArgumentException.ThrowIfNullOrWhiteSpace(passwordHash);

        var result = _hasher.VerifyHashedPassword(
            new object(),
            passwordHash,
            password);

        return result != PasswordVerificationResult.Failed;
    }
}