using User.Contracts;

namespace User.Application.UserManagement;

public interface IAddInfo
{
    public Task<Domain.User?> addInfo(String username,Info info);
}