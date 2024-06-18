using System.Collections;

namespace User.Application.UserManagement;

public interface IGetNear
{
    public Task<ArrayList?> getAll(String username);
}