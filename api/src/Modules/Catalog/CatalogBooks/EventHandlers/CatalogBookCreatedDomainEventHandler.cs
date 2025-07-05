namespace Catalog.CatalogBooks.EventHandlers;
internal sealed class CatalogBookCreatedDomainEventHandler()
    : INotificationHandler<CatalogBookCreatedDomainEvent>
{
    public async Task Handle(CatalogBookCreatedDomainEvent notification, 
        CancellationToken cancellationToken)
    {
        Console.WriteLine($"CatalogBookCreatedDomainEventHandler: " +
            $"{notification.CatalogBookId} - " +
            $"{notification.Isbn} - {notification.Title}");
    }
}
