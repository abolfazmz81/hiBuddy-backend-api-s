using IAM.Application.common;
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

    public User? Add(User user)
    {
        _context.Hibuddy_user.Add(user);
        _context.SaveChanges();
        return user;
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
}