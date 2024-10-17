namespace PWMS.Persistence.PortgreSQL.Configuration;

internal sealed record DbConfigurationSection
{
    public const string SectionName = "connectionStrings:PostgresConnection";

    public DbConfigurationSection()
    { }

    public DbConfigurationSection(PostgresConnection postgresConnection) => PostgresConnection = postgresConnection;

    public PostgresConnection? PostgresConnection { get; init; }
}
