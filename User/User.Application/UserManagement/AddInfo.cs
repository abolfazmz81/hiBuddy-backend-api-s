using User.Application.common;
using User.Contracts;

namespace User.Application.UserManagement;

public class AddInfo : IAddInfo
{
    private readonly IUserRepository _userRepository;

    public AddInfo(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<string> addInfo(String username,Info info)
    {
        // get user by username
        Domain.User? user = await _userRepository.GetByUsername(username);
        if (user is null)
        {
            return "not exits";
        }
        // update user
        await _userRepository.UpdateInfo(user, info);
        return "ok";
    }
}