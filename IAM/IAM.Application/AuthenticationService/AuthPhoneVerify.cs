using IAM.Application.common;
using IAM.Domain;
using IAM.Contracts.Authentication;
namespace IAM.Application.AuthenticationService;

public class AuthPhoneVerify : IAuthPhoneVerify
{
    private readonly IInMemoryRepository _inMemoryRepository;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IUserRepository _userRepository;

    public AuthPhoneVerify(IInMemoryRepository inMemoryRepository, IJwtGenerator jwtGenerator, IUserRepository userRepository)
    {
        _inMemoryRepository = inMemoryRepository;
        _jwtGenerator = jwtGenerator;
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(PhoneAuth phoneAuth)
    {
        // extract phone number from token
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
        // get code from cache database
        String? result = await _inMemoryRepository.Get(user.phone_number.ToString());
        // check if code exists
        if (result is null)
        {
            throw new Exception("phone number expired, get new code");
        }
        // check if code is correct
        else if (!result.Equals(phoneAuth.pass))
        {
            throw new Exception("wrong code");
        }
        // veify user
        user.verify();
        await _userRepository.Update(user);
        return true;
    }
}