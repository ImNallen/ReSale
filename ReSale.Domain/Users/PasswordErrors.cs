using ReSale.Domain.Common;

namespace ReSale.Domain.Users;

public static class PasswordErrors
{
    public static readonly Error Empty = Error.Problem("Password.Empty", "Password is empty");

    public static readonly Error TooShort = Error.Problem("Password.TooShort", "Password is too short");
}
