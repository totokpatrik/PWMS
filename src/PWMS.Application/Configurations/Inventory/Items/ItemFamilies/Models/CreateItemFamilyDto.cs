namespace PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Models;

public class CreateItemFamilyDto
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid ItemfamilyGroupId { get; set; }
}
