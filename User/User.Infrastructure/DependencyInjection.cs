using Microsoft.Extensions.DependencyInjection;
using User.Application.common;
using User.Infrastructure.Hasher;
using User.Infrastructure.UserRepository;

namespace User.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository.UserRepository>();
        services.AddScoped<IHasher, SHA256Hasher>();
        
        services.AddDbContext<SQLServerContext>();
        return services;
    }
}