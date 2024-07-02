﻿using ReSale.Domain.Common;

namespace ReSale.Application.Abstractions.Authentication;

public interface IJwtService
{
    Task<Result<Token>> GetAccessTokenAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default);
}