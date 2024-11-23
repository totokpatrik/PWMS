using MudBlazor;
using PWMS.Application.Common.Paging;
using PWMS.Application.Core.Sites.Models;
using PWMS.Domain.Auth.Entities;
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
    public async Task<Result<Guid>> CreateAsync(CreateSiteDto createSiteDto)
    {
        var result = await _httpService.Post<Result<Guid>>("api/v1/sites", createSiteDto);
        if (result.IsSuccess)
        {
            _snackbar.Add("Site created successfully.", Severity.Success);
        }
        else
        {
            _snackbar.Add("There was an error: " + result.Errors, Severity.Error);
        }
        return result;
    }

    public Task<Result<Guid>> DeleteAsync(DeleteSiteDto deleteSiteDto)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<Guid>>> DeleteRangeAsync(List<DeleteSiteDto> deleteSiteDtos)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<SiteDto>> GetSiteAsync(Guid siteId)
    {
        var result = await _httpService.Get<Result<SiteDto>>($"api/v1/sites/{siteId}");
        if (!result.IsSuccess)
        {
            _snackbar.Add("There was an error: " + result.Errors, Severity.Error);
        }
        return result;
    }

    public async Task<Result<CollectionViewModel<SiteDto>>> GetSitesAsync(PageContext pageContext)
    {
        var result = await _httpService.Post<Result<CollectionViewModel<SiteDto>>>("api/v1/sites/page", pageContext);
        return result;
    }

    public async Task<Result<Token>> SelectSiteAsync(SelectSiteDto selectSiteDto)
    {
        var result = await _httpService.Post<Result<Token>>("api/v1/sites/select", selectSiteDto);
        if (result.IsSuccess)
        {
            _snackbar.Add("Site selected successfully.", Severity.Success);
        }
        else
        {
            var errorList = "";
            foreach (var error in result.Errors)
            {
                errorList += error.Message + "\n";
            }
            _snackbar.Add("There was an error: " + errorList, Severity.Error);
        }
        return result;
    }

    public Task<Result<SiteDto>> UpdateSiteAsync(UpdateSiteDto updateSiteDto)
    {
        throw new NotImplementedException();
    }
}
