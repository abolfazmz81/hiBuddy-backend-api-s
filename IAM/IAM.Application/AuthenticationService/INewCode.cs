using IAM.Contracts.Authentication;

namespace IAM.Application.AuthenticationService;

public interface INewCode
{
    Task Generate(PhoneAuth phoneAuth);
}