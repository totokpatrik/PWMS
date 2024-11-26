using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using MudExtensions.Services;
using PWMS.Web.Blazor;
using PWMS.Web.Blazor.Identity;
using PWMS.Web.Blazor.Services.AuthService;
using PWMS.Web.Blazor.Services.Configuration;
using PWMS.Web.Blazor.Services.Core;
using PWMS.Web.Blazor.Services.HttpService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy("SiteAndWarehouseSelected", policy =>
    {
        policy.AddRequirements(new SiteSelectedRequirement());
        policy.AddRequirements(new WarehouseSelectedRequirement());
    });
});

// set base address for default host
builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri(builder.Configuration["BackendUrl"] ?? "https://localhost:5001") });
builder.Services.AddScoped<IHttpService, HttpService>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<ISiteService, SiteService>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();

// adding localstorage
builder.Services.AddBlazoredLocalStorage();

// adding mud blazor
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});
builder.Services.AddMudExtensions();

// Add localization
builder.Services.AddLocalization();

await builder.Build().RunAsync();