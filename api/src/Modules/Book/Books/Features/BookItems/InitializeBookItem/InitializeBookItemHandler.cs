namespace Book.Books.Features.BookItems.InitializeBookItem;
internal class InitializeBookItemHandler(
    BookDbContext dbContext)
    : ICommandHandler<InitializeBookItemCommand, InitializeBookItemResult>
{
    public async Task<InitializeBookItemResult> Handle(InitializeBookItemCommand command, 
        CancellationToken cancellationToken)
    {
        var bookItem = CreateBookItem(command);
        var bookStock = CreateBookStock(bookItem.CatalogBookId);

        dbContext.BookItems.Add(bookItem);
        dbContext.BookStocks.Add(bookStock);

        await dbContext.SaveChangesAsync(cancellationToken);
        return new InitializeBookItemResult(bookItem.Id);
    }

    private static BookItem CreateBookItem(InitializeBookItemCommand command)
    {
        return BookItem.Create(
            Guid.NewGuid(),
            command.CatalogBookId,
            command.Barcode,
            command.Location,
            command.Status ?? BookStatus.Available,
            command.ConditionNote);
    }

    private static BookStock CreateBookStock(Guid catalogBookId)
    {
        var bookStock = BookStock.Create(Guid.NewGuid(), catalogBookId);
        return bookStock;
    }
}
