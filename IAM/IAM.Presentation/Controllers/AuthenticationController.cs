using IAM.Application.AuthenticationService;
using IAM.Contracts.Authentication;
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
    private readonly ILoginService _loginService;

    public AuthenticationController(IRegisterService registerService, IAuthPhoneRegister authPhoneRegister, ILoginService loginService)
    {
        _registerService = registerService;
        _authPhoneRegister = authPhoneRegister;
        _loginService = loginService;
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
    public ActionResult AddUser(SignupAllDetails user)
    {
        var result = _registerService.Handle(user);
        if (result is null)
        {
            return BadRequest("user already exists");
        }
        if (result.Token.Equals("email"))
        {
            return BadRequest("user with this email already exits");
        }
        if (result.Token.Equals("username"))
        {
            return BadRequest("user with this username already exits");
        }
        return Ok(result);
    }

    [HttpPost("Login")]
    public ActionResult Login( LoginDetails loginDetails)
    {
        var result = _loginService.Handle(loginDetails);
        if (result is null)
        {
            return BadRequest("user doesnt exists");
        }

        if (result.Token.Equals("incorrect"))
        {
            return BadRequest("wrong password");
        }
        return Ok(result);
    }
}