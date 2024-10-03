using NetArchTest.Rules;
using PWMS.Domain.Abstractions.Entities;
using System.Reflection;

namespace PWMS.Arch.Tests
{
    public abstract class BaseTests
    {
        protected static Assembly ApiAssembly = typeof(Api.Controllers.Version10.AddressesController).Assembly;
        protected static Assembly ApplicationAssembly = typeof(Application.DependencyInjection).Assembly;
        protected static Assembly InfrastuctureAssembly = typeof(Infrastructure.DependencyInjection).Assembly;
        protected static Assembly CoreAssembly = typeof(Entity).Assembly;
        protected static Types AllTypes = Types.InAssemblies(new List<Assembly> { ApiAssembly, ApplicationAssembly, InfrastuctureAssembly, CoreAssembly });
    }
}
