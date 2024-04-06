namespace IAM.Application.AuthenticationService;

public interface ILoginService
{
    AuthResult? Handle(LoginDetails loginDetails);
}