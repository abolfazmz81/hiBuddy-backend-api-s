using System.Security.Cryptography;
using System.Text;
using hiBuddy.Data;
using hiBuddy.models;
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
                return NotFound("No such id exists");
            }

            return Ok(user);
        }
        
        
        [HttpPost]
        public virtual async Task<IActionResult> AddUser(userLocationCompositeModel compositeModel)
        {
            UserManagement user = compositeModel.User;
            locations loc = compositeModel.Locations;
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

            return Ok(user);
        }
        
        [HttpDelete]
        public async Task<IActionResult> DelUser(userLocationCompositeModel compositeModel)
        {
            UserManagement user = compositeModel.User;

            if (user == null)
            {
                    return NotFound("No such id exists");
            }

            locations loc = compositeModel.Locations;
            if (loc !=null)
            {
                _context.locations.Remove(loc);
                await _context.SaveChangesAsync();
                userloc aa = new userloc();
                aa.user_id = user.user_id;
                aa.location_id = loc.location_id;
                _context.user_locations.Remove(aa);
                await _context.SaveChangesAsync();
            }
            _context.Hibuddy_user.Remove(user);
            await _context.SaveChangesAsync();
            
            return Ok("Row deleted successfully");
        }

        [HttpPut]
        public virtual async Task<IActionResult> EditUser(userLocationCompositeModel compositeModel)
        {
            UserManagement user = compositeModel.User;
            locations local = compositeModel.Locations;
            if (!_context.Hibuddy_user.Any(u => u.user_id == user.user_id))
            {
                return NotFound("User not found");
            }
            if (_context.Hibuddy_user.Any(u => u.username == user.username && u.user_id != user.user_id))
            {
                return BadRequest("The chosen username already exists");
            }

            
            
            //UserManagement test = user;
            
            SHA256 sha256 = SHA256.Create();
            byte[] codeBytes = Encoding.UTF8.GetBytes(user.user_password);
            byte[] hasBytes = sha256.ComputeHash(codeBytes);
            StringBuilder hashed = new StringBuilder();
            for (int i = 0; i < hasBytes.Length; i++)
            {
                hashed.Append(hasBytes[i].ToString("X2"));
            }
            user.user_password = hashed.ToString();
            

            if (local != null)
            {

                if (local.location_id == 0)
                {
                    _context.locations.Add(local);
                    await _context.SaveChangesAsync();
                    userloc adder = new userloc();
                    adder.user_id = user.user_id;
                    adder.location_id = local.location_id;
                    _context.user_locations.Add(adder);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    if (_context.locations.FindAsync(local.location_id) == null)
                    {
                        return BadRequest("location id invalid");
                    }
                    _context.locations.Update(local);
                    await _context.SaveChangesAsync();

                }
            }
            _context.Hibuddy_user.Update(user);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("failed, try again");
            }

            compositeModel.User = user;
            compositeModel.Locations = local;
            return Ok(compositeModel);
        }

        public virtual bool HibuddyUserExists(int id)
        {
            return _context.Hibuddy_user.Any(e => e.user_id == id);
        }
    }
}