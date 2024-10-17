using PWMS.Domain.Addresses.Events;

namespace PWMS.Application.Addresses.EventHandlers;

public class AddressCreatedDomainEventHandler : INotificationHandler<AddressCreatedDomainEvent>
{
    private readonly ILogger<AddressCreatedDomainEventHandler> _logger;

    public AddressCreatedDomainEventHandler(ILogger<AddressCreatedDomainEventHandler> logger)
    {
        _logger = logger;
    }
    public Task Handle(AddressCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Address created domain event handled");

        return Task.CompletedTask;
    }
}
