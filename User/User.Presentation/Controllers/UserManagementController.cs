using System.Collections;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using User.Application.UserManagement;
using User.Contracts;
using User.Domain;

namespace User.Presentation.Controllers;

[ApiController]
[Produces("application/json")]
[Route("UserManagement")]
public class UserManagementController : ControllerBase
{
    
    private readonly HttpClient _httpClient;
    private readonly IDeleteUser _deleteUser;
    private readonly IAddInfo _addInfo;
    private readonly IAddDetails _addDetails;
    private readonly IGetNear _getNear;
    private readonly IConfiguration _configuration;
    private string checkUrl = "http://localhost:5000/auth/CheckToken";
    public UserManagementController(HttpClient httpClient, IDeleteUser deleteUser, IAddInfo addInfo, IAddDetails addDetails, IGetNear getNear, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _deleteUser = deleteUser;
        _addInfo = addInfo;
        _addDetails = addDetails;
        _getNear = getNear;
        _configuration = configuration;
    }
    // delete user
    [HttpDelete("delete")]
    [Authorize]
    public async Task<ActionResult> DeleteUser([FromBody]String password)
    {
        if (password == "")
        {
            return BadRequest("no password provided");
        }
        //extract token from header
        string? token = HttpContext.Request.Headers.Authorization;
        token = token.Split(" ")[1];

        token = await CheckToken(token);
        if (token.Equals("invalid token provided"))
        {
            return BadRequest("invalid token provided");
        }

        if (token.Equals("wrong token"))
        {
            return BadRequest("wrong token");
        }

        String result = await _deleteUser.delete(token,password);
        if (result.Equals("not exists"))
        {
            return BadRequest("user with this token doesnt exists");
        }
        if (result.Equals("wrong password"))
        {
            return BadRequest("password is incorrect");
        }
        // send message via RabbitMQ
        var factory = new ConnectionFactory()
        {
            HostName = _configuration["RabbitMQ:Host"]
        };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();
        channel.QueueDeclare(queue: _configuration["RabbitMQ:Queue"],
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        var message = Encoding.UTF8.GetBytes("user " + token + " deleted");
        channel.BasicPublish(exchange: "",
            routingKey: _configuration["RabbitMQ:Queue"],
            basicProperties: null,
            body: message);
        return Ok("");
    }

    // add additional info
    [HttpPut("AddInfo")]
    [Authorize]
    public async Task<ActionResult> AddInfo(Info info)
    {
        //extract token from header
        string? token = HttpContext.Request.Headers.Authorization;
        token = token.Split(" ")[1];

        token = await CheckToken(token);
        if (token.Equals("invalid token provided"))
        {
            return BadRequest("invalid token provided");
        }
        if (token.Equals("wrong token"))
        {
            return BadRequest("wrong token");
        }
        
        Domain.User? result = await _addInfo.addInfo(token,info);
        if (result is null)
        {
            return BadRequest("user with this token doesnt exists");
        }
        return Ok(result);
    }
    
    // add important details
    [HttpPut("AddDetails")]
    [Authorize]
    public async Task<ActionResult> AddDetails(Additional info)
    {
        //extract token from header
        string? token = HttpContext.Request.Headers.Authorization;
        token = token.Split(" ")[1];

        token = await CheckToken(token);
        if (token.Equals("invalid token provided"))
        {
            return BadRequest("invalid token provided");
        }
        if (token.Equals("wrong token"))
        {
            return NotFound("wrong token");
        }
        Domain.User res = await _addDetails.addDetails(token,info);
        if (res is null)
        {
            return NotFound("user with this token doesnt exists");
        }
       /* if (res.user_id == -1)
        {
            return BadRequest(res);
        }*/
        return Ok(res);
    }
    
    // get all near user
    [HttpPost("GetNear")]
    [Authorize]
    public async Task<IActionResult> GetNear(locations location)
    {
        //extract token from header
        string? token = HttpContext.Request.Headers.Authorization;
        token = token.Split(" ")[1];
        // check token validation
        token = await CheckToken(token);
        if (token.Equals("invalid token provided"))
        {
            return BadRequest("invalid token provided");
        }
        if (token.Equals("wrong token"))
        {
            return NotFound("wrong token");
        }

        ArrayList? result = await _getNear.getAll(token,location);
        if (result is null)
        {
            return NotFound("user with this token doesnt exists");
        }
        return Ok(result);
    }
    // token check function
    private async Task<String> CheckToken(String token)
    {
        try
        {
            string jsonToken = JsonConvert.SerializeObject(token);
            // create http content to send
            HttpContent content = new StringContent(jsonToken, Encoding.UTF8, "application/json");
            // send request using post
            HttpResponseMessage response = await _httpClient.PostAsync(checkUrl, content);
            
            if (!response.IsSuccessStatusCode)
            {
                return "invalid token provided";
            }

            token = await response.Content.ReadAsStringAsync();
            token = token.Remove(0, 1).Remove(token.Length - 2);
            return token;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return "wrong token";
        }
    }
}