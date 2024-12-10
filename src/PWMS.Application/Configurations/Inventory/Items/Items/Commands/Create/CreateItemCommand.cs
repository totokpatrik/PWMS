namespace PWMS.Application.Configurations.Inventory.Items.Items.Commands.Create;

public sealed record CreateItemCommand(string Name,
                                       string Description,
                                       string ShortDescription,
                                       bool ReceiveStatus,
                                       Guid ItemFamilyId) : IRequest<Result<Guid>>;