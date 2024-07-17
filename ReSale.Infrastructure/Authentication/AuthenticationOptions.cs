namespace ReSale.Infrastructure.Authentication;

public sealed class AuthenticationOptions
{
    public string Audience { get; set; }
    public string MetadataUrl { get; set; }
    public bool RequireHttpsMetadata { get; set; }
    public string Issuer { get; set; }
}