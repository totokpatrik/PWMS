using PWMS.Application.Addresses.Models;

namespace PWMS.Application.Addresses.Queries.GetById;

public sealed record GetAddressByIdQuery(Guid Id) : IRequest<Result<AddressDto>>;