using IAM.Application.common;
using IAM.Infrastructure.CodeGenerator;
using IAM.Infrastructure.hasher;
using IAM.Infrastructure.InMemoryRepository;
using IAM.Infrastructure.Logger;
using IAM.Infrastructure.UserRepository;

namespace IAM.Infrastructure;
using IAM.Application.AuthenticationService;
using Microsoft.Extensions.DependencyInjection;

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
        services.AddDbContext<SQLServerContext>();
        return services;
    }
}