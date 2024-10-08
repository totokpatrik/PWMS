using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PWMS.Application.Addresses.Repositories;
using PWMS.Infrastructure.Addresses.Repositories;
using PWMS.Infrastructure.Data;
using PWMS.Infrastructure.Data.Interceptors;

namespace PWMS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        //Add services to the container
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddScoped<IAddressRepository, AddressRepository>();

        /*
        services.Scan(scan => scan
            .FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses(classes => classes.AssignableTo(typeof(RepositoryBase<>)))
            .AsMatchingInterface()
            .WithScopedLifetime());
        */

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetService<ISaveChangesInterceptor>()!);

            options.UseNpgsql(configuration.GetConnectionString("Database")!);
        });

        return services;
    }
}