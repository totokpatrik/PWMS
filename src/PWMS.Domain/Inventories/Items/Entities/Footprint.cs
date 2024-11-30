namespace PWMS.Domain.Inventories.Items.Entities;

public class Footprint
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Item Item { get; set; } = default!;
    public List<FootprintDetail> FootprintDetails { get; set; } = default!;
}
