using PWMS.Application.Common.Handlers;
using PWMS.Application.Common.Interfaces;
using PWMS.Application.Common.Paging;
using PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Models;
using PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Repositories;
using PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Specifications;
using System.Globalization;

namespace PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Queries.Get;

internal sealed class GetItemFamilyGroupQueryHandler
    : PagingDbQueryHandlerDb<GetItemFamilyGroupQuery, Result<CollectionViewModel<ItemFamilyGroupDto>>, ItemFamilyGroupDto>
{
    private readonly IItemFamilyGroupRepository _itemFamilyGroupRepository;

    public GetItemFamilyGroupQueryHandler(
        IItemFamilyGroupRepository itemFamilyGroupRepository,
        IApplicationDbContext applicationDbContext,
        IMapper mapper,
        ICurrentUserService currentUserService) : base(applicationDbContext, mapper, currentUserService)
    {
        _itemFamilyGroupRepository = itemFamilyGroupRepository.ThrowIfNull();
    }

    public async override Task<Result<CollectionViewModel<ItemFamilyGroupDto>>> Handle(GetItemFamilyGroupQuery request, CancellationToken cancellationToken)
    {
        var specification = ItemFamilyGroupSpecification.Create(request.PageContext);

        var entities = await _itemFamilyGroupRepository
            .GetAllItemFamilyGroups(specification, cancellationToken, request.PageContext.Filter);

        var count = await _itemFamilyGroupRepository.CountAsync();

        var currentCulture = CultureInfo.CurrentCulture;

        var dtoItemFamilyGroups = await entities
            .BuildAdapter(Mapper.Config)
            .AdaptToTypeAsync<List<ItemFamilyGroupDto>>()
            .ConfigureAwait(false);

        return Result.Ok(new CollectionViewModel<ItemFamilyGroupDto>(
            dtoItemFamilyGroups, count));
    }
}
