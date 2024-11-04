namespace PWMS.Infrastructure.Core;

using Microsoft.Extensions.DependencyInjection;
using Services;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IDateTime, MachineDateTime>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}
