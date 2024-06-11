using Microsoft.EntityFrameworkCore;
using User.Application.common;

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
}