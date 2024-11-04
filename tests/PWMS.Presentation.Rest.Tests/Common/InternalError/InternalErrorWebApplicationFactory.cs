using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using PWMS.Application.Addresses.Repositories;
using PWMS.Application.Common.Interfaces;
using PWMS.Common.Tests;
using PWMS.Persistence.PortgreSQL.Configuration;
using PWMS.Presentation.Rest.Tests.SeedData;
using Testcontainers.PostgreSql;

namespace PWMS.Presentation.Rest.Tests.Common.InternalError;

public sealed class InternalErrorWebApplicationFactory<TStartup> : BaseWebApplicationFactory<TStartup> where TStartup : class
{
    private const int Port = 5434;
    private const string EnvironmentName = "TestInternalError";

    public InternalErrorWebApplicationFactory() : base(Port)
    {
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.UseEnvironment(EnvironmentName);
        builder.ConfigureServices((_, services) =>
        {
            services
                .Replace<IDbInitializer, SeedDataContext>()
                .Replace(p =>
                    Options.Create(new PostgresConnection()
                    {
                        ConnectionString = GetContainer<PostgreSqlContainer>().GetConnectionString(),
                        HealthCheckEnabled = false,
                        LoggingEnabled = true
                    }))
                .Replace<IAddressRepository, MockAddressRepositoryInternalError>();
        });
    }
}
