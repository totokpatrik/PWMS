using Microsoft.Extensions.Logging;
using PWMS.Application.Abstractions.DomainEventHandlers;
using PWMS.Domain.Addresses.DomainEvents;

namespace PWMS.Application.Addresses.DomainEventHandlers;

public sealed class AddressCreatedDomainEventHandler : DomainEventHandler<AddressCreatedDomainEvent>
{
    public AddressCreatedDomainEventHandler(ILogger<DomainEventHandler<AddressCreatedDomainEvent>> logger) : base(logger)
    {
    }

    protected override async Task OnHandleAsync(AddressCreatedDomainEvent @event)
    {
        Logger.LogInformation("Addres Created Domain Event Handled: " + @event);
    }
}
