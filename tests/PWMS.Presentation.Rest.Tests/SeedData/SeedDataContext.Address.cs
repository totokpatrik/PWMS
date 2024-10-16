using PWMS.Domain.Addresses.Entities;

namespace PWMS.Presentation.Rest.Tests.SeedData;
internal sealed partial class SeedDataContext
{
    public static IEnumerable<Address> Addresses
    {
        get
        {
            yield return new Address("TestAddress1");
            yield return new Address("TestAddress2");
            yield return new Address("TestAddress3");
            yield return new Address("TestAddress4");
            yield return new Address("TestAddress5");
        }
    }
}
