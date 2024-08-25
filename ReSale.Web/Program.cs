using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;
using ReSale.Web;
using ReSale.Web.Auth;
using ReSale.Web.Clients;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<AuthenticationHandler>();

builder.Services.AddBlazoredLocalStorage();

const string apiUrl = $"https://localhost:5001";

builder.Services
    .AddRefitClient<IAuthenticationClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiUrl))
    .AddHttpMessageHandler<AuthenticationHandler>();

builder.Services
    .AddRefitClient<ICustomersClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiUrl))
    .AddHttpMessageHandler<AuthenticationHandler>();

builder.Services
    .AddRefitClient<IEmployeesClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiUrl))
    .AddHttpMessageHandler<AuthenticationHandler>();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthorizationStateProvider>();

await builder.Build().RunAsync();
