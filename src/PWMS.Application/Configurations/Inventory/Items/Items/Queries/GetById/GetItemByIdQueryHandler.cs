using PWMS.Application.Common.Handlers;
using PWMS.Application.Common.Interfaces;
using PWMS.Application.Configurations.Inventory.Items.Items.Models;
using PWMS.Application.Configurations.Inventory.Items.Items.Repositories;
using PWMS.Application.Configurations.Inventory.Items.Items.Specifications;

namespace PWMS.Application.Configurations.Inventory.Items.Items.Queries.GetById;

public sealed class GetItemByIdQueryHandler : HandlerDbQueryBase<GetItemByIdQuery, Result<ItemDto>>
{
    private readonly IItemRepository _itemRepository;

    public GetItemByIdQueryHandler(
        IItemRepository itemRepository,
        IApplicationDbContext contextDb,
        IMapper mapper,
        ICurrentUserService currentUserService) : base(contextDb, mapper, currentUserService)
    {
        _itemRepository = itemRepository.ThrowIfNull();
    }

    public async override Task<Result<ItemDto>> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _itemRepository
            .SingleOrDefaultAsync(new ItemByIdSpecification(request.Id, true), cancellationToken)
            .ConfigureAwait(false);

        entity.ThrowIfNull(new NotFoundException());

        var dtoEntity = await entity
            .BuildAdapter(Mapper.Config)
            .AdaptToTypeAsync<ItemDto>()
            .ConfigureAwait(false);

        return Result.Ok(dtoEntity);
    }
}
