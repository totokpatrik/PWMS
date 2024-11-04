using PWMS.Common.Tests;

namespace PWMS.Presentation.Rest.Tests.Common.NoDbConnection;

[CollectionDefinition(nameof(NoDbConnectionCollectionDefinition))]
public class NoDbConnectionCollectionDefinition : CoreCollectionDefinition<NoDbConnectionWebApplicationFactory<Program>>
{
}
