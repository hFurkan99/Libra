namespace Catalog.CatalogBooks.Features.CatalogBooks.AddCatalogBook;
internal class AddCatalogBookHandler(
    CatalogDbContext dbContext,
    ICapPublisher capBus)
    : ICommandHandler<AddCatalogBookCommand, AddCatalogBookResult>
{
    public async Task<AddCatalogBookResult> Handle(
        AddCatalogBookCommand request,
        CancellationToken cancellationToken)
    {
        using var tx = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        
        var catalogBook = CreateCatalogBook(request);
        dbContext.CatalogBooks.Add(catalogBook);

        await PublishAsync(catalogBook, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
        await tx.CommitAsync(cancellationToken);
        return new AddCatalogBookResult(catalogBook.Id);
    }

    private static CatalogBook CreateCatalogBook(AddCatalogBookCommand request)
    {
        return CatalogBook.Create(
            Guid.NewGuid(),
            request.Title,
            Isbn.Of(request.Isbn),
            request.AuthorId,
            request.CategoryId);
    }

    private async Task PublishAsync(CatalogBook catalogBook, CancellationToken cancellationToken)
    {
        await capBus.PublishAsync(
            "catalog.catalogbooks.added",
            CreateIntegrationEvent(catalogBook),
            cancellationToken: cancellationToken);
    }

    private static CatalogBookCreatedIntegrationEvent CreateIntegrationEvent(CatalogBook catalogBook)
    {
        return new CatalogBookCreatedIntegrationEvent
        {
            CatalogBookId = catalogBook.Id,
            Isbn = catalogBook.Isbn.Value,
            Title = catalogBook.Title,
        };
    }
}