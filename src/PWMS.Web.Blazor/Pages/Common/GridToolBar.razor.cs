using Microsoft.AspNetCore.Components;

namespace PWMS.Web.Blazor.Pages.Common;

public partial class GridToolBar
{
    [Parameter] public EventCallback RefreshGrid { get; set; }
    [Parameter] public int PageSize { get; set; }
    [Parameter] public EventCallback<int> PageSizeChanged { get; set; }
    [Parameter] public int PageIndex { get; set; }
    [Parameter] public EventCallback<int> PageIndexChanged { get; set; }
    [Parameter] public int TotalItems { get; set; }

    int[] _pageSizeOptions = [10, 25, 50, 100];

    bool _firstPageButtonDisabled;
    bool _navigateBeforeButtonDisabled;
    bool _navigateNextButtonDisabled;
    bool _lastPageButtonDisabled;
    protected override bool ShouldRender()
    {
        if (PageIndex == 1)
        {
            _firstPageButtonDisabled = true;
            _navigateBeforeButtonDisabled = true;
        }
        else
        {
            _firstPageButtonDisabled = false;
            _navigateBeforeButtonDisabled = false;
        }

        if (PageIndex * PageSize >= TotalItems)
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

    private async Task NavigateToFirstPageAsync()
    {
        PageIndex = 1;
        await PageIndexChanged.InvokeAsync(PageIndex);
        await RefreshGrid.InvokeAsync();
    }
    private async Task NavigateToPreviousPage()
    {
        PageIndex--;
        await PageIndexChanged.InvokeAsync(PageIndex);
        await RefreshGrid.InvokeAsync();
    }
    private async Task NavigateToLastPage()
    {
        PageIndex = (int)Math.Ceiling((decimal)(TotalItems / (decimal)PageSize));
        await PageIndexChanged.InvokeAsync(PageIndex);
        await RefreshGrid.InvokeAsync();
    }
    private async Task NavigateToNextPage()
    {
        PageIndex++;
        await PageIndexChanged.InvokeAsync(PageIndex);
        await RefreshGrid.InvokeAsync();
    }
    private async Task PageSizeChangedAsync(int pageSize)
    {
        PageSize = pageSize;
        await PageSizeChanged.InvokeAsync(pageSize);
        await RefreshGrid.InvokeAsync();
    }
}