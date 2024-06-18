using User.Contracts;

namespace User.Application.common;

public interface IUserRepository
{
    public Task<Domain.User?> GetByUsername(String username);
    public Task DelUser(Domain.User user);
    public Task<Domain.User> UpdateInfo(Domain.User user, Info info);

    public Task<Domain.User> UpdateDetails(Domain.User user, Additional info);
    public Task<Array> GetNear(Domain.User user);

}