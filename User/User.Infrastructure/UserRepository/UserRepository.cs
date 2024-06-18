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
}