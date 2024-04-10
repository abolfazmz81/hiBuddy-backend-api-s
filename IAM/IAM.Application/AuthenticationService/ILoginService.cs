namespace IAM.Application.AuthenticationService;

public interface ILoginService
{
    Task<AuthResult?> Handle(LoginDetails loginDetails);
}