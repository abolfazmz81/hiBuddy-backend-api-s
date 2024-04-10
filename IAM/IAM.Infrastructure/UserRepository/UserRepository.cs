using IAM.Application.common;
using IAM.Contracts.Authentication;
using IAM.Domain;
using Microsoft.EntityFrameworkCore;

namespace IAM.Infrastructure.UserRepository;

public class UserRepository : IUserRepository
{
    private readonly SQLServerContext _context;

    public UserRepository(SQLServerContext context)
    {
        _context = context;
    }

    public async Task Add(User user)
    {
        _context.Hibuddy_user.Add(user);
        _context.SaveChanges();
    }

    public async Task<User?> GetByEmail(string email)
    {
        return _context.Hibuddy_user.SingleOrDefault(user => user.email == email);
    }

    public async Task<User?> GetByPhone(long Phone_number)
    {
       return _context.Hibuddy_user.SingleOrDefault(user => user.phone_number == Phone_number);
    }

    public async Task<User?> GetByUsername(string username)
    {
        return _context.Hibuddy_user.SingleOrDefault(user => user.username == username);

    }

    public async Task<int> GetLastId()
    {
        int? test = _context.Hibuddy_user.Max(u => (int?)u.user_id);
        if (test is null)
        {
            return 0;
        }

        return test.Value;
    }

    public async Task Update(User user)
    { 
        _context.Hibuddy_user.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DelUser(User user)
    {
        _context.Hibuddy_user.Remove(user);
        await _context.SaveChangesAsync();
    }

    
}