using OnlineStore.CartingService.Infrastructure.IntegrationEvents.EventHandling;
using OnlineStore.CartingService.Infrastructure.IntegrationEvents.Events;
using OnlineStore.EventBus.Abstractions;

namespace OnlineStore.CartingService.Configurations.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureEventBus(this WebApplication app)
        {
            var eventBus = app.Services.GetRequiredService<IEventBus>();

            eventBus.Subscribe<CatalogItemChangedIntegrationEvent, CatalogItemChangedIntegrationEventHandler>();
        }
    }
}
