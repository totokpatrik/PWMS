using PWMS.Persistence.PortgreSQL.Data;
namespace My.Nkz.NewApp.Persistence.PostgreSQL;
[ExcludeFromCodeCoverage]
public class ApplicationDbDesignTimeContextFactory : DesignTimeDbContextFactoryBase<ApplicationDbContext>
{
    protected override ApplicationDbContext CreateNewInstance(DbContextOptions<ApplicationDbContext> options)
    {
        return new ApplicationDbContext(options);
    }
}