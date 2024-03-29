using IAM.Application.common;
using IAM.Domain;

namespace IAM.Application.AuthenticationService;

public class LoginService: ILoginService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IHasher _hasher;

    public LoginService(IUserRepository userRepository, IJwtGenerator jwtGenerator, IHasher hasher)
    {
        _userRepository = userRepository;
        _jwtGenerator = jwtGenerator;
        _hasher = hasher;
    }
    public AuthResult? Handle(LoginDetails loginDetails)
    {
            // check if user exists
            User? user = _userRepository.GetByEmail(loginDetails.email);
            if (user is null)
            {
                return null;
            }
            // ceck if the password is correct
            if (!user.password.Equals(_hasher.Hash(loginDetails.pass)))
            {
                return new AuthResult(new User(), "incorrect");
            }
            // generate token
            String token = _jwtGenerator.Generate(user, "auth/login", "Login");
            // return token and user
            user.password = "none";
            return new AuthResult(user, token);
        
    }
}