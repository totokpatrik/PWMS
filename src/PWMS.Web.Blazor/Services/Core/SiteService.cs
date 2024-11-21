using MudBlazor;
using PWMS.Application.Common.Paging;
using PWMS.Application.Core.Sites.Models;
using PWMS.Web.Blazor.Models;
using PWMS.Web.Blazor.Services.HttpService;

namespace PWMS.Web.Blazor.Services.Core;

public class SiteService : ISiteService
{
    private readonly IHttpService _httpService;
    private readonly ISnackbar _snackbar;

    public SiteService(IHttpService httpService, ISnackbar Snackbar)
    {
        _httpService = httpService;
        _snackbar = Snackbar;
    }
    public Task<Result<Guid>> CreateAsync(CreateSiteDto createSiteDto)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Guid>> DeleteAsync(DeleteSiteDto deleteSiteDto)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<Guid>>> DeleteRangeAsync(List<DeleteSiteDto> deleteSiteDtos)
    {
        throw new NotImplementedException();
    }

    public Task<Result<SiteDto>> GetSiteAsync(Guid siteId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<CollectionViewModel<SiteDto>>> GetSitesAsync(PageContext pageContext)
    {
        var result = await _httpService.Post<Result<CollectionViewModel<SiteDto>>>("api/v1/sites/page", pageContext);
        return result;
    }

    public Task<Result<SiteDto>> UpdateSiteAsync(UpdateSiteDto updateSiteDto)
    {
        throw new NotImplementedException();
    }
}
