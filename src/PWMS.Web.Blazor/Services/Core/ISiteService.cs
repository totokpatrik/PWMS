using PWMS.Application.Common.Paging;
using PWMS.Application.Core.Sites.Models;
using PWMS.Domain.Auth.Entities;
using PWMS.Web.Blazor.Models;

namespace PWMS.Web.Blazor.Services.Core;

public interface ISiteService
{
    Task<Result<SiteDto>> GetSiteAsync(Guid siteId);
    Task<Result<SiteDto>> UpdateSiteAsync(UpdateSiteDto updateSiteDto);
    Task<Result<CollectionViewModel<SiteDto>>> GetSitesAsync(PageContext pageContext);
    Task<Result<Guid>> CreateAsync(CreateSiteDto createSiteDto);
    Task<Result<Guid>> DeleteAsync(DeleteSiteDto deleteSiteDto);
    Task<Result<List<Guid>>> DeleteRangeAsync(List<DeleteSiteDto> deleteSiteDtos);
    Task<Result<Token>> SelectSiteAsync(SelectSiteDto selectSiteDto);
}
