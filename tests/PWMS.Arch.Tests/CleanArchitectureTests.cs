using Ardalis.Specification;
using PWMS.Arch.Tests.Extensions;

namespace PWMS.Arch.Tests;

[Collection("Sequential")]
public class CleanArchitectureTests : BaseTests
{
    [Fact]
    public void CleanArchitecture_Layers_ApplicationDoesNotReferenceInfrastructure()
    {
        AllTypes.That().ResideInNamespace("PWMS.Application")
        .ShouldNot().HaveDependencyOn("PWMS.Infrastructure")
        .AssertIsSuccessful();
    }

    [Fact]
    public void CleanArchitecture_Layers_CoreDoesNotReferenceOuter()
    {
        var coreTypes = AllTypes.That().ResideInNamespace("PWMS.Domain");

        coreTypes.ShouldNot().HaveDependencyOn("PWMS.Infrastructure")
            .AssertIsSuccessful();

        coreTypes.ShouldNot().HaveDependencyOn("PWMS.Application")
            .AssertIsSuccessful();
    }

    [Fact]
    public void CleanArchitecture_Repositories_OnlyInInfrastructure()
    {
        AllTypes.That().HaveNameEndingWith("Repository")
        .And().AreClasses()
        .Should().ResideInNamespaceStartingWith("PWMS.Infrastructure")
        .AssertIsSuccessful();

        AllTypes.That().HaveNameEndingWith("Repository")
            .And().AreClasses()
            .Should().ImplementInterface(typeof(IRepositoryBase<>))
            .AssertIsSuccessful();
    }

    [Fact]
    public void CleanArchitecture_Repositories_ShouldEndWithRepository()
    {
        AllTypes.That().Inherit(typeof(IRepositoryBase<>))
            .Should().HaveNameEndingWith("Repository")
            .AssertIsSuccessful();
    }
}