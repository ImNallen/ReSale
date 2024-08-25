﻿using ReSale.Domain.Common;

namespace ReSale.Domain.Users;

public static class UserErrors
{
    public static readonly Error NotFound = Error.NotFound("User.NotFound", "User not found.");

    public static readonly Error InvalidCredentials = Error.Problem("User.InvalidCredentials", "Invalid credentials.");
}
