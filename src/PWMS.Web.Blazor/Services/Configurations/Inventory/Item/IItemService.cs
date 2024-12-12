using PWMS.Application.Common.Paging;
using PWMS.Application.Configurations.Inventory.Items.Items.Models;
using PWMS.Web.Blazor.Models;

namespace PWMS.Web.Blazor.Services.Configurations.Inventory.Item;

public interface IItemService
{
    Task<Result<ItemDto>> GetItemAsync(Guid itemId);
    Task<Result<ItemDto>> UpdateItemAsync(UpdateItemDto updateItemDto);
    Task<Result<CollectionViewModel<ItemDto>>> GetItemsAsync(PageContext pageContext);
    Task<Result<Guid>> CreateAsync(CreateItemDto createItemDto);
    Task<Result<Guid>> DeleteAsync(DeleteItemDto deleteItemDto);
    Task<Result<List<Guid>>> DeleteRangeAsync(List<DeleteItemDto> deleteItemDtos);
}
