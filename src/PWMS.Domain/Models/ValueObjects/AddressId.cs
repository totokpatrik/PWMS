namespace PWMS.Domain.Models.ValueObjects;

public record AddressId
{
    public Guid Value { get; }
    public AddressId(Guid value)
    {
        Value = value;
    }

    public static AddressId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);

        if (value == Guid.Empty)
        {
            throw new DomainException("AddressId cannot be empty.");
        }

        return new AddressId(value);
    }
}