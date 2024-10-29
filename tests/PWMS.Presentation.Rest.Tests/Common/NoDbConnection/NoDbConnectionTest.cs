using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace PWMS.Presentation.Rest.Tests.Common.NoDbConnection;

public class NoDbConnectionTest : WebApplicationFactory<Program>
{
    [Fact]
    public void Test_NodbConnection()
    {
        var asd = new WebApplication();
        var client = asd.CreateClient();
    }
}

internal class WebApplication : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.UseEnvironment("Test");
        Environment.SetEnvironmentVariable("connectionStrings:PostgresConnection:ConnectionString",
            "Host=localhost;Port=50000;Uid=postgres;Password=Admin1234!;Database=pwms_db;");
    }
}
