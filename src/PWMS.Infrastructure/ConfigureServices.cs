using Microsoft.Extensions.DependencyInjection;
using PWMS.Application.Addresses.Repositories;
using PWMS.Core.SharedKernel;
using PWMS.Infrastructure.Data;
using PWMS.Infrastructure.Data.Context;
using PWMS.Infrastructure.Data.Repositories.Addresses;
using PWMS.Infrastructure.Data.Repositories.Common;

namespace PWMS.Infrastructure;

public static class ConfigureServices
{
    /// <summary>
    /// Adds the infrastructure services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) =>
        services
            .AddScoped<ApplicationDbContext>()
            .AddScoped<EventStoreDbContext>()
            .AddScoped<IUnitOfWork, UnitOfWork>();

    /// <summary>
    /// Adds the repositories to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    public static IServiceCollection AddRepositories(this IServiceCollection services) =>
         services
            .AddScoped<IEventStoreRepository, EventStoreRepository>()
            .AddScoped<IAddressRepository, AddressRepository>();
}
