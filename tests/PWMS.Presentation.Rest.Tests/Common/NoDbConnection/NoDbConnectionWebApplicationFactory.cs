using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
namespace PWMS.Presentation.Rest.Tests.Common.NoDbConnection;

public class NoDbConnectionWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    private const string EnvironmentName = "TestNoDbConnection";
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.UseEnvironment(EnvironmentName);
    }
}
