using Microsoft.Extensions.DependencyInjection;
using User.Application.UserManagement;

namespace User.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IDeleteUser, DeleteUser>();
        services.AddScoped<IAddInfo, AddInfo>();
        return services;
    }
}