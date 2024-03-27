using IAM.Application.common;
using IAM.Domain;

namespace IAM.Application.AuthenticationService;

public class RegisterService : IRegisterService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtGenerator _jwtGenerator;

    public RegisterService(IUserRepository userRepository, IJwtGenerator jwtGenerator)
    {
        _userRepository = userRepository;
        _jwtGenerator = jwtGenerator;
    }

    public AuthResult? Handle(User user)
    {
        // check if user exists
        // check with username
        if (_userRepository.GetByUsername(user.username) is not null)
        {
            return new AuthResult(new User(), "username");
            //throw new Exception("user with this username already exists");
        }
        // check with email
        if (_userRepository.GetByEmail(user.email) is not null)
        {
            return new AuthResult(new User(), "email");
            //throw new Exception("user with this email address already exists");
        }
        // create user
        
        // add user to database
        var newUser = _userRepository.Add(user);
        // create token
        String token = _jwtGenerator.Generate(user,"auth/Register","IAM");
        // return newly created user
        return new AuthResult(newUser, token);
    }
    
}