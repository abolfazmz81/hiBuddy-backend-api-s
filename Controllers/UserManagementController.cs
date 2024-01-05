using System.Security.Cryptography;
using System.Text;
using hiBuddy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace hiBuddy.Controllers;

[ApiController]
[Route("[controller]")]
public class UserManagementController : ControllerBase
{
    private String connectionstring = "data source=DESKTOP-QN1UJLM;initial catalog=HiBuddy;trusted_connection=true;TrustServerCertificate=True";
    
    [HttpGet]
    public IActionResult GetUser(int id)
    {
        UserManagement user = new UserManagement();
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("select * from Hibuddy_user where user_id = @p1 ",connection)
                )
                {
                    cmd.Parameters.AddWithValue("@p1", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        
                        
                        user.user_id = reader.GetInt32(0);
                        
                        user.username = reader.GetString(1);
                        
                        user.profilename = reader.GetString(2);
                        
                        user.job = reader.GetString(5);
                        
                        
                        user.education = reader.GetString(6);
                        connection.Close();
                        return Ok(user);
                    }
                }

                
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return BadRequest("no such id exists");
            throw;
            
        }
        
    }

    [HttpPost]
    public IActionResult AddUser(String username,String profile_name,String pic,String user_password,String job,String education)
    {
        UserManagement user = new UserManagement();
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("insert into Hibuddy_user (username,profile_name,pic,user_password,job,education) values (@username, @profile_name,NULL,@pass,@job,@education); ",connection)
                      )
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@profile_name", profile_name);
                    SHA256 sha256 = SHA256.Create();
                    
                    Byte[] codeBytes = Encoding.UTF8.GetBytes(user_password);
                    Byte[] hasBytes = sha256.ComputeHash(codeBytes);
                    StringBuilder hashed = new StringBuilder();
                    for (int i = 0; i < hasBytes.Length; i++)
                    {
                        hashed.Append(hasBytes[i].ToString("X2"));
                    }
                    cmd.Parameters.AddWithValue("@pass", hashed.ToString());
                    cmd.Parameters.AddWithValue("@job", job);
                    cmd.Parameters.AddWithValue("@education", education);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        return Ok("inserted successfully");
                    }
                    catch (Exception e)
                    {
                        
                        //Console.WriteLine(e);
                        return BadRequest("user with this username already exists!");
                        throw;
                    }
                }
                
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return BadRequest("no such id exists");
            throw;
            
        }
    }
    
    [HttpDelete]
    public IActionResult DelUser(int id)
    {
        UserManagement user = new UserManagement();
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("delete from Hibuddy_user where user_id = @p1 ",connection)
                      )
                {
                    cmd.Parameters.AddWithValue("@p1", id);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        return Ok("row deleted successfully");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        return BadRequest("the row didnt exist");
                        throw;
                    }
                }

                
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return BadRequest("no such id exists");
            throw;
            
        }
        
    }
    
    [HttpPut]
    public IActionResult editUser(int id,String username,String profile_name,String pic,String user_password,String job,String education)
    {
        UserManagement user = new UserManagement();
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("update Hibuddy_user Set username=@username,profile_name=@profile_name,pic=NULL,user_password= @pass,job=@job,education=@education where user_id = @id; ",connection)
                      )
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@profile_name", profile_name);
                    SHA256 sha256 = SHA256.Create();
                    
                    Byte[] codeBytes = Encoding.UTF8.GetBytes(user_password);
                    Byte[] hasBytes = sha256.ComputeHash(codeBytes);
                    StringBuilder hashed = new StringBuilder();
                    for (int i = 0; i < hasBytes.Length; i++)
                    {
                        hashed.Append(hasBytes[i].ToString("X2"));
                    }
                    cmd.Parameters.AddWithValue("@pass", hashed.ToString());
                    cmd.Parameters.AddWithValue("@job", job);
                    cmd.Parameters.AddWithValue("@education", education);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        return Ok("inserted successfully");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        return BadRequest("the chosen username already exists");
                        throw;
                    }
                }
                
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return BadRequest("no such id exists");
            throw;
            
        }
    }
    
}