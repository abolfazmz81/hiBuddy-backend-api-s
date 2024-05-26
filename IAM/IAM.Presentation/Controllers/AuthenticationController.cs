using System.IdentityModel.Tokens.Jwt;
using IAM.Application.AuthenticationService;
using IAM.Contracts.Authentication;
using IAM.Domain;
using Microsoft.AspNetCore.Authorization;
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
    private readonly INewCode _newCode;
    private readonly ITokenCheck _tokenCheck;

    public AuthenticationController(IRegisterService registerService, ILoginService loginService, IAuthPhoneVerify authPhoneVerify, INewCode newCode, ITokenCheck tokenCheck)
    {
        _registerService = registerService;
        _loginService = loginService;
        _authPhoneVerify = authPhoneVerify;
        _newCode = newCode;
        _tokenCheck = tokenCheck;
    }
    

    [HttpPut("Verify")]
    [Authorize]
    public async Task<ActionResult> Verify(PhoneAuth phoneAuth)
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
    
    [HttpPost("Register")]
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

    // extra information related
    [HttpPost("NewCode")]
    public async Task<ActionResult> NewCode(PhoneAuth phoneAuth)
    {
        await _newCode.Generate(phoneAuth);
        
        return Ok("code generated");
    }
    
    [HttpPost("Login")]
    public async Task<ActionResult >Login( LoginDetails loginDetails)
    {
        var result = await _loginService.Handle(loginDetails);
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
    
    // called by other api's
    [HttpPost("CheckToken")]
    public async Task<ActionResult> Check([FromBody] String token)
    {
        String? user = await _tokenCheck.Handle(token);
        if (user is not null)
        {
            return Ok(user);
        }
        return BadRequest();
    }
}