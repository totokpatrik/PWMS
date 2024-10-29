using PWMS.Domain.Addresses.Entities;
using PWMS.Domain.Auth.Entities;

namespace PWMS.Presentation.Rest.Tests.SeedData;
public sealed partial class SeedDataContext
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

    public static User User
    {
        get {
            return new User
            {
                Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                UserName = "Admin",
                Email = "admin@ddd.com",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
        }
    }
}
