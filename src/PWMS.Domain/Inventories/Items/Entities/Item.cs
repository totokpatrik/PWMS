namespace PWMS.Domain.Inventories.Items.Entities;

public class Item
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ShortDescription { get; set; } = default!;
    public ItemFamily ItemFamily { get; set; } = default!;
    public bool ReceiveStatus { get; set; } = false;
}
