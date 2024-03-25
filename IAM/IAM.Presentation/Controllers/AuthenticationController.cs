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
    private readonly IAuthPhoneRegister _authPhoneRegister;

    public AuthenticationController(IRegisterService registerService, IAuthPhoneRegister authPhoneRegister)
    {
        _registerService = registerService;
        _authPhoneRegister = authPhoneRegister;
    }
    
    
    [HttpPost("register")]
    public ActionResult Register(PhoneAuth phoneAuth)
    {
        Boolean result = _authPhoneRegister.handle(phoneAuth);
        if (!result)
        {
            return BadRequest("phone number already exists");
        }
        return Ok("code generated");
    }

    [HttpPut("TwoStep")]
    public ActionResult TwoStep()
    {
        return Ok();
    }
    
    
    [HttpPost("AddUser")]
    public ActionResult AddUser(User user)
    {
        var result = _registerService.Handle(user);
        if (result is null)
        {
            return BadRequest("user already exists");
        }
        return Ok(result);
    }
}