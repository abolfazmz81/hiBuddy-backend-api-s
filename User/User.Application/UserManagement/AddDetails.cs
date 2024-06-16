using User.Application.common;
using User.Contracts;

namespace User.Application.UserManagement;

public class AddDetails : IAddDetails
{
    private readonly IUserRepository _userRepository;

    public AddDetails(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<string> addDetails(string username, Additional info)
    {
        // get user by username
        Domain.User? user = await _userRepository.GetByUsername(username);
        if (user is null)
        {
            return "not exits";
        }
        // check for not duplicate username
        Domain.User? newuser = await _userRepository.GetByUsername(info.username);
        if ((newuser is not null) && (newuser.username != user.username))
        {
            return "user with this username already exists";
        }
        // update user
        await _userRepository.UpdateDetails(user, info);
        return "ok";
    }
}