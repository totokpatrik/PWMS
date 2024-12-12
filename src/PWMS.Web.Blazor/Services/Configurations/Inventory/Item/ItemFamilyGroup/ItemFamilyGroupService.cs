using MudBlazor;
using PWMS.Application.Common.Paging;
using PWMS.Application.Configurations.Inventory.Items.ItemFamilyGroups.Models;
using PWMS.Web.Blazor.Models;
using PWMS.Web.Blazor.Services.HttpService;

namespace PWMS.Web.Blazor.Services.Configurations.Inventory.Item.ItemFamilyGroup;

public class ItemFamilyGroupService : IItemFamilyGroupService
{
    const string baseUrl = "api/v1/configuration/inventory/items/ItemFamilyGroups";

    private readonly IHttpService _httpService;
    private readonly ISnackbar _snackbar;

    public ItemFamilyGroupService(IHttpService httpService, ISnackbar Snackbar)
    {
        _httpService = httpService;
        _snackbar = Snackbar;
    }
    public async Task<Result<Guid>> CreateAsync(CreateItemFamilyGroupDto createItemFamilyGroupDto)
    {
        var result = await _httpService.Post<Result<Guid>>(baseUrl, createItemFamilyGroupDto);
        if (result.IsSuccess)
        {
            _snackbar.Add("Item family group created successfully.", Severity.Success);
        }
        else
        {
            _snackbar.Add("There was an error: " + result.Errors, Severity.Error);
        }
        return result;
    }

    public async Task<Result<Guid>> DeleteAsync(DeleteItemFamilyGroupDto deleteItemFamilyGroupDto)
    {
        var result = await _httpService.Delete<Result<Guid>>(baseUrl, deleteItemFamilyGroupDto);
        if (result.IsSuccess)
        {
            _snackbar.Add("Item family group deleted successfully.", Severity.Success);
        }
        else
        {
            _snackbar.Add("There was an error: " + result.Errors, Severity.Error);
        }
        return result;
    }

    public async Task<Result<List<Guid>>> DeleteRangeAsync(List<DeleteItemFamilyGroupDto> deleteItemFamilyGroupDtos)
    {
        var result = await _httpService.Delete<Result<List<Guid>>>(baseUrl + "/range", deleteItemFamilyGroupDtos);
        if (result.IsSuccess)
        {
            _snackbar.Add("Item family groups deleted successfully.", Severity.Success);
        }
        else
        {
            _snackbar.Add("There was an error: " + result.Errors, Severity.Error);
        }
        return result;
    }

    public async Task<Result<ItemFamilyGroupDto>> GetAsync(Guid id)
    {
        var result = await _httpService.Get<Result<ItemFamilyGroupDto>>($"{baseUrl}/{id}");
        if (!result.IsSuccess)
        {
            _snackbar.Add("There was an error: " + result.Errors, Severity.Error);
        }
        return result;
    }

    public async Task<Result<CollectionViewModel<ItemFamilyGroupDto>>> GetPageAsync(PageContext pageContext)
    {
        var result = await _httpService.Post<Result<CollectionViewModel<ItemFamilyGroupDto>>>(baseUrl + "/page", pageContext);
        return result;
    }

    public async Task<Result<ItemFamilyGroupDto>> UpdateAsync(UpdateItemFamilyGroupDto updateItemFamilyGroupDto)
    {
        var result = await _httpService.Put<Result<ItemFamilyGroupDto>>(baseUrl, updateItemFamilyGroupDto);
        if (result.IsSuccess)
        {
            _snackbar.Add("Item family group updated successfully.", Severity.Success);
        }
        else
        {
            _snackbar.Add("There was an error: " + result.Errors, Severity.Error);
        }
        return result;
    }
}
