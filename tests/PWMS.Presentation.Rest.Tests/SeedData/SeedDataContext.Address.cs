using PWMS.Domain.Addresses.Entities;

namespace PWMS.Presentation.Rest.Tests.SeedData;
internal sealed partial class SeedDataContext
{
    public static IEnumerable<Address> Addresses
    {
        get
        {
            yield return new Address(Guid.Parse("4B178375-845F-4D84-9E5B-31A14F097AA1"), "TestAddress1");
            yield return new Address("TestAddress2");
            yield return new Address("TestAddress3");
            yield return new Address("TestAddress4");
            yield return new Address("TestAddress5");
        }
    }
}
