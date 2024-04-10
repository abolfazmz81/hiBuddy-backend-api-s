using IAM.Application.common;
using IAM.Contracts.Authentication;
using IAM.Domain;

namespace IAM.Application.AuthenticationService;

public class NewCode : INewCode
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IInMemoryRepository _inMemoryRepository;
    private readonly ICodeGenerator _codeGenerator;
    private readonly IUserRepository _userRepository;

    public NewCode( IJwtGenerator jwtGenerator, IInMemoryRepository inMemoryRepository, ICodeGenerator codeGenerator, IUserRepository userRepository)
    {
        _jwtGenerator = jwtGenerator;
        _inMemoryRepository = inMemoryRepository;
        _codeGenerator = codeGenerator;
        _userRepository = userRepository;
    }

    public async Task Generate(PhoneAuth phoneAuth)
    {
        // extract username from token
        String? username = _jwtGenerator.GetUsername(phoneAuth.token);
        // check if token or phone number is valid
        if (username is null)
        {
            throw new Exception("token is fake");
        }
        User? user = await _userRepository.GetByUsername(username);
        if (user is null)
        {
            throw new Exception("user doesnt exist");
        }
        // generate new code
        String code = _codeGenerator.Generator();
        // add phone in in-memory database
        await _inMemoryRepository.Add(user.phone_number.ToString(), code);
        
    }
}