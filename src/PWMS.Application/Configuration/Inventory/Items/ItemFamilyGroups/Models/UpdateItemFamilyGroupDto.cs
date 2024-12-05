namespace PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Models;

public class UpdateItemFamilyGroupDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}
