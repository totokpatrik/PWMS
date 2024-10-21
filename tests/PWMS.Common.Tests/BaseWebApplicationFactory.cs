namespace PWMS.Common.Tests;

using DotNet.Testcontainers.Configurations;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using PWMS.Application.Common.Interfaces;
using PWMS.Common.Tests.Containers;
using Testcontainers.PostgreSql;

public class BaseWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>, IAsyncLifetime
    where TStartup : class
{
    protected const string EnvironmentName = "Test";

    private Dictionary<Type, IContainer> Containers { get; }

    protected BaseWebApplicationFactory()
    {
        TestcontainersSettings.ResourceReaperEnabled = false;

        Containers = new Dictionary<Type, IContainer>()
        {
            {
                typeof(PostgreSqlContainer ), ContainerFactory.Create<PostgreSqlContainer >()
            }
        };
    }

    public async Task InitializeAsync()
    {
        await Task.WhenAll(Containers.Select(c => c.Value.StartAsync()));

        using var scope = Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var context = scopedServices.GetRequiredService<IApplicationDbContext>();

        //The local machine may still have old volumes
        await context.AppDbContext.Database.EnsureDeletedAsync();

        await context.MigrateAsync();
        await context.SeedAsync(scope);
    }

    public new async Task DisposeAsync()
    {
        await base.DisposeAsync();
        await Task.WhenAll(Containers.Select(c => c.Value.DisposeAsync().AsTask()));
    }

    protected T GetContainer<T>() where T : class
    {
        if (!Containers.TryGetValue(typeof(T), out var container))
        {
            throw new ArgumentException($"Couldn't find any container of {nameof(T)}");
        }

        return (container as T)!;
    }
}
