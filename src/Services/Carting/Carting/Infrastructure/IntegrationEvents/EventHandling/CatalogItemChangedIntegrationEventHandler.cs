using OnlineStore.CartingService.Infrastructure.IntegrationEvents.Events;
using OnlineStore.CartingService.Services;
using OnlineStore.EventBus.Abstractions;

namespace OnlineStore.CartingService.Infrastructure.IntegrationEvents.EventHandling;

public class CatalogItemChangedIntegrationEventHandler : IIntegrationEventHandler<CatalogItemChangedIntegrationEvent>
{
    private readonly ILogger<CatalogItemChangedIntegrationEventHandler> _logger;
    private readonly ICartService _cartService;

    public CatalogItemChangedIntegrationEventHandler(
        ICartService cartService,
        ILogger<CatalogItemChangedIntegrationEventHandler> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
    }

    public async Task Handle(CatalogItemChangedIntegrationEvent @event)
    {

        _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

        var ids = _cartService.GetCartsIds();

        foreach (var cartId in ids)
        {
            await _cartService.UpdateCartItems(cartId, @event.ItemId, @event.NewPrice, @event.NewName);
        }
    }
}
