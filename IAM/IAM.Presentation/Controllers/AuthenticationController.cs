using IAM.Application.AuthenticationService;
using IAM.Domain;
using Microsoft.AspNetCore.Mvc;

namespace IAM.Presentation.Controllers;


[ApiController]
[Produces("application/json")]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IRegisterService _registerService;

    public AuthenticationController(IRegisterService registerService)
    {
        _registerService = registerService;
    }

    [HttpPost("register")]
    public ActionResult Register(User user)
    {
        var result = _registerService.Handle(user);
        return Ok(result);
    }

    [HttpPut("TwoStep")]
    public ActionResult TwoStep()
    {
        return Ok();
    }
}