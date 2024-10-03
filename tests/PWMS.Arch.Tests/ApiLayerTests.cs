using Microsoft.AspNetCore.Mvc;
using NetArchTest.Rules;
using PWMS.Arch.Tests.Extensions;

namespace PWMS.Arch.Tests;

[Collection("Sequential")]
public class ApiLayerTests : BaseTests
{
    [Fact]
    public void Api_Controllers_ShouldOnlyResideInApi()
    {
        AllTypes.That().Inherit(typeof(ControllerBase))
            .Should().ResideInNamespaceStartingWith("PWMS.Api")
            .AssertIsSuccessful();
    }

    [Fact]
    public void Api_Controllers_ShouldInheritFromControllerBase()
    {
        Types.InAssembly(ApiAssembly)
            .That().HaveNameEndingWith("Controller")
            .And().DoNotHaveName("VersionController")
            .Should().Inherit(typeof(ControllerBase))
            .AssertIsSuccessful();
    }

    [Fact]
    public void Api_Controllers_ShouldEndWithController()
    {
        AllTypes.That().Inherit(typeof(ControllerBase))
            .Should().HaveNameEndingWith("Controller")
            .AssertIsSuccessful();
    }
}
