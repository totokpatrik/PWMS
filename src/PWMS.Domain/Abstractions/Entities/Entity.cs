
namespace PWMS.Domain.Abstractions.Entities;

public abstract class Entity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}
