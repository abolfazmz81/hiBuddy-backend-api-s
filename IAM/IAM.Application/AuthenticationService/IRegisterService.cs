using IAM.Contracts.Authentication;
using IAM.Domain;

namespace IAM.Application.AuthenticationService;

public interface IRegisterService
{
    Task<AuthResult?> Handle(SignupAllDetails details);
    
}