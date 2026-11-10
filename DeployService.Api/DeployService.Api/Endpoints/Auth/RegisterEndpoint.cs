using DeployService.Api.Contacts.Auth;
using DeployService.Application.Services;
using FastEndpoints;

namespace DeployService.Api.Endpoints.Auth;

public sealed class RegisterEndpoint(
    AuthService authService)
    : Endpoint<RegisterRequest, RegisterResponse>
{
    public override void Configure()
    {
        Post("/api/auth/register");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        RegisterRequest req,
        CancellationToken ct)
    {
        var userId = await authService.RegisterAsync(
            req.Email,
            req.Password,
            req.DisplayName,
            ct);

        await Send.OkAsync(
            new RegisterResponse(userId),
            ct);
    }
}

public sealed record RegisterResponse(uint UserId);