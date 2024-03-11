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
using NRedisStack;
using NRedisStack.RedisStackCommands;
using StackExchange.Redis;

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

            DateTime newDate = DateTime.Now;
            
            Random random = new Random();
            int rand = random.Next(10000000,99999999);
            
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
            IDatabase db = redis.GetDatabase();
            db.StringSet(auth.phone_number.ToString(),rand);
            Console.WriteLine(rand);
            Console.WriteLine("db");
            Console.WriteLine(db.StringGet(auth.phone_number.ToString()));
            Console.WriteLine(newDate.ToString());
            return Ok("text generated");
        }

        [HttpPut]
        public async Task<IActionResult> twoStep(phoneAuth auth)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
            IDatabase db = redis.GetDatabase();
            String? check = db.StringGet(auth.phone_number.ToString());
            if (check == null)
            {
                return BadRequest("wrong number");
            }

            if (check.Equals(auth.pass))
            {
                return Ok("successful");
            }

            return BadRequest("wrong code");
        }
    }
}

