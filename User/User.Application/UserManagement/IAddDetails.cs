using User.Contracts;

namespace User.Application.UserManagement;

public interface IAddDetails
{
    public Task<Domain.User?> addDetails(String username, Additional info);
}