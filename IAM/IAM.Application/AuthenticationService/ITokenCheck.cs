namespace IAM.Application.AuthenticationService;

public interface ITokenCheck
{
    public Task<String?> Handle(String token);
}