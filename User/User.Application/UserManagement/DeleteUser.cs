using User.Application.common;
namespace User.Application.UserManagement;

public class DeleteUser : IDeleteUser
{

    private readonly IUserRepository _userRepository;
    private readonly IHasher _hasher;

    public DeleteUser(IUserRepository userRepository, IHasher hasher)
    {
        _userRepository = userRepository;
        _hasher = hasher;
    }

    public async Task<string> delete(string username,String password)
    {
        // get user by username from user table
        Domain.User? user = await _userRepository.GetByUsername(username);
        if (user is null)
        {
            return "not exists";
        }
        // check if the password is correct
        if (user.password != _hasher.Hash(password))
        {
            return "wrong password";
        }
        // delete user
        await _userRepository.DelUser(user);
        return "ok";
    }
}