using Asp.Versioning;
using CorrelationId;
using CorrelationId.DependencyInjection;
using FluentValidation;
using FluentValidation.Resources;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OwaspHeaders.Core.Extensions;
using PWMS.Api.Extensions;
using PWMS.Application;
using PWMS.Core;
using PWMS.Core.Extensions;
using PWMS.Infrastructure;
using Shop.PublicApi.Extensions;
using StackExchange.Profiling;
using System.Globalization;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .Configure<GzipCompressionProviderOptions>(compressionOptions => compressionOptions.Level = CompressionLevel.Fastest)
    .Configure<JsonOptions>(jsonOptions => jsonOptions.JsonSerializerOptions.Configure())
    .Configure<RouteOptions>(routeOptions => routeOptions.LowercaseUrls = true)
    .AddHttpClient()
    .AddHttpContextAccessor()
    .AddResponseCompression(compressionOptions =>
    {
        compressionOptions.EnableForHttps = true;
        compressionOptions.Providers.Add<GzipCompressionProvider>();
    })
    .AddEndpointsApiExplorer()
    .AddApiVersioning(versioningOptions =>
    {
        versioningOptions.DefaultApiVersion = ApiVersion.Default;
        versioningOptions.ReportApiVersions = true;
        versioningOptions.AssumeDefaultVersionWhenUnspecified = true;
    })
    .AddApiExplorer(explorerOptions =>
    {
        explorerOptions.GroupNameFormat = "'v'VVV";
        explorerOptions.SubstituteApiVersionInUrl = true;
    });

builder.Services.AddSwagger();
builder.Services.AddDataProtection();
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(behaviorOptions =>
    {
        behaviorOptions.SuppressMapClientErrors = true;
        behaviorOptions.SuppressModelStateInvalidFilter = true;
    })
    .AddJsonOptions(_ => { });

// Adding the application services in ASP.NET Core DI.
builder.Services
    .ConfigureAppSettings()
    .AddInfrastructure()
    .AddCommandHandlers()
    .AddApplicationDbContext()
    .AddRepositories()
    .AddHealthChecks(builder.Configuration);

builder.Services.AddDefaultCorrelationId();

// MiniProfiler for .NET
// https://miniprofiler.com/dotnet/
builder.Services.AddMiniProfiler(options =>
{
    // Route: /profiler/results-index
    options.RouteBasePath = "/profiler";
    options.ColorScheme = ColorScheme.Dark;
    options.EnableServerTimingHeader = true;
    options.TrackConnectionOpenClose = true;
    options.EnableDebugMode = builder.Environment.IsDevelopment();
}).AddEntityFramework();

// Validating the services added in the ASP.NET Core DI.
builder.Host.UseDefaultServiceProvider((context, serviceProviderOptions) =>
{
    serviceProviderOptions.ValidateScopes = context.HostingEnvironment.IsDevelopment();
    serviceProviderOptions.ValidateOnBuild = true;
});

// FluentValidation global configuration.
ValidatorOptions.Global.DisplayNameResolver = (_, member, _) => member?.Name;
ValidatorOptions.Global.LanguageManager = new LanguageManager { Enabled = true, Culture = new CultureInfo("en-US") };

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseErrorHandling();
app.UseSwagger();
app.UseSwaggerUI();
app.UseResponseCompression();
app.UseHttpsRedirection();
app.UseSecureHeadersMiddleware();
app.UseMiniProfiler();
app.UseCorrelationId();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.RunAppAsync();