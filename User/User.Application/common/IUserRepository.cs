using User.Contracts;

namespace User.Application.common;

public interface IUserRepository
{
    public Task<Domain.User?> GetByUsername(String username);
    public Task DelUser(Domain.User user);
    public Task UpdateInfo(Domain.User user, Info info);

}