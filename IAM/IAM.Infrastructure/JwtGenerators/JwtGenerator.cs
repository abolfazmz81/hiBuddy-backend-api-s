using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IAM.Application.common;
using IAM.Domain;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
namespace IAM.Infrastructure.JwtGenerators;

public class JwtGenerator : IJwtGenerator
{
    public string Generate(User user,String issuer,String audience)
    {
        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sid,user.user_id.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub,user.username),
            new Claim(JwtRegisteredClaimNames.UniqueName,user.phone_number.ToString())

        };

        var signing = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("HiBuddy_users_top_secret_key_ggs"))
            ,SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            issuer:issuer,
            audience:audience,
            claims:claims,
            null,
            DateTime.Now.AddMinutes(30),
            signingCredentials:signing);

        String tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenValue;
    }

    public string GetUsername(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var tokens = handler.ReadJwtToken(token);
        if (tokens is null)
        {
            return null;
        }
        var res = tokens.Claims.First(c => c.Type.Equals("sub")).Value;
        return res;
    }
}