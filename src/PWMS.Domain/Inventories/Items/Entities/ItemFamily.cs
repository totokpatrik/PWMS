namespace PWMS.Domain.Inventories.Items.Entities;

public class ItemFamily
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public ItemFamilyGroup ItemFamilyGroup { get; set; } = default!;
}
