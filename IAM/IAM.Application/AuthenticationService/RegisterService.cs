using IAM.Domain;

namespace IAM.Application.AuthenticationService;

public class RegisterService : IRegisterService
{
    
    
    public AuthResult Handle(User user)
    {
        // check if user exists
    
        // create user
        
        // add user to database
        
        // create token
        String token = Guid.NewGuid().ToString();
        // return newly created user
        return new AuthResult(user, token);
    }
    
}