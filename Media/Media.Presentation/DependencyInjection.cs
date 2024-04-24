using System.Text;
using Media.Infrastructure.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Media.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
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
            ExpiryMinutes = 60*24*15
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                });
        return services;
    }
}