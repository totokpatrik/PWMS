using PWMS.Application.Addresses.Models;
using PWMS.Application.Addresses.Repositories;
using PWMS.Application.Addresses.Specifications;
using PWMS.Application.Common.Handlers;
using PWMS.Application.Common.Interfaces;
using PWMS.Application.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMS.Application.Addresses.Queries.Get;

internal sealed class GetAddressQueryHandler
    : PagingDbQueryHandlerDb<GetAddressQuery, Result<CollectionViewModel<AddressDto>>, AddressDto>
{
    private readonly IAddressRepository _addressRepository;

    public GetAddressQueryHandler(
        IAddressRepository addressRepository,
        IApplicationDbContext applicationDbContext,
        IMapper mapper,
        ICurrentUserService currentUserService) : base(applicationDbContext, mapper, currentUserService)
    {
        _addressRepository = addressRepository;
    }

    public async override Task<Result<CollectionViewModel<AddressDto>>> Handle(GetAddressQuery request, CancellationToken cancellationToken)
    {
        var specification = AddressSpecification.Create(request.PageContext);

        var entities = await _addressRepository
            .ListAsync(specification, cancellationToken)
            .ConfigureAwait(false);

        var dtoAddresses = await entities
            .BuildAdapter(Mapper.Config)
            .AdaptToTypeAsync<List<AddressDto>>()
            .ConfigureAwait(false);

        return Result.Ok(new CollectionViewModel<AddressDto>(
            dtoAddresses, dtoAddresses.Count));
    }
}
