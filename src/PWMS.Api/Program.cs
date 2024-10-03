using PWMS.Api;
using PWMS.Application;
using PWMS.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services
    .AddApiServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request piepeline
app.UseApiServices();


app.Run();
