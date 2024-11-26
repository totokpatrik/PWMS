using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using PWMS.Application.Common.Paging;
using PWMS.Application.Core.Sites.Models;
using PWMS.Web.Blazor.Models;
using PWMS.Web.Blazor.Services.Core;

namespace PWMS.Web.Blazor.Pages.Core.Site;

public partial class IndexSite
{
    [Inject] ISiteService SiteService { get; set; } = default!;
    [Inject] ISnackbar Snackbar { get; set; } = default!;
    [Inject] NavigationManager NavigationManager { get; set; } = default!;
    [Inject] ILocalStorageService LocalStorage { get; set; } = default!;
    [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

    MudDataGrid<SiteDto> dataGrid = new();
    HashSet<SiteDto> selectedSites = new HashSet<SiteDto>();
    int _pageSize = 10;
    int _pageIndex = 1;
    int _totalItems = 0;
    int[] _pageSizeOptions = [10, 25, 50, 100];
    bool _loading = false;

    bool _firstPageButtonDisabled;
    bool _navigateBeforeButtonDisabled;
    bool _navigateNextButtonDisabled;
    bool _lastPageButtonDisabled;

    bool deleteDisabled = true;
    bool selectDisabled = true;

    string currentUserId = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        currentUserId = (await AuthenticationStateProvider.GetAuthenticationStateAsync())
            .User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value ?? string.Empty;
        await GetSitesAsync();
    }
    private Task PageSizeChangedAsync(int pageSize)
    {
        _pageSize = pageSize;
        return dataGrid.ReloadServerData();
    }

    private async Task<Result<CollectionViewModel<SiteDto>>> GetSitesAsync()
    {
        _loading = true;
        var result = await SiteService.GetSitesAsync(new Application.Common.Paging.PageContext(_pageIndex, _pageSize));

        if (result.IsSuccess)
        {
            _totalItems = result.Data.TotalCount;
            _loading = false;
        }
        return result;
    }
    private async Task<GridData<SiteDto>> ServerReload(GridState<SiteDto> state)
    {
        var result = await GetSitesAsync();
        return new GridData<SiteDto>
        {
            TotalItems = result.Data.TotalCount,
            Items = result.Data.Data
        };
    }
    protected override bool ShouldRender()
    {
        deleteDisabled = selectedSites.Count < 1;
        selectDisabled = selectedSites.Count != 1;
        if (_pageIndex == 1)
        {
            _firstPageButtonDisabled = true;
            _navigateBeforeButtonDisabled = true;
        }
        else
        {
            _firstPageButtonDisabled = false;
            _navigateBeforeButtonDisabled = false;
        }

        if (_pageIndex * _pageSize >= _totalItems)
        {
            _navigateNextButtonDisabled = true;
            _lastPageButtonDisabled = true;
        }
        else
        {
            _navigateNextButtonDisabled = false;
            _lastPageButtonDisabled = false;
        }


        return base.ShouldRender();
    }
    async void Delete()
    {
        _loading = true;
        if (selectedSites.Count > 1)
        {
            List<DeleteSiteDto> deleteSiteDtos = new List<DeleteSiteDto>();
            foreach (var item in selectedSites)
            {
                var deleteSiteDto = new DeleteSiteDto() { Id = item.Id };
                deleteSiteDtos.Add(deleteSiteDto);
            }
            await SiteService.DeleteRangeAsync(deleteSiteDtos);
        }
        else
        {
            await SiteService.DeleteAsync(new DeleteSiteDto { Id = selectedSites.Select(a => a.Id).First() });
        }
        await GetSitesAsync();

        // clear selection
        selectedSites.Clear();

        // reload data grid
        await dataGrid.ReloadServerData();
        StateHasChanged();

        _loading = false;
    }
    void Add()
    {
        NavigationManager.NavigateTo("/site/create");
    }
    async Task SelectAsync()
    {
        var selectSiteResult = await SiteService.SelectSiteAsync(new SelectSiteDto { SiteId = selectedSites.First().Id });
        if (selectSiteResult.IsSuccess)
        {
            await LocalStorage.SetItemAsync("authToken", selectSiteResult.Data.TokenString);
            await AuthenticationStateProvider.GetAuthenticationStateAsync();
        }
        // clear selection
        selectedSites.Clear();

        // reload data grid
        await dataGrid.ReloadServerData();
        StateHasChanged();
        NavigationManager.Refresh(true);
    }
    private async Task NavigateToFirstPageAsync()
    {
        _pageIndex = 1;
        await dataGrid.ReloadServerData();
    }
    private async Task NavigateToPreviousPage()
    {
        _pageIndex--;
        await dataGrid.ReloadServerData();
    }
    private async Task NavigateToLastPage()
    {
        _pageIndex = (int)Math.Ceiling((decimal)(_totalItems / (decimal)_pageSize));
        await dataGrid.ReloadServerData();
    }
    private async Task NavigateToNextPage()
    {
        _pageIndex++;
        await dataGrid.ReloadServerData();

    }
}