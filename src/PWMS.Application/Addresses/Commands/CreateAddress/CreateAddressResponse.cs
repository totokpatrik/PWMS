using System;

namespace PWMS.Application.Addresses.Commands.CreateAddress;

public class CreateAddressResponse(Guid id)
{
    public Guid Id { get; } = id;
}
