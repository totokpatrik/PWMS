using PWMS.Application.Common.Paging;
using PWMS.Application.Core.Sites.Models;
using PWMS.Web.Blazor.Models;

namespace PWMS.Web.Blazor.Services.Core;

public class SiteService : ISiteService
{
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

    public Task<Result<CollectionViewModel<SiteDto>>> GetSitesAsync(PageContext pageContext)
    {
        throw new NotImplementedException();
    }

    public Task<Result<SiteDto>> UpdateSiteAsync(UpdateSiteDto updateSiteDto)
    {
        throw new NotImplementedException();
    }
}
