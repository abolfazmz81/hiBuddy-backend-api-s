using IAM.Infrastructure.CodeGenerator;

namespace IAM.Infrastructure;
using IAM.Application.AuthenticationService;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ICodeGenerator, RandomCodeGenerator>();
        return services;
    }
}