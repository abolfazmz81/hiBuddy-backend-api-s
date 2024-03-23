using Microsoft.AspNetCore.Mvc;

namespace IAM.Presentation.Controllers;


[ApiController]
[Produces("application/json")]
[Route("auth")]
public class Authentication : ControllerBase
{

    [HttpPost("register")]
    public ActionResult Register()
    {
        return Ok();
    }

    [HttpPut("TwoStep")]
    public ActionResult TwoStep()
    {
        return Ok();
    }
}