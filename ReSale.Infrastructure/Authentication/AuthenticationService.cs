using System.Net.Http.Json;
using ReSale.Application.Abstractions.Authentication;
using ReSale.Domain.Users;
using ReSale.Infrastructure.Authentication.Models;

namespace ReSale.Infrastructure.Authentication;

public class AuthenticationService(HttpClient httpClient) : IAuthenticationService
{
    private const string PasswordCredentialType = "password";
    
    public async Task<string> RegisterAsync(
        User user, 
        string password, 
        CancellationToken cancellationToken)
    {
        var userRepresentationModel = UserRepresentationModel.FromUser(user);
        
        userRepresentationModel.Credentials = new CredentialRepresentationModel[]
        {
            new()
            {
                Value = password,
                Temporary = false,
                Type = PasswordCredentialType
            }
        };

        var response = await httpClient.PostAsJsonAsync(
            "users",
            userRepresentationModel,
            cancellationToken);

        return ExtractIdentityIdFromLocationHeader(response);
    }

    private static string ExtractIdentityIdFromLocationHeader(
        HttpResponseMessage response)
    {
        const string usersSegmentName = "users/";

        var locationHeader = response.Headers.Location?.PathAndQuery;
        
        if (locationHeader is null)
        {
            throw new InvalidOperationException("Location header can't be null.");
        }

        var userSegmentValueIndex = locationHeader.IndexOf(usersSegmentName, StringComparison.CurrentCultureIgnoreCase);

        var userIdentityId = locationHeader.Substring(userSegmentValueIndex + usersSegmentName.Length);

        return userIdentityId;
    }
}