using Microsoft.AspNetCore.Identity;
using PWMS.Domain.Core.Sites.Entities;
using PWMS.Domain.Core.Warehouses.Entities;

namespace PWMS.Domain.Auth.Entities;

public class User : IdentityUser
{
    public ICollection<Warehouse> UserWarehouses { get; set; } = new List<Warehouse>();
    public ICollection<Warehouse> AdminWarehouses { get; set; } = new List<Warehouse>();
    public ICollection<Site> UserSites { get; set; } = new List<Site>();
    public ICollection<Site> AdminSites { get; set; } = new List<Site>();

}
