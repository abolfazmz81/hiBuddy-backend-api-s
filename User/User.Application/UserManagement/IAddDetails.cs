using User.Contracts;

namespace User.Application.UserManagement;

public interface IAddDetails
{
    public Task<String> addDetails(String username, Additional info);
}