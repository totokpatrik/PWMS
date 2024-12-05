using PWMS.Application.Common.Handlers;
using PWMS.Application.Common.Interfaces;
using PWMS.Application.Common.Paging;
using PWMS.Application.Configuration.Inventory.Items.ItemFamilies.Models;
using PWMS.Application.Configuration.Inventory.Items.ItemFamilies.Repositories;
using PWMS.Application.Configuration.Inventory.Items.ItemFamilies.Specifications;

namespace PWMS.Application.Configuration.Inventory.Items.ItemFamilies.Queries.Get;
internal sealed class GetItemFamilyQueryHandler
    : PagingDbQueryHandlerDb<GetItemFamilyQuery, Result<CollectionViewModel<ItemFamilyDto>>, ItemFamilyDto>
{
    private readonly IItemFamilyRepository _itemFamilyRepository;

    public GetItemFamilyQueryHandler(
        IItemFamilyRepository itemFamilyRepository,
        IApplicationDbContext applicationDbContext,
        IMapper mapper,
        ICurrentUserService currentUserService) : base(applicationDbContext, mapper, currentUserService)
    {
        _itemFamilyRepository = itemFamilyRepository.ThrowIfNull();
    }

    public async override Task<Result<CollectionViewModel<ItemFamilyDto>>> Handle(GetItemFamilyQuery request, CancellationToken cancellationToken)
    {
        var specification = ItemFamilySpecification.Create(request.PageContext);

        var entities = await _itemFamilyRepository
            .GetAllItemFamilies(specification, cancellationToken, request.PageContext.Filter);

        var count = await _itemFamilyRepository.CountAsync();

        var dtos = await entities
            .BuildAdapter(Mapper.Config)
            .AdaptToTypeAsync<List<ItemFamilyDto>>()
            .ConfigureAwait(false);

        return Result.Ok(new CollectionViewModel<ItemFamilyDto>(
            dtos, count));
    }
}