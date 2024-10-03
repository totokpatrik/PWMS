namespace PWMS.Application.Abstractions.Models;

public abstract class BaseDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}
