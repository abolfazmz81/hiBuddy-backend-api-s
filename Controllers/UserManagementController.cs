using System.Security.Cryptography;
using System.Text;
using hiBuddy.Data;
using hiBuddy.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace hiBuddy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserManagementController : ControllerBase
    {
        private readonly HiBuddyContext _context;

        public UserManagementController(HiBuddyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.Hibuddy_user.FindAsync(id);

            if (user == null)
            {
                return BadRequest("No such id exists");
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserManagement user)
        {
            if (_context.Hibuddy_user.Any(u => u.username == user.username))
            {
                return BadRequest("User with this username already exists!");
            }

            SHA256 sha256 = SHA256.Create();
            byte[] codeBytes = Encoding.UTF8.GetBytes(user.user_password);
            byte[] hasBytes = sha256.ComputeHash(codeBytes);
            StringBuilder hashed = new StringBuilder();
            for (int i = 0; i < hasBytes.Length; i++)
            {
                hashed.Append(hasBytes[i].ToString("X2"));
            }
            user.user_password = hashed.ToString();

            _context.Hibuddy_user.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Inserted successfully");
        }
        
        [HttpDelete]
        public async Task<IActionResult> DelUser(int id)
        {
            var user = await _context.Hibuddy_user.FindAsync(id);

            if (user == null)
            {
                return BadRequest("No such id exists");
            }

            _context.Hibuddy_user.Remove(user);
            await _context.SaveChangesAsync();

            return Ok("Row deleted successfully");
        }

        [HttpPut]
        public async Task<IActionResult> EditUser(UserManagement user)
        {
            
            var dbUser = await _context.Hibuddy_user.FindAsync(user.user_id);
            if (dbUser == null)
            {
                return NotFound("User not found");
            }
            if (_context.Hibuddy_user.Any(u => u.username == user.username && u.user_id != user.user_id))
            {
                return BadRequest("The chosen username already exists");
            }

            

            dbUser.username = user.username;
            dbUser.profile_name = user.profile_name;
            dbUser.pic = user.pic;
            dbUser.education = user.education;
            dbUser.job = user.job;
            
            SHA256 sha256 = SHA256.Create();
            byte[] codeBytes = Encoding.UTF8.GetBytes(user.user_password);
            byte[] hasBytes = sha256.ComputeHash(codeBytes);
            StringBuilder hashed = new StringBuilder();
            for (int i = 0; i < hasBytes.Length; i++)
            {
                hashed.Append(hasBytes[i].ToString("X2"));
            }
            dbUser.user_password = hashed.ToString();
            
            _context.Entry(dbUser).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HibuddyUserExists(dbUser.user_id))
                {
                    return NotFound("User not found");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Updated successfully");
        }

        private bool HibuddyUserExists(int id)
        {
            return _context.Hibuddy_user.Any(e => e.user_id == id);
        }
    }
}