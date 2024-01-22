using System.Security.Cryptography;
using System.Text;
using hiBuddy.Data;
using hiBuddy.models;
using hiBuddy.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
namespace hiBuddy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly HiBuddyContext _context;

        public LogsController(HiBuddyContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Login(Logs login)
        {
            
            var user = _context.Hibuddy_user.FromSqlRaw("select * from Hibuddy_user where email = '" + login.email+ "';");
            
            if (user.ToList().Count == 0)
            {
                return NotFound("username doesnt exists");
            }
            SHA256 sha256 = SHA256.Create();
            byte[] codeBytes = Encoding.UTF8.GetBytes(login.password);
            byte[] hasBytes = sha256.ComputeHash(codeBytes);
            StringBuilder hashed = new StringBuilder();
            for (int i = 0; i < hasBytes.Length; i++)
            {
                hashed.Append(hasBytes[i].ToString("X2"));
            }
            login.password = hashed.ToString();
            UserManagement suser = user.ToList()[0];
            
            if (login.password.Equals(user.ToList()[0].password))
            {
                var userloc = _context.user_locations.FromSqlRaw("select * from user_locations where user_id = " + suser.user_id);
                userLocationCompositeModel compositeModel = new userLocationCompositeModel();
                if (userloc.ToList().Count != 0){
                    locations loc = await _context.locations.FindAsync(userloc.ToList()[0].location_id);                    
                    compositeModel.Locations = loc;                
                }
                compositeModel.User = suser;
                return Ok(compositeModel);
            }

            return BadRequest("incorrect password");
        }

        
    }
}

