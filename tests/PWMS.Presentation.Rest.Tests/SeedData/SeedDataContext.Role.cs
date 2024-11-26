using PWMS.Domain.Auth.Entities;

namespace PWMS.Presentation.Rest.Tests.SeedData;
public sealed partial class SeedDataContext
{
    public static Role AdminRole =>
    new Role
    {
        Id = "18721dd6-0da7-401d-8dfc-995d5d0b6645",
        Name = "Admin"
    };

    public static Role SiteOwnerRole =>
    new Role
    {
        Id = "9d4dd8f9-77a4-4047-81df-71344dd3c445",
        Name = PWMS.Application.Common.Identity.Roles.Role.SiteOwner
    };

    public static Role SiteAdminRole =>
    new Role
    {
        Id = "336df8bb-d85d-4b15-9801-6f1b4c03b5e9",
        Name = PWMS.Application.Common.Identity.Roles.Role.SiteAdmin
    };

    public static Role SiteUserRole =>
    new Role
    {
        Id = "dc622705-69db-4265-a426-b117ffed038c",
        Name = PWMS.Application.Common.Identity.Roles.Role.SiteUser
    };

    public static Role WarehouseOwnerRole =>
    new Role
    {
        Id = "74fd0aa4-ff15-4530-8e0e-46b0bf6d7d99",
        Name = PWMS.Application.Common.Identity.Roles.Role.WarehouseOwner
    };

    public static Role WarehouseAdminRole =>
    new Role
    {
        Id = "d0d693b5-6289-487d-8322-e70e2dfa1393",
        Name = PWMS.Application.Common.Identity.Roles.Role.WarehouseAdmin
    };

    public static Role WarehouseUserRole =>
    new Role
    {
        Id = "bb1798b2-50d5-4875-9baf-ea8c3ea8defc",
        Name = PWMS.Application.Common.Identity.Roles.Role.WarehouseUser
    };

    public static List<Role> Roles =>
    new List<Role>
    {
        AdminRole,
        SiteOwnerRole,
        SiteAdminRole,
        SiteUserRole,
        WarehouseOwnerRole,
        WarehouseAdminRole,
        WarehouseUserRole
    };
}
