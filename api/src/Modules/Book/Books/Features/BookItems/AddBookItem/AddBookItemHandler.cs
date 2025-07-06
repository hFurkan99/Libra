using Book.Books.Exceptions;

namespace Book.Books.Features.BookItems.AddBookItem;
internal class AddBookItemHandler(
    BookDbContext dbContext)
    : ICommandHandler<AddBookItemCommand, AddBookItemResult>
{
    public async Task<AddBookItemResult> Handle(AddBookItemCommand command, 
        CancellationToken cancellationToken)
    {
        var bookItem = CreateBookItem(command);
        dbContext.BookItems.Add(bookItem);
        await AddCopy(bookItem.CatalogBookId, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new AddBookItemResult(bookItem.Id);
    }

    private static BookItem CreateBookItem(AddBookItemCommand request)
    {
        return BookItem.Create(
            Guid.NewGuid(),
            request.CatalogBookId,
            request.Barcode,
            request.Location,
            request.Status ?? BookStatus.Available,
            request.ConditionNote);
    }

    private async Task AddCopy(Guid catalogBookId, CancellationToken cancellationToken)
    {
        var bookStock = await dbContext.BookStocks
            .FirstOrDefaultAsync(x => x.CatalogBookId == catalogBookId, cancellationToken) 
            ?? throw new BookStockNotFoundException(catalogBookId);

        bookStock.AddCopy();
    }
}
