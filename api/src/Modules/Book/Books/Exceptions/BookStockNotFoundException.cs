namespace Book.Books.Exceptions;
public class BookStockNotFoundException(Guid bookStockId)
    : NotFoundException("BookStock for CatalaogBookId", bookStockId)
{
}
