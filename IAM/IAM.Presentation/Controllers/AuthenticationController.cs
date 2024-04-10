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
    private readonly ILoginService _loginService;
    private readonly IAuthPhoneVerify _authPhoneVerify;

    public AuthenticationController(IRegisterService registerService, ILoginService loginService, IAuthPhoneVerify authPhoneVerify)
    {
        _registerService = registerService;
        _loginService = loginService;
        _authPhoneVerify = authPhoneVerify;
    }
    

    [HttpPut("TwoStep")]
    public async Task<ActionResult> TwoStep(PhoneAuth phoneAuth)
    {
        if (phoneAuth.pass is null)
        {
            return BadRequest("didnt send the code");
        }
        Boolean result =await _authPhoneVerify.Handle(phoneAuth);
        if (!result)
        {
            return BadRequest("wrong code");
        }
        return Ok("phone verified");
    }
    
    // extra information related
    [HttpPost("register")]
    public async Task<ActionResult> Register(SignupAllDetails user)
    {
        var result =await _registerService.Handle(user);
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
        if (result.Token.Equals("phone"))
        {
            return BadRequest("user with this phone number already exits");
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