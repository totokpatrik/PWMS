namespace PWMS.Domain.Inventories.Items.Entities;

public class ItemFamilyGroup
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public ICollection<ItemFamily> ItemFamilies { get; set; }
}
