using PWMS.Application.Common.Paging;
using PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Models;
using PWMS.Web.Blazor.Models;

namespace PWMS.Web.Blazor.Services.Configurations.Inventory.Item.ItemFamily;

public interface IItemFamilyService
{
    Task<Result<ItemFamilyDto>> GetAsync(Guid id);
    Task<Result<ItemFamilyDto>> UpdateAsync(UpdateItemFamilyDto updateItemFamilyDto);
    Task<Result<CollectionViewModel<ItemFamilyDto>>> GetPageAsync(PageContext pageContext);
    Task<Result<Guid>> CreateAsync(CreateItemFamilyDto createItemFamilyDto);
    Task<Result<Guid>> DeleteAsync(DeleteItemFamilyDto deleteItemFamilyDto);
    Task<Result<List<Guid>>> DeleteRangeAsync(List<DeleteItemFamilyDto> deleteItemFamilyDtos);
}
