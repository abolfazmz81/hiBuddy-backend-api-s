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
using NuGet.Protocol;


namespace hiBuddy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class UserManagementController : ControllerBase
    {
        private readonly HiBuddyContext _context;

        public UserManagementController(HiBuddyContext context)
        {
            _context = context;
        }
        /// <summary>
        /// find and sends back all the user near the input user
        /// </summary>
        /// <returns> all near users </returns>
        /// <response code="200"> found the users </response>
        [Produces("application/json")]
        [HttpGet]
        public async Task<IActionResult> GetNearUser(userLocationCompositeModel compositeModel)
        {
            UserManagement user = compositeModel.User;
            //.WriteLine("here");
           // Console.WriteLine(user);
            var allusers = _context.user_locations.FromSqlRaw("with us(id,ax,ay) as (select Hibuddy_user.user_id,x,y from Hibuddy_user join user_locations on Hibuddy_user.user_id = user_locations.user_id join locations on user_locations.location_id = locations.location_id where Hibuddy_user.user_id = "+user.user_id+ ")\nselect * from user_locations as t where t.user_id in (select s.user_id from us, Hibuddy_user as s join user_locations on s.user_id = user_locations.user_id join locations on user_locations.location_id = locations.location_id where s.user_id != us.id and SQRT(POWER(x-ax,2) + POWER(y-ay,2)) <= 0.025)  \n");
            //Console.WriteLine("here 2");
            ArrayList all = new ArrayList();
            
            foreach (var x in allusers.ToList())
            {
                //Console.WriteLine(x.ToJson());
                UserManagement a = await _context.Hibuddy_user.FindAsync(x.user_id);
                locations b = await _context.locations.FindAsync(x.location_id);
                userLocationCompositeModel c = new userLocationCompositeModel();
                a.user_password = "none";
                c.User = a;
                c.Locations = b;
                all.Add(c);
            }

            return Ok(all);
        }
        
        /// <summary>
        /// used for sign up in our website
        /// </summary>
        /// <return> information that got stored </return>
        /// <response code="200"> successfull signup </response>
        /// <response code="400"> user or email already being used</response>
        [Produces("application/json")]
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

            compositeModel.User = user;
            compositeModel.Locations = loc;
            return Ok(compositeModel);
        }
        
        /// <summary>
        /// used for deleting your account
        /// </summary>
        /// <response code="200"> successfull delete </response>
        /// <response code="404"> given user doent exits</response>
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
        
        /// <summary>
        /// used for editing users information
        /// </summary>
        /// <response code="200"> information successfully changed </response>
        /// <response code="404"> given user doent exits</response>
        /// <response code="400"> the chosen username already exist and its not yours</response>
        [Produces("application/json")]
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
                    if (!_context.locations.Any(u => u.location_id == local.location_id))
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
            var res = GetNearUser(compositeModel);
            var res1 = (OkObjectResult)res.Result;
            var res2 = res1.Value;
            compositeModel.OtherLocation = (ArrayList)res2;

            return Ok(compositeModel);
        }
        
    }
}