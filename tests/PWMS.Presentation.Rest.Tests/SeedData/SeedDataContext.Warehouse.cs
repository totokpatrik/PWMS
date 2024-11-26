using PWMS.Domain.Auth.Entities;
using PWMS.Domain.Core.Sites.Entities;
using PWMS.Domain.Core.Warehouses.Entities;

namespace PWMS.Presentation.Rest.Tests.SeedData;
public sealed partial class SeedDataContext
{
    public static Warehouse Warehouse(Site site, User user) =>
    new Warehouse(WarehouseName, site, user);
    public static string WarehouseName => "TestWarehouse";
}
