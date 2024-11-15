using PWMS.Application.Addresses.Models;
using PWMS.Application.Common.Paging;
using PWMS.Web.Blazor.Models;

namespace PWMS.Web.Blazor.Services.Configuration;

public interface IAddressService
{
    Task<Result<CollectionViewModel<AddressDto>>> GetAddressesAsync(PageContext pageContext);
    Task<Result<Guid>> DeleteAsync(DeleteAddressDto deleteAddressDto);
    Task<Result<List<Guid>>> DeleteRangeAsync(List<Guid> ids);
}
