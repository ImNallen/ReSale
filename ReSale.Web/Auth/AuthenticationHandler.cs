using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace ReSale.Web.Auth;

public class AuthenticationHandler(ILocalStorageService localStorage)
    : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        string? token = await localStorage.GetItemAsStringAsync("accessToken", cancellationToken);

        request.Headers.Authorization = new AuthenticationHeaderValue(
            "Bearer", token?.Replace("\"", string.Empty, StringComparison.CurrentCulture));

        return await base.SendAsync(request, cancellationToken);
    }
}
