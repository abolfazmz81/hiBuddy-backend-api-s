using IAM.Domain;

namespace IAM.Application.AuthenticationService;

public interface IAuthPhoneVerify
{
    Boolean Handle(PhoneAuth phoneAuth);
}