using System.IdentityModel.Tokens.Jwt;
using IAM.Application.AuthenticationService;
using IAM.Application.common;
using IAM.Domain;

namespace IAM.Application.UserManagement;

public class UserManagement : IUserManagement
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IHasher _hasher;

    public UserManagement(IUserRepository userRepository, IJwtGenerator jwtGenerator, IHasher hasher)
    {
        _userRepository = userRepository;
        _jwtGenerator = jwtGenerator;
        _hasher = hasher;
    }

    public String DeleteUser(AuthResult details)
    {
        // check if user exists
        User? user = _userRepository.GetByUsername(details.User.username).Result;
        if (user is null)
        {
            return "not exists";
        }
        // check token calidation
        if (user.phone_number.Equals(_userRepository.GetByUsername(_jwtGenerator.GetUsername(details.Token)).Result.phone_number))
        {
            return "invalid token";
        }
        // check if password is provided
        if (details.User.password is null)
        {
            return "no password";
        }
        // check if pass is correct
        if (user.password.Equals(_hasher.Hash(details.User.password)))
        {
            return "ok";
        }

        return "wrong password";
    }

    public AuthResult? UpdateUser(AuthResult details)
    {
        throw new NotImplementedException();
    }
}