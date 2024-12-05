namespace PWMS.Application.Configuration.Inventory.Items.ItemFamilies.Models;

public class UpdateItemFamilyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid ItemFamilyGroupId { get; set; }
}
