using ReSale.Domain.Common;

namespace ReSale.Domain.Users;

public sealed record Password
{
    public string Value { get; }
    private Password(string value) => Value = value;

    public static Result<Password> Create(string? password)
    {
        if (string.IsNullOrEmpty(password))
            return Result.Failure<Password>(PasswordErrors.Empty);
        
        if (password.Length < 8)
            return Result.Failure<Password>(PasswordErrors.ToShort);
        
        return new Password(BCrypt.Net.BCrypt.EnhancedHashPassword(password));
    }
    
    public bool Verify(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, Value);
    }
}