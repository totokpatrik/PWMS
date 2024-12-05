using PWMS.Application.Addresses.Commands.Create;
using PWMS.Application.Addresses.Repositories;
using PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Repositories;
using PWMS.Domain.Addresses.Entities;
using PWMS.Domain.Inventories.Items.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Commands.Create;

public sealed class CreateItemFamilyGroupCommandHandler(IItemFamilyGroupRepository itemFamilyGroupRepository)
    : IRequestHandler<CreateItemFamilyGroupCommand, Result<Guid>>
{
    private readonly IItemFamilyGroupRepository _itemFamilyGroupRepository = itemFamilyGroupRepository.ThrowIfNull();

    public async Task<Result<Guid>> Handle(CreateItemFamilyGroupCommand request, CancellationToken cancellationToken)
    {
        var entity = new ItemFamilyGroup(request.Name, request.Description);

        await _itemFamilyGroupRepository
            .AddAsync(entity, cancellationToken);

        await _itemFamilyGroupRepository
            .SaveChangesAsync(cancellationToken);

        return Result.Ok(entity.Id);
    }
}
