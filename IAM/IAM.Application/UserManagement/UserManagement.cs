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

    public Boolean DeleteUser(AuthResult details)
    {
        // check if user exists
        User? user = _userRepository.GetByUsername(details.User.username);
        if (user is null)
        {
            return false;
        }
        // check if password is provided
        if (details.User.password is null)
        {
            return false;
        }
        // check if pass is correct
        if (user.password.Equals(_hasher.Hash(details.User.password)))
        {
            return true;
        }

        return false;
    }

    public AuthResult? UpdateUser(AuthResult details)
    {
        throw new NotImplementedException();
    }
}