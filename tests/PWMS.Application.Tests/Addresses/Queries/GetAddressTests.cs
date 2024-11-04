using Castle.DynamicLinqQueryBuilder;
using FluentAssertions;
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
    public async Task ShouldReturnAddressPage(int pageIndex, int pageSize)
    {
        var command = new GetAddressQuery(new PageContext(pageIndex, pageSize));
        var result = await Mediator.Send(command);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }

    [Theory]
    [InlineData(1, 1)]
    public async Task ShouldReturnAddressPageWithFilter(int pageIndex, int pageSize)
    {
        var filter = new QueryBuilderFilterRule();
        var command = new GetAddressQuery(new PageContext(pageIndex, pageSize, filter));


        var result = await Mediator.Send(command);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }

    [Theory]
    [InlineData(1, 10)]
    public async Task ShouldReturnItemPageSorting(int pageIndex, int pageSize)
    {
        var command = new GetAddressQuery(new PageContext(pageIndex, pageSize, null,
            new[] { new SortDescriptor("addressLine", EnumSortDirection.Desc) }
            ));

        var result = await Mediator.Send(command);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Data.FirstOrDefault().Should().NotBeNull();
    }
}
