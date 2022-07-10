using OnlineStore.EventBus.Abstractions;
using OnlineStore.EventBus.Events;

namespace OnlineStore.Catalog.API.IntegrationEvents;

public class CatalogIntegrationEventService : ICatalogIntegrationEventService
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<CatalogIntegrationEventService> _logger;

    public CatalogIntegrationEventService(
        ILogger<CatalogIntegrationEventService> logger,
        IEventBus eventBus)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
    }

    public void PublishThroughEventBusAsync(IntegrationEvent evt)
    {
        try
        {
            _logger.LogInformation("----- Publishing integration event: {IntegrationEventId_published} - ({@IntegrationEvent})", evt.Id, evt);

            _eventBus.Publish(evt);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ERROR Publishing integration event: {IntegrationEventId} - ({@IntegrationEvent})", evt.Id, evt);
        }
    }

}
