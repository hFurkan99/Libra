namespace Catalog.CatalogBooks.Features.CatalogBooks.AddCatalogBook;
internal class AddCatalogBookHandler(
    CatalogDbContext dbContext,
    ICapPublisher capPublisher)
    : ICommandHandler<AddCatalogBookCommand, AddCatalogBookResult>
{
    public async Task<AddCatalogBookResult> Handle(
        AddCatalogBookCommand request,
        CancellationToken cancellationToken)
    {
        using var transaction = await dbContext.Database
            .BeginTransactionAsync(cancellationToken);

        var catalogBook = CreateCatalogBook(request);

        dbContext.CatalogBooks.Add(catalogBook);

        await dbContext.SaveChangesAsync(cancellationToken);
        await capPublisher.PublishAsync("catalog.catalogbooks.created",
            CreateIntegrationEvent(catalogBook), cancellationToken: cancellationToken);

        await transaction.CommitAsync(cancellationToken);

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

    private static CatalogBookCreatedIntegrationEvent CreateIntegrationEvent(CatalogBook catalogBook)
    {
        return new CatalogBookCreatedIntegrationEvent
        {
            CatalogBookId = catalogBook.Id,
            Isbn = catalogBook.Isbn.Value,
            Title = catalogBook.Title
        };
    }
}