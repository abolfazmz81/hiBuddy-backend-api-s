using IAM.Application.common;
using IAM.Domain;

namespace IAM.Application.AuthenticationService;

public class AuthPhoneVerify : IAuthPhoneVerify
{
    private readonly IInMemoryRepository _inMemoryRepository;

    public AuthPhoneVerify(IInMemoryRepository inMemoryRepository)
    {
        _inMemoryRepository = inMemoryRepository;
    }

    public bool Handle(PhoneAuth phoneAuth)
    {
        String? result = _inMemoryRepository.Get(phoneAuth.phone_number.ToString());
        //check if code exists
        if (result is null)
        {
            throw new Exception("phone number expired, get new code");
        }
        //check if code is correct
        else if (!result.Equals(phoneAuth.pass))
        {
            throw new Exception("wrong code");
        }
        return true;
    }
}