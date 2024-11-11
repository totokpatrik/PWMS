using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using MudExtensions.Services;
using PWMS.Web.Blazor;
using PWMS.Web.Blazor.Identity;
using PWMS.Web.Blazor.Services.AuthService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

// set base address for default host
builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri(builder.Configuration["BackendUrl"] ?? "https://localhost:5001") });

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

// adding localstorage
builder.Services.AddBlazoredLocalStorage();

// adding mud blazor
builder.Services.AddMudServices();
builder.Services.AddMudExtensions();

await builder.Build().RunAsync();