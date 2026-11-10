
using DeployService.Application.Interfaces;
using DeployService.Infrastructure.Authentication;
using DeployService.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
namespace DeployService.Infrastructure;

public static class Middleware
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        return services;
    }

}