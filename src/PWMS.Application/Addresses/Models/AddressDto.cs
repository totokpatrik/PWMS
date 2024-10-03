using PWMS.Application.Abstractions.Models;
using PWMS.Domain.Addresses.Entities;

namespace PWMS.Application.Addresses.Models;

public sealed class AddressDto : BaseDto
{
    public string Name { get; set; } = default!;

}