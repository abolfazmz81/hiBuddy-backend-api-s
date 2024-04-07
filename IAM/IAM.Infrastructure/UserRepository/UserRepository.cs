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

    public void Add(User user)
    {
        _context.Hibuddy_user.Add(user);
        _context.SaveChanges();
    }

    public User? GetByEmail(string email)
    {
        return _context.Hibuddy_user.SingleOrDefault(user => user.email == email);
    }

    public User? GetByPhone(long Phone_number)
    {
       return _context.Hibuddy_user.SingleOrDefault(user => user.phone_number == Phone_number);
    }

    public User? GetByUsername(string username)
    {
        return _context.Hibuddy_user.SingleOrDefault(user => user.username == username);

    }

    public int GetLastId()
    {
        int? test =  _context.Hibuddy_user.Max(u => (int?)u.user_id);
        if (test is null)
        {
            return 0;
        }

        return test.Value;
    }

    public void DelUser(User user)
    {
        _context.Hibuddy_user.Remove(user);
        _context.SaveChanges();
    }

    
}