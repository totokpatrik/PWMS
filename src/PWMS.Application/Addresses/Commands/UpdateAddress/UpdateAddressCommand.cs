﻿using MediatR;

namespace PWMS.Application.Addresses.Commands.UpdateAddress;

public sealed record UpdateAddressCommand(
    Guid Id,
    string Name,
    string EmailAddress,
    string AddressLine,
    string Country,
    string State,
    string ZipCode
    ) : IRequest;
