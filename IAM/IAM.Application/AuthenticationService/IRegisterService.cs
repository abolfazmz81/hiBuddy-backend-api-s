using IAM.Domain;

namespace IAM.Application.AuthenticationService;

public interface IRegisterService
{
    AuthResult Handle(User user);
}