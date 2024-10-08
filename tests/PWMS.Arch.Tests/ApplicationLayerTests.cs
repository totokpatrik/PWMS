using AutoMapper;
using MediatR;
using PWMS.Application.Abstractions.Commands;
using PWMS.Application.Abstractions.Queries;
using PWMS.Arch.Tests.Extensions;

namespace PWMS.Arch.Tests;

[Collection("Sequential")]
public class ApplicationLayerTests : BaseTests
{
    [Fact]
    public void ApplicationLayer_Cqrs_QueriesEndWithQuery()
    {
        AllTypes.That().Inherit(typeof(Query<>))
        .Should().HaveNameEndingWith("Query")
        .AssertIsSuccessful();
    }

    [Fact]
    public void ApplicationLayer_Cqrs_ContainsAllQueries()
    {
        AllTypes.That().HaveNameEndingWith("Query")
        .Should().ResideInNamespace("PWMS.Application")
        .AssertIsSuccessful();
    }

    [Fact]
    public void ApplicationLayer_Cqrs_CommandsEndWithCommand()
    {
        AllTypes.That().Inherit(typeof(ICommand<>))
        .Should().HaveNameEndingWith("Command")
        .AssertIsSuccessful();

        AllTypes.That().Inherit(typeof(ICommand))
        .Should().HaveNameEndingWith("Command")
        .AssertIsSuccessful();
    }

    [Fact]
    public void ApplicationLayer_Cqrs_ContainsAllCommands()
    {
        AllTypes.That().HaveNameEndingWith("Command")
        .Should().ResideInNamespace("PWMS.Application")
        .AssertIsSuccessful();
    }

    [Fact]
    public void ApplicationLayer_Cqrs_QueryHandlersEndWithQueryHandler()
    {
        AllTypes.That().Inherit(typeof(QueryHandler<,>))
        .Should().HaveNameEndingWith("QueryHandler")
        .AssertIsSuccessful();
    }

    [Fact]
    public void ApplicationLayer_Cqrs_ContainsAllQueryHandlers()
    {
        AllTypes.That().HaveNameEndingWith("QueryHandler")
        .Should().ResideInNamespace("PWMS.Application")
        .AssertIsSuccessful();
    }

    [Fact]
    public void ApplicationLayer_Cqrs_CommandHandlersEndWithCommandHandler()
    {
        AllTypes.That().Inherit(typeof(IRequestHandler<>))
        .Should().HaveNameEndingWith("CommandHandler")
        .AssertIsSuccessful();
    }

    [Fact]
    public void ApplicationLayer_Cqrs_ContainsAllCommandHandlers()
    {
        AllTypes.That().HaveNameEndingWith("CommandHandler")
            .Should().ResideInNamespace("PWMS.Application")
            .AssertIsSuccessful();
    }

    [Fact]
    public void ApplicationLayer_Dtos_ShouldBeMutable()
    {
        AllTypes.That().HaveNameEndingWith("Dto")
            .Should().BeMutable()
            .AssertIsSuccessful();
    }
    [Fact]
    public void ApplicationLayer_Dtos_ShouldResideInApplication()
    {
        AllTypes.That().HaveNameEndingWith("Dto")
            .Should().ResideInNamespace("PWMS.Application")
            .AssertIsSuccessful();
    }
    [Fact]
    public void ApplicationLayer_MappingProfiles_ShouldOnlyResideInApplication()
    {
        AllTypes.That().Inherit(typeof(Profile))
            .Should().ResideInNamespace("PWMS.Application")
            .AssertIsSuccessful();
    }

    [Fact]
    public void ApplicationLayer_MappingProfiles_ShouldEndWithProfile()
    {
        AllTypes.That().Inherit(typeof(Profile))
            .Should().HaveNameEndingWith("Profile")
            .AssertIsSuccessful();
    }
}