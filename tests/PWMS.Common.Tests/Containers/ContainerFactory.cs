using Testcontainers.PostgreSql;

namespace PWMS.Common.Tests.Containers;
public static class ContainerFactory
{
    private const string Database = "template_db";
    private const string PostgresUsername = "postgres";
    private const string PostgresPassword = "postgres";

    private const string RabbitMqUsername = "rabbitmq";
    private const string RabbitMqPassword = "rabbitmq";

    public static IContainer Create<T>() where T : IContainer
    {
        var type = typeof(T);
        return type switch
        {
            not null when type.IsAssignableFrom(typeof(PostgreSqlContainer)) => CreatePostgreSql(),
            _ => throw new ArgumentException($"Couldn't create a container of {nameof(T)}")
        };
    }

    private static PostgreSqlContainer CreatePostgreSql() =>
        new PostgreSqlBuilder()
            .WithUsername(PostgresUsername)
            .WithPassword(PostgresPassword)
            .WithDatabase(Database)
            .WithImage("postgres:17")
            .WithPortBinding(5432, 5432)
            .WithAutoRemove(true)
            .WithCleanUp(true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5432))
            .Build();
}
