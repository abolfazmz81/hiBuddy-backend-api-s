using IAM.Application.AuthenticationService;

namespace IAM.Application.UserManagement;

public interface IUserManagement
{
    Boolean DeleteUser(AuthResult details);
    AuthResult? UpdateUser(AuthResult details);
}