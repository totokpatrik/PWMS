using Microsoft.EntityFrameworkCore;
using PWMS.Application.Common.Interfaces;
using PWMS.Application.Tests.SeedData;
using PWMS.Common.Tests;
using PWMS.Infrastructure.Core.Services;
using PWMS.Persistence.PortgreSQL.Data;

namespace PWMS.Application.Tests.Common;

public static class ApplicationDbContextFactory
{
    public static async Task<IApplicationDbContext> CreateAsync()
    {

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new ApplicationDbContext(options);
        context.InitContext(
            AppMockFactory.CreateCurrentUserServiceMock(),
            new SeedDataContext(),
            new MachineDateTime(),
            AppMockFactory.CreateMediatorMock());

        await context.Database.EnsureCreatedAsync();

        // TODO
        //await context.SeedAsync();

        return context;
    }

    public static void Destroy(IApplicationDbContext context)
    {
        context.AppDbContext.Database.EnsureDeleted();

        context.AppDbContext.Dispose();
    }
}
