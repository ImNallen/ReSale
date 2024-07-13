using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace ReSale.Web.Clients;

public class AuthenticationHandler : DelegatingHandler
{
    private readonly ILocalStorageService _localStorage;

    public AuthenticationHandler(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, 
        CancellationToken cancellationToken)
    {
        var token = await _localStorage.GetItemAsStringAsync("accessToken", cancellationToken);
        
        request.Headers.Authorization = new AuthenticationHeaderValue(
            "Bearer", token?.Replace("\"", ""));
        
        return await base.SendAsync(request, cancellationToken);
    }
}