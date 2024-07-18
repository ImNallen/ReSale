using ReSale.Domain.Common;

namespace ReSale.Domain.Users;

public record Password
{
    private Password(string value) => Value = value;
    public string Value { get; }
    
    public static implicit operator string(Password password) => password.Value;
    
    public static Result<Password> Create(string password)
    {
        return new Password(password);
    }
}