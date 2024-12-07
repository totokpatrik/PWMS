namespace PWMS.Application.Configuration.Inventory.Items.Items.Models;

public class UpdateItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ShortDescription { get; set; } = default!;
    public bool ReceiveStatus { get; set; }
}
