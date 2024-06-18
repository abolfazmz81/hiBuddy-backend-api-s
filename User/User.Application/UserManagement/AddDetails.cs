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
    public async Task<Domain.User?> addDetails(string username, Additional info)
    {
        // get user by username
        Domain.User? user = await _userRepository.GetByUsername(username);
        if (user is null)
        {
            return null;
        }
        /*// check for not duplicate username
        Domain.User? newuser = await _userRepository.GetByUsername(info.username);
        if ((newuser is not null) && (newuser.username != user.username))
        {
            return new Domain.User
            {
                user_id = -1
            };
        }*/
        // update user
        Domain.User nuser = await _userRepository.UpdateDetails(user, info);
        return nuser;
    }
}