using FluentAssertions;
using PWMS.Application.Addresses.Queries.GetById;
using PWMS.Application.Common.Exceptions;
using PWMS.Application.Tests.Common;
using PWMS.Domain.Addresses.Entities;

namespace PWMS.Application.Tests.Addresses.Queries;

[Collection("QueryCollection")]
public class GetAddressByIdTests : TestBase
{
    public GetAddressByIdTests(QueryTestFixture fixture) : base(fixture)
    {
    }
    [Fact]
    public async Task ShouldReturnItem()
    {
        // Arrange
        var firstEntity = Context.Set<Address>().First();
        var query = new GetAddressByIdQuery(firstEntity.Id);

        // Act
        var result = await Mediator.Send(query);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }
    [Fact]
    public async Task ShouldNotReturnItem()
    {
        // Arrange
        var query = new GetAddressByIdQuery(Guid.NewGuid());

        // Act
        Func<Task> act = () => Mediator.Send(query);

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => act());
    }
}
