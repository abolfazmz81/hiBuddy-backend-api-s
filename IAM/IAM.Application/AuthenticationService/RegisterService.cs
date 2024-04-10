using IAM.Application.common;
using IAM.Contracts.Authentication;
using IAM.Domain;

namespace IAM.Application.AuthenticationService;

public class RegisterService : IRegisterService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IHasher _hasher;
    private readonly IInMemoryRepository _inMemoryRepository;
    private readonly ICodeGenerator _codeGenerator;

    public RegisterService(IUserRepository userRepository, IJwtGenerator jwtGenerator, IHasher hasher, IInMemoryRepository inMemoryRepository, ICodeGenerator codeGenerator)
    {
        _userRepository = userRepository;
        _jwtGenerator = jwtGenerator;
        _hasher = hasher;
        _inMemoryRepository = inMemoryRepository;
        _codeGenerator = codeGenerator;
    }

    public async Task<AuthResult?> Handle(SignupAllDetails details)
    {
        // check if user exists
        // check with username
        if (await _userRepository.GetByUsername(details.username) is not null)
        {
            return new AuthResult(new User(), "username");
            //throw new Exception("user with this username already exists");
        }
        // check with email
        if (await _userRepository.GetByEmail(details.email) is not null)
        {
            return new AuthResult(new User(), "email");
            //throw new Exception("user with this email address already exists");
        }
        // check phone exists
        if (await _userRepository.GetByPhone(details.phone_number) is not null)
        {
            //throw new Exception("user with this phone number already exists");
            return new AuthResult(new User(), "phone");
        }
        // generate random code
        String code = _codeGenerator.Generator();
        // add phone in in-memory database
        await _inMemoryRepository.Add(details.phone_number.ToString(),code);
        // get last id
        int last = await _userRepository.GetLastId();
        // create user
        var user = User.Create(last+1, details.username, details.name, details.email, _hasher.Hash(details.password), details.phone_number);
        // add user to database
        await _userRepository.Add(user);
        // create token
        String token = _jwtGenerator.Generate(user,"auth/Register","IAM");
        // return newly created user
        return new AuthResult(user, token);
    }

    
}