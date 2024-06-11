using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using User.Application.UserManagement;

namespace User.Presentation.Controllers;

[ApiController]
[Produces("application/json")]
[Route("UserManagement")]
public class UserManagementController : ControllerBase
{
    
    private readonly HttpClient _httpClient;
    private readonly IDeleteUser _deleteUser;
    private string checkUrl = "http://localhost:5000/auth/CheckToken";

    [HttpDelete("delete")]
    public async Task<ActionResult> DeleteUser(String token)
    {
        
    [Authorize]
    public async Task<ActionResult> DeleteUser()
    {
        string? token = HttpContext.Request.Headers.Authorization;
        token = token.Split(" ")[1];
        
        try
        {
            string jsonToken = JsonConvert.SerializeObject(token);
            // create http content to send
            HttpContent content = new StringContent(jsonToken, Encoding.UTF8, "application/json");
            // send request using post
            HttpResponseMessage response = await _httpClient.PostAsync(checkUrl, content);
            
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("invalid token provided");
            }

            token = await response.Content.ReadAsStringAsync();
            token = token.Remove(0, 1).Remove(token.Length - 2);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("wrong token");
        }
        return Ok("");
    }
}