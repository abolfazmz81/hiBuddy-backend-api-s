namespace User.Application.UserManagement;

public interface IDeleteUser
{
    public Task<String> delete(String token);
}