using FastEndpoints;

namespace DeployService.Api.Endpoints.InitEndpoint;

public class InitEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/test");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await Send.OkAsync(ct, ct);
    }
}