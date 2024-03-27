using IAM.Application.AuthenticationService;
using IAM.Application.UserManagement;
using Microsoft.AspNetCore.Mvc;

namespace IAM.Presentation.Controllers;

[ApiController]
[Produces("application/json")]
[Route("UserManagement")]
public class UserManagementController : ControllerBase
{
    private readonly IUserManagement _userManagement;

    public UserManagementController(IUserManagement userManagement)
    {
        _userManagement = userManagement;
    }
    
    [HttpDelete("DelUser")]
    public ActionResult DeleteUser(AuthResult details)
    {
        _userManagement.DeleteUser(details);
        return Ok("user deleted successfully");
    }
}