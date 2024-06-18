﻿using User.Application.common;

namespace User.Application.UserManagement;

public class GetNear : IGetNear
{
    private readonly IUserRepository _userRepository;

    public GetNear(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Array?> getAll(string username)
    {
        // get user by username from user table
        Domain.User? user = await _userRepository.GetByUsername(username);
        if (user is null)
        {
            return null;
        }
        // get near users
        Array res = await _userRepository.GetNear(user);
        return res;
    }
}