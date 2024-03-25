using IAM.Application.common;
using IAM.Domain;
using Microsoft.EntityFrameworkCore;

namespace IAM.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly SQLServerContext _context;

    public UserRepository(SQLServerContext context)
    {
        _context = context;
    }

    public User? Add(User user)
    {
        throw new NotImplementedException();
    }

    public User? GetByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public User? GetByPhone(long Phone_number)
    {
       return _context.Hibuddy_user.SingleOrDefault(user => user.phone_number == Phone_number);
    }
}