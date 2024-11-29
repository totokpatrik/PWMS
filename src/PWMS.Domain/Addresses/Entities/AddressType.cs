using System.ComponentModel;

namespace PWMS.Domain.Addresses.Entities;

public enum AddressType
{
    [Description("Inbound Address")]
    InboundAddress = 0,
    OutboundAddress = 1
}
