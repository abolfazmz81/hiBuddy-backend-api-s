using System.Collections;
using User.Domain;

namespace User.Application.UserManagement;

public interface IGetNear
{
    public Task<ArrayList?> getAll(String username,locations locations);
}