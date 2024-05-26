using IAM.Application.common;

namespace IAM.Application.AuthenticationService;

public class TokenCheck : ITokenCheck
{
    private readonly IJwtGenerator _jwtGenerator;

    public TokenCheck(IJwtGenerator jwtGenerator)
    {
        _jwtGenerator = jwtGenerator;
    }

    public async Task<string?> Handle(string token)
    {
        String? username = _jwtGenerator.GetUsername(token);
        if (username is null)
        {
            return null;
        }
        return username;
    }
}