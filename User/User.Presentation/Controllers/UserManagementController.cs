using Microsoft.AspNetCore.Mvc;

namespace User.Presentation.Controllers;

[ApiController]
[Produces("application/json")]
[Route("UserManagement")]
public class UserManagementController : ControllerBase
{

    [HttpDelete("delete")]
    public async Task<ActionResult> DeleteUser(String token)
    {
        
        return Ok("");
    }
}