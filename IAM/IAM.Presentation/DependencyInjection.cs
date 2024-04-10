using System.Text;
using IAM.Infrastructure.JwtGenerators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace IAM.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddAuth();
        return services;
    }

    private static IServiceCollection AddAuth(this IServiceCollection services)
    {
        var jwtSettings = new JwtSettings
        {
            Secret = "HiBuddy_users_top_secret_key_ggs",
            Issuer = "http://localhost:5000",
            Audience = "http://localhost:5000",
            ExpiryMinutes = 120
        };
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("HiBuddy_users_top_secret_key_ggs"))
                });
        return services;
    }
}