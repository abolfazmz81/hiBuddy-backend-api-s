using System.Collections;
using Microsoft.EntityFrameworkCore;
using User.Application.common;
using User.Contracts;
using User.Domain;

namespace User.Infrastructure.UserRepository;

public class UserRepository : IUserRepository
{
    private readonly SQLServerContext _context;

    public UserRepository(SQLServerContext context)
    {
        _context = context;
    }
    public async Task<Domain.User?> GetByUsername(string username)
    {
        return await _context.Hibuddy_user.SingleOrDefaultAsync(user => user.username == username);
    }

    public async Task DelUser(Domain.User user)
    {
        User_location? userLocation = await _context.user_locations.SingleOrDefaultAsync(u => u.user_id == user.user_id);
        if (userLocation is not null)
        {
            _context.user_locations.Remove(userLocation);
            await _context.SaveChangesAsync();
        }
        _context.Hibuddy_user.Remove(user);
        await _context.SaveChangesAsync();    
    }

    public async Task<Domain.User> UpdateInfo(Domain.User user, Info info)
    {
        user.education = info.education;
        user.favorites = info.favorites;
        user.hobbies = info.hobbies;
        user.job = info.job;
        _context.Hibuddy_user.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<Domain.User> UpdateDetails(Domain.User user, Additional info)
    {
        //user.username = info.username;
        user.gender = info.gender;
        user.pic = info.pic;
        user.Date = info.Date;
        user.name = info.name;
        _context.Hibuddy_user.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<ArrayList> GetNear(Domain.User user)
    {
        var allusers = _context.user_locations.FromSqlRaw("with us(id,ax,ay) as (select Hibuddy_user.user_id,x,y from Hibuddy_user join user_locations on Hibuddy_user.user_id = user_locations.user_id join locations on user_locations.location_id = locations.location_id where Hibuddy_user.user_id = "+user.user_id+ ")\nselect * from user_locations as t where t.user_id in (select s.user_id from us, Hibuddy_user as s join user_locations on s.user_id = user_locations.user_id join locations on user_locations.location_id = locations.location_id where s.user_id != us.id and SQRT(POWER(x-ax,2) + POWER(y-ay,2)) <= 5)  \n");
        //Console.WriteLine("here 2");
        ArrayList all = new ArrayList();
            
        foreach (var x in allusers.ToList())
        {
            //Console.WriteLine(x.ToJson());
            Domain.User a = await _context.Hibuddy_user.FindAsync(x.user_id);
            locations b = await _context.locations.FindAsync(x.location_id);
            userLocationCompositeModel c = new userLocationCompositeModel();
            a.password = "";
            c.User = a;
            c.Locations = b;
            all.Add(c);
        }

        return all;
    }

    public async Task Addlocation(Domain.User user, locations location)
    {
        var loc = _context.locations.Add(location).Entity;
        await _context.SaveChangesAsync();
        User_location ul = new User_location
        {
            user_id = user.user_id,
            location_id = loc.location_id
        };
        User_location? a = await _context.user_locations.SingleOrDefaultAsync(ul1 => ul1.user_id == user.user_id);
        if (a is not null)
        {
            _context.Remove(a);
        }
        _context.user_locations.Add(ul);
        await _context.SaveChangesAsync();
    }
}