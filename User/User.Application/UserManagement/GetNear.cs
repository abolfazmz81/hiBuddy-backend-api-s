using System.Collections;
using User.Application.common;
using User.Domain;

namespace User.Application.UserManagement;

public class GetNear : IGetNear
{
    private readonly IUserRepository _userRepository;

    public GetNear(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ArrayList?> getAll(string username,locations location)
    {
        // get user by username from user table
        Domain.User? user = await _userRepository.GetByUsername(username);
        if (user is null)
        {
            return null;
        }
        // add location to the users location
        await _userRepository.Addlocation(user, location);
        // get near users
        ArrayList res = await _userRepository.GetNear(user);
        return res;
    }
}