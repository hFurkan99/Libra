namespace Shared.Messaging.Events;
public record CatalogBookCreatedIntegrationEvent
{
    public Guid CatalogBookId { get; set; }
    public string Title { get; set; } = default!;
    public string Isbn { get; set; } = default!;
}