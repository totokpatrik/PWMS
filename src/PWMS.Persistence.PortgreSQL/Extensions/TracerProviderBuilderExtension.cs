namespace PWMS.Persistence.PortgreSQL.Extensions;

public static class TracerProviderBuilderExtension
{
    public static TracerProviderBuilder AddNgpSqlPersistenceOpenTelemetry(this TracerProviderBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.AddEntityFrameworkCoreInstrumentation(o => o.SetDbStatementForText = true)
            .AddNpgsql();
        return builder;
    }
}
