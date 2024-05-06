using Media.Application.Common;
using Media.Infrastructure.JWT;
using Media.Infrastructure.MediaRepository;
using Microsoft.Extensions.DependencyInjection;

namespace Media.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IJWTChecker, JWTChecker>();
        services.AddScoped<IMongoRepository, MongoRepository>();
        services.AddScoped<IMediaRepository, MediaRepository.MediaRepository>();
        return services;
    }
}