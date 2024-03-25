using IAM.Application.common;
using IAM.Domain;

namespace IAM.Application.AuthenticationService;

public class AuthPhoneRegister : IAuthPhoneRegister
{
    private readonly IUserRepository _userRepository;
    private readonly IInMemoryRepository _inMemoryRepository;

    public AuthPhoneRegister(IUserRepository userRepository, IInMemoryRepository inMemoryRepository)
    {
        _userRepository = userRepository;
        _inMemoryRepository = inMemoryRepository;
    }

    public Boolean handle(PhoneAuth phoneAuth)
    {
        // check phone exists
    /*    if (_userRepository.GetByPhone(phoneAuth.phone_number.ToString()) is not null)
        {
            throw new Exception("user with this phone number already exists");
            return false
        }*/
        // add phone in in-memory database
        _inMemoryRepository.Add(phoneAuth.phone_number.ToString());
        // return True
        return true;
    }

    public Boolean verify(PhoneAuth phoneAuth)
    {
        Boolean check = _inMemoryRepository.Check(phoneAuth.phone_number.ToString(), phoneAuth.pass);
        return check;
    }
}