namespace ReSale.Domain.Common;

public class Token
{
    public string AccessToken { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;

    public int ExpiresIn { get; init; }

    public int RefreshExpiresIn { get; init; }
}
