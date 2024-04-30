using System.IdentityModel.Tokens.Jwt;

namespace Media.Infrastructure.JWT;
using Media.Application.Common;
public class JWTChecker : IJWTChecker
{
    public string? get_Username(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        try
        {
            var tokens = handler.ReadJwtToken(token);
            if (tokens is null)
            {
                return null;
            }

            var res = tokens.Claims.First(c => c.Type.Equals("name")).Value;
            return res;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}