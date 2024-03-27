using IAM.Application.common;
using IAM.Domain;
using Microsoft.EntityFrameworkCore;

namespace IAM.Infrastructure.UserRepository;

public class UserRepository : IUserRepository
{
    private readonly SQLServerContext _context;
    private readonly IHasher _hasher;

    public UserRepository(SQLServerContext context, IHasher hasher)
    {
        _context = context;
        _hasher = hasher;
    }

    public User? Add(User user)
    {
        user.password = _hasher.Hash(user.password);
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

    public void DelUser(User user)
    {
        _context.Hibuddy_user.Remove(user);
        _context.SaveChanges();
    }
}