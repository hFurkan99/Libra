namespace Catalog.CatalogBooks.Events;
public record CatalogBookCreatedDomainEvent(
    Guid CatalogBookId,
    string Title,
    string Isbn) : IDomainEvent;
