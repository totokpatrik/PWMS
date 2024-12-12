using MudBlazor;
using PWMS.Application.Common.Paging;
using PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Models;
using PWMS.Web.Blazor.Models;
using PWMS.Web.Blazor.Services.HttpService;

namespace PWMS.Web.Blazor.Services.Configurations.Inventory.Item.ItemFamily;

public class ItemFamilyService : IItemFamilyService
{
    const string baseUrl = "api/v1/configuration/inventory/items/ItemFamilies";

    private readonly IHttpService _httpService;
    private readonly ISnackbar _snackbar;

    public ItemFamilyService(IHttpService httpService, ISnackbar Snackbar)
    {
        _httpService = httpService;
        _snackbar = Snackbar;
    }
    public async Task<Result<Guid>> CreateAsync(CreateItemFamilyDto createItemFamilyDto)
    {
        var result = await _httpService.Post<Result<Guid>>(baseUrl, createItemFamilyDto);
        if (result.IsSuccess)
        {
            _snackbar.Add("Item family created successfully.", Severity.Success);
        }
        else
        {
            _snackbar.Add("There was an error: " + result.Errors, Severity.Error);
        }
        return result;
    }

    public async Task<Result<Guid>> DeleteAsync(DeleteItemFamilyDto deleteItemFamilyDto)
    {
        var result = await _httpService.Delete<Result<Guid>>(baseUrl, deleteItemFamilyDto);
        if (result.IsSuccess)
        {
            _snackbar.Add("Item family deleted successfully.", Severity.Success);
        }
        else
        {
            _snackbar.Add("There was an error: " + result.Errors, Severity.Error);
        }
        return result;
    }

    public async Task<Result<List<Guid>>> DeleteRangeAsync(List<DeleteItemFamilyDto> deleteItemFamilyDtos)
    {
        var result = await _httpService.Delete<Result<List<Guid>>>(baseUrl + "/range", deleteItemFamilyDtos);
        if (result.IsSuccess)
        {
            _snackbar.Add("Item family deleted successfully.", Severity.Success);
        }
        else
        {
            _snackbar.Add("There was an error: " + result.Errors, Severity.Error);
        }
        return result;
    }

    public async Task<Result<ItemFamilyDto>> GetAsync(Guid id)
    {
        var result = await _httpService.Get<Result<ItemFamilyDto>>($"{baseUrl}/{id}");
        if (!result.IsSuccess)
        {
            _snackbar.Add("There was an error: " + result.Errors, Severity.Error);
        }
        return result;
    }

    public async Task<Result<CollectionViewModel<ItemFamilyDto>>> GetPageAsync(PageContext pageContext)
    {
        var result = await _httpService.Post<Result<CollectionViewModel<ItemFamilyDto>>>(baseUrl + "/page", pageContext);
        return result;
    }

    public async Task<Result<ItemFamilyDto>> UpdateAsync(UpdateItemFamilyDto updateItemFamilyDto)
    {
        var result = await _httpService.Put<Result<ItemFamilyDto>>(baseUrl, updateItemFamilyDto);
        if (result.IsSuccess)
        {
            _snackbar.Add("Item family updated successfully.", Severity.Success);
        }
        else
        {
            _snackbar.Add("There was an error: " + result.Errors, Severity.Error);
        }
        return result;
    }
}
