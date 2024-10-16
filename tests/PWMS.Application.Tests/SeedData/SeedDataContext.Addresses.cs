using PWMS.Domain.Addresses.Entities;

namespace PWMS.Application.Tests.SeedData;

public sealed partial class SeedDataContext
{
    public static IEnumerable<Address> Addresses
    {
        get
        {
            yield return new Address("TestAddress_1");
            yield return new Address("TestAddress_2");
            yield return new Address("TestAddress_3");
            yield return new Address("TestAddress_4");
            yield return new Address("TestAddress_5");
        }
    }
}
