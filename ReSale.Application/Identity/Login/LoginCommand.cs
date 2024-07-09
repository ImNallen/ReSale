﻿using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Identity.Shared;

namespace ReSale.Application.Identity.Login;

public sealed record LoginCommand(
    string Email,
    string Password) : ICommand<AccessTokenResponse>;