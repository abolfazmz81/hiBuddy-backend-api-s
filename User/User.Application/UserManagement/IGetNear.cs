namespace User.Application.UserManagement;

public interface IGetNear
{
    public Task<Array?> getAll(String username);
}