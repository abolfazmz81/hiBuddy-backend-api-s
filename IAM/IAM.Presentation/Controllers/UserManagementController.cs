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
        String result = _userManagement.DeleteUser(details);
        if (result.Equals("not exists"))
        {
            return BadRequest("user with this email doesnt exists");
        }
        if (result.Equals("invalid token"))
        {
            return BadRequest("token provided is not valid, login");
        }
        if (result.Equals("no password"))
        {
            return BadRequest("no password provided");
        }
        if (result.Equals("wrong password"))
        {
            return BadRequest("password is incorrect");
        }
        return Ok("user deleted successfully");
    }
}