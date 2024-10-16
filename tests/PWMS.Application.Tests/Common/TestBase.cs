using MediatR;
using PWMS.Application.Common.Interfaces;
using PWMS.Application.Tests.SeedData;

namespace PWMS.Application.Tests.Common;

public class TestBase
{
    protected IApplicationDbContext Context { get; init; }

    protected ICurrentUserService CurrentUserService { get; init; }

    protected SeedDataContext SeedDataContext { get; init; }

    protected IMediator Mediator { get; init; }

    protected TestBase(QueryTestFixture fixture)
    {
        Context = fixture.Context;
        Mediator = fixture.Mediator;
        CurrentUserService = fixture.CurrentUserService;
        SeedDataContext = fixture.SeedDataContext;
    }
}
