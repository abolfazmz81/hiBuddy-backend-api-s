using IAM.Application.common;
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

    public AuthResult? Login(LoginDetails loginDetails)
    {
        // check if user exists
        User? user = _userRepository.GetByEmail(loginDetails.email);
        if (user is null)
        {
            return null;
        }
        // ceck if the password is correct
        if (user.password.Equals(_hasher.Hash(loginDetails.pass)))
        {
            return new AuthResult(new User(), "incorrect");
        }
        // generate token
        String token = _jwtGenerator.Generate(user, "auth/login", "Login");
        // return token and user
        return new AuthResult(user, token);
    }
}