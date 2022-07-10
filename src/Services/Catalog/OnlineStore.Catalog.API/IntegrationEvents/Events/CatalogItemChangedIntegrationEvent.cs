using OnlineStore.EventBus.Events;

namespace OnlineStore.Catalog.API.IntegrationEvents.Events;

// Integration Events notes: 
// An Event is “something that has happened in the past”, therefore its name has to be past tense
public record CatalogItemChangedIntegrationEvent : IntegrationEvent
{
    public int ItemId { get; private init; }

    public decimal NewPrice { get; private init; }

    public decimal OldPrice { get; private init; }
    public string NewName { get; private init; }
    public string OldName { get; private init; }

    public CatalogItemChangedIntegrationEvent(int itemId, decimal newPrice, decimal oldPrice, string newName, string oldName)
    {
        ItemId = itemId;
        NewPrice = newPrice;
        OldPrice = oldPrice;
        NewName = newName;
        OldName = oldName;
    }
}
