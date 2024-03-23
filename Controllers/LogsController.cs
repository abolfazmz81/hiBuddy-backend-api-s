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
    public class LogsController : ControllerBase
    {
        private readonly HiBuddyContext _context;

        public LogsController(HiBuddyContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// used for login
        /// </summary>
        /// <returns> information for logged user </returns>
        /// <response code="200"> correct email and password </response>
        /// <response code="404"> given email doesnt exist </response>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Logs
        ///     {
        ///        "email": "example@gmail.com",
        ///        "password": "goodpassword"
        ///     }
        ///
        /// </remarks> 
        [HttpPost]
        public async Task<IActionResult> Login(Logs login)
        {
            
            var user = _context.Hibuddy_user.FromSqlRaw("select * from Hibuddy_user where email = '" + login.email+ "';");
            
            if (user.ToList().Count == 0)
            {
                return NotFound("email doesnt exists");
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
        
        /// <summary>
        /// returns all available users
        /// </summary>
        /// <returns> information for all users </returns>
        /// <response code="200"> no internal error </response>
        [HttpPut]
        public async Task<IActionResult> getAllUser()
        {
            var users = _context.Hibuddy_user.FromSqlRaw("select * from Hibuddy_user;");
            ArrayList all = new ArrayList();
            foreach (var user in users)
            {
                userLocationCompositeModel compositeModel = new userLocationCompositeModel();
                compositeModel.User = user;
                all.Add(compositeModel);
            }
            
            return Ok(all);

        }

        
    }
}

