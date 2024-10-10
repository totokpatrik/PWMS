using PWMS.Domain.Addresses.Aggregates;

namespace PWMS.Domain.Addresses.Factories;

public static class AddressFactory
{
    public static Address Create(string addressLine)
        => new(addressLine);
}
