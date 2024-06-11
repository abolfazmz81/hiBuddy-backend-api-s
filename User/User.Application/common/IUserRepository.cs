namespace User.Application.common;

public interface IUserRepository
{
    public Task<Domain.User?> GetByUsername(String username);
    Task DelUser(Domain.User user);
}