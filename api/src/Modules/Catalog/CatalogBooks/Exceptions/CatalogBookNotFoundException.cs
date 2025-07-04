namespace Catalog.CatalogBooks.Exceptions;
public class CatalogBookNotFoundException(Guid catalogBookId)
    : NotFoundException("CatalogBook", catalogBookId)
{
}
