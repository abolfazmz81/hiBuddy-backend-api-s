using IAM.Contracts.Authentication;
using IAM.Domain;

namespace IAM.Application.AuthenticationService;

public interface IRegisterService
{
    AuthResult? Handle(SignupAllDetails details);
    AuthResult? Login(LoginDetails loginDetails);
}