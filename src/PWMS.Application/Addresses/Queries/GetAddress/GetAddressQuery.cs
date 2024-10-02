using PWMS.Application.Abstractions.Queries;
using PWMS.Application.Addresses.Models;

namespace PWMS.Application.Addresses.Queries.GetAddress;

public sealed record GetAddressQuery(Guid Id) : Query<AddressDto>;
