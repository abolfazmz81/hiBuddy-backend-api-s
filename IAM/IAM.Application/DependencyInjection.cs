using IAM.Application.AuthenticationService;
using Microsoft.Extensions.DependencyInjection;

namespace IAM.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRegisterService, RegisterService>();
        services.AddScoped<IAuthPhoneRegister, AuthPhoneRegister>();
        return services;
    }
}