using IAM.Contracts.Authentication;
using IAM.Domain;

namespace IAM.Application.common;

public interface IUserRepository
{
    Task Add(User user);
    Task<User?> GetByEmail(String email);
    Task<User?> GetByPhone(long phone_number);
    Task<User?> GetByUsername(String username);
    Task<int> GetLastId();
    Task Update(User user);
    Task DelUser(User user);
}