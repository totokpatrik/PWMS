using PWMS.Domain.Addresses.Entities;

namespace PWMS.Application.Addresses.Models;

public sealed class AddressDto 
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;

}