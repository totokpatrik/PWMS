namespace PWMS.Presentation.Rest.Tests.Common;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using PWMS.Application.Common.Interfaces;
using PWMS.Common.Tests;
using PWMS.Persistence.PortgreSQL.Configuration;
using PWMS.Presentation.Rest.Tests.SeedData;
using Testcontainers.PostgreSql;

public sealed class RestWebApplicationFactory<TStartup> : BaseWebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.UseEnvironment(EnvironmentName);
        builder.ConfigureServices((_, services) =>
        {
            services
                .Replace<IDbInitializer, SeedDataContext>()
                .Replace<IOptions<PostgresConnection>>(p =>
                    Options.Create(new PostgresConnection()
                    {
                        ConnectionString = GetContainer<PostgreSqlContainer>().GetConnectionString(),
                        HealthCheckEnabled = false,
                        LoggingEnabled = true
                    }));
        });
    }
}
