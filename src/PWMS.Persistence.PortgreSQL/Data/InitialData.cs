using PWMS.Domain.Auth.Entities;
using PWMS.Domain.Core.Sites.Entities;
using PWMS.Domain.Core.Warehouses.Entities;

namespace PWMS.Persistence.PortgreSQL.Data;

public static class InitialData
{
    public static User User =>
    new User
    {
        Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
        UserName = "Admin",
        Email = "admin@ddd.com",
        EmailConfirmed = true,
        SecurityStamp = Guid.NewGuid().ToString("D")
    };

    /*
    public static IdentityRole Role =>
        new IdentityRole
        {
            Id = "18721dd6-0da7-401d-8dfc-995d5d0b6645",
            Name = "Admin"
        };
    */
}
