using DeployService.Api.Contacts.Auth;
using DeployService.Application.Services;
using FastEndpoints;

namespace DeployService.Api.Endpoints.Auth;

public sealed class LoginEndpoint(
    AuthService authService)
    : Endpoint<LoginRequest, AuthResponse>
{
    public override void Configure()
    {
        Post("/api/auth/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        LoginRequest req,
        CancellationToken ct)
    {
        var result = await authService.LoginAsync(
            req.Email,
            req.Password,
            ct);

        await Send.OkAsync(
            new AuthResponse(
                result.AccessToken,
                result.ExpiresAtUtc),
            ct);
    }
}