namespace PWMS.Persistence.PortgreSQL.Data;
[ExcludeFromCodeCoverage]
public class ApplicationDbDesignTimeContextFactory : DesignTimeDbContextFactoryBase<ApplicationDbContext>
{
    protected override ApplicationDbContext CreateNewInstance(DbContextOptions<ApplicationDbContext> options)
    {
        return new ApplicationDbContext(options);
    }
}