namespace ReSale.Domain.Common;

public class Token
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public int ExpiresIn { get; init; }
    public int RefreshExpiresIn { get; init; }
    public string TokenType { get; init; } = string.Empty;
    public string IdToken { get; init; } = string.Empty;
    public int NotBeforePolicy { get; init; }
    public string SessionState { get; init; } = string.Empty;
    public string Scope { get; init; } = string.Empty;
}