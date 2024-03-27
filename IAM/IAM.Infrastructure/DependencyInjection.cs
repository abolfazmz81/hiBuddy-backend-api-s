using IAM.Application.common;
using IAM.Infrastructure.CodeGenerator;
using IAM.Infrastructure.hasher;
using IAM.Infrastructure.InMemoryRepository;
using IAM.Infrastructure.JwtGenerators;
using IAM.Infrastructure.Logger;
using IAM.Infrastructure.UserRepository;
using Microsoft.Extensions.DependencyInjection;

namespace IAM.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ICodeGenerator, RandomCodeGenerator>();
        services.AddScoped<IInmemoryContext, RedisInMemoryContext>();
        services.AddScoped<IInMemoryRepository, InMemoryRepository.InMemoryRepository>();
        services.AddScoped<IUserRepository, UserRepository.UserRepository>();
        services.AddScoped<IMLogger, Logger.Logger>();
        services.AddScoped<IHasher,SHA256Hasher>();
        services.AddScoped<IJwtGenerator,JwtGenerator>();
        
        services.AddDbContext<SQLServerContext>();
        return services;
    }
}