using IAM.Domain;

namespace IAM.Application.common;

public interface IUserRepository
{
    User? Add(User user);
    User? GetByEmail(String email);
    User? GetByPhone(long phone_number);
    User? GetByUsername(String username);
    void DelUser(User user);
}