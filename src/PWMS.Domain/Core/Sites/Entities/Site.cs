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
        Owner = new User();
    }
    public string Name { get; }
    public List<Warehouse> Warehouses { get; }
    public ICollection<User> Users { get; }
    public ICollection<User> Admins { get; }
    public User Owner { get; }
    public Site(Guid id, string name, List<Warehouse> warehouses, List<User> users, List<User> admins, User owner) : base(id)
    {
        Name = name;
        Warehouses = warehouses;
        Users = users;
        Admins = admins;
        Owner = owner;
    }
    public Site(string name, List<Warehouse> warehouses, List<User> users, List<User> admins, User owner) : base(Guid.NewGuid())
    {
        Name = name;
        Warehouses = warehouses;
        Users = users;
        Admins = admins;
        Owner = owner;
    }
}