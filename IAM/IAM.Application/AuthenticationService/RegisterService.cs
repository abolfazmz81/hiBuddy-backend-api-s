using IAM.Application.common;
using IAM.Contracts.Authentication;
using IAM.Domain;

namespace IAM.Application.AuthenticationService;

public class RegisterService : IRegisterService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IHasher _hasher;

    public RegisterService(IUserRepository userRepository, IJwtGenerator jwtGenerator, IHasher hasher)
    {
        _userRepository = userRepository;
        _jwtGenerator = jwtGenerator;
        _hasher = hasher;
    }

    public AuthResult? Handle(SignupAllDetails details)
    {
        // check if user exists
        // check with username
        if (_userRepository.GetByUsername(details.username) is not null)
        {
            return new AuthResult(new User(), "username");
            //throw new Exception("user with this username already exists");
        }
        // check with email
        if (_userRepository.GetByEmail(details.email) is not null)
        {
            return new AuthResult(new User(), "email");
            //throw new Exception("user with this email address already exists");
        }
        // create user
        var user = _userRepository.CreateUser(details);
        // add user to database
        var newUser = _userRepository.Add(user);
        // create token
        String token = _jwtGenerator.Generate(user,"auth/Register","IAM");
        // return newly created user
        return new AuthResult(newUser, token);
    }

    
}