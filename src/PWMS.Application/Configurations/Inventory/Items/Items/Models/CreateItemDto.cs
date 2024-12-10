namespace PWMS.Application.Configurations.Inventory.Items.Items.Models;

public class CreateItemDto
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ShortDescription { get; set; } = default!;
    public bool ReceiveStatus { get; set; }
}
