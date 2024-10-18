using PWMS.Application.Addresses.Models;
using PWMS.Application.Addresses.Repositories;
using PWMS.Application.Addresses.Specifications;
using PWMS.Application.Common.Handlers;
using PWMS.Application.Common.Interfaces;
using PWMS.Common.Extensions;

namespace PWMS.Application.Addresses.Queries.GetById;

public sealed class GetAddressByIdQueryHandler : HandlerDbQueryBase<GetAddressByIdQuery, Result<AddressDto>>
{
    private readonly IAddressRepository _addressRepository;

    public GetAddressByIdQueryHandler(
        IAddressRepository addressRepository,
        IApplicationDbContext contextDb,
        IMapper mapper,
        ICurrentUserService currentUserService) : base(contextDb, mapper, currentUserService)
    {
        _addressRepository = addressRepository.ThrowIfNull();
    }

    public async override Task<Result<AddressDto>> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _addressRepository
            .SingleOrDefaultAsync(new AddressByIdSpecification(request.Id, true), cancellationToken)
            .ConfigureAwait(false);

        entity.ThrowIfNull(new NotFoundException());

        var dtoItem = await entity
            .BuildAdapter(Mapper.Config)
            .AdaptToTypeAsync<AddressDto>()
            .ConfigureAwait(false);

        return Result.Ok(dtoItem);
    }
}
