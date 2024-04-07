using IAM.Contracts.Authentication;
using IAM.Domain;

namespace IAM.Application.common;

public interface IUserRepository
{
    void Add(User user);
    User? GetByEmail(String email);
    User? GetByPhone(long phone_number);
    User? GetByUsername(String username);
    int GetLastId();
    void DelUser(User user);
}