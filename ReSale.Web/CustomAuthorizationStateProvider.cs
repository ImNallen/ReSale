using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace ReSale.Web;

public class CustomAuthorizationStateProvider(
    ILocalStorageService localStorageService) 
    : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity();
        
        var accessToken = await localStorageService.GetItemAsStringAsync("accessToken");

        if (!string.IsNullOrWhiteSpace(accessToken))
        {
            var token = new JwtSecurityToken(accessToken.Replace("\"", ""));
            
            if (IsTokenExpired(token))
            {
                await LogoutUser();
            }
            else
            {
                try
                {
                    identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, token.Claims.First(c => c.Type == "name").Value),
                        new Claim(ClaimTypes.Email, token.Claims.First(c => c.Type == "email").Value),
                        new Claim(ClaimTypes.GivenName, token.Claims.First(c => c.Type == "given_name").Value),
                        new Claim(ClaimTypes.Surname, token.Claims.First(c => c.Type == "family_name").Value),
                        new Claim("access_token", accessToken)
                    }, "jwt");
                }
                catch (Exception)
                {
                    await LogoutUser();
                }
            }
        }
        
        var user = new ClaimsPrincipal(identity);
        var state = new AuthenticationState(user);
        
        NotifyAuthenticationStateChanged(Task.FromResult(state));

        return state;
    }
    
    private bool IsTokenExpired(JwtSecurityToken token)
    {
        return token.ValidTo < DateTime.UtcNow;
    }

    
    private async Task LogoutUser()
    {
        await localStorageService.RemoveItemAsync("accessToken");
        await localStorageService.RemoveItemAsync("refreshToken");
        var identity = new ClaimsIdentity();
        var user = new ClaimsPrincipal(identity);
        var state = new AuthenticationState(user);
        NotifyAuthenticationStateChanged(Task.FromResult(state));
    }
}