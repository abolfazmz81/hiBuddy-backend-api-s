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
    
    // phone number related
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
    public ActionResult TwoStep(PhoneAuth phoneAuth)
    {
        if (phoneAuth.pass is null)
        {
            return BadRequest("didnt send the code");
        }
        Boolean result = _authPhoneRegister.verify(phoneAuth);
        if (!result)
        {
            return BadRequest("wrong code");
        }
        return Ok("phone verified");
    }
    
    // extra information related
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