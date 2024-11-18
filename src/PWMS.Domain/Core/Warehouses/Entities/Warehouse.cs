using PWMS.Domain.Auth.Entities;
using PWMS.Domain.Common;
using PWMS.Domain.Core.Sites.Entities;
using System.Diagnostics.CodeAnalysis;

namespace PWMS.Domain.Core.Warehouses.Entities;

public class Warehouse : BaseAuditableEntity<Guid, string>, IAggregateRoot
{
    [ExcludeFromCodeCoverage]
    public Warehouse() : base(Guid.NewGuid())
    {
        Name = string.Empty;
        Site = new Site();
        Users = new List<User>();
        Admins = new List<User>();
        Owner = new User();
    }
    public string Name { get; }
    public Site Site { get; } = new Site();
    public ICollection<User>? Users { get; }
    public ICollection<User>? Admins { get; }
    public User Owner { get; set; }
    public Warehouse(Guid id, string name, Site site, User owner) : base(id)
    {
        Name = name;
        Site = site;
        Owner = owner;
    }
    public Warehouse(string name, Site site, User owner) : base(Guid.NewGuid())
    {
        Name = name;
        Site = site;
        Owner = owner;
    }
}
