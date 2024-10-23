using FluentAssertions;
using PWMS.Application.Addresses.Models;
using PWMS.Application.Addresses.Queries.Get;
using PWMS.Application.Common.Paging;
using PWMS.Application.Tests.Common;

namespace PWMS.Application.Tests.Addresses.Queries;

[Collection("QueryCollection")]
public class GetAddressTests : TestBase
{
    public GetAddressTests(QueryTestFixture fixture) : base(fixture)
    {
    }

    [Theory]
    [InlineData(1, 1)]
    public async Task ShouldReturnItemPage(int pageIndex, int pageSize)
    {
        var command = new GetAddressQuery(new PageContext<AddressFilter>(pageIndex, pageSize));
        var result = await Mediator.Send(command);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }
}
