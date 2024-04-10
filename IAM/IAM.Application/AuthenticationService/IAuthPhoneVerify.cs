using IAM.Domain;
using IAM.Contracts.Authentication;
namespace IAM.Application.AuthenticationService;

public interface IAuthPhoneVerify
{
    Task<bool> Handle(PhoneAuth phoneAuth);
}