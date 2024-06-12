using Microsoft.EntityFrameworkCore;
using User.Application.common;
using User.Contracts;

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
        _context.Hibuddy_user.Remove(user);
        await _context.SaveChangesAsync();    }

    public async Task UpdateInfo(Domain.User user, Info info)
    {
        user.education = info.education;
        user.favorites = info.favorites;
        user.hobbies = info.hobbies;
        user.job = info.job;
        _context.Hibuddy_user.Update(user);
        await _context.SaveChangesAsync();
    }
}