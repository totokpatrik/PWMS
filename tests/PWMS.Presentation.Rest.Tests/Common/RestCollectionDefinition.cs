using PWMS.Common.Tests;

namespace PWMS.Presentation.Rest.Tests.Common;

[CollectionDefinition(nameof(RestCollectionDefinition))]
public sealed class RestCollectionDefinition : CoreCollectionDefinition<RestWebApplicationFactory<Program>>
{
}
