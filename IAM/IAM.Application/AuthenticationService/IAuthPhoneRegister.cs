using IAM.Domain;

namespace IAM.Application.AuthenticationService;

public interface IAuthPhoneRegister
{
    Boolean handle(PhoneAuth phoneAuth);
    
}