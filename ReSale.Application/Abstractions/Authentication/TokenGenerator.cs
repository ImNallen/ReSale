using System.Security.Cryptography;

namespace ReSale.Application.Abstractions.Authentication;

public static class TokenGenerator
{
    public static string Generate()
    {
        byte[] randomNumber = new byte[64];

        using var rng = RandomNumberGenerator.Create();

        rng.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }
}
