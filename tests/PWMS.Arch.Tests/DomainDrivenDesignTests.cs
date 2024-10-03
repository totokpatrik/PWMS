using NetArchTest.Rules;
using PWMS.Application.Abstractions.DomainEventHandlers;
using PWMS.Arch.Tests.Extensions;
using PWMS.Domain.Abstractions.DomainEvents;
using PWMS.Domain.Abstractions.Entities;
using PWMS.Domain.Abstractions.ValueObjects;

namespace PWMS.Arch.Tests;

[Collection("Sequential")]
public class DomainDrivenDesignTests : BaseTests
{
    [Fact]
    public void DomainDrivenDesign_ValueObjects_ShouldBeImmutable()
    {
        Types.InAssembly(CoreAssembly)
            .That().Inherit(typeof(ValueObject))
            .Should().BeImmutable()
            .AssertIsSuccessful();
    }

    [Fact]
    public void DomainDrivenDesign_Aggregates_ShouldBeHavePrivateSettings()
    {
        Types.InAssembly(CoreAssembly)
            .That().Inherit(typeof(Aggregate))
            .Should().BeImmutable()
            .AssertIsSuccessful();
    }

    [Fact]
    public void DomainDrivenDesign_Entities_ShouldBeHavePrivateSettings()
    {
        Types.InAssembly(CoreAssembly).That().Inherit(typeof(Entity))
            .Should().BeImmutable()
            .AssertIsSuccessful();
    }

    [Fact]
    public void DomainDrivenDesign_Aggregates_ShouldOnlyResideInCore()
    {
        AllTypes.That().Inherit(typeof(Aggregate))
            .Should().ResideInNamespaceStartingWith("PWMS.Domain")
            .AssertIsSuccessful();
    }

    [Fact]
    public void DomainDrivenDesign_DomainEvents_ShouldOnlyResideInCore()
    {
        AllTypes.That().Inherit(typeof(IDomainEvent))
            .Should().ResideInNamespaceStartingWith("PWMS.Core")
            .AssertIsSuccessful();
    }

    [Fact]
    public void DomainDrivenDesign_DomainEvents_ShouldEndWithDomainEvent()
    {
        AllTypes.That().Inherit(typeof(IDomainEvent))
            .Should().HaveNameEndingWith("DomainEvent")
            .AssertIsSuccessful();
    }

    [Fact]
    public void DomainDrivenDesign_DomainEventHandlers_ShouldOnlyResideInApplication()
    {
        AllTypes.That().Inherit(typeof(DomainEventHandler<>))
            .Should().ResideInNamespaceStartingWith("PWMS.Application")
            .AssertIsSuccessful();
    }

    [Fact]
    public void DomainDrivenDesign_DomainEventHandlers_ShouldEndWithDomainEventHandler()
    {
        AllTypes.That().Inherit(typeof(DomainEventHandler<>))
            .Should().HaveNameEndingWith("DomainEventHandler")
            .AssertIsSuccessful();
    }
}
