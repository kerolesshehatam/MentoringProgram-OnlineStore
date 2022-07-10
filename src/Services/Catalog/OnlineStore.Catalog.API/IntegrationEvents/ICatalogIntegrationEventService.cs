using OnlineStore.EventBus.Events;

namespace OnlineStore.Catalog.API.IntegrationEvents;

public interface ICatalogIntegrationEventService
{
    void PublishThroughEventBusAsync(IntegrationEvent evt);
}
