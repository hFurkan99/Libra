namespace Book.Books.Models;
public class BookStock : Entity<Guid>
{
    public Guid CatalogBookId { get; private set; }
    public int Total { get; private set; }
    public int Available { get; private set; }

    protected BookStock() { }

    public BookStock(Guid catalogBookId, int total)
    {
        Id = Guid.NewGuid();
        CatalogBookId = catalogBookId;
        Total = total;
        Available = total;
    }

    public static BookStock Create(
        Guid Id,
        Guid catalogBookId)
    {
        if(catalogBookId == Guid.Empty)
            throw new ArgumentException("Catalog Book ID cannot be empty.", nameof(catalogBookId));

        return new BookStock
        {
            Id = Id,
            CatalogBookId = catalogBookId,
            Total = 1,
            Available = 1
        };
    }

    public void AddCopy()
    {
        Total++;
        Available++;
    }

    public void RemoveCopy()
    {
        Available--;
        Total--;
    }

    public void DecreaseAvailable() => Available--;
    public void IncreaseAvailable() => Available++;
}
