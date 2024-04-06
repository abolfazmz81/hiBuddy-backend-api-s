using IAM.Application.AuthenticationService;

namespace IAM.Application.UserManagement;

public interface IUserManagement
{
    String DeleteUser(AuthResult details);
    AuthResult? UpdateUser(AuthResult details);
}