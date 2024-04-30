using Media.Application.Common;
using Media.Infrastructure.JWT;
using Microsoft.Extensions.DependencyInjection;

namespace Media.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IJWTChecker, JWTChecker>();
        services.AddScoped<IMediaRepository, MediaRepository.MediaRepository>();
        return services;
    }
}