using System.Collections;
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
    [Produces("application/json")]
    public class phoneAuthController : ControllerBase
    {
        private readonly HiBuddyContext _context;

        public phoneAuthController(HiBuddyContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> register(phoneAuth auth)
        {
            if (_context.Hibuddy_user.Any(u => u.phone_number == auth.phone_number))
            {
                return BadRequest("User with this phone_number already exists!");
            }
            
            return Ok("text generated");
        }
    }
}

