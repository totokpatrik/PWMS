using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PWMS.Application.Addresses.Repositories;
using PWMS.Application.Tests.Common;
using PWMS.Common.Tests;

namespace PWMS.Application.Tests;
internal sealed class Startup
{
    public static async void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication();
        services.TryAddSingleton(AppMockFactory.CreateCurrentUserServiceMock());

        services.TryAddSingleton(await ApplicationDbContextFactory.CreateAsync());


        services.TryAddScoped<IAddressRepository, MockAddressRepository>();
    }
}
