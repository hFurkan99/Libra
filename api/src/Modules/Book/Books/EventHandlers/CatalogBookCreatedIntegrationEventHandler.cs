namespace Book.Books.EventHandlers;
public sealed class CatalogBookCreatedIntegrationEventHandler(ISender sender)
    : ICapSubscribe
{
    [CapSubscribe("catalog.catalogbooks.created")]
    public async Task Handle(CatalogBookCreatedIntegrationEvent @event)
    {
        var command = new InitializeBookItemCommand(
            @event.CatalogBookId,
            null, 
            null, 
            BookStatus.Available,
            string.Empty);

        await sender.Send(command);
    }
}
