using IAM.Application.common;
using IAM.Domain;

namespace IAM.Application.AuthenticationService;

public class RegisterService : IRegisterService
{
    private readonly IUserRepository _userRepository;

    public RegisterService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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
        String token = Guid.NewGuid().ToString();
        // return newly created user
        return new AuthResult(newUser, token);
    }
    
}