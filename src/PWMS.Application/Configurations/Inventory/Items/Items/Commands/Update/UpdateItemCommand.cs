﻿using PWMS.Application.Configurations.Inventory.Items.Items.Models;

namespace PWMS.Application.Configurations.Inventory.Items.Items.Commands.Update;

public sealed record UpdateItemCommand(Guid Id, string Name, string Description, string ShortDescription, bool ReceiveStatus, Guid ItemFamilyId)
    : IRequest<Result<ItemDto>>;