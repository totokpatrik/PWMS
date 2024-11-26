using PWMS.Domain.Auth.Entities;

namespace PWMS.Presentation.Rest.Tests.SeedData;
public sealed partial class SeedDataContext
{
    public static User AdminUser =>
    new User
    {
        Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
        UserName = "TestAdmin",
        Email = "admin@ddd.com",
        EmailConfirmed = true,
        SecurityStamp = Guid.NewGuid().ToString("D")
    };

    public static List<User> Users =>
    new List<User>
    {
        AdminUser
    };
}
