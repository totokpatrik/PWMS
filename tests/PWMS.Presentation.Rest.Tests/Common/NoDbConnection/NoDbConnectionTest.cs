using Microsoft.AspNetCore.Mvc.Testing;
using Npgsql;

namespace PWMS.Presentation.Rest.Tests.Common.NoDbConnection;
[Collection(nameof(NoDbConnectionCollectionDefinition))]
public class NoDbConnectionTest : WebApplicationFactory<Program>
{
    private readonly NoDbConnectionWebApplicationFactory<Program> _factory;
    public NoDbConnectionTest(NoDbConnectionWebApplicationFactory<Program> factory) => _factory = factory;
    [Fact]
    public void TestNonExistingDatabase()
    {
        // Assert
        Assert.Throws<NpgsqlException>(() => _factory.CreateDefaultClient());
    }
}
