using Media.Application.Media;
using Microsoft.Extensions.DependencyInjection;
namespace Media.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ISaveMedia, SaveMedia>();
        services.AddScoped<IGetMedia,GetMedia>();
        services.AddScoped<IDeleteMedia, DeleteMedia>();
        return services;
    }
}