using Microsoft.AspNetCore.Identity;
using PWMS.Domain.Core.Sites.Entities;
using PWMS.Domain.Core.Warehouses.Entities;

namespace PWMS.Domain.Auth.Entities;

public class User : IdentityUser
{
    public ICollection<Warehouse>? UserWarehouses { get; set; }
    public ICollection<Warehouse>? AdminWarehouses { get; set; }
    public ICollection<Site>? UserSites { get; set; }
    public ICollection<Site>? AdminSites { get; set; }
    public Site? SelectedSite { get; set; }

    public void SelectSite(Site site)
    {
        SelectedSite = site;
    }
}
