using IAM.Application.common;
using IAM.Domain;

namespace IAM.Infrastructure;

public class UserRepository : IUserRepository
{
    public User? Add(User user)
    {
        throw new NotImplementedException();
    }

    public User? GetByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public User? GetByPhone(string Phone_number)
    {
        throw new NotImplementedException();
    }
}