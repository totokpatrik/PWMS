using PWMS.Application.Common.Handlers;
using PWMS.Application.Common.Interfaces;
using PWMS.Application.Common.Paging;
using PWMS.Application.Configurations.Inventory.Items.Items.Models;
using PWMS.Application.Configurations.Inventory.Items.Items.Repositories;
using PWMS.Application.Configurations.Inventory.Items.Items.Specifications;

namespace PWMS.Application.Configurations.Inventory.Items.Items.Queries.Get;

public sealed class GetItemQueryHandler
    : PagingDbQueryHandlerDb<GetItemQuery, Result<CollectionViewModel<ItemDto>>, ItemDto>
{
    private readonly IItemRepository _itemRepository;

    public GetItemQueryHandler(
        IItemRepository itemRepository,
        IApplicationDbContext applicationDbContext,
        IMapper mapper,
        ICurrentUserService currentUserService) : base(applicationDbContext, mapper, currentUserService)
    {
        _itemRepository = itemRepository.ThrowIfNull();
    }

    public async override Task<Result<CollectionViewModel<ItemDto>>> Handle(GetItemQuery request, CancellationToken cancellationToken)
    {
        var specification = ItemSpecification.Create(request.PageContext);

        var entities = await _itemRepository
            .GetAllItems(specification, cancellationToken, request.PageContext.Filter);

        var count = await _itemRepository.CountAsync();

        var dtos = await entities
            .BuildAdapter(Mapper.Config)
            .AdaptToTypeAsync<List<ItemDto>>()
            .ConfigureAwait(false);

        return Result.Ok(new CollectionViewModel<ItemDto>(
            dtos, count));
    }
}
