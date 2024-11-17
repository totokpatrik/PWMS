namespace PWMS.Application.Addresses.Models;

public class UpdateAddressDto
{
    public Guid Id { get; set; }
    public string AddressLine { get; set; } = string.Empty;
}
