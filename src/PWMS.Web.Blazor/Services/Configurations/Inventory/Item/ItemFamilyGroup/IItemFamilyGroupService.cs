using PWMS.Application.Common.Paging;
using PWMS.Application.Configurations.Inventory.Items.ItemFamilyGroups.Models;
using PWMS.Web.Blazor.Models;

namespace PWMS.Web.Blazor.Services.Configurations.Inventory.Item.ItemFamilyGroup;

public interface IItemFamilyGroupService
{
    Task<Result<ItemFamilyGroupDto>> GetAsync(Guid id);
    Task<Result<ItemFamilyGroupDto>> UpdateAsync(UpdateItemFamilyGroupDto updateItemFamilyGroupDto);
    Task<Result<CollectionViewModel<ItemFamilyGroupDto>>> GetPageAsync(PageContext pageContext);
    Task<Result<Guid>> CreateAsync(CreateItemFamilyGroupDto createItemFamilyGroupDto);
    Task<Result<Guid>> DeleteAsync(DeleteItemFamilyGroupDto deleteItemFamilyGroupDto);
    Task<Result<List<Guid>>> DeleteRangeAsync(List<DeleteItemFamilyGroupDto> deleteItemFamilyGroupDtos);
}
