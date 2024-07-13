using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;
using ReSale.Web;
using ReSale.Web.Clients;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<AuthenticationHandler>();

builder.Services.AddBlazoredLocalStorage();

builder.Services
    .AddRefitClient<IIdentityClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:5001"))
    .AddHttpMessageHandler<AuthenticationHandler>();

builder.Services
    .AddRefitClient<ICustomerClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:5001"))
    .AddHttpMessageHandler<AuthenticationHandler>();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthorizationStateProvider>();

await builder.Build().RunAsync();