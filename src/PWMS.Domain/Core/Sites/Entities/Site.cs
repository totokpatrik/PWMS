using PWMS.Domain.Auth.Entities;
using PWMS.Domain.Common;
using PWMS.Domain.Core.Warehouses.Entities;
using System.Diagnostics.CodeAnalysis;

namespace PWMS.Domain.Core.Sites.Entities;

public class Site : BaseAuditableEntity<Guid, string>, IAggregateRoot
{
    [ExcludeFromCodeCoverage]
    public Site() : base(Guid.NewGuid())
    {
        Name = string.Empty;
        Warehouses = new List<Warehouse>();
        Users = new List<User>();
        Admins = new List<User>();
        Owner = default!;
    }
    public string Name { get; }
    public List<Warehouse>? Warehouses { get; }
    public ICollection<User>? Users { get; }
    public ICollection<User>? Admins { get; }
    public ICollection<User>? UsersSelected { get; set; }
    public User Owner { get; }
    public Site(Guid id, string name, User owner) : base(id)
    {
        Name = name;
        Owner = owner;
    }
    public Site(string name, User owner) : base(Guid.NewGuid())
    {
        Name = name;
        Owner = owner;
    }
}