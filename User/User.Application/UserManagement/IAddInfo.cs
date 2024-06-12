using User.Contracts;

namespace User.Application.UserManagement;

public interface IAddInfo
{
    public Task<String> AddInfo(Info info);
}