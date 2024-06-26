﻿using IAM.Domain;

namespace IAM.Application.common;

public interface IJwtGenerator
{
    string Generate(User user);
    String? GetUsername(String token);
}