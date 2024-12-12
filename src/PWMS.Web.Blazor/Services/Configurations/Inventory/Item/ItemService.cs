using MudBlazor;
using PWMS.Application.Common.Paging;
using PWMS.Application.Configurations.Inventory.Items.Items.Models;
using PWMS.Web.Blazor.Models;
using PWMS.Web.Blazor.Services.HttpService;

namespace PWMS.Web.Blazor.Services.Configurations.Inventory.Item;

public class ItemService : IItemService
{
    const string baseUrl = "api/v1/configuration/inventory/items/Items";

    private readonly IHttpService _httpService;
    private readonly ISnackbar _snackbar;

    public ItemService(IHttpService httpService, ISnackbar Snackbar)
    {
        _httpService = httpService;
        _snackbar = Snackbar;
    }
    public async Task<Result<Guid>> CreateAsync(CreateItemDto createItemDto)
    {
        var result = await _httpService.Post<Result<Guid>>(baseUrl, createItemDto);
        if (result.IsSuccess)
        {
            _snackbar.Add("Item created successfully.", Severity.Success);
        }
        else
        {
            _snackbar.Add("There was an error: " + result.Errors, Severity.Error);
        }
        return result;
    }

    public async Task<Result<Guid>> DeleteAsync(DeleteItemDto deleteItemDto)
    {
        var result = await _httpService.Delete<Result<Guid>>(baseUrl, deleteItemDto);
        if (result.IsSuccess)
        {
            _snackbar.Add("Item deleted successfully.", Severity.Success);
        }
        else
        {
            _snackbar.Add("There was an error: " + result.Errors, Severity.Error);
        }
        return result;
    }

    public async Task<Result<List<Guid>>> DeleteRangeAsync(List<DeleteItemDto> deleteItemDtos)
    {
        var result = await _httpService.Delete<Result<List<Guid>>>(baseUrl + "/range", deleteItemDtos);
        if (result.IsSuccess)
        {
            _snackbar.Add("Items deleted successfully.", Severity.Success);
        }
        else
        {
            _snackbar.Add("There was an error: " + result.Errors, Severity.Error);
        }
        return result;
    }

    public async Task<Result<ItemDto>> GetItemAsync(Guid itemId)
    {
        var result = await _httpService.Get<Result<ItemDto>>($"{baseUrl}/{itemId}");
        if (!result.IsSuccess)
        {
            _snackbar.Add("There was an error: " + result.Errors, Severity.Error);
        }
        return result;
    }

    public async Task<Result<CollectionViewModel<ItemDto>>> GetItemsAsync(PageContext pageContext)
    {
        var result = await _httpService.Post<Result<CollectionViewModel<ItemDto>>>(baseUrl + "/page", pageContext);
        return result;
    }

    public async Task<Result<ItemDto>> UpdateItemAsync(UpdateItemDto updateItemDto)
    {
        var result = await _httpService.Put<Result<ItemDto>>(baseUrl, updateItemDto);
        if (result.IsSuccess)
        {
            _snackbar.Add("Item updated successfully.", Severity.Success);
        }
        else
        {
            _snackbar.Add("There was an error: " + result.Errors, Severity.Error);
        }
        return result;
    }
}
