using PWMS.Domain.Models;
using PWMS.Domain.Models.ValueObjects;

namespace TestCommon.Addresses;

public static class AddressFactory
{
    public static Address CreateAddress(string name, string emailAddress, string addressLine, string country, string state, string zipCode)
    {
        return Address.Create(
        id: AddressId.Of(Guid.NewGuid()),
            name: name,
            emailAddress: emailAddress,
            addressLine: addressLine,
            country: country,
            state: state,
            zipCode: zipCode
            );
    }
}
