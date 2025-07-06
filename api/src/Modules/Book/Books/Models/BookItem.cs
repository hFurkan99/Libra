namespace Book.Books.Models;
public class BookItem : Aggregate<Guid>
{
    public Guid CatalogBookId { get; private set; }
    public string? Barcode { get; private set; }
    public string? Location { get; private set; }
    public string ConditionNote { get; private set; } = string.Empty;
    public BookStatus? Status { get; private set; } = BookStatus.Available;

    protected BookItem() { }

    private BookItem(Guid id, 
        Guid catalogBookId, 
        string? barcode, 
        string? location, 
        BookStatus? status, 
        string conditionNote)
    {
        Id = id;
        CatalogBookId = catalogBookId;
        Barcode = barcode;
        Location = location;
        Status = status;
        ConditionNote = conditionNote;
    }

    public static BookItem Create(
        Guid id, 
        Guid catalogBookId, 
        string? barcode, 
        string? location, 
        BookStatus status, 
        string conditionNote)
    {
        if (catalogBookId == Guid.Empty) 
            throw new ArgumentException("Catalog Book ID cannot be empty.", nameof(catalogBookId));

        return new BookItem(id, catalogBookId, barcode, 
            location, status, conditionNote);
    }

    public void MarkAsAvailable() => Status = BookStatus.Available;
    public void MarkAsBorrowed() => Status = BookStatus.Borrowed;
    public void MarkAsLost() => Status = BookStatus.Lost;
    public void MarkAsDamaged() => Status = BookStatus.Damaged;
    public void MarkAsReserved() => Status = BookStatus.Reserved;
}