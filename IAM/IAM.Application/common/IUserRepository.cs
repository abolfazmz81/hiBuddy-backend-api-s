﻿using IAM.Domain;

namespace IAM.Application.common;

public interface IUserRepository
{
    User? Add(User user);
    User? GetByEmail(String email);
    User? GetByPhone(String phone_number);
}