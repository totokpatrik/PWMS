namespace PWMS.Persistence.PortgreSQL.Configuration;

public sealed record PostgresConnection
{
    [NotNull]
    public string? ConnectionString { get; init; }

    public bool HealthCheckEnabled { get; init; } = true;

    public bool LoggingEnabled { get; init; }
}
